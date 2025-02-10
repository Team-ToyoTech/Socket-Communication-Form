using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server_test
{
    public partial class Form1 : Form
    {
        static TcpListener server;
        static List<TcpClient> clients;
        Thread T;
        List<Thread> Tt;
        static bool isServerRun;
        static bool isClosing;
        public Form1()
        {
            InitializeComponent();
            isServerRun = false;
            T = new Thread(() => ServerLoop(1111));
            Tt = new List<Thread>();
            button2.Enabled = false;
            button3.Enabled = false;
            isClosing = false;
            label2.Text = "로컬 IP주소:\n" + GetLocalIPAddress() + "\n외부 IP주소:\n" + GetExternalIPAddress();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int port) && 0 < port && port < 100000)
            {

                T = new Thread(() => ServerLoop(port));
                T.IsBackground = true;
                T.Start();
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
                isServerRun = true;
                listBox1.Items.Add("Server started");
            }
            else
            {
                MessageBox.Show("포트는 1에서 99999 사이의 정수를 입력해 주세요");
            }
        }
        /*
        입력 코드
        0:채팅
        1:연결종료
        2:번호 지정
         */
        //Split 문자 : ⧫

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

        //Thread func
        void ServerLoop(int port)
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            isServerRun = true;
            clients = new List<TcpClient>();
            int count = 0;
            byte[] buffer;

            while (true)
            {
                try
                {
                    clients.Add(server.AcceptTcpClient());
                    count++;
                    Invoke(new Action(() => listBox1.Items.Add($"Client{count} joined")));
                    buffer = Encoding.UTF8.GetBytes($"0⧫Client{count} joined");
                    foreach (TcpClient client in clients)
                    {
                        NetworkStream stream = client.GetStream();
                        stream.Write(buffer, 0, buffer.Length);
                    }
                    Tt.Add(new Thread(() => ClientCheck(clients[clients.Count - 1], count)));
                    Delay(10);
                    clients[clients.Count - 1].GetStream().Write(Encoding.UTF8.GetBytes($"2⧫{count}"));
                    Tt[Tt.Count - 1].IsBackground = true;
                    Tt[Tt.Count - 1].Start();
                }
                catch (Exception ex)
                {
                    break;
                }
            }
        }

        void ClientCheck(TcpClient client, int clientn)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[102400];

            while (isServerRun)
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

                        foreach (var c in clients)
                        {
                            if (c != client)
                            {
                                NetworkStream cStream = c.GetStream();
                                byte[] responseBytes = buffer;
                                cStream.Write(responseBytes, 0, responseBytes.Length);
                            }
                        }
                    }
                    else if (message[0] == "1")
                    {
                        client.Close();
                        clients.Remove(client);
                        Invoke(new Action(() => listBox1.Items.Add($"Client{clientn} disconnected...")));
                        break;
                    }
                }
                catch (Exception e)
                {
                    clients.Remove(client);
                    if (!isClosing) Invoke(new Action(() => listBox1.Items.Add($"Client{clientn} disconnected...")));
                    break;
                }
            }
        }
        //TODO: UI작업이랑 비정상 종료에 대한 처리 하기
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;
            foreach (var c in clients)
            {
                NetworkStream n = c.GetStream();
                n.Write(Encoding.UTF8.GetBytes("1⧫"));
                c.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var c in clients)
            {
                c.GetStream().Write(Encoding.UTF8.GetBytes("1⧫"));
                c.Close();
            }
            button2.Enabled = false;
            button1.Enabled = true;
            button3.Enabled = false;
            isServerRun = false;
            listBox1.Items.Add("Server stopped");
            server.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var c in clients)
            {
                c.GetStream().Write(Encoding.UTF8.GetBytes("0⧫"+"Server:" + textBox2.Text));
            }
            listBox1.Items.Add("Server:" + textBox2.Text);
            textBox2.Text = "";
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
    }
}
