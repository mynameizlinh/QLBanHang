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
    public partial class QuanLy : Form
    {
        public QuanLy()
        {
            InitializeComponent();
        }

        private void mnuQLNV_Click(object sender, EventArgs e)
        {
            QLNV quanlyNV = new QLNV();
            this.Hide();
            quanlyNV.Show();
        }
    }
}
