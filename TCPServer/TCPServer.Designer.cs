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

            // Initialize components
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
            this.columnFileName = new System.Windows.Forms.ColumnHeader();
            this.columnFileSize = new System.Windows.Forms.ColumnHeader();
            this.columnStatus = new System.Windows.Forms.ColumnHeader();
            this.columnProgress = new System.Windows.Forms.ColumnHeader();
            this.groupBoxConnectedClients = new System.Windows.Forms.GroupBox();
            this.listViewClients = new System.Windows.Forms.ListView();
            this.columnClientIP = new System.Windows.Forms.ColumnHeader();
            this.columnClientPort = new System.Windows.Forms.ColumnHeader();
            this.columnConnectedTime = new System.Windows.Forms.ColumnHeader();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);

            // Group Box Server
            this.groupBoxServer.SuspendLayout();
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

            // Server IP Label and TextBox
            this.labelServerIP.AutoSize = true;
            this.labelServerIP.Location = new System.Drawing.Point(10, 25);
            this.labelServerIP.Name = "labelServerIP";
            this.labelServerIP.Size = new System.Drawing.Size(54, 13);
            this.labelServerIP.Text = "Server IP:";

            this.textBoxServerIP.Location = new System.Drawing.Point(10, 45);
            this.textBoxServerIP.Name = "textBoxServerIP";
            this.textBoxServerIP.ReadOnly = true;
            this.textBoxServerIP.Size = new System.Drawing.Size(230, 20);
            this.textBoxServerIP.TabIndex = 1;

            // Port Label and TextBox
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(10, 75);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(60, 13);
            this.labelPort.Text = "Listen Port:";

            this.textBoxPort.Location = new System.Drawing.Point(10, 95);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(230, 20);
            this.textBoxPort.TabIndex = 2;

            // Start Button
            this.buttonStart.Location = new System.Drawing.Point(10, 120);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(110, 25);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "Start Server";
            this.buttonStart.UseVisualStyleBackColor = true;

            // Stop Button
            this.buttonStop.Location = new System.Drawing.Point(130, 120);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(110, 25);
            this.buttonStop.TabIndex = 4;
            this.buttonStop.Text = "Stop Server";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Enabled = false;

            // Files ListView
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.columnFileName,
                this.columnFileSize,
                this.columnStatus,
                this.columnProgress});
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.GridLines = true;
            this.listViewFiles.Location = new System.Drawing.Point(280, 12);
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Size = new System.Drawing.Size(600, 300);
            this.listViewFiles.TabIndex = 5;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;

            // File List Columns
            this.columnFileName.Text = "File Name";
            this.columnFileName.Width = 250;
            this.columnFileSize.Text = "Size";
            this.columnFileSize.Width = 100;
            this.columnStatus.Text = "Status";
            this.columnStatus.Width = 100;
            this.columnProgress.Text = "Progress";
            this.columnProgress.Width = 150;

            // Save Location Group Box
            this.groupBoxSaveLocation.Controls.Add(this.textBoxSaveLocation);
            this.groupBoxSaveLocation.Controls.Add(this.buttonBrowse);
            this.groupBoxSaveLocation.Location = new System.Drawing.Point(12, 170);
            this.groupBoxSaveLocation.Name = "groupBoxSaveLocation";
            this.groupBoxSaveLocation.Size = new System.Drawing.Size(250, 80);
            this.groupBoxSaveLocation.TabIndex = 6;
            this.groupBoxSaveLocation.Text = "Save Location";

            this.textBoxSaveLocation.Location = new System.Drawing.Point(10, 20);
            this.textBoxSaveLocation.Name = "textBoxSaveLocation";
            this.textBoxSaveLocation.ReadOnly = true;
            this.textBoxSaveLocation.Size = new System.Drawing.Size(230, 20);

            this.buttonBrowse.Location = new System.Drawing.Point(10, 45);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(230, 25);
            this.buttonBrowse.Text = "Browse...";

            // Connected Clients ListView Group
            this.groupBoxConnectedClients.Controls.Add(this.listViewClients);
            this.groupBoxConnectedClients.Location = new System.Drawing.Point(280, 320);
            this.groupBoxConnectedClients.Name = "groupBoxConnectedClients";
            this.groupBoxConnectedClients.Size = new System.Drawing.Size(600, 200);
            this.groupBoxConnectedClients.Text = "Connected Clients";

            this.listViewClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.columnClientIP,
                this.columnClientPort,
                this.columnConnectedTime});
            this.listViewClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewClients.FullRowSelect = true;
            this.listViewClients.GridLines = true;
            this.listViewClients.View = System.Windows.Forms.View.Details;

            // Client List Columns
            this.columnClientIP.Text = "IP Address";
            this.columnClientIP.Width = 200;
            this.columnClientPort.Text = "Port";
            this.columnClientPort.Width = 100;
            this.columnConnectedTime.Text = "Connected Time";
            this.columnConnectedTime.Width = 150;

            // Progress and Status
            this.labelCurrentFile.AutoSize = true;
            this.labelCurrentFile.Location = new System.Drawing.Point(12, 260);
            this.labelCurrentFile.Text = "Current File: None";

            this.labelTransferStatus.AutoSize = true;
            this.labelTransferStatus.Location = new System.Drawing.Point(12, 280);
            this.labelTransferStatus.Text = "Status: Ready";

            this.progressBarTransfer.Location = new System.Drawing.Point(12, 300);
            this.progressBarTransfer.Size = new System.Drawing.Size(250, 20);

            // Status Bar
            this.statusBar.Location = new System.Drawing.Point(0, 528);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(894, 22);
            this.statusBar.Text = "Server Ready";

            // ToolTips
            this.toolTip.SetToolTip(this.textBoxPort, "Enter the port number to listen for connections");
            this.toolTip.SetToolTip(this.buttonBrowse, "Select folder to save received files");
            this.toolTip.SetToolTip(this.textBoxServerIP, "Your server's IP address");

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 550);
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
        private System.Windows.Forms.ColumnHeader columnClientPort;
        private System.Windows.Forms.ColumnHeader columnConnectedTime;
        private System.Windows.Forms.ToolTip toolTip;
    }
}