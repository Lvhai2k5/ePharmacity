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
    public partial class frmPhanHoiKH : Form
    {
        DataConnection dc = new DataConnection();
      
        public frmPhanHoiKH()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void frmPhanHoiKH_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(1900, 1, 1);
            dtpDenNgay.Value = DateTime.Now;

            cbbRating.Items.Clear();
            cbbRating.Items.Add("Tất cả");
            cbbRating.Items.Add("5");
            cbbRating.Items.Add("4");
            cbbRating.Items.Add("3");
            cbbRating.Items.Add("2");
            cbbRating.Items.Add("1");
            cbbRating.SelectedIndex = 0;

            LoadPhanHoi();
        }
        private void LoadPhanHoi()
        {
            string keyword = txtTimKiem.Text.Trim();
            DateTime fromDate = dtpTuNgay.Value.Date;
            DateTime toDate = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);
            string rating = cbbRating.SelectedItem?.ToString();

            string sql = @"
        SELECT ID,
               Hovaten AS [Họ và tên KH],
               Sodienthoai AS [Số điện thoại],
               Phanhoi AS [Phản hồi KH],
               Trangthai AS [Đánh giá],
               Ngaytao AS [Ngày phản hồi],
               Manhanvien AS [Nhân viên phản hồi]
        FROM Phanhoikhachhang
        WHERE Ngaytao BETWEEN @from AND @to";

            List<SqlParameter> prms = new List<SqlParameter>()
    {
        new SqlParameter("@from", fromDate),
        new SqlParameter("@to", toDate)
    };

            // Lọc theo từ khóa (SĐT hoặc họ tên)
            if (!string.IsNullOrEmpty(keyword))
            {
                sql += " AND (Sodienthoai = @kw OR Hovaten LIKE N'%' + @kw + '%')";
                prms.Add(new SqlParameter("@kw", keyword));
            }

            // Lọc theo rating (nếu có)
            if (!string.IsNullOrEmpty(rating) && rating != "Tất cả")
            {
                sql += " AND Trangthai = @rating";
                prms.Add(new SqlParameter("@rating", rating));
            }

            sql += " ORDER BY Ngaytao DESC";

            DataTable dt = dc.GetData(sql, prms.ToArray());
            dgvPhanHoi.DataSource = dt;
        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadPhanHoi();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgvPhanHoi.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng để thêm phản hồi!", "Thông báo");
                return;
            }

            var row = dgvPhanHoi.SelectedRows[0];

            string sdt = row.Cells["Số điện thoại"].Value.ToString();
            string hoTen = row.Cells["Họ và tên KH"].Value.ToString();

            frmChiTietPhanHoi frm = new frmChiTietPhanHoi(sdt);
            frm.ShowDialog();

            LoadPhanHoi();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPhanHoi.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phản hồi cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvPhanHoi.SelectedRows[0].Cells["ID"].Value);
            DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa phản hồi này?",
                                              "Xác nhận",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                string sql = "DELETE FROM Phanhoikhachhang WHERE ID = @id";
                SqlParameter param = new SqlParameter("@id", id);
                dc.GetData(sql, param);
                LoadPhanHoi();
            }
        }
    }
}
