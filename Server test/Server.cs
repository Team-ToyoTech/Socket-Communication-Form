using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server_test
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
            stopServerButton.Enabled = false;
            sendButton.Enabled = false;
            isClosing = false;
            ipLabel.Text = "로컬 IP주소:\n" + GetLocalIPAddress() + "\n외부 IP주소:\n" + GetExternalIPAddress();
        }

        private void startServerButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(portTextBox.Text, out int port) && 0 < port && port < 100000)
            {
                T = new Thread(() => ServerLoop(port));
                T.IsBackground = true;
                T.Start();
                startServerButton.Enabled = false;
                stopServerButton.Enabled = true;
                sendButton.Enabled = true;
                isServerRun = true;
                logListBox.Items.Add("Server started");
            }
            else
            {
                MessageBox.Show("포트는 1에서 99999 사이의 정수를 입력해 주세요");
            }
        }

        /*
        입력 코드
        0: 채팅
        1: 연결종료
        2: 번호 지정(서버 → 클라이언트)
        3: 닉네임 전송(클라이언트 → 서버)
        4: 접속한 클라이언트 이름
        5: 접속 종료한 클라이언트 이름
         */
        // Split 문자: ⧫
        // 송신 Check 문자: ◊

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

        // Thread func
        void ServerLoop(int port)
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            isServerRun = true;

            int count = 0;
            byte[] buffer;

            while (true)
            {
                try
                {
                    clients.Add(new Client(server.AcceptTcpClient(), count));
                    Invoke(new Action(() => connectedUsersListBox.Items.Add(clients[clients.Count - 1].nickname)));
                    count++;

                    Tt.Add(new Thread(() => ClientCheck(clients.Count - 1, count)));
                    Delay(10);
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
            bool error = false;
            while (isServerRun)
            {
                try
                {
                    string msg = "";

                    while (true)
                    {
                        buffer = new byte[102400];
                        byte[] data = new byte[256];
                        int bytesRead = stream.Read(data, 0, data.Length);
                        data = data.Where(x => x != 0).ToArray();
                        if (buffer.Length == 102400)
                        {
                            buffer = data;
                        }
                        else
                        {
                            buffer = buffer.Concat(data).ToArray();
                        }
                        if (bytesRead == 0)
                        {
                            break;
                        }
                        msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                        if (msg.Contains('◊'))
                        {
                            break;
                        }
                    }
                    msg = Encoding.UTF8.GetString(buffer, 0, buffer.Length).Replace("◊", "");
                    string[] message = msg.Split('⧫');
                    if (message[0] == "0")
                    {
                        Invoke(new Action(() => logListBox.Items.Add(message[1])));

                        foreach (var c in clients)
                        {
                            if (c != client)
                            {
                                NetworkStream cStream = c.client.GetStream();
                                byte[] responseBytes = Encoding.UTF8.GetBytes("0⧫" + message[1] + '◊');
                                cStream.Write(responseBytes, 0, responseBytes.Length);
                            }
                        }
                    }
                    else if (message[0] == "1")
                    {
                        Invoke(new Action(() => logListBox.Items.Add($"{client.nickname} disconnected...")));
                        Invoke(new Action(() => connectedUsersListBox.Items.Remove(client.nickname)));

                        foreach (var c in clients)
                        {
                            NetworkStream cStream = c.client.GetStream();
                            byte[] responseBytes = buffer;
                            if (c != client)
                            {
                                cStream.Write(Encoding.UTF8.GetBytes($"0⧫{client.nickname} disconnected...◊"));
                                cStream.Flush();
                                Delay(10);
                                cStream.Write(Encoding.UTF8.GetBytes($"5⧫{client.nickname}◊"));
                                cStream.Flush();
                            }

                        }
                        break;
                    }
                    else if (message[0] == "3")
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
                                int b = 0;
                                error = true;
                                int a = 10 / b;
                            }
                        }
                        clients.Remove(client);
                        Invoke(new Action(() => connectedUsersListBox.Items.Remove(client.nickname)));
                        client.nickname = message[1];
                        foreach (var c in clients)
                        {
                            client.client.GetStream().Write(Encoding.UTF8.GetBytes("4⧫" + c.nickname + '◊'));
                            Delay(10);
                        }
                        clients.Add(client);
                        foreach (var c in clients)
                        {
                            c.client.GetStream().Write(Encoding.UTF8.GetBytes("4⧫" + client.nickname + '◊'));
                        }
                        Invoke(new Action(() => connectedUsersListBox.Items.Add(client.nickname)));
                        Invoke(new Action(() => logListBox.Items.Add($"{message[1]} joined")));
                        buffer = Encoding.UTF8.GetBytes($"0⧫{client.nickname} joined◊");
                        foreach (var c in clients)
                        {
                            NetworkStream s = c.client.GetStream();
                            s.Write(buffer, 0, buffer.Length);
                        }
                    }
                    Invoke(new Action(() => logListBox.TopIndex = logListBox.Items.Count - 1));
                }
                catch (Exception e)
                {
                    break;
                }
            }

            //if (!isClosing && !error)
            //{
            //    foreach (var c in clients)
            //    {
            //        if (c != clients[clientrealnumber])
            //        {
            //            NetworkStream cStream = c.client.GetStream();
            //            byte[] responseBytes = buffer;
            //            cStream.Write(Encoding.UTF8.GetBytes($"{clients[clientrealnumber].nickname} disconnected..."));
            //        }
            //    }
            //    Invoke(new Action(() => listBox1.Items.Add($"{clients[clientrealnumber].nickname} disconnected...")));
            //}

            client.client.Close();
            if (!isClosing)
            {
                Invoke(new Action(() => logListBox.Items.Remove(client.nickname)));
                clients.Remove(client);
            }
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;
            foreach (var c in clients)
            {
                NetworkStream n = c.client.GetStream();
                n.Write(Encoding.UTF8.GetBytes("1⧫◊"));
                c.client.Close();
            }
        }

        private void stopServerButton_Click(object sender, EventArgs e)
        {
            foreach (var c in clients)
            {
                c.client.GetStream().Write(Encoding.UTF8.GetBytes("1⧫◊"));
                c.client.Close();
            }
            stopServerButton.Enabled = false;
            startServerButton.Enabled = true;
            sendButton.Enabled = false;
            isServerRun = false;
            logListBox.Items.Add("Server stopped");
            server.Stop();
            connectedUsersListBox.Items.Clear();
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

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (messageTextBox.Text != "")
            {
                foreach (var c in clients)
                {
                    c.client.GetStream().Write(Encoding.UTF8.GetBytes("0⧫" + "Server:" + messageTextBox.Text + '◊'));
                }
                logListBox.Items.Add("Server:" + messageTextBox.Text);
                messageTextBox.Text = "";
            }
        }

        private void Server_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && isServerRun && messageTextBox.Text != "")
            {
                foreach (var c in clients)
                {
                    c.client.GetStream().Write(Encoding.UTF8.GetBytes("0⧫" + "Server:" + messageTextBox.Text + '◊'));
                }
                logListBox.Items.Add("Server:" + messageTextBox.Text);
                messageTextBox.Text = "";
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