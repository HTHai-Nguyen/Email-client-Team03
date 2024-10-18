using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm
{
    public partial class ChatForm : Form
    {
        private int userId; // ID người dùng
        private TcpClient client;
        private NetworkStream stream;

        public ChatForm(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            StartClient();
        }

        public ChatForm()
        {
            InitializeComponent();

        }

        private void StartClient()
        {
            client = new TcpClient("127.0.0.1", 5000);
            stream = client.GetStream();
            // Mở một thread hoặc Task để nhận dữ liệu từ server
            Task.Run(() => ReceiveMessages());
        }
        private void ChatForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text;
            if (!string.IsNullOrEmpty(message))
            {
                string formattedMessage = $"{userId}: {message}";
                byte[] data = Encoding.UTF8.GetBytes(formattedMessage);
                stream.Write(data, 0, data.Length);
                txtMessage.Clear();
            }
        }

        private void ReceiveMessages()
        {
            while (true)
            {
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Invoke((MethodInvoker)delegate
                    {
                        txtChat.AppendText(receivedMessage + Environment.NewLine);
                    });
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            stream?.Close();
            client?.Close();
        }
    }
}
