namespace Client_test
{
    partial class Client
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox messageListBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Label nicknameLabel;
        private System.Windows.Forms.TextBox nicknameTextBox;
        private System.Windows.Forms.GroupBox connectedUsersGroupBox;
        private System.Windows.Forms.ListBox connectedUsersListBox;

        private void InitializeComponent()
        {
            messageListBox = new ListBox();
            connectButton = new Button();
            disconnectButton = new Button();
            messageTextBox = new TextBox();
            ipLabel = new Label();
            ipTextBox = new TextBox();
            portLabel = new Label();
            portTextBox = new TextBox();
            sendButton = new Button();
            nicknameLabel = new Label();
            nicknameTextBox = new TextBox();
            connectedUsersGroupBox = new GroupBox();
            connectedUsersListBox = new ListBox();
            connectedUsersGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // messageListBox
            // 
            messageListBox.FormattingEnabled = true;
            messageListBox.ItemHeight = 25;
            messageListBox.Location = new Point(9, 9);
            messageListBox.Margin = new Padding(2);
            messageListBox.Name = "messageListBox";
            messageListBox.Size = new Size(530, 554);
            messageListBox.TabIndex = 0;
            // 
            // connectButton
            // 
            connectButton.Location = new Point(702, 34);
            connectButton.Margin = new Padding(2);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(154, 36);
            connectButton.TabIndex = 1;
            connectButton.Text = "연결";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // disconnectButton
            // 
            disconnectButton.Location = new Point(702, 94);
            disconnectButton.Margin = new Padding(2);
            disconnectButton.Name = "disconnectButton";
            disconnectButton.Size = new Size(154, 36);
            disconnectButton.TabIndex = 2;
            disconnectButton.Text = "연결 해제";
            disconnectButton.UseVisualStyleBackColor = true;
            disconnectButton.Click += disconnectButton_Click;
            // 
            // messageTextBox
            // 
            messageTextBox.Location = new Point(9, 570);
            messageTextBox.Margin = new Padding(2);
            messageTextBox.Name = "messageTextBox";
            messageTextBox.Size = new Size(410, 31);
            messageTextBox.TabIndex = 3;
            // 
            // ipLabel
            // 
            ipLabel.AutoSize = true;
            ipLabel.Location = new Point(543, 9);
            ipLabel.Margin = new Padding(2, 0, 2, 0);
            ipLabel.Name = "ipLabel";
            ipLabel.Size = new Size(63, 25);
            ipLabel.TabIndex = 4;
            ipLabel.Text = "IP주소";
            // 
            // ipTextBox
            // 
            ipTextBox.Location = new Point(543, 37);
            ipTextBox.Margin = new Padding(2);
            ipTextBox.Name = "ipTextBox";
            ipTextBox.Size = new Size(155, 31);
            ipTextBox.TabIndex = 5;
            // 
            // portLabel
            // 
            portLabel.AutoSize = true;
            portLabel.Location = new Point(543, 70);
            portLabel.Margin = new Padding(2, 0, 2, 0);
            portLabel.Name = "portLabel";
            portLabel.Size = new Size(48, 25);
            portLabel.TabIndex = 6;
            portLabel.Text = "포트";
            // 
            // portTextBox
            // 
            portTextBox.Location = new Point(543, 97);
            portTextBox.Margin = new Padding(2);
            portTextBox.Name = "portTextBox";
            portTextBox.Size = new Size(155, 31);
            portTextBox.TabIndex = 7;
            // 
            // sendButton
            // 
            sendButton.Location = new Point(423, 567);
            sendButton.Margin = new Padding(2);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(115, 36);
            sendButton.TabIndex = 8;
            sendButton.Text = "전송";
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += sendButton_Click;
            // 
            // nicknameLabel
            // 
            nicknameLabel.AutoSize = true;
            nicknameLabel.Location = new Point(543, 130);
            nicknameLabel.Margin = new Padding(2, 0, 2, 0);
            nicknameLabel.Name = "nicknameLabel";
            nicknameLabel.Size = new Size(66, 25);
            nicknameLabel.TabIndex = 9;
            nicknameLabel.Text = "닉네임";
            // 
            // nicknameTextBox
            // 
            nicknameTextBox.Location = new Point(543, 157);
            nicknameTextBox.Margin = new Padding(2);
            nicknameTextBox.Name = "nicknameTextBox";
            nicknameTextBox.Size = new Size(155, 31);
            nicknameTextBox.TabIndex = 10;
            // 
            // connectedUsersGroupBox
            // 
            connectedUsersGroupBox.Controls.Add(connectedUsersListBox);
            connectedUsersGroupBox.Location = new Point(543, 192);
            connectedUsersGroupBox.Margin = new Padding(2);
            connectedUsersGroupBox.Name = "connectedUsersGroupBox";
            connectedUsersGroupBox.Padding = new Padding(2);
            connectedUsersGroupBox.Size = new Size(314, 415);
            connectedUsersGroupBox.TabIndex = 11;
            connectedUsersGroupBox.TabStop = false;
            connectedUsersGroupBox.Text = "접속자";
            // 
            // connectedUsersListBox
            // 
            connectedUsersListBox.FormattingEnabled = true;
            connectedUsersListBox.ItemHeight = 25;
            connectedUsersListBox.Location = new Point(6, 28);
            connectedUsersListBox.Margin = new Padding(2);
            connectedUsersListBox.Name = "connectedUsersListBox";
            connectedUsersListBox.Size = new Size(304, 379);
            connectedUsersListBox.TabIndex = 0;
            // 
            // Client
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(868, 614);
            Controls.Add(connectedUsersGroupBox);
            Controls.Add(nicknameTextBox);
            Controls.Add(nicknameLabel);
            Controls.Add(sendButton);
            Controls.Add(portTextBox);
            Controls.Add(portLabel);
            Controls.Add(ipTextBox);
            Controls.Add(ipLabel);
            Controls.Add(messageTextBox);
            Controls.Add(disconnectButton);
            Controls.Add(connectButton);
            Controls.Add(messageListBox);
            KeyPreview = true;
            Margin = new Padding(2);
            Name = "Client";
            Text = "Client";
            FormClosing += Form1_FormClosing;
            KeyDown += Form1_KeyDown;
            connectedUsersGroupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}