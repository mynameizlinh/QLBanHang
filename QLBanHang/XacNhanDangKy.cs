using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace QLBanHang
{
    public partial class XacNhanDangKy : Form
    {
        public XacNhanDangKy()
        {
            InitializeComponent();
        }

        Modify modify = new Modify();
        string randomCode;
        public static string to;
        public bool CheckEmail(string em)//check email
        {
            return Regex.IsMatch(em, @"^[a-zA-Z0-9_.]{3,20}@gmail.com(.vn|)$");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            if (!CheckEmail(email)) { MessageBox.Show("Email không đúng! Vui lòng nhập lại!"); return; }
            if (modify.TaiKhoans("Select * from TaiKhoan where Email = '" + email + "'").Count != 0) { MessageBox.Show("Email này đã được dùng để đăng ký tài khoản, vui lòng dùng email khác!"); return; }
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
            message.Subject = "Xác nhận tạo tài khoản";
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
            if (randomCode == (txtMaXacNhan.Text).ToString())
            {
                DangKy dangKy = new DangKy();
                this.Hide();
                dangKy.Show();
            }
            else
            {
                MessageBox.Show("Mã xác nhận sai! Vui lòng nhập lại mã xác nhận!");
            }
        }
    }
}
