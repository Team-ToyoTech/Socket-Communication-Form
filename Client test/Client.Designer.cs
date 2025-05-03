namespace Client
{
    partial class Client
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MessageListBox = new ListBox();
            ConnectButton = new Button();
            DisconnectButton = new Button();
            IPLabel = new Label();
            IPTextBox = new TextBox();
            PortLabel = new Label();
            PortTextBox = new TextBox();
            NicknameLabel = new Label();
            NickNameTextBox = new TextBox();
            ConnectedGroupBox = new GroupBox();
            ConnectedListBox = new ListBox();
            ConnectedGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // MessageListBox
            // 
            MessageListBox.FormattingEnabled = true;
            MessageListBox.ItemHeight = 25;
            MessageListBox.Location = new Point(9, 9);
            MessageListBox.Margin = new Padding(2);
            MessageListBox.Name = "MessageListBox";
            MessageListBox.Size = new Size(313, 479);
            MessageListBox.TabIndex = 0;
            // 
            // ConnectButton
            // 
            ConnectButton.Location = new Point(485, 34);
            ConnectButton.Margin = new Padding(2);
            ConnectButton.Name = "ConnectButton";
            ConnectButton.Size = new Size(154, 36);
            ConnectButton.TabIndex = 1;
            ConnectButton.Text = "연결";
            ConnectButton.UseVisualStyleBackColor = true;
            ConnectButton.Click += ConnectButton_Click;
            // 
            // DisconnectButton
            // 
            DisconnectButton.Location = new Point(485, 94);
            DisconnectButton.Margin = new Padding(2);
            DisconnectButton.Name = "DisconnectButton";
            DisconnectButton.Size = new Size(154, 36);
            DisconnectButton.TabIndex = 2;
            DisconnectButton.Text = "연결 해제";
            DisconnectButton.UseVisualStyleBackColor = true;
            DisconnectButton.Click += DisconnectButton_Click;
            // 
            // IPLabel
            // 
            IPLabel.AutoSize = true;
            IPLabel.Location = new Point(326, 9);
            IPLabel.Margin = new Padding(2, 0, 2, 0);
            IPLabel.Name = "IPLabel";
            IPLabel.Size = new Size(63, 25);
            IPLabel.TabIndex = 4;
            IPLabel.Text = "IP주소";
            // 
            // IPTextBox
            // 
            IPTextBox.Location = new Point(326, 37);
            IPTextBox.Margin = new Padding(2);
            IPTextBox.Name = "IPTextBox";
            IPTextBox.Size = new Size(155, 31);
            IPTextBox.TabIndex = 5;
            // 
            // PortLabel
            // 
            PortLabel.AutoSize = true;
            PortLabel.Location = new Point(326, 70);
            PortLabel.Margin = new Padding(2, 0, 2, 0);
            PortLabel.Name = "PortLabel";
            PortLabel.Size = new Size(48, 25);
            PortLabel.TabIndex = 6;
            PortLabel.Text = "포트";
            // 
            // PortTextBox
            // 
            PortTextBox.Location = new Point(326, 97);
            PortTextBox.Margin = new Padding(2);
            PortTextBox.Name = "PortTextBox";
            PortTextBox.Size = new Size(155, 31);
            PortTextBox.TabIndex = 7;
            // 
            // NicknameLabel
            // 
            NicknameLabel.AutoSize = true;
            NicknameLabel.Location = new Point(326, 130);
            NicknameLabel.Margin = new Padding(2, 0, 2, 0);
            NicknameLabel.Name = "NicknameLabel";
            NicknameLabel.Size = new Size(66, 25);
            NicknameLabel.TabIndex = 9;
            NicknameLabel.Text = "닉네임";
            // 
            // NickNameTextBox
            // 
            NickNameTextBox.Location = new Point(326, 157);
            NickNameTextBox.Margin = new Padding(2);
            NickNameTextBox.Name = "NickNameTextBox";
            NickNameTextBox.Size = new Size(155, 31);
            NickNameTextBox.TabIndex = 10;
            NickNameTextBox.KeyDown += NickNameTextBox_KeyDown;
            // 
            // ConnectedGroupBox
            // 
            ConnectedGroupBox.Controls.Add(ConnectedListBox);
            ConnectedGroupBox.Location = new Point(326, 192);
            ConnectedGroupBox.Margin = new Padding(2);
            ConnectedGroupBox.Name = "ConnectedGroupBox";
            ConnectedGroupBox.Padding = new Padding(2);
            ConnectedGroupBox.Size = new Size(313, 296);
            ConnectedGroupBox.TabIndex = 11;
            ConnectedGroupBox.TabStop = false;
            ConnectedGroupBox.Text = "접속자";
            // 
            // ConnectedListBox
            // 
            ConnectedListBox.FormattingEnabled = true;
            ConnectedListBox.ItemHeight = 25;
            ConnectedListBox.Location = new Point(4, 28);
            ConnectedListBox.Margin = new Padding(2);
            ConnectedListBox.Name = "ConnectedListBox";
            ConnectedListBox.Size = new Size(304, 254);
            ConnectedListBox.TabIndex = 0;
            // 
            // Client
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(651, 498);
            Controls.Add(ConnectedGroupBox);
            Controls.Add(NickNameTextBox);
            Controls.Add(NicknameLabel);
            Controls.Add(PortTextBox);
            Controls.Add(PortLabel);
            Controls.Add(IPTextBox);
            Controls.Add(IPLabel);
            Controls.Add(DisconnectButton);
            Controls.Add(ConnectButton);
            Controls.Add(MessageListBox);
            KeyPreview = true;
            Margin = new Padding(2);
            Name = "Client";
            Text = "Client";
            ConnectedGroupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox MessageListBox;
        private Button ConnectButton;
        private Button DisconnectButton;
        private Label IPLabel;
        private TextBox IPTextBox;
        private Label PortLabel;
        private TextBox PortTextBox;
        private Label NicknameLabel;
        private TextBox NickNameTextBox;
        private GroupBox ConnectedGroupBox;
        private ListBox ConnectedListBox;
    }
}
