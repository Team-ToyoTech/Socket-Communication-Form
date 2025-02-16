using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Client_test
{
    public partial class Client : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;
        int mynum;
        bool isConnected;
        string nickname;
        static string str;

        public Client()
        {
            InitializeComponent();
            disconnectButton.Enabled = false;
            sendButton.Enabled = false;
            isConnected = false;
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(portTextBox.Text, out int port))
                {
                    client = new TcpClient(ipTextBox.Text, port);
                    stream = client.GetStream();
                    receiveThread = new Thread(ReceiveMessages);
                    receiveThread.IsBackground = true;
                    receiveThread.Start();
                    messageListBox.Items.Add("Connected to server...");
                    isConnected = true;
                    disconnectButton.Enabled = true;
                    sendButton.Enabled = true;
                    connectButton.Enabled = false;
                    nicknameTextBox.Enabled = false;
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
                    string msg = "";
                    while (true)
                    {
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);

                        if (bytesRead == 0)
                        {
                            break;
                        }
                        msg += Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        if (msg.Contains('◊'))
                        {
                            break;
                        }
                    }
                    msg = msg.Replace("◊", "");
                    string[] message = msg.Split('⧫');

                    if (message[0] == "0") // message
                    {
                        Invoke(new Action(() => messageListBox.Items.Add(message[1])));
                    }
                    else if (message[0] == "1") // disconnect
                    {
                        client.Close();
                        if (message[1] != "")
                        {
                            MessageBox.Show(message[1]);
                        }
                        Invoke(new Action(() =>
                        {
                            messageListBox.Items.Add("DisConnected from server...");
                            disconnectButton.Enabled = false;
                            sendButton.Enabled = false;
                            connectButton.Enabled = true;
                            isConnected = false;
                            nicknameTextBox.Enabled = true;
                            connectedUsersListBox.Items.Clear();
                        }));
                        break;
                    }
                    else if (message[0] == "2") // get my number
                    {
                        mynum = int.Parse(message[1]);
                        Invoke(new Action(() => str = nicknameTextBox.Text));
                        if (str == "")
                        {
                            nickname = "Client" + mynum.ToString();
                            Invoke(new Action(() => nicknameTextBox.Text = nickname));
                        }
                        else if (!str.Contains('⧫'))
                        {
                            nickname = str;
                        }
                        else
                        {
                            MessageBox.Show("이름에 다음 문자가 포함되어서는 안됩니다: ⧫\n기본 이름으로 진행합니다.");
                            nickname = "Client" + mynum.ToString();
                            Invoke(new Action(() => nicknameTextBox.Text = nickname));
                        }
                        stream.Write(Encoding.UTF8.GetBytes("3⧫" + nickname + '◊'));
                        stream.Flush();
                    }
                    else if (message[0] == "4") // add client
                    {
                        Invoke(new Action(() => connectedUsersListBox.Items.Add(message[1])));
                    }
                    else if (message[0] == "5") // remove client
                    {
                        Invoke(new Action(() => connectedUsersListBox.Items.Remove(message[1])));
                    }
                    Invoke(new Action(() => messageListBox.TopIndex = messageListBox.Items.Count - 1));
                }
                catch (Exception ex)
                {
                    break;
                }
            }
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            stream.Write(Encoding.UTF8.GetBytes("1⧫◊"));
            stream.Flush();
            stream.Close();
            client.Close();
            messageListBox.Items.Add("DisConnected from server...");
            disconnectButton.Enabled = false;
            sendButton.Enabled = false;
            connectButton.Enabled = true;
            isConnected = false;
            nicknameTextBox.Enabled = true;
            connectedUsersListBox.Items.Clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isConnected)
            {
                stream.Write(Encoding.UTF8.GetBytes("1⧫◊"));
                stream.Flush();
                stream.Close();
                client.Close();
                messageListBox.Items.Add("DisConnected from server...");
                disconnectButton.Enabled = false;
                sendButton.Enabled = false;
                connectButton.Enabled = true;
                isConnected = false;
                nicknameTextBox.Enabled = true;
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (messageTextBox.Text != "")
            {
                if (!messageTextBox.Text.Contains('⧫'))
                {
                    stream.Write(Encoding.UTF8.GetBytes("0⧫" + $"{nickname}:" + messageTextBox.Text + '◊'));
                    messageListBox.Items.Add($"{nickname}:" + messageTextBox.Text);
                    messageTextBox.Text = "";
                }
                else
                {
                    MessageBox.Show("채팅에 다음 문자는 포함되면 안됩니다: ⧫");
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && isConnected)
            {
                if (messageTextBox.Text != "")
                {
                    if (!messageTextBox.Text.Contains('⧫'))
                    {
                        stream.Write(Encoding.UTF8.GetBytes("0⧫" + $"{nickname}:" + messageTextBox.Text + '◊'));
                        stream.Flush();
                        messageListBox.Items.Add($"{nickname}:" + messageTextBox.Text);
                        messageTextBox.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("채팅에 다음 문자는 포함되면 안됩니다: ⧫");
                    }
                }
            }
        }
    }
}