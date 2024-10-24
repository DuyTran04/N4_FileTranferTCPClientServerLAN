using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace N4_FileTranferTCPClientServerLAN
{
    public partial class TCPClient : Form
    {
        private TcpClient client;
        private NetworkStream networkStream;
        private string selectedFilePath;
        private bool isConnected = false;

        public TCPClient()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            // Đăng ký events
            connectButton.Click += ConnectButton_Click;
            disconnectButton.Click += DisconnectButton_Click;
            btnBrowse.Click += BrowseButton_Click;
            btnSendFile.Click += SendFileButton_Click;
            FormClosing += TCPClient_FormClosing;

            // Thiết lập ban đầu
            disconnectButton.Enabled = false;
            btnSendFile.Enabled = false;
            txtSelectedFile.ReadOnly = true;
        }

        private async void ConnectButton_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                try
                {
                    string ip = hostTextBox.Text.Trim();
                    if (string.IsNullOrEmpty(ip))
                    {
                        MessageBox.Show("Please enter the server IP address.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Validate IP address format
                    if (!IPAddress.TryParse(ip, out IPAddress address))
                    {
                        MessageBox.Show("Please enter a valid IP address.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int port;
                    if (string.IsNullOrEmpty(portTextBox.Text) ||
                        !int.TryParse(portTextBox.Text, out port))
                    {
                        MessageBox.Show("Please enter a valid port number.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    client = new TcpClient();
                    statusBar1.Text = $"Connecting to {ip}:{port}...";

                    // Set timeout for connection attempt
                    var timeoutTask = Task.Delay(5000); // 5 second timeout
                    var connectTask = client.ConnectAsync(ip, port);

                    if (await Task.WhenAny(connectTask, timeoutTask) == timeoutTask)
                    {
                        throw new TimeoutException("Connection attempt timed out");
                    }

                    networkStream = client.GetStream();
                    isConnected = true;

                    // Update UI
                    connectButton.Enabled = false;
                    disconnectButton.Enabled = true;
                    btnSendFile.Enabled = selectedFilePath != null;
                    hostTextBox.Enabled = false;
                    portTextBox.Enabled = false;

                    statusBar1.Text = $"Connected to {ip}:{port}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection failed: {ex.Message}\n\nPlease verify:\n" +
                        "1. Server is running\n" +
                        "2. IP address is correct\n" +
                        "3. Port number matches server\n" +
                        "4. Network connection is available",
                        "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    statusBar1.Text = "Connection failed";
                    client?.Close();
                }
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            DisconnectFromServer();
        }

        private void DisconnectFromServer()
        {
            try
            {
                if (isConnected)
                {
                    networkStream?.Close();
                    client?.Close();

                    isConnected = false;
                    connectButton.Enabled = true;
                    disconnectButton.Enabled = false;
                    btnSendFile.Enabled = false;
                    hostTextBox.Enabled = true;
                    portTextBox.Enabled = true;
                    statusBar1.Text = "Disconnected from server";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Disconnect error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFilePath = openFileDialog.FileName;
                    txtSelectedFile.Text = selectedFilePath;
                    btnSendFile.Enabled = isConnected;
                }
            }
        }

        private async void SendFileButton_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("Please connect to server first.", "Not Connected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Please select a file first.", "No File Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnSendFile.Enabled = false;
                btnBrowse.Enabled = false;
                await SendFileAsync(selectedFilePath);
                btnSendFile.Enabled = true;
                btnBrowse.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending file: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSendFile.Enabled = true;
                btnBrowse.Enabled = true;
                DisconnectFromServer();
            }
        }

        private async Task SendFileAsync(string filePath)
        {
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                string fileName = Path.GetFileName(filePath);
                var fileItem = AddFileToListView(fileName, fileStream.Length);

                try
                {
                    // Gửi tên file
                    byte[] fileNameBytes = Encoding.UTF8.GetBytes(fileName);
                    byte[] fileNameLengthBytes = BitConverter.GetBytes(fileNameBytes.Length);
                    await networkStream.WriteAsync(fileNameLengthBytes, 0, 4);
                    await networkStream.WriteAsync(fileNameBytes, 0, fileNameBytes.Length);

                    // Gửi kích thước file
                    byte[] fileSizeBytes = BitConverter.GetBytes(fileStream.Length);
                    await networkStream.WriteAsync(fileSizeBytes, 0, 8);

                    // Gửi nội dung file
                    byte[] buffer = new byte[8192];
                    long totalBytes = 0;
                    int bytesRead;

                    while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await networkStream.WriteAsync(buffer, 0, bytesRead);
                        totalBytes += bytesRead;

                        // Cập nhật progress
                        int percentage = (int)((totalBytes * 100) / fileStream.Length);
                        UpdateFileProgress(fileItem, percentage, totalBytes, fileStream.Length);
                    }

                    UpdateFileStatus(fileItem, "Completed");
                    statusBar1.Text = $"File {fileName} sent successfully";
                }
                catch (Exception)
                {
                    UpdateFileStatus(fileItem, "Failed");
                    throw;
                }
            }
        }

        private ListViewItem AddFileToListView(string fileName, long fileSize)
        {
            if (fileListView.InvokeRequired)
            {
                return (ListViewItem)fileListView.Invoke(
                    new Func<string, long, ListViewItem>(AddFileToListView), fileName, fileSize);
            }

            var item = new ListViewItem(fileName);
            item.SubItems.Add(FormatFileSize(fileSize));
            item.SubItems.Add("Sending");
            item.SubItems.Add("0%");
            fileListView.Items.Add(item);
            return item;
        }

        private void UpdateFileProgress(ListViewItem item, int percentage, long sent, long total)
        {
            if (fileListView.InvokeRequired)
            {
                fileListView.Invoke(new Action<ListViewItem, int, long, long>(UpdateFileProgress),
                    item, percentage, sent, total);
                return;
            }

            item.SubItems[3].Text = $"{percentage}% ({FormatFileSize(sent)}/{FormatFileSize(total)})";
            progressBar.Value = percentage;
        }

        private void UpdateFileStatus(ListViewItem item, string status)
        {
            if (fileListView.InvokeRequired)
            {
                fileListView.Invoke(new Action<ListViewItem, string>(UpdateFileStatus), item, status);
                return;
            }

            item.SubItems[2].Text = status;
            progressBar.Value = status == "Completed" ? 0 : progressBar.Value;
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            double size = bytes;

            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size /= 1024;
            }

            return $"{size:0.##} {sizes[order]}";
        }

        private void TCPClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisconnectFromServer();
        }
    }
}