using NhaThuoc.GiaoDien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaThuoc
{
    public partial class frmCSKH : Form
    {
        public string Sodienthoai;
        public string Manhanvien;
        public string Hovaten;
        public frmCSKH()
        {
            InitializeComponent();
        }
        public frmCSKH(string hoten, string sdt, string manv)
        {
            InitializeComponent();
            Sodienthoai = sdt;
            Manhanvien = manv;
            Hovaten = hoten;
        }

        private void frmCSKH_Load(object sender, EventArgs e)
        {
            frmInfoKH f = new frmInfoKH();
            LoadFormToPanel(f);
        }
        private void LoadFormToPanel(Form frm)
        {
            panel_phu.Controls.Clear();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            panel_phu.Controls.Add(frm);
            frm.Show();
        }

        private void btnInfoKH_Click(object sender, EventArgs e)
        {
            frmInfoKH f = new frmInfoKH();
            LoadFormToPanel(f);
        }

        private void btnLSDH_Click(object sender, EventArgs e)
        {
            frmLichSuDonHang f = new frmLichSuDonHang();
            LoadFormToPanel(f);
        }

        private void btnPhanHoiKH_Click(object sender, EventArgs e)
        {
            frmPhanHoiKH f = new frmPhanHoiKH();
            LoadFormToPanel(f);
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage();
            homepage.Show();
            this.Hide();
        }

        private void panel_phu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Notice notice = new Notice(Sodienthoai);

            this.Hide();
            notice.ShowDialog();
            this.Show();
        }
    }
}
