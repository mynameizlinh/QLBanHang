using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBanHang
{
    public partial class XacNhanQL : Form
    {
        public XacNhanQL()
        {
            InitializeComponent();
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if(txtMKXN.Text != "quanly123")
            {
                MessageBox.Show("Mật khẩu xác nhận sai!", "Thông báo");
                return;
            }
            else
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo");
                QuanLy quanLy = new QuanLy();
                this.Hide();
                quanLy.Show();
            }
        }
    }
}
