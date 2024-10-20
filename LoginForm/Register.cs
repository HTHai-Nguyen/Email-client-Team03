using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Net.Sockets;

namespace LoginForm
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            txtFullName.Select();
        }
        SqlConnection conn = new SqlConnection(@"Server=DESKTOP-0ARU1C6;Database=BTLT2;Trusted_Connection=True;");

        public class PasswordHandler
        {
            public static string HashPassword(string password)
            {
                using (SHA256 sha256Hash = SHA256.Create())
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

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) ||
            //    string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtConfirmPass.Text))
            //{
            //    MessageBox.Show("Please enter information completely.", "Register Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //if (!IsValidEmail(txtEmail.Text))
            //{
            //    MessageBox.Show("Email is not correct format.", "egister Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtEmail.Focus();
            //    return;
            //}

            //if (!IsValidPassword(txtPassword.Text))
            //{
            //    MessageBox.Show("Password must be at least 8 characters, including uppercase letters, lowercase letters, numbers and special characters.", "Register Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtPassword.Clear();
            //    txtConfirmPass.Clear();
            //    txtPassword.Focus();
            //    return;
            //}

            //if (txtPassword.Text != txtConfirmPass.Text)
            //{
            //    MessageBox.Show("Password does not match, Please try again.", "Register Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtPassword.Clear();
            //    txtConfirmPass.Clear();
            //    txtPassword.Focus();
            //    return;
            //}

            //try
            //{
            //    conn.Open();

            //    // Kiểm tra username đã tồn tại
            //    string checkUsername = "SELECT COUNT(*) FROM Register WHERE username = @username";
            //    using (SqlCommand cmdCheck = new SqlCommand(checkUsername, conn))
            //    {
            //        cmdCheck.Parameters.AddWithValue("@username", txtUsername.Text);
            //        int userCount = (int)cmdCheck.ExecuteScalar();
            //        if (userCount > 0)
            //        {
            //            MessageBox.Show("Username already exists, Please try again.", "Register Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            txtUsername.Focus();
            //            return;
            //        }
            //    }

            //    // Mã hóa mật khẩu
            //    string hashedPassword = PasswordHandler.HashPassword(txtPassword.Text);

            //    // Sử dụng hashedPassword khi chèn vào cơ sở dữ liệu
            //    string register = "INSERT INTO Register (username, email, password) VALUES (@username, @email, @password)";
            //    using (SqlCommand cmd = new SqlCommand(register, conn))
            //    {
            //        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
            //        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            //        cmd.Parameters.AddWithValue("@password", hashedPassword);
            //        cmd.ExecuteNonQuery();
            //    }

            //    MessageBox.Show("Your Account created successfully.", "Registration Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    ClearFields();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("An error has occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    conn.Close();
            //}

            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtConfirmPass.Text) ||
                string.IsNullOrWhiteSpace(txtFullName.Text) || dtpBirthday.Value == DateTimePicker.MinimumDateTime)
            {
                MessageBox.Show("Please enter information completely.", "Register Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email is not correct format.", "Register Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }

            if (!IsValidPassword(txtPassword.Text))
            {
                MessageBox.Show("Password must be at least 8 characters, including uppercase letters, lowercase letters, numbers and special characters.", "Register Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtConfirmPass.Clear();
                txtPassword.Focus();
                return;
            }

            if (txtPassword.Text != txtConfirmPass.Text)
            {
                MessageBox.Show("Password does not match, Please try again.", "Register Failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtConfirmPass.Clear();
                txtPassword.Focus();
                return;
            }

            // Mã hóa mật khẩu
            string hashedPassword = PasswordHandler.HashPassword(txtPassword.Text);

            // Gửi thông tin đăng ký đến server
            try
            {
                using (TcpClient client = new TcpClient("127.0.0.1", 5000)) // Địa chỉ và cổng của TCPServer
                {
                    NetworkStream stream = client.GetStream();
                    string registerInfo = $"{txtUsername.Text}:{txtEmail.Text}:{hashedPassword}"; // Gửi thông tin đăng ký
                    byte[] data = Encoding.UTF8.GetBytes(registerInfo);
                    stream.Write(data, 0, data.Length);

                    // Nhận phản hồi từ server
                    byte[] responseData = new byte[256];
                    int bytes = stream.Read(responseData, 0, responseData.Length);
                    string responseMessage = Encoding.UTF8.GetString(responseData, 0, bytes);
                    MessageBox.Show(responseMessage, "Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (responseMessage == "Registration Successful")
                    {
                        ClearFields();
                    }
                }

                // Mã hóa mật khẩu
                string hashedPassword = PasswordHandler.HashPassword(txtPassword.Text);

                // Sử dụng hashedPassword khi chèn vào cơ sở dữ liệu
                string register = "INSERT INTO Register (username, email, password, fullname, birthday) VALUES (@username, @email, @password, @fullname, @birthday)";
                using (SqlCommand cmd = new SqlCommand(register, conn))
                {
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@password", hashedPassword);
                    cmd.Parameters.AddWithValue("@fullname", txtFullName.Text); // Thêm fullname
                    cmd.Parameters.AddWithValue("@birthday", dtpBirthday.Value); // Thêm birthday
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Your Account created successfully.", "Registration Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPassword(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            return password.Length >= 8 &&
                   hasNumber.IsMatch(password) &&
                   hasUpperChar.IsMatch(password) &&
                   hasLowerChar.IsMatch(password) &&
                   hasSymbols.IsMatch(password);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
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

        private void ClearFields()
        {
            txtUsername.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            txtConfirmPass.Clear();
            txtUsername.Focus();
        }

        private void linklblLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new LoginForm().Show();
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
