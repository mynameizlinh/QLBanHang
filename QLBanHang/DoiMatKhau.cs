using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBanHang
{
    public partial class DoiMatKhau : Form
    {
        public DoiMatKhau()
        {
            InitializeComponent();
        }

        public bool CheckAccount(string ac)//check ttk & mk
        {
            return Regex.IsMatch(ac, "^[a-zA-Z0-9]{6,20}$");
        }

        string Email = QuenMatKhau.to;

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (txtResetPass.Text == txtResetPassVer.Text)
            {

                SqlConnection con = new SqlConnection("Data Source=(local)\\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("UPDATE [TaiKhoan] SET [MatKhau] = '" + txtResetPassVer.Text + "' WHERE Email = '" + Email + "' ", con);
                if (!CheckAccount(txtResetPassVer.Text)) { MessageBox.Show("Vui lòng nhập mật khẩu từ 6-20 ký tự với các ký tự chữ và số, chữ hoa và chữ thường!"); return; }
                if (!CheckAccount(txtResetPass.Text)) { MessageBox.Show("Vui lòng nhập mật khẩu từ 6-20 ký tự với các ký tự chữ và số, chữ hoa và chữ thường!"); return; }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Đổi mật khẩu thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Mật khẩu không khớp! Vui lòng nhập đúng mật khẩu!");
            }
        }
    }
}
