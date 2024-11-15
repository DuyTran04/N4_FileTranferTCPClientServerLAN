namespace N4_FileTranferTCPClientServerLAN
{
    partial class TCPServer
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBoxServer = new System.Windows.Forms.GroupBox();
            this.labelServerIP = new System.Windows.Forms.Label();
            this.textBoxServerIP = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.groupBoxSaveLocation = new System.Windows.Forms.GroupBox();
            this.textBoxSaveLocation = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.progressBarTransfer = new System.Windows.Forms.ProgressBar();
            this.labelCurrentFile = new System.Windows.Forms.Label();
            this.labelTransferStatus = new System.Windows.Forms.Label();
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.columnFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnFileSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHistoryStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxConnectedClients = new System.Windows.Forms.GroupBox();
            this.listViewConnectionHistory = new System.Windows.Forms.ListView();
            this.columnHistoryIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHistoryPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHistoryTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewClients = new System.Windows.Forms.ListView();
            this.columnClientIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHostName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnClientStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnConnectedTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxServer.SuspendLayout();
            this.groupBoxSaveLocation.SuspendLayout();
            this.groupBoxConnectedClients.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxServer
            // 
            this.groupBoxServer.Controls.Add(this.labelServerIP);
            this.groupBoxServer.Controls.Add(this.textBoxServerIP);
            this.groupBoxServer.Controls.Add(this.labelPort);
            this.groupBoxServer.Controls.Add(this.textBoxPort);
            this.groupBoxServer.Controls.Add(this.buttonStart);
            this.groupBoxServer.Controls.Add(this.buttonStop);
            this.groupBoxServer.Location = new System.Drawing.Point(12, 12);
            this.groupBoxServer.Name = "groupBoxServer";
            this.groupBoxServer.Size = new System.Drawing.Size(250, 150);
            this.groupBoxServer.TabIndex = 0;
            this.groupBoxServer.TabStop = false;
            this.groupBoxServer.Text = "Server Configuration";
            // 
            // labelServerIP
            // 
            this.labelServerIP.AutoSize = true;
            this.labelServerIP.Location = new System.Drawing.Point(10, 25);
            this.labelServerIP.Name = "labelServerIP";
            this.labelServerIP.Size = new System.Drawing.Size(54, 13);
            this.labelServerIP.TabIndex = 0;
            this.labelServerIP.Text = "Server IP:";
            // 
            // textBoxServerIP
            // 
            this.textBoxServerIP.Location = new System.Drawing.Point(10, 45);
            this.textBoxServerIP.Name = "textBoxServerIP";
            this.textBoxServerIP.ReadOnly = true;
            this.textBoxServerIP.Size = new System.Drawing.Size(230, 20);
            this.textBoxServerIP.TabIndex = 1;
            this.toolTip.SetToolTip(this.textBoxServerIP, "Your server\'s IP address");
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(10, 75);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(60, 13);
            this.labelPort.TabIndex = 2;
            this.labelPort.Text = "Listen Port:";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(10, 95);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(230, 20);
            this.textBoxPort.TabIndex = 2;
            this.toolTip.SetToolTip(this.textBoxPort, "Enter the port number to listen for connections");
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(10, 120);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(110, 25);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "Start Server";
            this.buttonStart.UseVisualStyleBackColor = true;
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(130, 120);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(110, 25);
            this.buttonStop.TabIndex = 4;
            this.buttonStop.Text = "Stop Server";
            this.buttonStop.UseVisualStyleBackColor = true;
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 528);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(897, 22);
            this.statusBar.TabIndex = 11;
            this.statusBar.Text = "Server Ready";
            // 
            // groupBoxSaveLocation
            // 
            this.groupBoxSaveLocation.Controls.Add(this.textBoxSaveLocation);
            this.groupBoxSaveLocation.Controls.Add(this.buttonBrowse);
            this.groupBoxSaveLocation.Location = new System.Drawing.Point(12, 170);
            this.groupBoxSaveLocation.Name = "groupBoxSaveLocation";
            this.groupBoxSaveLocation.Size = new System.Drawing.Size(250, 80);
            this.groupBoxSaveLocation.TabIndex = 6;
            this.groupBoxSaveLocation.TabStop = false;
            this.groupBoxSaveLocation.Text = "Save Location";
            // 
            // textBoxSaveLocation
            // 
            this.textBoxSaveLocation.Location = new System.Drawing.Point(10, 20);
            this.textBoxSaveLocation.Name = "textBoxSaveLocation";
            this.textBoxSaveLocation.ReadOnly = true;
            this.textBoxSaveLocation.Size = new System.Drawing.Size(230, 20);
            this.textBoxSaveLocation.TabIndex = 0;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(10, 45);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(230, 25);
            this.buttonBrowse.TabIndex = 1;
            this.buttonBrowse.Text = "Browse...";
            this.toolTip.SetToolTip(this.buttonBrowse, "Select folder to save received files");
            // 
            // progressBarTransfer
            // 
            this.progressBarTransfer.Location = new System.Drawing.Point(12, 300);
            this.progressBarTransfer.Name = "progressBarTransfer";
            this.progressBarTransfer.Size = new System.Drawing.Size(250, 20);
            this.progressBarTransfer.TabIndex = 8;
            // 
            // labelCurrentFile
            // 
            this.labelCurrentFile.AutoSize = true;
            this.labelCurrentFile.Location = new System.Drawing.Point(12, 260);
            this.labelCurrentFile.Name = "labelCurrentFile";
            this.labelCurrentFile.Size = new System.Drawing.Size(92, 13);
            this.labelCurrentFile.TabIndex = 9;
            this.labelCurrentFile.Text = "Current File: None";
            // 
            // labelTransferStatus
            // 
            this.labelTransferStatus.AutoSize = true;
            this.labelTransferStatus.Location = new System.Drawing.Point(12, 280);
            this.labelTransferStatus.Name = "labelTransferStatus";
            this.labelTransferStatus.Size = new System.Drawing.Size(74, 13);
            this.labelTransferStatus.TabIndex = 10;
            this.labelTransferStatus.Text = "Status: Ready";
            // 
            // listViewFiles
            // 
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFileName,
            this.columnFileSize,
            this.columnStatus,
            this.columnProgress});
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.GridLines = true;
            this.listViewFiles.HideSelection = false;
            this.listViewFiles.Location = new System.Drawing.Point(280, 12);
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Size = new System.Drawing.Size(600, 300);
            this.listViewFiles.TabIndex = 5;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            // 
            // columnFileName
            // 
            this.columnFileName.Text = "File Name";
            this.columnFileName.Width = 250;
            // 
            // columnFileSize
            // 
            this.columnFileSize.Text = "Size";
            this.columnFileSize.Width = 100;
            // 
            // columnStatus
            // 
            this.columnStatus.Text = "Status";
            this.columnStatus.Width = 100;
            // 
            // columnProgress
            // 
            this.columnProgress.Text = "Progress";
            this.columnProgress.Width = 150;
            // 
            // groupBoxConnectedClients
            // 
            this.groupBoxConnectedClients.Controls.Add(this.listViewClients);
            this.groupBoxConnectedClients.Controls.Add(this.listViewConnectionHistory);
            this.groupBoxConnectedClients.Location = new System.Drawing.Point(280, 320);
            this.groupBoxConnectedClients.Name = "groupBoxConnectedClients";
            this.groupBoxConnectedClients.Size = new System.Drawing.Size(600, 400);
            this.groupBoxConnectedClients.TabIndex = 7;
            this.groupBoxConnectedClients.TabStop = false;
            this.groupBoxConnectedClients.Text = "Connected Clients and History";
            // 
            // listViewConnectionHistory
            // 
            this.listViewConnectionHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHistoryIP,
            this.columnHistoryPort,
            this.columnHistoryTime,
            this.columnHistoryStatus});
            this.listViewConnectionHistory.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listViewConnectionHistory.FullRowSelect = true;
            this.listViewConnectionHistory.GridLines = true;
            this.listViewConnectionHistory.HideSelection = false;
            this.listViewConnectionHistory.Location = new System.Drawing.Point(3, 216);
            this.listViewConnectionHistory.Name = "listViewConnectionHistory";
            this.listViewConnectionHistory.Size = new System.Drawing.Size(594, 181);
            this.listViewConnectionHistory.TabIndex = 1;
            this.listViewConnectionHistory.UseCompatibleStateImageBehavior = false;
            this.listViewConnectionHistory.View = System.Windows.Forms.View.Details;
            // 
            // columnHistoryIP
            // 
            this.columnHistoryIP.Text = "IP Address";
            this.columnHistoryIP.Width = 150;
            // 
            // columnHistoryPort
            // 
            this.columnHistoryPort.Text = "Port";
            this.columnHistoryPort.Width = 100;
            // 
            // columnHistoryTime
            // 
            this.columnHistoryTime.Text = "Transfer Time";
            this.columnHistoryTime.Width = 150;

            this.columnHistoryStatus.Text = "Status";
            this.columnHistoryStatus.Width = 100;
            // 
            // listViewClients
            // 
            this.listViewClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnClientIP,
            this.columnHostName,
            this.columnClientStatus,
            this.columnConnectedTime});
            this.listViewClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewClients.FullRowSelect = true;
            this.listViewClients.GridLines = true;
            this.listViewClients.HideSelection = false;
            this.listViewClients.Location = new System.Drawing.Point(3, 16);
            this.listViewClients.Name = "listViewClients";
            this.listViewClients.Size = new System.Drawing.Size(594, 200);
            this.listViewClients.TabIndex = 0;
            this.listViewClients.UseCompatibleStateImageBehavior = false;
            this.listViewClients.View = System.Windows.Forms.View.Details;
            // 
            // columnClientIP
            // 
            this.columnClientIP.Text = "IP Address";
            this.columnClientIP.Width = 200;
            // 
            // columnHostName
            // 
            this.columnHostName.Text = "Host Name";
            this.columnHostName.Width = 150;
            // 
            // columnClientPort
            // 
            this.columnClientStatus.Text = "Status";
            this.columnClientStatus.Width = 100;
            // 
            // columnConnectedTime
            // 
            this.columnConnectedTime.Text = "Connected Time";
            this.columnConnectedTime.Width = 150;
            // 
            // TCPServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 550);
            this.Controls.Add(this.groupBoxServer);
            this.Controls.Add(this.groupBoxSaveLocation);
            this.Controls.Add(this.listViewFiles);
            this.Controls.Add(this.groupBoxConnectedClients);
            this.Controls.Add(this.progressBarTransfer);
            this.Controls.Add(this.labelCurrentFile);
            this.Controls.Add(this.labelTransferStatus);
            this.Controls.Add(this.statusBar);
            this.MinimumSize = new System.Drawing.Size(910, 589);
            this.Name = "TCPServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TCP File Transfer Server";
            this.groupBoxServer.ResumeLayout(false);
            this.groupBoxServer.PerformLayout();
            this.groupBoxSaveLocation.ResumeLayout(false);
            this.groupBoxSaveLocation.PerformLayout();
            this.groupBoxConnectedClients.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.GroupBox groupBoxServer;
        private System.Windows.Forms.Label labelServerIP;
        private System.Windows.Forms.TextBox textBoxServerIP;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.ColumnHeader columnFileName;
        private System.Windows.Forms.ColumnHeader columnFileSize;
        private System.Windows.Forms.ColumnHeader columnStatus;
        private System.Windows.Forms.ColumnHeader columnProgress;
        private System.Windows.Forms.GroupBox groupBoxSaveLocation;
        private System.Windows.Forms.TextBox textBoxSaveLocation;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.ProgressBar progressBarTransfer;
        private System.Windows.Forms.Label labelCurrentFile;
        private System.Windows.Forms.Label labelTransferStatus;
        private System.Windows.Forms.GroupBox groupBoxConnectedClients;
        private System.Windows.Forms.ListView listViewClients;
        private System.Windows.Forms.ColumnHeader columnClientIP;
        private System.Windows.Forms.ColumnHeader columnClientStatus;
        private System.Windows.Forms.ColumnHeader columnConnectedTime;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ListView listViewConnectionHistory;
        private System.Windows.Forms.ColumnHeader columnHistoryIP;
        private System.Windows.Forms.ColumnHeader columnHistoryPort;
        private System.Windows.Forms.ColumnHeader columnHistoryTime;
        private System.Windows.Forms.ColumnHeader columnHistoryStatus;
        private System.Windows.Forms.ColumnHeader columnHostName;
    }
}