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

namespace NhaThuoc
{
    public partial class frmLichSuDonHang : Form
    {
        DataConnection dc = new DataConnection();
        private string sdtFilter;
        public frmLichSuDonHang()
        {
            InitializeComponent();
        }
        public frmLichSuDonHang(string sdt)
        {
            InitializeComponent();
            this.sdtFilter = sdt;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void LoadDonHang()
        {
            string keyword = txtSearch.Text.Trim();
            DateTime fromDate = dtpFrom.Value.Date;
            DateTime toDate = dtpTo.Value.Date.AddDays(1).AddTicks(-1); // cuối ngày

            string sql = @"
                SELECT dh.Madonhang,
                       kh.Sodienthoai AS SoDienThoai,
                       kh.Hovaten AS HoTen,
                       dh.Ngaytaodon,
                       SUM(ct.Tongtiensanpham) AS TongTien
                FROM Donhang dh
                JOIN Thongtinkhachhang kh ON dh.Sodienthoaikhachhang = kh.Sodienthoai
                JOIN Chitietdonhang ct ON dh.Madonhang = ct.Madonhang
                WHERE (kh.Sodienthoai LIKE @kw + '%' OR kh.Hovaten LIKE N'%' + @kw + '%')
                  AND dh.Ngaytaodon BETWEEN @fromDate AND @toDate
                GROUP BY dh.Madonhang, kh.Sodienthoai, kh.Hovaten, dh.Ngaytaodon
                ORDER BY dh.Ngaytaodon DESC";

            SqlParameter[] param =
            {
                new SqlParameter("@kw", keyword),
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate", toDate)
            };

            DataTable dt = dc.GetData(sql, param);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("❌ Không tìm thấy thông tin đơn hàng phù hợp.",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            dgvLichSuDH.DataSource = dt;
        }

        private void frmLichSuDonHang_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTo.Value = DateTime.Now;

            if (!string.IsNullOrEmpty(sdtFilter))
            {
                txtSearch.Text = sdtFilter;
            }

            LoadDonHang();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadDonHang();
        }

        private void dgvLichSuDH_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int madonhang = Convert.ToInt32(dgvLichSuDH.Rows[e.RowIndex].Cells["Madonhang"].Value);
                frmLSDHChiTiet frm = new frmLSDHChiTiet(madonhang);
                frm.ShowDialog();
            }
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
