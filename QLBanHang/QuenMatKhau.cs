using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBanHang
{
    public partial class QuenMatKhau : Form
    {
        public QuenMatKhau()
        {
            InitializeComponent();
        }

        public bool CheckAccount(string ac)//check ttk & mk
        {
            return Regex.IsMatch(ac, "^[a-zA-Z0-9]{6,20}$");
        }

        string randomCode;
        public static string to;
        Modify modify = new Modify();
        private void btnSend_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            if (modify.TaiKhoans("Select * from TaiKhoan where Email = '" + email + "'").Count == 0) { MessageBox.Show("Email chưa được đùng để đăng ký! Vui lòng đăng ký tài khoản!"); return; }

            string from, pass, messageBody;
            Random rand = new Random();
            randomCode = (rand.Next(999999)).ToString();
            MailMessage message = new MailMessage();
            to = (txtEmail.Text).ToString();
            from = "lelinhngoc2002@gmail.com";
            pass = "mxmxtkuvxilxzxdi";
            messageBody = "Mã xác nhận của bạn là " + randomCode;
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = messageBody;
            message.Subject = "Xác nhận đổi mật khẩu";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);
            try
            {
                smtp.Send(message);
                MessageBox.Show("Code đã được gửi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (randomCode == (txtVerifyCode.Text).ToString())
            {
                DoiMatKhau doimk = new DoiMatKhau();
                this.Hide();
                doimk.Show();
            }
            else
            {
                MessageBox.Show("Mã xác nhận sai! Vui lòng nhập lại!");
            }
        }
    }
}
