using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public partial class Server : Form
    {
        static TcpListener server;
        static List<Client> clients;
        Thread T;
        List<Thread> Tt;
        static bool isServerRun;
        static bool isClosing;
        public Server()
        {
            InitializeComponent();
            clients = new List<Client>();
            isServerRun = false;
            T = new Thread(() => ServerLoop(1111));
            Tt = new List<Thread>();
            StopButton.Enabled = false;
            isClosing = false;
            IPLabel.Text = "로컬 IP주소:\n" + GetLocalIPAddress() + "\n외부 IP주소:\n" + GetExternalIPAddress();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(PortTextBox.Text, out int port) && 0 < port && port < 100000)
            {
                T = new Thread(() => ServerLoop(port));
                T.IsBackground = true;
                T.Start();
                StartButton.Enabled = false;
                StopButton.Enabled = true;
                isServerRun = true;
                MessageListBox.Items.Add("Server started");
            }
            else
            {
                MessageBox.Show("포트는 1에서 99999 사이의 정수를 입력해 주세요");
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

        public void Delay(int ms)
        {
            DateTime dateTimeNow = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, ms);
            DateTime dateTimeAdd = dateTimeNow.Add(duration);
            while (dateTimeAdd >= dateTimeNow)
            {
                System.Windows.Forms.Application.DoEvents();
                dateTimeNow = DateTime.Now;
            }
            return;
        }

        // Thread function
        void ServerLoop(int port)
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            isServerRun = true;

            int count = 0;

            while (true)
            {
                try
                {
                    clients.Add(new Client(server.AcceptTcpClient(), count));
                    Invoke(new Action(() => ConnectedListBox.Items.Add(clients[clients.Count - 1].nickname)));
                    count++;

                    Tt.Add(new Thread(() => ClientCheck(clients.Count - 1, count)));
                    Delay(100);
                    clients[clients.Count - 1].client.GetStream().Write(Encoding.UTF8.GetBytes($"2⧫{count}◊"));
                    Tt[Tt.Count - 1].IsBackground = true;
                    Tt[Tt.Count - 1].Start();
                }
                catch (Exception ex)
                {
                    break;
                }
            }
        }

        void ClientCheck(int clientrealnumber, int clientn)
        {
            Client client = clients[clientrealnumber];
            NetworkStream stream = clients[clientrealnumber].client.GetStream();
            byte[] buffer = new byte[102400];
            buffer[102399] = 255;
            string msg = "";
            while (isServerRun)
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
                        Invoke(new Action(() => MessageListBox.Items.Add($"{client.nickname} disconnected")));
                        Invoke(new Action(() => ConnectedListBox.Items.Remove(client.nickname)));
                        foreach (var c in clients)
                        {
                            NetworkStream cStream = c.client.GetStream();
                            byte[] responseBytes = buffer;
                            if (c != client)
                            {
                                cStream.Write(Encoding.UTF8.GetBytes($"0⧫{client.nickname} disconnected◊"));
                                cStream.Flush();
                                Delay(100);
                                cStream.Write(Encoding.UTF8.GetBytes($"5⧫{client.nickname}◊"));
                                cStream.Flush();
                            }

                        }
                        break;
                    }

                    else if (message[0] == "3") // 닉네임 전송
                    {
                        foreach (var c in clients)
                        {
                            if (c.nickname == message[1])
                            {
                                string nickname = "";
                                foreach (var c2 in clients)
                                {
                                    if (c2 != client)
                                    {
                                        nickname += c2.nickname + ", ";
                                    }
                                }
                                client.client.GetStream().Write(Encoding.UTF8.GetBytes("1⧫닉네임은 다음과 같을 수 없습니다:" + nickname + '◊'));
                                clients.Remove(client);
                                Invoke(new Action(() => ConnectedListBox.Items.Remove(client.nickname)));
                                int b = 0;
                                int a = 10 / b;
                            }
                        }
                        clients.Remove(client);
                        Invoke(new Action(() => ConnectedListBox.Items.Remove(client.nickname)));
                        client.nickname = message[1];
                        foreach (var c in clients)
                        {
                            client.client.GetStream().Write(Encoding.UTF8.GetBytes("4⧫" + c.nickname + '◊'));
                            client.client.GetStream().Flush();
                            Delay(100);
                        }
                        clients.Add(client);
                        foreach (var c in clients)
                        {
                            c.client.GetStream().Write(Encoding.UTF8.GetBytes("4⧫" + client.nickname + '◊'));
                        }

                        Invoke(new Action(() => ConnectedListBox.Items.Add(client.nickname)));

                        Invoke(new Action(() => MessageListBox.Items.Add($"{message[1]} joined")));

                        buffer = Encoding.UTF8.GetBytes($"0⧫{client.nickname} joined◊");

                        foreach (var c in clients)
                        {
                            NetworkStream s = c.client.GetStream();
                            s.Write(buffer, 0, buffer.Length);
                        }
                    }

                    Invoke(new Action(() => MessageListBox.TopIndex = MessageListBox.Items.Count - 1));
                }
                catch (Exception e)
                {
                    break;
                }
            }

            client.client.Close();
            if (!isClosing)
            {
                Invoke(new Action(() => MessageListBox.Items.Remove(client.nickname)));
                clients.Remove(client);
            }
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;
            foreach (var c in clients)
            {
                NetworkStream n = c.client.GetStream();
                n.Write(Encoding.UTF8.GetBytes("1⧫◊"));
                c.client.Close();
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            foreach (var c in clients)
            {
                c.client.GetStream().Write(Encoding.UTF8.GetBytes("1⧫◊"));
                c.client.Close();
            }
            StopButton.Enabled = false;
            StartButton.Enabled = true;
            isServerRun = false;
            MessageListBox.Items.Add("Server stopped");
            server.Stop();
            ConnectedListBox.Items.Clear();
        }

        static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("로컬 IP 주소를 찾을 수 없습니다.");
        }

        static string GetExternalIPAddress()
        {
            using (WebClient client = new WebClient())
            {
                string response = client.DownloadString("https://api.ipify.org");
                return response;
            }
        }

        private void PortTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                StartButton.PerformClick();
            }
        }
    }

    class Client
    {
        public TcpClient client;
        public string nickname;

        public Client(TcpClient client, int n)
        {
            this.client = client;
            nickname = "Client" + n.ToString();
        }
        public Client(TcpClient client, string str)
        {
            this.client = client;
            nickname = str;
        }
    }
}