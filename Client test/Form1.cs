using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Client_test
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;
        int mynum;
        bool isconnected;
        string nickname;
        static string str;
        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            button3.Enabled = false;
            isconnected = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(textBox3.Text, out int port))
                {
                    client = new TcpClient(textBox2.Text, port);
                    stream = client.GetStream();
                    receiveThread = new Thread(ReceiveMessages);
                    receiveThread.IsBackground = true;
                    receiveThread.Start();
                    listBox1.Items.Add("Connected to server...");
                    isconnected = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button1.Enabled = false;
                    textBox4.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void ReceiveMessages()
        {
            byte[] buffer = new byte[102400];

            while (true)
            {
                try
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        continue;

                    string[] message = Encoding.UTF8.GetString(buffer, 0, bytesRead).Split('⧫');

                    if (message[0] == "0")
                    {
                        Invoke(new Action(() => listBox1.Items.Add(message[1])));
                    }
                    else if (message[0] == "1")
                    {
                        
                        client.Close();
                        if (message[1] != "")
                            MessageBox.Show(message[1]);

                        Invoke(new Action(() =>
                        {
                            listBox1.Items.Add("Disconnected from server...");
                            button2.Enabled = false;
                            button3.Enabled = false;
                            button1.Enabled = true;
                            isconnected = false;
                            textBox4.Enabled = true;
                        }));

                        break;
                    }
                    else if (message[0] == "2")
                    {
                        mynum = int.Parse(message[1]);
                        Invoke(new Action(() => str = textBox4.Text));
                        if (str == "")
                        {
                            nickname = "Client" + mynum.ToString();
                            Invoke(new Action(() => textBox4.Text = nickname));
                        }
                        else
                        {
                            nickname = str;
                        }
                        stream.Write(Encoding.UTF8.GetBytes("3⧫" + nickname));
                    }
                }
                catch (Exception ex)
                {
                    break;
                }


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stream.Write(Encoding.UTF8.GetBytes("1⧫"));
            stream.Close();
            client.Close();
            listBox1.Items.Add("Disconnected from server...");
            button2.Enabled = false;
            button3.Enabled = false;
            button1.Enabled = true;
            isconnected = false;
            textBox4.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isconnected)
            {
                stream.Write(Encoding.UTF8.GetBytes("1⧫"));
                stream.Close();
                client.Close();
                listBox1.Items.Add("Disconnected from server...");
                button2.Enabled = false;
                button3.Enabled = false;
                button1.Enabled = true;
                isconnected = false;
                textBox4.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            stream.Write(Encoding.UTF8.GetBytes("0⧫" + $"{nickname}:" + textBox1.Text));
            listBox1.Items.Add($"{nickname}:" + textBox1.Text);
            textBox1.Text = "";
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && isconnected)
            {
                stream.Write(Encoding.UTF8.GetBytes("0⧫" + $"{nickname}:" + textBox1.Text));
                listBox1.Items.Add($"{nickname}:" + textBox1.Text);
                textBox1.Text = "";
            }
        }
    }
}
