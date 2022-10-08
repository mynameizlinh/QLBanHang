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
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
        }
        public bool CheckAccount(string ac)//check ttk & mk
        {
            return Regex.IsMatch(ac, "^[a-zA-Z0-9]{6,20}$");
        }
        public bool CheckEmail(string em)//check email
        {
            return Regex.IsMatch(em, @"^[a-zA-Z0-9_.]{3,20}@gmail.com(.vn|)$");
        }
        string email = XacNhanDangKy.to;
        Modify modify = new Modify();

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string tentk = txtAccountName.Text;
            string matkhau = txtPassword.Text;
            string xacnhanmk = txtCPassword.Text;
            if (!CheckAccount(tentk)) { MessageBox.Show("Vui lòng nhập tên tài khoản từ 6-20 ký tự với các ký tự chữ và số, chữ hoa và chữ thường!"); return; }
            if (!CheckAccount(matkhau)) { MessageBox.Show("Vui lòng nhập mật khẩu từ 6-20 ký tự với các ký tự chữ và số, chữ hoa và chữ thường!!"); return; }
            if (xacnhanmk != matkhau) { MessageBox.Show("Mật khẩu không khớp, vui lòng nhập lại!"); return; }
            if (modify.TaiKhoans("Select * from TaiKhoan where TenTaiKhoan = '" + tentk + "'").Count != 0) { MessageBox.Show("Tên tài khoản đã được dùng để đăng ký, vui lòng đặt tên khác!"); return; }

            try
            {
                string query = "Insert into TaiKhoan values ('" + tentk + "', '" + matkhau + "', '" + email + "')";
                modify.Command(query);
                if (MessageBox.Show("Đăng ký thành công! Bạn có muốn đăng nhập?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Tên tài khoản này đã được đăng ký, vui lòng đặt tên tài khoản khác!");
            }
        }
    }
}
