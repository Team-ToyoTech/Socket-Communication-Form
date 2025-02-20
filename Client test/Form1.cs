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
            string msg = "";

            while (true)
            {
                try
                {

                    buffer = new byte[102400];
                    if (msg != "")
                    {
                        buffer = Encoding.UTF8.GetBytes(msg);
                    }
                    while (true)
                    {
                        byte[] data = new byte[256];
                        int bytesRead = stream.Read(data, 0, data.Length);
                        if (bytesRead == 0)
                            break;
                        data = data.Where(x => x != 0).ToArray();
                        if (buffer.Length == 102400) buffer = data;
                        else buffer = buffer.Concat(data).ToArray();

                        msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                        if (msg.Contains('◊')) break;
                    }
                    if (Encoding.UTF8.GetString(buffer, 0, buffer.Length).Split("◊").Length == 1)
                        msg = "";
                    else msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length).Split("◊")[1];
                    string[] message = Encoding.UTF8.GetString(buffer, 0, buffer.Length).Split("◊")[0].Split('⧫');

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
                            listBox2.Items.Clear();
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
                        else if(!str.Contains('⧫'))
                        {
                            nickname = str;
                        }
                        else
                        {
                            MessageBox.Show("이름에 다음 문자가 포함되어서는 안됩니다: ⧫\n기본 이름으로 진행합니다.");
                            nickname = "Client" + mynum.ToString();
                            Invoke(new Action(() => textBox4.Text = nickname));
                        }
                        stream.Write(Encoding.UTF8.GetBytes("3⧫" + nickname + '◊'));
                        stream.Flush();
                    }
                    else if (message[0] == "4")
                    {
                        Invoke(new Action(() => listBox2.Items.Add(message[1])));
                    }
                    else if(message[0] == "5")
                    {
                        Invoke(new Action(() => listBox2.Items.Remove(message[1])));
                    }
                    Invoke(new Action(() => listBox1.TopIndex = listBox1.Items.Count - 1));
                }
                catch (Exception ex)
                {
                    break;
                }


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stream.Write(Encoding.UTF8.GetBytes("1⧫◊"));
            stream.Flush();
            stream.Close();
            client.Close();
            listBox1.Items.Add("Disconnected from server...");
            button2.Enabled = false;
            button3.Enabled = false;
            button1.Enabled = true;
            isconnected = false;
            textBox4.Enabled = true;
            listBox2.Items.Clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isconnected)
            {
                stream.Write(Encoding.UTF8.GetBytes("1⧫◊"));
                stream.Flush();
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
            if (!textBox1.Text.Contains('⧫') && !textBox1.Text.Contains('◊'))
            {
                if (textBox1.Text != "")
                {
                    stream.Write(Encoding.UTF8.GetBytes("0⧫" + $"{nickname}:" + textBox1.Text + '◊'));
                    stream.Flush();
                    listBox1.Items.Add($"{nickname}:" + textBox1.Text);
                    textBox1.Text = "";
                    listBox1.TopIndex = listBox1.Items.Count - 1;
                }
                else
                {
                    MessageBox.Show("문자는 공백이면 안됩니다.");
                }
            }
            else
            {
                MessageBox.Show("채팅에 다음 문자는 포함되면 안됩니다: ⧫, ◊");
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && isconnected)
            {
                if (!textBox1.Text.Contains('⧫') && !textBox1.Text.Contains('◊'))
                {
                    if (textBox1.Text != "")
                    {
                        stream.Write(Encoding.UTF8.GetBytes("0⧫" + $"{nickname}:" + textBox1.Text + '◊'));
                        stream.Flush();
                        listBox1.Items.Add($"{nickname}:" + textBox1.Text);
                        textBox1.Text = "";
                        listBox1.TopIndex = listBox1.Items.Count - 1;
                    }
                    else
                    {
                        MessageBox.Show("문자는 공백이면 안됩니다.");
                    }
                }
                else
                {
                    MessageBox.Show("다음 문자는 포함되어서는 안됩니다: ⧫, ◊");
                }
            }
        }
    }
}
