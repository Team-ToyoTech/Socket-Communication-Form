namespace Server
{
    partial class Server
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
            PortLabel = new Label();
            PortTextBox = new TextBox();
            StartButton = new Button();
            StopButton = new Button();
            MessageListBox = new ListBox();
            IPLabel = new Label();
            ConnectedGroupBox = new GroupBox();
            ConnectedListBox = new ListBox();
            ConnectedGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // PortLabel
            // 
            PortLabel.AutoSize = true;
            PortLabel.Location = new Point(328, 9);
            PortLabel.Margin = new Padding(2, 0, 2, 0);
            PortLabel.Name = "PortLabel";
            PortLabel.Size = new Size(48, 25);
            PortLabel.TabIndex = 0;
            PortLabel.Text = "포트";
            // 
            // PortTextBox
            // 
            PortTextBox.Location = new Point(328, 36);
            PortTextBox.Margin = new Padding(2);
            PortTextBox.Name = "PortTextBox";
            PortTextBox.Size = new Size(155, 31);
            PortTextBox.TabIndex = 1;
            PortTextBox.KeyDown += PortTextBox_KeyDown;
            // 
            // StartButton
            // 
            StartButton.Location = new Point(487, 33);
            StartButton.Margin = new Padding(2);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(154, 36);
            StartButton.TabIndex = 2;
            StartButton.Text = "서버 시작";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // StopButton
            // 
            StopButton.Location = new Point(487, 74);
            StopButton.Margin = new Padding(2);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(154, 36);
            StopButton.TabIndex = 3;
            StopButton.Text = "서버 종료";
            StopButton.UseVisualStyleBackColor = true;
            StopButton.Click += StopButton_Click;
            // 
            // MessageListBox
            // 
            MessageListBox.FormattingEnabled = true;
            MessageListBox.ItemHeight = 25;
            MessageListBox.Location = new Point(11, 11);
            MessageListBox.Margin = new Padding(2);
            MessageListBox.Name = "MessageListBox";
            MessageListBox.Size = new Size(313, 479);
            MessageListBox.TabIndex = 4;
            // 
            // IPLabel
            // 
            IPLabel.AutoSize = true;
            IPLabel.Location = new Point(328, 79);
            IPLabel.Margin = new Padding(2, 0, 2, 0);
            IPLabel.Name = "IPLabel";
            IPLabel.Size = new Size(109, 100);
            IPLabel.TabIndex = 7;
            IPLabel.Text = "로컬 IP주소:\r\n0.0.0.0\r\n외부 IP주소:\r\n0.0.0.0";
            // 
            // ConnectedGroupBox
            // 
            ConnectedGroupBox.Controls.Add(ConnectedListBox);
            ConnectedGroupBox.Location = new Point(333, 182);
            ConnectedGroupBox.Margin = new Padding(2);
            ConnectedGroupBox.Name = "ConnectedGroupBox";
            ConnectedGroupBox.Padding = new Padding(2);
            ConnectedGroupBox.Size = new Size(308, 308);
            ConnectedGroupBox.TabIndex = 8;
            ConnectedGroupBox.TabStop = false;
            ConnectedGroupBox.Text = "접속자";
            // 
            // ConnectedListBox
            // 
            ConnectedListBox.FormattingEnabled = true;
            ConnectedListBox.ItemHeight = 25;
            ConnectedListBox.Location = new Point(5, 34);
            ConnectedListBox.Margin = new Padding(2);
            ConnectedListBox.Name = "ConnectedListBox";
            ConnectedListBox.Size = new Size(299, 254);
            ConnectedListBox.TabIndex = 0;
            // 
            // Server
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 502);
            Controls.Add(ConnectedGroupBox);
            Controls.Add(MessageListBox);
            Controls.Add(IPLabel);
            Controls.Add(StopButton);
            Controls.Add(StartButton);
            Controls.Add(PortTextBox);
            Controls.Add(PortLabel);
            KeyPreview = true;
            Margin = new Padding(2);
            Name = "Server";
            Text = "Server";
            FormClosing += Client_FormClosing;
            ConnectedGroupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label PortLabel;
        private TextBox PortTextBox;
        private Button StartButton;
        private Button StopButton;
        private ListBox MessageListBox;
        private Label IPLabel;
        private GroupBox ConnectedGroupBox;
        private ListBox ConnectedListBox;
    }
}
