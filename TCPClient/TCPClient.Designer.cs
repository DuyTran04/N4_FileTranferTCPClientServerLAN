using System;

namespace N4_FileTranferTCPClientServerLAN
{
    partial class TCPClient
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.chkLimitFiles = new System.Windows.Forms.CheckBox();
            this.numericFileLimit = new System.Windows.Forms.NumericUpDown();
            this.connectButton = new System.Windows.Forms.Button();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.fileListView = new System.Windows.Forms.ListView();
            this.columnFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnFileSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxFileTransfer = new System.Windows.Forms.GroupBox();
            this.lblSelectedFile = new System.Windows.Forms.Label();
            this.txtSelectedFile = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBoxFileTransfer.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(12, 112);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(95, 45);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            // 
            // disconnectButton
            // 
            this.disconnectButton.Enabled = false;
            this.disconnectButton.Location = new System.Drawing.Point(113, 112);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(95, 45);
            this.disconnectButton.TabIndex = 1;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 483);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(820, 22);
            this.statusBar1.TabIndex = 2;
            this.statusBar1.Text = "Ready";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.hostTextBox);
            this.groupBox1.Controls.Add(this.portTextBox);
            this.groupBox1.Controls.Add(this.chkLimitFiles);
            this.groupBox1.Controls.Add(this.numericFileLimit);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(196, 94);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Connection";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // hostTextBox
            // 
            this.hostTextBox.Location = new System.Drawing.Point(73, 19);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(117, 20);
            this.hostTextBox.TabIndex = 2;
            this.toolTip.SetToolTip(this.hostTextBox, "Enter the IP address of the server computer");
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(73, 54);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(117, 20);
            this.portTextBox.TabIndex = 3;
            this.toolTip.SetToolTip(this.portTextBox, "Enter the port number configured on the server");
            // 
            // fileListView
            // 
            this.fileListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFileName,
            this.columnFileSize,
            this.columnStatus,
            this.columnProgress});
            this.fileListView.FullRowSelect = true;
            this.fileListView.GridLines = true;
            this.fileListView.HideSelection = false;
            this.fileListView.Location = new System.Drawing.Point(214, 12);
            this.fileListView.Name = "fileListView";
            this.fileListView.Size = new System.Drawing.Size(594, 355);
            this.fileListView.TabIndex = 4;
            this.fileListView.UseCompatibleStateImageBehavior = false;
            this.fileListView.View = System.Windows.Forms.View.Details;
            // 
            // columnFileName
            // 
            this.columnFileName.Text = "File Name";
            this.columnFileName.Width = 200;
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
            // groupBoxFileTransfer
            // 
            this.groupBoxFileTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFileTransfer.Controls.Add(this.lblSelectedFile);
            this.groupBoxFileTransfer.Controls.Add(this.txtSelectedFile);
            this.groupBoxFileTransfer.Controls.Add(this.btnBrowse);
            this.groupBoxFileTransfer.Controls.Add(this.btnSendFile);
            this.groupBoxFileTransfer.Controls.Add(this.progressBar);
            this.groupBoxFileTransfer.Location = new System.Drawing.Point(214, 373);
            this.groupBoxFileTransfer.Name = "groupBoxFileTransfer";
            this.groupBoxFileTransfer.Size = new System.Drawing.Size(594, 104);
            this.groupBoxFileTransfer.TabIndex = 5;
            this.groupBoxFileTransfer.TabStop = false;
            this.groupBoxFileTransfer.Text = "File Transfer";
            // 
            // lblSelectedFile
            // 
            this.lblSelectedFile.AutoSize = true;
            this.lblSelectedFile.Location = new System.Drawing.Point(6, 22);
            this.lblSelectedFile.Name = "lblSelectedFile";
            this.lblSelectedFile.Size = new System.Drawing.Size(71, 13);
            this.lblSelectedFile.TabIndex = 0;
            this.lblSelectedFile.Text = "Selected File:";
            // 
            // txtSelectedFile
            // 
            this.txtSelectedFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSelectedFile.Location = new System.Drawing.Point(79, 19);
            this.txtSelectedFile.Name = "txtSelectedFile";
            this.txtSelectedFile.ReadOnly = true;
            this.txtSelectedFile.Size = new System.Drawing.Size(347, 20);
            this.txtSelectedFile.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(432, 17);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.toolTip.SetToolTip(this.btnBrowse, "Select a file to send");
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // btnSendFile
            // 
            this.btnSendFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendFile.Enabled = false;
            this.btnSendFile.Location = new System.Drawing.Point(513, 17);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(75, 23);
            this.btnSendFile.TabIndex = 3;
            this.btnSendFile.Text = "Send File";
            this.toolTip.SetToolTip(this.btnSendFile, "Send the selected file to server");
            this.btnSendFile.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(6, 75);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(582, 23);
            this.progressBar.TabIndex = 4;
            // 
            // TCPClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 505);
            this.Controls.Add(this.groupBoxFileTransfer);
            this.Controls.Add(this.fileListView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.connectButton);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "TCPClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TCP File Transfer Client";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxFileTransfer.ResumeLayout(false);
            this.groupBoxFileTransfer.PerformLayout();
            this.ResumeLayout(false);

            // Checkbox
            this.chkLimitFiles.AutoSize = true;
            this.chkLimitFiles.Checked = true; // Mặc định bật giới hạn
            this.chkLimitFiles.Location = new System.Drawing.Point(6, 80);
            this.chkLimitFiles.Name = "chkLimitFiles";
            this.chkLimitFiles.Size = new System.Drawing.Size(90, 17);
            this.chkLimitFiles.TabIndex = 4;
            this.chkLimitFiles.Text = "Giới hạn file:";
            this.toolTip.SetToolTip(this.chkLimitFiles, "Bật/tắt giới hạn số lượng file");

            // NumericUpDown
            this.numericFileLimit.Location = new System.Drawing.Point(102, 78);
            this.numericFileLimit.Name = "numericFileLimit";
            this.numericFileLimit.Size = new System.Drawing.Size(60, 20);
            this.numericFileLimit.TabIndex = 5;
            this.numericFileLimit.Minimum = 1;
            this.numericFileLimit.Maximum = 100;
            this.numericFileLimit.Value = 5;
            this.numericFileLimit.Enabled = true; // Mặc định cho phép nhập số

            // Thêm event cho checkbox để enable/disable NumericUpDown
            this.chkLimitFiles.CheckedChanged += new System.EventHandler(this.chkLimitFiles_CheckedChanged);

        }

        #endregion

        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.ListView fileListView;
        private System.Windows.Forms.ColumnHeader columnFileName;
        private System.Windows.Forms.ColumnHeader columnFileSize;
        private System.Windows.Forms.ColumnHeader columnStatus;
        private System.Windows.Forms.ColumnHeader columnProgress;
        private System.Windows.Forms.GroupBox groupBoxFileTransfer;
        private System.Windows.Forms.Label lblSelectedFile;
        private System.Windows.Forms.TextBox txtSelectedFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox chkLimitFiles;
        private System.Windows.Forms.NumericUpDown numericFileLimit;

        // Event handler để bật/tắt NumericUpDown khi checkbox thay đổi
        private void chkLimitFiles_CheckedChanged(object sender, EventArgs e)
        {
            numericFileLimit.Enabled = chkLimitFiles.Checked;
        }
    }
}