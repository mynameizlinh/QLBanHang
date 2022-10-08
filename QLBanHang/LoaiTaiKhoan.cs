using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBanHang
{
    public partial class LoaiTaiKhoan : Form
    {
        public LoaiTaiKhoan()
        {
            InitializeComponent();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            DangNhap dangNhap = new DangNhap();
            this.Hide();
            dangNhap.Show();
        }

        private void btnQuanly_Click(object sender, EventArgs e)
        {
            XacNhanQL XNQL = new XacNhanQL();
            this.Hide();
            XNQL.Show();
        }
    }
}
