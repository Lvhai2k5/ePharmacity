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
    public partial class frmLSDHChiTiet : Form
    {
        DataConnection dc = new DataConnection();
        private int madonhang;
        public frmLSDHChiTiet()
        {
            InitializeComponent();
        }
        public frmLSDHChiTiet(int madonhang)
        {
            InitializeComponent();
            this.madonhang = madonhang;
        }
        private void frmLSDHChiTiet_Load(object sender, EventArgs e)
        {
            LoadThongTinDonHang();
            LoadChiTietSanPham();
        }
        private void LoadThongTinDonHang()
        {
            string sql = @"
                SELECT dh.Madonhang,
                       kh.Sodienthoai,
                       kh.Hovaten,
                       dh.Ngaytaodon,
                       tk.Manguoidung AS NhanVienTao,
                       SUM(ct.Tongtiensanpham) AS TongTien
                FROM Donhang dh
                JOIN Thongtinkhachhang kh ON dh.Sodienthoaikhachhang = kh.Sodienthoai
                JOIN Chitietdonhang ct ON dh.Madonhang = ct.Madonhang
                JOIN Taikhoan tk ON dh.Manhanvien = tk.Manguoidung
                WHERE dh.Madonhang = @id
                GROUP BY dh.Madonhang, kh.Sodienthoai, kh.Hovaten, dh.Ngaytaodon, tk.Manguoidung";

            SqlParameter param = new SqlParameter("@id", madonhang);
            DataTable dt = dc.GetData(sql, param);

            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                txtMaKH.Text = r["Hovaten"].ToString();
                txtMaDH.Text = r["Madonhang"].ToString();
                txtPhone.Text = r["Sodienthoai"].ToString();
                txtNgayTao.Text = Convert.ToDateTime(r["Ngaytaodon"]).ToString("dd/MM/yyyy HH:mm");
                txtNVTao.Text = r["NhanVienTao"].ToString();
                txtTongTien.Text = string.Format("{0:N0} đ", r["TongTien"]);
            }
        }

        private void LoadChiTietSanPham()
        {
            string sql = @"
                SELECT sp.Tenhang AS [Tên sản phẩm],
                       ct.Soluong AS [Số lượng],
                       ct.DonGia AS [Đơn giá],
                       ct.Tongtiensanpham AS [Thành tiền]
                FROM Chitietdonhang ct
                JOIN Sanphamthuoc sp ON ct.Masanpham = sp.ID
                WHERE ct.Madonhang = @id";

            SqlParameter param = new SqlParameter("@id", madonhang);
            DataTable dt = dc.GetData(sql, param);
            dgvChiTiet.DataSource = dt;
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
