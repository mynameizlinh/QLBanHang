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

namespace QLBanHang
{
    public partial class QLNV : Form
    {
        SqlConnection con;
        public QLNV()
        {
            InitializeComponent();
            gbxThongTinChiTiet.Enabled = false;
        }

        public bool ketNoi(string server, string database)
        {
            try
            {
                String s = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True";
                con = new SqlConnection(s);
                //mở kết nối
                con.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
                //e.Message: hiển thị chi tiết lỗi cho người dùng
                MessageBox.Show(e.Message, "Thông báo");

            }
        }
        bool themXoaSua(string s)
        {
            try//them xoa sua thanh cong
            {
                SqlCommand cmd = new SqlCommand(s, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception e)//them xoa sua that bai
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
        DataTable truyvanDuLieu(string s)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            try//tru van thanh cong
            {
                da = new SqlDataAdapter(s, con);
                da.Fill(ds, "KQ");
                con.Close();//đóng kết nối
                return ds.Tables["KQ"];//trả về kết quả của truy vấn dữ liệu
            }
            catch (Exception e)//truy vấn thất bại
            {
                MessageBox.Show("Truy vấn thất bại", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }

        void reset()
        {
            txtDiaChi.Text = txtDienThoai.Text = txtHoTen.Text = txtEmail.Text = "";
            // cbxBangCap.SelectedIndex = 0;
            dtpNgaySinh.Value = DateTime.Now;
        }

        bool kiemTraTrong()
        {
            if (txtDiaChi.Text == "" || txtDienThoai.Text == "" || txtHoTen.Text == "" || txtEmail.Text == "")
            {
                return true;
            }
            return false;
        }

        void layDuLieu_VaoListview()
        {
            //xóa toàn bộ dữ liệu trong listview
            lsvThongTinChung.Items.Clear();
            string s = "select * from NhanVien";
            DataTable dt = new DataTable();
            dt = truyvanDuLieu(s);
            //đổ dữ liệu vào listview
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem lvi = lsvThongTinChung.Items.Add(dt.Rows[i]["MaNhanVien"].ToString());
                lvi.SubItems.Add(dt.Rows[i][1].ToString());
                lvi.SubItems.Add(dt.Rows[i][2].ToString());
                lvi.SubItems.Add(dt.Rows[i][3].ToString());
                lvi.SubItems.Add(dt.Rows[i][4].ToString());
                lvi.SubItems.Add(dt.Rows[i]["Email"].ToString());
            }
        }

        private void QLNV_Load(object sender, EventArgs e)
        {
            if (ketNoi("DESKTOP-T06NIC5\\SQLEXPRESS", "QLBanHang") == true)
            {
                layDuLieu_VaoListview();
            }
            else
            {
                MessageBox.Show("Không kết nối được cơ sở dữ liệu", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            reset();
            gbxThongTinChiTiet.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (kiemTraTrong() == true)
            {
                MessageBox.Show("Phải nhập đủ dữ liệu", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //reset();
                return;
            }
            else//them vao Database 
            {
                con.Open();
                string hoTen = txtHoTen.Text;
                string diaChi = txtDiaChi.Text;
                string dienThoai = txtDienThoai.Text;
                string email = txtEmail.Text;
                string ngaySinh = dtpNgaySinh.Value.ToShortDateString();
                string s = "insert into NhanVien values(N'" + hoTen + "',N'" + ngaySinh + "',N'" + diaChi + "',N'" + dienThoai + "',N'" + email + "')";
                if (themXoaSua(s))//thêm thành công
                {
                    layDuLieu_VaoListview();
                    reset();
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            reset();
            gbxThongTinChiTiet.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "DELETE FROM NhanVien where MaNhanVien = '" + txtXoa.Text + "'";

            SqlCommand cmd = new SqlCommand(query, con);
            var result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Lỗi xóa dữ liệu! Vui lòng thử lại!", "Thông báo");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }
    }
}
