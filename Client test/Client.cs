using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public partial class Client : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;
        int mynum;
        bool isconnected;
        string nickname;
        static string str;
        public Client()
        {
            InitializeComponent();
            DisconnectButton.Enabled = false;
            isconnected = false;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(PortTextBox.Text, out int port))
                {
                    client = new TcpClient(IPTextBox.Text, port);
                    stream = client.GetStream();
                    receiveThread = new Thread(ReceiveMessages);
                    receiveThread.IsBackground = true;
                    receiveThread.Start();
                    MessageListBox.Items.Add("Connected to server");
                    isconnected = true;
                    DisconnectButton.Enabled = true;
                    ConnectButton.Enabled = false;
                    NickNameTextBox.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /*
        입력 코드
        1: 연결종료
        2: 번호 지정(서버=>클라이언트)
        3: 닉네임 전송(클라이언트=>서버)
        4: 접속한 클라이언트 이름
        5: 접속 종료한 클라이언트 이름
         */
        // Split 문자 : ⧫
        // 송신 Check 문자 : ◊

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
                        {
                            break;
                        }
                        data = data.Where(x => x != 0).ToArray();
                        if (buffer.Length == 102400)
                        {
                            buffer = data;
                        }
                        else
                        {
                            buffer = buffer.Concat(data).ToArray();
                        }

                        msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                        if (msg.Contains('◊'))
                        {
                            break;
                        }
                    }
                    if (Encoding.UTF8.GetString(buffer, 0, buffer.Length).Split("◊").Length == 1)
                    {
                        msg = "";
                    }
                    else
                    {
                        msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length).Split("◊")[1];
                    }
                    string[] message = Encoding.UTF8.GetString(buffer, 0, buffer.Length).Split("◊")[0].Split('⧫');

                    if (message[0] == "1") // 연결 종료
                    {

                        client.Close();
                        if (message[1] != "")
                            MessageBox.Show(message[1]);

                        Invoke(new Action(() =>
                        {
                            MessageListBox.Items.Add("Disconnected from server");
                            DisconnectButton.Enabled = false;
                            ConnectButton.Enabled = true;
                            isconnected = false;
                            NickNameTextBox.Enabled = true;
                            ConnectedListBox.Items.Clear();
                        }));

                        break;
                    }

                    else if (message[0] == "2") // 번호 지정
                    {
                        mynum = int.Parse(message[1]);
                        Invoke(new Action(() => str = NickNameTextBox.Text));
                        if (str == "")
                        {
                            nickname = "Client" + mynum.ToString();
                            Invoke(new Action(() => NickNameTextBox.Text = nickname));
                        }
                        else if (!str.Contains('⧫'))
                        {
                            nickname = str;
                        }
                        else
                        {
                            MessageBox.Show("이름에 다음 문자가 포함되어서는 안됩니다: ⧫\n기본 이름으로 진행합니다.");
                            nickname = "Client" + mynum.ToString();
                            Invoke(new Action(() => NickNameTextBox.Text = nickname));
                        }
                        stream.Write(Encoding.UTF8.GetBytes("3⧫" + nickname + '◊'));
                        stream.Flush();
                    }

                    else if (message[0] == "4") // 닉네임 전송
                    {
                        Invoke(new Action(() => ConnectedListBox.Items.Add(message[1])));
                    }

                    else if (message[0] == "5") // 접속 종료
                    {
                        Invoke(new Action(() => ConnectedListBox.Items.Remove(message[1])));
                    }

                    Invoke(new Action(() => MessageListBox.TopIndex = MessageListBox.Items.Count - 1));
                }
                catch (Exception ex)
                {
                    break;
                }
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            stream.Write(Encoding.UTF8.GetBytes("1⧫◊"));
            stream.Flush();
            stream.Close();
            client.Close();
            MessageListBox.Items.Add("Disconnected from server");
            DisconnectButton.Enabled = false;
            ConnectButton.Enabled = true;
            isconnected = false;
            NickNameTextBox.Enabled = true;
            ConnectedListBox.Items.Clear();
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isconnected)
            {
                stream.Write(Encoding.UTF8.GetBytes("1⧫◊"));
                stream.Flush();
                stream.Close();
                client.Close();
                MessageListBox.Items.Add("Disconnected from server");
                DisconnectButton.Enabled = false;
                ConnectButton.Enabled = true;
                isconnected = false;
                NickNameTextBox.Enabled = true;
            }
        }

        private void NickNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectButton.PerformClick();
            }
        }
    }
}