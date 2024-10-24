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
    public partial class TCPServer : Form
    {
        private TcpListener server;
        private bool isRunning;
        private string savePath;
        private CancellationTokenSource cancellationToken;

        public TCPServer()
        {
            InitializeComponent();
            InitializeServer();
        }

        private void InitializeServer()
        {
            buttonStop.Enabled = false;

            // Đăng ký events
            buttonStart.Click += ButtonStart_Click;
            buttonStop.Click += ButtonStop_Click;
            buttonBrowse.Click += ButtonBrowse_Click;
            FormClosing += TCPServer_FormClosing;

            // Thiết lập mặc định save location là thư mục Documents
            savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ReceivedFiles");
            Directory.CreateDirectory(savePath);
            textBoxSaveLocation.Text = savePath;

            // Hiển thị tất cả IP của máy
            DisplayServerIPs();
        }

        private void DisplayServerIPs()
        {
            try
            {
                // Lấy tất cả IP của máy
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                // Lọc ra các IPv4 address và không phải loopback
                var ipv4Addresses = localIPs
                    .Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    .Where(ip => !IPAddress.IsLoopback(ip))
                    .Select(ip => ip.ToString())
                    .ToList();

                if (ipv4Addresses.Count > 0)
                {
                    // Hiển thị IP trong textbox
                    textBoxServerIP.Text = string.Join(", ", ipv4Addresses);
                }
                else
                {
                    textBoxServerIP.Text = "No network connection found";
                }
            }
            catch (Exception ex)
            {
                textBoxServerIP.Text = "Error getting IP address";
                MessageBox.Show($"Error getting IP address: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select folder to save received files";
                folderDialog.SelectedPath = savePath;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    savePath = folderDialog.SelectedPath;
                    textBoxSaveLocation.Text = savePath;
                }
            }
        }

        private async void ButtonStart_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                try
                {
                    int port;
                    if (string.IsNullOrWhiteSpace(textBoxPort.Text) ||
                        !int.TryParse(textBoxPort.Text, out port))
                    {
                        MessageBox.Show("Please enter a valid port number.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    server = new TcpListener(IPAddress.Any, port);
                    server.Start();
                    isRunning = true;

                    buttonStart.Enabled = false;
                    buttonStop.Enabled = true;
                    textBoxPort.Enabled = false;

                    // Hiển thị thông báo chi tiết hơn
                    string serverIPs = textBoxServerIP.Text;
                    statusBar.Text = $"Server started on port {port}. IP Address(es): {serverIPs}";

                    // Thêm hướng dẫn cho client
                    MessageBox.Show($"Server is running!\n\nIP Address(es): {serverIPs}\nPort: {port}\n\n" +
                        "Share this information with clients to connect.",
                        "Server Started", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cancellationToken = new CancellationTokenSource();
                    await ListenForClientsAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error starting server: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    StopServer();
                }
            }
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            StopServer();
        }

        private void StopServer()
        {
            try
            {
                cancellationToken?.Cancel();
                server?.Stop();
                isRunning = false;

                buttonStart.Enabled = true;
                buttonStop.Enabled = false;
                textBoxPort.Enabled = true;
                statusBar.Text = "Server stopped";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error stopping server: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ListenForClientsAsync()
        {
            try
            {
                while (!cancellationToken.Token.IsCancellationRequested)
                {
                    TcpClient client = await server.AcceptTcpClientAsync();
                    _ = HandleClientAsync(client);
                }
            }
            catch (Exception) when (cancellationToken.Token.IsCancellationRequested)
            {
                // Server đã được dừng
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error accepting clients: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                StopServer();
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                string clientInfo = $"{((IPEndPoint)client.Client.RemoteEndPoint).Address}:" +
                                  $"{((IPEndPoint)client.Client.RemoteEndPoint).Port}";

                AddClientToListView(clientInfo);
                statusBar.Text = $"Client connected: {clientInfo}";

                using (NetworkStream stream = client.GetStream())
                {
                    while (client.Connected && !cancellationToken.Token.IsCancellationRequested)
                    {
                        // Đọc tên file
                        byte[] fileNameLengthBytes = new byte[4];
                        await stream.ReadAsync(fileNameLengthBytes, 0, 4);
                        int fileNameLength = BitConverter.ToInt32(fileNameLengthBytes, 0);

                        byte[] fileNameBytes = new byte[fileNameLength];
                        await stream.ReadAsync(fileNameBytes, 0, fileNameLength);
                        string fileName = Encoding.UTF8.GetString(fileNameBytes);

                        // Đọc kích thước file
                        byte[] fileSizeBytes = new byte[8];
                        await stream.ReadAsync(fileSizeBytes, 0, 8);
                        long fileSize = BitConverter.ToInt64(fileSizeBytes, 0);

                        // Tạo item trong ListView
                        var fileItem = AddFileToListView(fileName, fileSize);

                        // Nhận file
                        await ReceiveFileAsync(stream, fileName, fileSize, fileItem);
                    }
                }
            }
            catch (Exception ex) when (!cancellationToken.Token.IsCancellationRequested)
            {
                statusBar.Text = $"Client error: {ex.Message}";
            }
            finally
            {
                RemoveClientFromListView(client);
                client.Close();
            }
        }

        private string GetUniqueFilePath(string originalPath)
        {
            if (!File.Exists(originalPath))
            {
                return originalPath;
            }

            string folderPath = Path.GetDirectoryName(originalPath);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(originalPath);
            string extension = Path.GetExtension(originalPath);
            int counter = 1;
            string newFilePath;

            do
            {
                string newFileName = $"{fileNameWithoutExt} ({counter}){extension}";
                newFilePath = Path.Combine(folderPath, newFileName);
                counter++;
            } while (File.Exists(newFilePath));

            return newFilePath;
        }

        private async Task ReceiveFileAsync(NetworkStream stream, string fileName, long fileSize, ListViewItem fileItem)
        {
            string originalFilePath = Path.Combine(savePath, fileName);
            string filePath = GetUniqueFilePath(originalFilePath);

            // Cập nhật tên file trong ListView nếu đã thay đổi
            if (filePath != originalFilePath)
            {
                string newFileName = Path.GetFileName(filePath);
                UpdateFileName(fileItem, newFileName);
            }

            long receivedBytes = 0;

            try
            {
                using (FileStream fileStream = File.Create(filePath))
                {
                    byte[] buffer = new byte[8192];
                    int bytesRead;

                    while (receivedBytes < fileSize &&
                           (bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await fileStream.WriteAsync(buffer, 0, bytesRead);
                        receivedBytes += bytesRead;

                        // Cập nhật progress
                        int percentage = (int)((receivedBytes * 100) / fileSize);
                        UpdateFileProgress(fileItem, percentage, receivedBytes, fileSize);
                    }
                }

                // Cập nhật trạng thái khi hoàn thành
                UpdateFileStatus(fileItem, "Completed");
                statusBar.Text = $"File received successfully: {Path.GetFileName(filePath)}";
            }
            catch (Exception ex)
            {
                UpdateFileStatus(fileItem, "Failed");
                statusBar.Text = $"Error receiving file {Path.GetFileName(filePath)}: {ex.Message}";

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        private void AddClientToListView(string clientInfo)
        {
            if (listViewClients.InvokeRequired)
            {
                listViewClients.Invoke(new Action<string>(AddClientToListView), clientInfo);
                return;
            }

            string[] clientData = clientInfo.Split(':');
            var item = new ListViewItem(clientData[0]); // IP
            item.SubItems.Add(clientData[1]); // Port
            item.SubItems.Add(DateTime.Now.ToString("HH:mm:ss")); // Time
            listViewClients.Items.Add(item);
        }

        private void RemoveClientFromListView(TcpClient client)
        {
            if (listViewClients.InvokeRequired)
            {
                listViewClients.Invoke(new Action<TcpClient>(RemoveClientFromListView), client);
                return;
            }

            string clientIp = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            foreach (ListViewItem item in listViewClients.Items)
            {
                if (item.Text == clientIp)
                {
                    listViewClients.Items.Remove(item);
                    break;
                }
            }
        }

        private ListViewItem AddFileToListView(string fileName, long fileSize)
        {
            if (listViewFiles.InvokeRequired)
            {
                return (ListViewItem)listViewFiles.Invoke(
                    new Func<string, long, ListViewItem>(AddFileToListView), fileName, fileSize);
            }

            var item = new ListViewItem(fileName);
            item.SubItems.Add(FormatFileSize(fileSize));
            item.SubItems.Add("Receiving");
            item.SubItems.Add("0%");
            listViewFiles.Items.Add(item);
            return item;
        }

        private void UpdateFileProgress(ListViewItem item, int percentage, long received, long total)
        {
            if (listViewFiles.InvokeRequired)
            {
                listViewFiles.Invoke(new Action<ListViewItem, int, long, long>(UpdateFileProgress),
                    item, percentage, received, total);
                return;
            }

            item.SubItems[3].Text = $"{percentage}% ({FormatFileSize(received)}/{FormatFileSize(total)})";
            progressBarTransfer.Value = percentage;
            labelCurrentFile.Text = $"Current File: {item.Text}";
            labelTransferStatus.Text = $"Status: Receiving - {percentage}%";
        }

        private void UpdateFileStatus(ListViewItem item, string status)
        {
            if (listViewFiles.InvokeRequired)
            {
                listViewFiles.Invoke(new Action<ListViewItem, string>(UpdateFileStatus), item, status);
                return;
            }

            item.SubItems[2].Text = status;
            if (status == "Completed")
            {
                progressBarTransfer.Value = 0;
                labelCurrentFile.Text = "Current File: None";
                labelTransferStatus.Text = "Status: Ready";
            }
        }

        private void UpdateFileName(ListViewItem item, string newFileName)
        {
            if (listViewFiles.InvokeRequired)
            {
                listViewFiles.Invoke(new Action<ListViewItem, string>(UpdateFileName), item, newFileName);
                return;
            }

            item.Text = newFileName;
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

        private void TCPServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }
    }
}