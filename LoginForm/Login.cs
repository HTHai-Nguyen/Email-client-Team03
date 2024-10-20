using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Net.Sockets;

namespace LoginForm
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            txtUsername.Select();
        }

        
        SqlConnection conn = new SqlConnection(@"Server=DESKTOP-0ARU1C6;Database=BTLT2;Trusted_Connection=True;");
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter data = new SqlDataAdapter();

        public class PasswordHandler
        {
            public static string HashPassword(string password)
            {
                using (SHA256 sha256Hash = SHA256.Create()) // Phương thức bảo mật SHA256
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }

            public static bool VerifyPassword(string inputPassword, string hashedPassword)
            {
                string hashedInputPassword = HashPassword(inputPassword);
                return hashedInputPassword == hashedPassword;
            }
        }

        private void linklblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Chuyển sang link đăng ký
        {
            new Register().Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    conn.Open();

            //    // Kiểm tra xem tên đăng nhập có tồn tại không
            //    string checkUsername = "SELECT COUNT(*) FROM Register WHERE username = @username";
            //    using (SqlCommand cmdCheckUsername = new SqlCommand(checkUsername, conn))
            //    {
            //        cmdCheckUsername.Parameters.AddWithValue("@username", txtUsername.Text);
            //        int userCount = (int)cmdCheckUsername.ExecuteScalar();

            //        if (userCount == 0)
            //        {
            //            MessageBox.Show("Invalid Username & Password, Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            txtUsername.Clear();
            //            txtPassword.Clear();
            //            txtUsername.Focus();
            //            return;
            //        }
            //    }

            //    // Mã hóa mật khẩu người dùng nhập vào
            //    string hashedPassword = PasswordHandler.HashPassword(txtPassword.Text);

            //    // Kiểm tra username và mật khẩu đã mã hóa
            //    string login = "SELECT * FROM Register WHERE username = @username AND password = @password";
            //    using (SqlCommand cmd = new SqlCommand(login, conn))
            //    {
            //        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
            //        cmd.Parameters.AddWithValue("@password", hashedPassword); // Sử dụng mật khẩu đã mã hóa

            //        using (SqlDataReader dr = cmd.ExecuteReader())
            //        {
            //            if (dr.Read())
            //            {
            //                string username = txtUsername.Text;
            //                Dashboard dashboard = new Dashboard(username);
            //                this.Hide();
            //                dashboard.Show();

            //            }
            //            else
            //            {
            //                MessageBox.Show("Password is incorrect, Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                txtPassword.Clear();
            //                txtPassword.Focus();
            //            }
            //        }

            //        // // Chỉnh sửa ngày 17/10/2024
            //        // // Sử Dụng ExecuteScalar: Sử dụng ExecuteScalar để nhận ID người dùng
            //        // object result = cmd.ExecuteScalar(); // Thay vì ExecuteReader
            //        // if (result != null)
            //        // {
            //        //     int userId = Convert.ToInt32(result); // Lấy ID người dùng
            //        //     Dashboard dashboard = new Dashboard(txtUsername.Text, userId); // Truyền username và ID
            //        //     this.Hide();
            //        //     dashboard.Show();
            //        // }
            //        // else
            //        // {
            //        // MessageBox.Show("Password is incorrect, Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        // txtPassword.Clear();
            //        // txtPassword.Focus();
            //        // }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("An error has occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    conn.Close();
            //}

            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Gửi thông tin đăng nhập đến TCPServer
            try
            {
                using (TcpClient client = new TcpClient("127.0.0.1", 5000)) // Địa chỉ và cổng của TCPServer
                {
                    NetworkStream stream = client.GetStream();

                    // Tạo chuỗi thông tin đăng nhập
                    string loginInfo = $"{username}:{password}";
                    byte[] data = Encoding.UTF8.GetBytes(loginInfo);

                    // Gửi dữ liệu đến server
                    stream.Write(data, 0, data.Length);

                    // Nhận phản hồi từ server
                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    string responseMessage = Encoding.UTF8.GetString(responseData, 0, bytes);

                    if (responseMessage == "Success")
                    {
                        Dashboard dashboard = new Dashboard(username); // Truyền username
                        this.Hide();
                        dashboard.Show();
                    }
                    else
                    {
                        MessageBox.Show(responseMessage, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsername.Clear();
                        txtPassword.Clear();
                        txtUsername.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

    
}
