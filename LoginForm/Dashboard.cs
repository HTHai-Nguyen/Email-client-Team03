using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm
{
    public partial class Dashboard : Form
    {
        private string username;
        private int userId; // Thêm ID người dùng
        string conn = @"Server=DESKTOP-0ARU1C6;Database=BTLT2;Trusted_Connection=True;";
        public Dashboard(string username)
        {
            InitializeComponent();
            this.username = username;
            lblName.Text = username;

        }

        // // Hàm khởi tạo với ID người dùng
        // public Dashboard(string username, int userId)
        // {
        //     InitializeComponent();
        //     this.username = username;
        //     lblName.Text = username;
        //     this.userId = userId; // Gán ID người dùng
        // }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linklblLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new LoginForm().Show();
            this.Hide();
        }

        private void mnuPersonalInfo_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();

                string query = $"SELECT * FROM USERS WHERE USERNAME = @username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // Tạo form hiển thị thông tin khách hàng
                    FormUserInfo formUserInfo = new FormUserInfo();

                    //formUserInfo.txtFullName.Text = reader["Tên_cột_của_FullName"].ToString();
                    formUserInfo.txtEmail.Text = reader["EMAIL"].ToString();
                    //formUserInfo.txtBirthday.Text = reader["Tên_cột_của_Birthday"].ToString();

                    // Hiển thị form
                    formUserInfo.ShowDialog();
                }
            }
        }
    }
}
