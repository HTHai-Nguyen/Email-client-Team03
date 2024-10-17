using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

       
    }
}
