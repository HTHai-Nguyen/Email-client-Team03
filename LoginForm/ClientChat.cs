using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm
{
    public partial class ClientChat : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private string username;
        private Thread receiveThread;
        public ClientChat()
        {
            InitializeComponent();
        }

        public ClientChat(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void chatText_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void chatView_TextChanged(object sender, EventArgs e)
        {

        }

        private void connect_btn_Click(object sender, EventArgs e)
        {
            if (client == null)
            {
                ConnectToServer();
            }

            if (!string.IsNullOrEmpty(chatText.Text))
            {
                SendMessage(username + ": " + chatText.Text);
                chatText.Clear();
            }
        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 2345);
                stream = client.GetStream();
                receiveThread = new Thread(ReceiveMessages);
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to server: " + ex.Message);
            }
        }

        private void ReceiveMessages()
        {
            try
            {
                byte[] buffer = new byte[1024];
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Invoke(new Action(() => chatView.AppendText(message + Environment.NewLine)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error receiving message: " + ex.Message);
            }
        }

        private void SendMessage(string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending message: " + ex.Message);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (client != null)
            {
                client.Close();
                receiveThread?.Join();
            }
        }
    }
}
