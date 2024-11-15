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
using System.Net.NetworkInformation;

namespace N4_FileTranferTCPClientServerLAN
{
    public partial class TCPServer : Form
    {
        private TcpListener server;
        private bool isRunning;
        private bool isShowingNetworkError = false;
        private string savePath;
        private CancellationTokenSource cancellationToken;
        private SemaphoreSlim maxConcurrentTransfers;
        private System.Windows.Forms.Timer networkCheckTimer;

        public TCPServer()
        {
            InitializeComponent();
            InitializeServer();
            // Giới hạn số lượng transfer đồng thời
            maxConcurrentTransfers = new SemaphoreSlim(5); // Tối đa 5 file cùng lúc
            // Đăng ký event lắng nghe thay đổi mạng
            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
            NetworkChange.NetworkAddressChanged += NetworkChange_NetworkAddressChanged;
        }

        private void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            // Chạy trên UI thread vì đang cập nhật UI
            this.Invoke(new Action(() =>
            {
                if (e.IsAvailable)
                {
                    DisplayServerIPs(); // Cập nhật lại IP khi có mạng
                    statusBar.Text = "Network connection restored.";
                }
                else
                {
                    textBoxServerIP.Text = "No network connection found";
                    statusBar.Text = "Network connection lost.";
                }
            }));
        }

        private void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
        {
            // Chạy trên UI thread vì đang cập nhật UI
            this.Invoke(new Action(() =>
            {
                DisplayServerIPs(); // Cập nhật lại IP khi địa chỉ mạng thay đổi
                statusBar.Text = "Network address changed.";
            }));
        }

        private void InitializeServer()
        {
            buttonStop.Enabled = false;

            // Đăng ký events
            buttonStart.Click += ButtonStart_Click;
            buttonStop.Click += ButtonStop_Click;
            buttonBrowse.Click += ButtonBrowse_Click;
            FormClosing += TCPServer_FormClosing;

            //Dừng time 
            networkCheckTimer = new System.Windows.Forms.Timer();
            networkCheckTimer.Interval = 1000;
            networkCheckTimer.Tick += NetworkCheckTimer_Tick;

            // Thiết lập mặc định save location là thư mục Documents
            savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ReceivedFiles");
            Directory.CreateDirectory(savePath);
            textBoxSaveLocation.Text = savePath;

            // Hiển thị tất cả IP của máy
            DisplayServerIPs();
        }

        private void NetworkCheckTimer_Tick(object sender, EventArgs e)
        {
            if (isRunning && !NetworkInterface.GetIsNetworkAvailable() && !isShowingNetworkError)
            {
                isShowingNetworkError = true; // Đánh dấu đang hiện thông báo
                networkCheckTimer.Stop(); // Dừng timer ngay lập tức

                MessageBox.Show("Network connection lost. Server will stop.",
                    "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Xóa tất cả items trong listViewFiles và listViewClients
                listViewFiles.Items.Clear();
                listViewClients.Items.Clear();

                StopServer();
                isShowingNetworkError = false; // Reset lại trạng thái
            }
        }
        private void DisplayServerIPs()
        {
            try
            {
                // Lấy tất cả IP của máy
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                // Lọc ra các IPv4 address và không phải loopback
                var ipv4Addresses = localIPs
                    .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                    .Where(ip => !IPAddress.IsLoopback(ip))
                    .Select(ip => ip.ToString())
                    .ToList();

                if (ipv4Addresses.Count > 0)
                {
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            StopServer();

            // Clear danh sách clients và lịch sử
            listViewClients.Items.Clear();
            listViewConnectionHistory.Items.Clear();
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

        private bool IsNetworkAvailable()
        {
            // Kiểm tra có kết nối mạng không
            return NetworkInterface.GetIsNetworkAvailable();
        }

        private bool HasValidIPAddress()
        {
            try
            {
                // Lấy tất cả IP của máy
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                // Lọc ra các IPv4 address và không phải loopback
                var ipv4Addresses = localIPs
                    .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                    .Where(ip => !IPAddress.IsLoopback(ip))
                    .ToList();

                return ipv4Addresses.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        private async void ButtonStart_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                // Kiểm tra kết nối mạng
                if (!IsNetworkAvailable())
                {
                    MessageBox.Show("No network connection found. Please check your network connection.",
                        "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra có IP hợp lệ không
                if (!HasValidIPAddress())
                {
                    MessageBox.Show("No valid IP address found. Please check your network settings.",
                        "IP Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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

                    // Bắt đầu kiểm tra mạng sau khi server start
                    networkCheckTimer.Start();

                    buttonStart.Enabled = false;
                    buttonStop.Enabled = true;
                    textBoxPort.Enabled = false;

                    string serverIPs = textBoxServerIP.Text;
                    statusBar.Text = $"Server is running on port {port}. IP: {serverIPs}";

                    MessageBox.Show($"Server started!\n\nIP Address(es): {serverIPs}\nPort: {port}\n\n" +
                        "Share this information with clients to connect.",
                        "Server Started", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cancellationToken = new CancellationTokenSource();
                    await ListenForClientsAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error starting server: {ex.Message}");
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
                networkCheckTimer.Stop();  // Dừng timer kiểm tra mạng
                isShowingNetworkError = false; // Reset trạng thái
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
                    _ = ProcessClientAsync(client); // Không đợi task hoàn thành
                }
            }
            catch (Exception) when (cancellationToken.Token.IsCancellationRequested)
            {
                // Server đã được dừng
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error accepting clients: {ex.Message}");
                StopServer();
            }
        }

        private async Task ProcessClientAsync(TcpClient client)
        {
            try
            {
                await maxConcurrentTransfers.WaitAsync(); // Đợi cho đến khi có slot trống


                string clientInfo = $"{((IPEndPoint)client.Client.RemoteEndPoint).Address}:" +
                                  $"{((IPEndPoint)client.Client.RemoteEndPoint).Port}";

                AddClientToListView(clientInfo);
                statusBar.Text = $"Client connected: {clientInfo}";

                using (NetworkStream stream = client.GetStream())
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
                    await ReceiveFileAsync(stream, fileName, fileSize, fileItem, client);
                }
            }
            catch (Exception ex) when (!cancellationToken.Token.IsCancellationRequested)
            {
                statusBar.Text = $"Client error: {ex.Message}";
            }
            finally
            {
                maxConcurrentTransfers.Release(); // Giải phóng slot
                RemoveClientFromListView(client);
                client.Close();
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
                        await ReceiveFileAsync(stream, fileName, fileSize, fileItem, client);
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

        private async Task ReceiveFileAsync(NetworkStream stream, string fileName, long fileSize, ListViewItem fileItem, TcpClient client) 
        {
            string originalFilePath = Path.Combine(savePath, fileName);
            string tempFilePath = originalFilePath + ".tmp"; // Tạo file tạm
            string finalFilePath = GetUniqueFilePath(originalFilePath);

            long receivedBytes = 0;
            const int bufferSize = 81920;

            try
            {
                // Tạo và ghi vào file tạm trước
                using (FileStream fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize))
                {
                    byte[] buffer = new byte[bufferSize];
                    int bytesRead;
                    DateTime lastUIUpdate = DateTime.Now;

                    while (receivedBytes < fileSize)
                    {
                        bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead == 0) break;

                        await fileStream.WriteAsync(buffer, 0, bytesRead);
                        receivedBytes += bytesRead;

                        if ((DateTime.Now - lastUIUpdate).TotalMilliseconds >= 100)
                        {
                            int percentage = (int)((receivedBytes * 100) / fileSize);
                            UpdateFileProgress(fileItem, percentage, receivedBytes, fileSize);
                            lastUIUpdate = DateTime.Now;
                            Application.DoEvents();
                        }
                    }
                }

                // Kiểm tra xem file đã nhận đủ chưa
                if (receivedBytes == fileSize)
                {
                    // Đổi tên file tạm thành tên file cuối cùng
                    if (File.Exists(finalFilePath))
                    {
                        File.Delete(finalFilePath);
                    }
                    File.Move(tempFilePath, finalFilePath);

                    //Lịch sử với status completed
                    string clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                    int clientPort = ((IPEndPoint)client.Client.RemoteEndPoint).Port;
                    AddTransferHistory(clientIP, clientPort, fileName, "Completed");

                    UpdateFileProgress(fileItem, 100, fileSize, fileSize);
                    UpdateFileStatus(fileItem, "Completed");
                    statusBar.Text = $"File received successfully: {Path.GetFileName(finalFilePath)}";
                }
                else
                {
                    // Lịch sử với status Failed
                    string clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                    int clientPort = ((IPEndPoint)client.Client.RemoteEndPoint).Port;
                    AddTransferHistory(clientIP, clientPort, fileName, "Failed");

                    throw new Exception("File transfer incomplete");
                }
            }
            catch (Exception ex)
            {
                UpdateFileStatus(fileItem, "Failed");
                statusBar.Text = $"Error receiving file {Path.GetFileName(finalFilePath)}: {ex.Message}";

                try
                {
                    // Xóa file tạm nếu có lỗi
                    if (File.Exists(tempFilePath))
                    {
                        File.Delete(tempFilePath);
                    }
                }
                catch { }
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
            string connectTime = DateTime.Now.ToString("HH:mm:ss");
            bool clientExists = false;

            // Kiểm tra client có trong danh sách không
            foreach (ListViewItem existingItem in listViewClients.Items)
            {
                if (existingItem.Text == clientData[0])
                {
                    // Cập nhật thông tin cho client đã tồn tại
                    existingItem.SubItems[2].Text = "Đã kết nối";
                    existingItem.SubItems[3].Text = connectTime;
                    clientExists = true;
                    statusBar.Text = $"Client {clientData[0]} reconnected at {connectTime}";
                    break;
                }
            }

            // Nếu là client mới
            if (!clientExists)
            {
                var item = new ListViewItem(clientData[0]);
                try
                {
                    string hostName = Dns.GetHostEntry(clientData[0]).HostName;
                    item.SubItems.Add(hostName);
                }
                catch
                {
                    item.SubItems.Add("Unknown");
                }
                item.SubItems.Add("Đã kết nối");
                item.SubItems.Add(connectTime);
                listViewClients.Items.Add(item);
                statusBar.Text = $"New client {clientData[0]} connected at {connectTime}";
            }
        }
        private void AddToConnectionHistory(string ip, string port, string time)
        {
            if (listViewConnectionHistory.InvokeRequired)
            {
                listViewConnectionHistory.Invoke(new Action<string, string, string>(AddToConnectionHistory),
                    ip, port, time);
                return;
            }

            var historyItem = new ListViewItem(ip);
            historyItem.SubItems.Add(port);
            historyItem.SubItems.Add(time);
            listViewConnectionHistory.Items.Add(historyItem);
        }

        private void AddTransferHistory(string clientIP, int port, string fileName, string status)
        {
            if (listViewConnectionHistory.InvokeRequired)
            {
                listViewConnectionHistory.Invoke(new Action<string, int, string, string>(AddTransferHistory),
                    clientIP, port, fileName, status);
                return;
            }

            var historyItem = new ListViewItem(clientIP);
            historyItem.SubItems.Add(port.ToString());
            historyItem.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));
            historyItem.SubItems.Add(status);  // "Completed" hoặc "Failed"
            listViewConnectionHistory.Items.Add(historyItem);
        }

        private void RemoveClientFromListView(TcpClient client)
        {
            try
            {
                if (listViewClients.InvokeRequired)
                {
                    listViewClients.Invoke(new Action<TcpClient>(RemoveClientFromListView), client);
                    return;
                }

                string clientIp = ((IPEndPoint)client.Client.RemoteEndPoint)?.Address.ToString();
                string disconnectTime = DateTime.Now.ToString("HH:mm:ss");

                foreach (ListViewItem item in listViewClients.Items)
                {
                    if (item.Text == clientIp)
                    {
                        // Cập nhật trạng thái ngắt kết nối
                        item.SubItems[2].Text = "Đã ngắt kết nối";
                        item.SubItems[3].Text = disconnectTime; // Cập nhật thời gian ngắt kết nối

                        // Cập nhật status bar
                        statusBar.Text = $"Client {clientIp} disconnected at {disconnectTime}";
                        break;
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                // Xử lý khi socket đã bị dispose
                string disconnectTime = DateTime.Now.ToString("HH:mm:ss");
                foreach (ListViewItem item in listViewClients.Items)
                {
                    item.SubItems[2].Text = "Đã ngắt kết nối";
                    item.SubItems[3].Text = disconnectTime;
                }
                statusBar.Text = "Client connection lost at " + disconnectTime;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling client disconnect: {ex.Message}");
            }
        }

        private void HandleNetworkLoss(string clientIp)
        {
            foreach (ListViewItem item in listViewClients.Items)
            {
                if (item.Text == clientIp)
                {
                    item.SubItems[2].Text = "Mất kết nối"; // Status khi mất mạng
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