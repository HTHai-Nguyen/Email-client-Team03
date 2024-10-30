using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class ServerChat : Form
    {
        private TcpListener server;
        private List<TcpClient> clients = new List<TcpClient>();
        private bool isRunning = false;
        public ServerChat()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartServer();
        }

        private void StartServer()
        {
            server = new TcpListener(IPAddress.Any, 2345);
            server.Start();
            isRunning = true;
            Thread acceptThread = new Thread(AcceptClients);
            acceptThread.Start();
            MessageBox.Show("Server started...");
        }

        private void AcceptClients()
        {
            while (isRunning)
            {
                TcpClient client = server.AcceptTcpClient();
                clients.Add(client);
                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(client);
            }
        }

        private void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            try
            {
                while (isRunning)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Invoke(new Action(() => lstChat.Items.Add(message))); // Cập nhật ListBox từ luồng khác
                    BroadcastMessage(message);
                }
            }
            catch (Exception ex)
            {
                Invoke(new Action(() => lstChat.Items.Add("Error: " + ex.Message)));
            }
            finally
            {
                clients.Remove(client);
                client.Close();
            }
        }

        private void BroadcastMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            foreach (var client in clients)
            {
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMessage.Text)) return;

            string message = "Server: " + txtMessage.Text;
            lstChat.Items.Add(message);
            BroadcastMessage(message);
            txtMessage.Clear();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isRunning = false;
            server.Stop();
            base.OnFormClosing(e);
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
