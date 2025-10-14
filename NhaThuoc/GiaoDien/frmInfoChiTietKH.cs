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
    public partial class frmInfoChiTietKH : Form
    {
        DataConnection dc = new DataConnection();
        private string sdt;
        public frmInfoChiTietKH(string sodienthoai)
        {
            InitializeComponent();
            this.sdt = sodienthoai;
        }
        private void LoadThongTinKhachHang()
        {
            string sql = "SELECT * FROM Thongtinkhachhang WHERE Sodienthoai = @sdt";
            SqlParameter param = new SqlParameter("@sdt", sdt);
            DataTable dt = dc.GetData(sql, param);

            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                txtPhone.Text = r["Sodienthoai"].ToString();
                txtName.Text = r["Hovaten"].ToString();
                txtDiemTichLuy.Text = r["Diemtichluy"].ToString();
                txtMaNV.Text = r["Manhanvien"].ToString();
            }
        }
        private void frmInfoChiTietKH_Load(object sender, EventArgs e)
        {
            LoadThongTinKhachHang();
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLSDH_Click(object sender, EventArgs e)
        {
            string sdt = txtPhone.Text.Trim();
            if (string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng chọn khách hàng trước khi xem lịch sử mua hàng!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mở form lịch sử đơn hàng, truyền số điện thoại để lọc sẵn
            frmLichSuDonHang frm = new frmLichSuDonHang(sdt);
            frm.ShowDialog();
        }
    }
}
