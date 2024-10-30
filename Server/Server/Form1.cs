//Truy vấn database
//SELECT TOP 100 * FROM Register;

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
using System.Data.SqlClient;
using System.Security.Cryptography;
namespace Server
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection;
        private TcpListener tcpListener;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Admin\Desktop\File chính Email-Client-TCP-Server-Client\Email-Client-TCP-Server-Client\Server\Server\ServerDatabase.mdf"";Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
                MessageBox.Show("Kết nối đến cơ sở dữ liệu thành công.");
                StartListening();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối: {ex.Message}");
            }
        }

        private void StartListening()
        {
            tcpListener = new TcpListener(IPAddress.Any, 5000);
            tcpListener.Start();
            Thread listenThread = new Thread(ListenForClients);
            listenThread.Start();
        }

        private void ListenForClients()
        {
            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.Start();
            }
        }

        private void HandleClient(TcpClient client)
        {
            using (client)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string clientInfo = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                string[] details = clientInfo.Split(':');

                if (details.Length == 3) // Đối với đăng ký
                {
                    string username = details[0];
                    string email = details[1];
                    string password = details[2];

                    // Kiểm tra username đã tồn tại
                    string checkUsername = "SELECT COUNT(*) FROM Register WHERE username = @username";
                    using (SqlCommand cmdCheck = new SqlCommand(checkUsername, sqlConnection))
                    {
                        cmdCheck.Parameters.AddWithValue("@username", username);
                        int userCount = (int)cmdCheck.ExecuteScalar();
                        if (userCount > 0)
                        {
                            byte[] responseData = Encoding.UTF8.GetBytes("Username already exists");
                            stream.Write(responseData, 0, responseData.Length);
                            return;
                        }
                    }

                    // Lưu thông tin người dùng
                    string registerQuery = "INSERT INTO Register (username, email, password) VALUES (@username, @email, @password)";
                    using (SqlCommand cmd = new SqlCommand(registerQuery, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.ExecuteNonQuery();
                    }

                    // Cập nhật giao diện hiển thị
                    Invoke(new Action(() =>
                    {
                        lstChat.Items.Add($"User {username} has signed up.");
                    }));

                    // Gửi phản hồi cho client
                    byte[] response = Encoding.UTF8.GetBytes("Registration Successful");
                    stream.Write(response, 0, response.Length);
                }
                else if (details.Length == 2) // Đối với đăng nhập
                {
                    string username = details[0];
                    string password = PasswordHandler.HashPassword(details[1]);

                    // Kiểm tra thông tin đăng nhập với cơ sở dữ liệu
                    string loginQuery = "SELECT COUNT(*) FROM Register WHERE username = @username AND password = @password";
                    using (SqlCommand cmd = new SqlCommand(loginQuery, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        int userCount = (int)cmd.ExecuteScalar();

                        // Gửi phản hồi về client
                        string responseMessage = userCount > 0 ? "Success" : "Invalid Username or Password";
                        byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);
                        stream.Write(responseData, 0, responseData.Length);

                        // Cập nhật giao diện hiển thị
                        Invoke(new Action(() =>
                        {
                            if (userCount > 0)
                            {
                                lstChat.Items.Add($"User {username} has logged in.");
                            }
                            else
                            {
                                lstChat.Items.Add($"Failed login attempt for user {username}.");
                            }
                        }));
                    }
                }
            }
        }

        public static class PasswordHandler
        {
            public static string HashPassword(string password)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    StringBuilder builder = new StringBuilder();
                    foreach (byte b in bytes)
                    {
                        builder.Append(b.ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }

            if (tcpListener != null)
            {
                tcpListener.Stop();
            }
        }


        private void btnSend_Click(object sender, EventArgs e)
        {
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {

        }

        private void lstChat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
