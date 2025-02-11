namespace Server_test
{
    partial class Form1
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
            label1 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            listBox1 = new ListBox();
            textBox2 = new TextBox();
            button3 = new Button();
            label2 = new Label();
            groupBox1 = new GroupBox();
            listBox2 = new ListBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(588, 9);
            label1.Name = "label1";
            label1.Size = new Size(62, 32);
            label1.TabIndex = 0;
            label1.Text = "포트";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(588, 44);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(200, 39);
            textBox1.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(794, 40);
            button1.Name = "button1";
            button1.Size = new Size(200, 46);
            button1.TabIndex = 2;
            button1.Text = "서버 시작";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(794, 92);
            button2.Name = "button2";
            button2.Size = new Size(200, 46);
            button2.TabIndex = 3;
            button2.Text = "서버 종료";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(12, 12);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(570, 612);
            listBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(12, 634);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(414, 39);
            textBox2.TabIndex = 5;
            // 
            // button3
            // 
            button3.Location = new Point(432, 630);
            button3.Name = "button3";
            button3.Size = new Size(150, 46);
            button3.TabIndex = 6;
            button3.Text = "전송";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(588, 99);
            label2.Name = "label2";
            label2.Size = new Size(141, 128);
            label2.TabIndex = 7;
            label2.Text = "로컬 IP주소:\r\n0.0.0.0\r\n외부 IP주소:\r\n0.0.0.0";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(listBox2);
            groupBox1.Location = new Point(594, 230);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(400, 446);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "접속자";
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.Location = new Point(6, 38);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(388, 388);
            listBox2.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1006, 711);
            Controls.Add(groupBox1);
            Controls.Add(label2);
            Controls.Add(button3);
            Controls.Add(textBox2);
            Controls.Add(listBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            KeyPreview = true;
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            KeyDown += Form1_KeyDown;
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private ListBox listBox1;
        private TextBox textBox2;
        private Button button3;
        private Label label2;
        private GroupBox groupBox1;
        private ListBox listBox2;
    }
}
