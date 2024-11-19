using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
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
        private bool isShowingNetworkError = false;
        private List<string> selectedFilePaths = new List<string>();
        private CancellationTokenSource cancellationSource;
        private System.Windows.Forms.Timer connectionCheckTimer;

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
            chkLimitFiles.CheckedChanged += ChkLimitFiles_CheckedChanged;

            // Thiết lập ban đầu
            disconnectButton.Enabled = false;
            btnSendFile.Enabled = false;
            txtSelectedFile.ReadOnly = true;
            numericFileLimit.Value = 5; // Mặc định giới hạn 5 file

            // Khởi tạo timer
            connectionCheckTimer = new System.Windows.Forms.Timer();
            connectionCheckTimer.Interval = 1000; // Kiểm tra mỗi 1 giây
            connectionCheckTimer.Tick += ConnectionCheckTimer_Tick;
        }

        private void ConnectionCheckTimer_Tick(object sender, EventArgs e)
        {
            if (isConnected && !NetworkInterface.GetIsNetworkAvailable() && !isShowingNetworkError)
            {
                isShowingNetworkError = true; // Đánh dấu đang hiện thông báo
                connectionCheckTimer.Stop(); // Dừng timer ngay lập tức

                MessageBox.Show("Network connection lost. Disconnecting from server.",
                    "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                DisconnectFromServer();
                isShowingNetworkError = false; // Reset lại trạng thái
            }
        }

        private async void ConnectButton_Click(object sender, EventArgs e)
        {
            // Kiểm tra trước đoạn if(!isConnected)
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                MessageBox.Show("No network connection available. Please check your network connection.",
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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

                    // Bắt đầu timer kiểm tra kết nối sau khi connect thành công
                    connectionCheckTimer.Start();

                    // Update UI
                    connectButton.Enabled = false;
                    disconnectButton.Enabled = true;
                    btnSendFile.Enabled = selectedFilePaths.Count > 0;
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

        private void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            DisconnectFromServer();
            progressBar.Value = 0; // Reset progress bar
        }

        private void DisconnectFromServer()
        {
            try
            {
                if (isConnected)
                {

                    // Gửi thông báo ngắt kết nối đến server trước khi đóng kết nối
                    if (networkStream != null && client != null && client.Connected)
                    {
                        byte[] disconnectMsg = Encoding.UTF8.GetBytes("DISCONNECT");
                        byte[] msgLength = BitConverter.GetBytes(disconnectMsg.Length);
                        networkStream.Write(msgLength, 0, 4);
                        networkStream.Write(disconnectMsg, 0, disconnectMsg.Length);
                        networkStream.Flush();

                        // Đợi một chút để đảm bảo server nhận được thông báo
                        Thread.Sleep(100);
                    }

                    // Dừng timer kiểm tra kết nối
                    connectionCheckTimer.Stop();
                    isShowingNetworkError = false; // Reset trạng thái

                    // Đóng kết nối
                    networkStream?.Close();
                    client?.Close();

                    isConnected = false;
                    connectButton.Enabled = true;
                    disconnectButton.Enabled = false;
                    btnSendFile.Enabled = false;
                    hostTextBox.Enabled = true;
                    portTextBox.Enabled = true;
                    statusBar1.Text = "Disconnected from server";

                    // Clear selected files when disconnecting
                    selectedFilePaths.Clear();
                    txtSelectedFile.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Disconnect error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChkLimitFiles_CheckedChanged(object sender, EventArgs e)
        {
            numericFileLimit.Enabled = chkLimitFiles.Checked;
        }
        private void BrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Kiểm tra giới hạn số lượng file nếu checkbox được bật
                    if (chkLimitFiles.Checked && openFileDialog.FileNames.Length > numericFileLimit.Value)
                    {
                        MessageBox.Show($"Chỉ được chọn tối đa {numericFileLimit.Value} file một lần.",
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    selectedFilePaths.Clear();
                    selectedFilePaths.AddRange(openFileDialog.FileNames);

                    if (selectedFilePaths.Count == 1)
                    {
                        txtSelectedFile.Text = Path.GetFileName(selectedFilePaths[0]);
                    }
                    else
                    {
                        txtSelectedFile.Text = $"Đã chọn {selectedFilePaths.Count} files";
                    }

                    btnSendFile.Enabled = isConnected && selectedFilePaths.Count > 0;
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

            if (selectedFilePaths.Count == 0)
            {
                MessageBox.Show("Please select a file first.", "No File Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thêm kiểm tra số lượng file trước khi gửi
            if (chkLimitFiles.Checked && selectedFilePaths.Count > numericFileLimit.Value)
            {
                MessageBox.Show($"Chỉ được gửi tối đa {numericFileLimit.Value} file một lần.",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnSendFile.Enabled = false;
                btnBrowse.Enabled = false;
                cancellationSource = new CancellationTokenSource();

                int successCount = 0;
                List<string> failedFiles = new List<string>();

                // Gửi các file đã chọn
                foreach (string filePath in selectedFilePaths)
                {
                    try
                    {
                        await SendFileAsync(filePath, cancellationSource.Token);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error sending file {Path.GetFileName(filePath)}: {ex.Message}");
                    }
                }
                // Hiện thông báo sau khi gửi xong tất cả file
                MessageBox.Show($"Successfully sent {selectedFilePaths.Count} files!",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                selectedFilePaths.Clear();
                txtSelectedFile.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in file transfer: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DisconnectFromServer();
            }
            finally
            {
                btnSendFile.Enabled = true;
                btnBrowse.Enabled = true;
                cancellationSource?.Dispose();
            }
        }

        private async Task SendFileAsync(string filePath, CancellationToken cancellationToken)
        {
            const int bufferSize = 81920;

            using (TcpClient fileClient = new TcpClient())
            {
                await fileClient.ConnectAsync(hostTextBox.Text, int.Parse(portTextBox.Text));
                using (NetworkStream fileStream = fileClient.GetStream())
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize))
                {
                    string fileName = Path.GetFileName(filePath);
                    var fileItem = AddFileToListView(fileName, fs.Length);
                    UpdateFileStatus(fileItem, "Sending"); // Thêm trạng thái sending

                    try
                    {
                        // Gửi tên file
                        byte[] fileNameBytes = Encoding.UTF8.GetBytes(fileName);
                        byte[] fileNameLengthBytes = BitConverter.GetBytes(fileNameBytes.Length);
                        await fileStream.WriteAsync(fileNameLengthBytes, 0, 4, cancellationToken);
                        await fileStream.WriteAsync(fileNameBytes, 0, fileNameBytes.Length, cancellationToken);

                        // Gửi kích thước file
                        byte[] fileSizeBytes = BitConverter.GetBytes(fs.Length);
                        await fileStream.WriteAsync(fileSizeBytes, 0, 8, cancellationToken);

                        byte[] buffer = new byte[bufferSize];
                        long totalBytes = 0;
                        int bytesRead;
                        DateTime lastUIUpdate = DateTime.Now;

                        while ((bytesRead = await fs.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                        {
                            await fileStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);
                            totalBytes += bytesRead;

                            if ((DateTime.Now - lastUIUpdate).TotalMilliseconds >= 100)
                            {
                                int percentage = (int)((totalBytes * 100) / fs.Length);
                                UpdateFileProgress(fileItem, percentage, totalBytes, fs.Length);
                                lastUIUpdate = DateTime.Now;
                            }
                        }

                        // Đợi một chút để đảm bảo server nhận được toàn bộ dữ liệu
                        await Task.Delay(500, cancellationToken);

                        UpdateFileProgress(fileItem, 100, fs.Length, fs.Length);
                        UpdateFileStatus(fileItem, "Completed");

                    }
                    catch (Exception)
                    {
                        UpdateFileStatus(fileItem, "Failed");
                        throw;
                    }
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
            item.SubItems.Add("Sending"); //Trạng thái đang gửi
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
            if (status == "Completed")
            {
                progressBar.Value = 0;  // Reset progress bar
                txtSelectedFile.Text = ""; // Reset label 
            }
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