namespace Server_test
{
    partial class Server
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Button startServerButton;
        private System.Windows.Forms.Button stopServerButton;
        private System.Windows.Forms.ListBox logListBox;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.GroupBox connectedUsersGroupBox;
        private System.Windows.Forms.ListBox connectedUsersListBox;

        private void InitializeComponent()
        {
            portLabel = new Label();
            portTextBox = new TextBox();
            startServerButton = new Button();
            stopServerButton = new Button();
            logListBox = new ListBox();
            messageTextBox = new TextBox();
            sendButton = new Button();
            ipLabel = new Label();
            connectedUsersGroupBox = new GroupBox();
            connectedUsersListBox = new ListBox();
            connectedUsersGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // portLabel
            // 
            portLabel.AutoSize = true;
            portLabel.Location = new Point(543, 8);
            portLabel.Margin = new Padding(2, 0, 2, 0);
            portLabel.Name = "portLabel";
            portLabel.Size = new Size(48, 25);
            portLabel.TabIndex = 0;
            portLabel.Text = "포트";
            // 
            // portTextBox
            // 
            portTextBox.Location = new Point(543, 35);
            portTextBox.Margin = new Padding(2);
            portTextBox.Name = "portTextBox";
            portTextBox.Size = new Size(155, 31);
            portTextBox.TabIndex = 1;
            // 
            // startServerButton
            // 
            startServerButton.Location = new Point(702, 32);
            startServerButton.Margin = new Padding(2);
            startServerButton.Name = "startServerButton";
            startServerButton.Size = new Size(154, 36);
            startServerButton.TabIndex = 2;
            startServerButton.Text = "서버 시작";
            startServerButton.UseVisualStyleBackColor = true;
            startServerButton.Click += startServerButton_Click;
            // 
            // stopServerButton
            // 
            stopServerButton.Location = new Point(702, 73);
            stopServerButton.Margin = new Padding(2);
            stopServerButton.Name = "stopServerButton";
            stopServerButton.Size = new Size(154, 36);
            stopServerButton.TabIndex = 3;
            stopServerButton.Text = "서버 종료";
            stopServerButton.UseVisualStyleBackColor = true;
            stopServerButton.Click += stopServerButton_Click;
            // 
            // logListBox
            // 
            logListBox.FormattingEnabled = true;
            logListBox.ItemHeight = 25;
            logListBox.Location = new Point(9, 9);
            logListBox.Margin = new Padding(2);
            logListBox.Name = "logListBox";
            logListBox.Size = new Size(530, 554);
            logListBox.TabIndex = 4;
            // 
            // messageTextBox
            // 
            messageTextBox.Location = new Point(9, 572);
            messageTextBox.Margin = new Padding(2);
            messageTextBox.Name = "messageTextBox";
            messageTextBox.Size = new Size(410, 31);
            messageTextBox.TabIndex = 5;
            // 
            // sendButton
            // 
            sendButton.Location = new Point(423, 569);
            sendButton.Margin = new Padding(2);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(115, 36);
            sendButton.TabIndex = 6;
            sendButton.Text = "전송";
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += sendButton_Click;
            // 
            // ipLabel
            // 
            ipLabel.AutoSize = true;
            ipLabel.Location = new Point(543, 78);
            ipLabel.Margin = new Padding(2, 0, 2, 0);
            ipLabel.Name = "ipLabel";
            ipLabel.Size = new Size(109, 100);
            ipLabel.TabIndex = 7;
            ipLabel.Text = "로컬 IP주소:\r\n0.0.0.0\r\n외부 IP주소:\r\n0.0.0.0";
            // 
            // connectedUsersGroupBox
            // 
            connectedUsersGroupBox.Controls.Add(connectedUsersListBox);
            connectedUsersGroupBox.Location = new Point(545, 190);
            connectedUsersGroupBox.Margin = new Padding(2);
            connectedUsersGroupBox.Name = "connectedUsersGroupBox";
            connectedUsersGroupBox.Padding = new Padding(2);
            connectedUsersGroupBox.Size = new Size(312, 413);
            connectedUsersGroupBox.TabIndex = 8;
            connectedUsersGroupBox.TabStop = false;
            connectedUsersGroupBox.Text = "접속자";
            // 
            // connectedUsersListBox
            // 
            connectedUsersListBox.FormattingEnabled = true;
            connectedUsersListBox.ItemHeight = 25;
            connectedUsersListBox.Location = new Point(4, 28);
            connectedUsersListBox.Margin = new Padding(2);
            connectedUsersListBox.Name = "connectedUsersListBox";
            connectedUsersListBox.Size = new Size(304, 379);
            connectedUsersListBox.TabIndex = 0;
            // 
            // Server
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(868, 614);
            Controls.Add(connectedUsersGroupBox);
            Controls.Add(ipLabel);
            Controls.Add(sendButton);
            Controls.Add(messageTextBox);
            Controls.Add(logListBox);
            Controls.Add(stopServerButton);
            Controls.Add(startServerButton);
            Controls.Add(portTextBox);
            Controls.Add(portLabel);
            KeyPreview = true;
            Margin = new Padding(2);
            Name = "Server";
            Text = "Server";
            FormClosing += Server_FormClosing;
            KeyDown += Server_KeyDown;
            connectedUsersGroupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}