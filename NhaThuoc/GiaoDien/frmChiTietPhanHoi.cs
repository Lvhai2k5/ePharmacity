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
    public partial class frmChiTietPhanHoi : Form
    {
        private string sdt;
        DataConnection dc = new DataConnection();

        public frmChiTietPhanHoi(string sdt)
        {
            InitializeComponent();
            this.sdt = sdt;
        }

        public frmChiTietPhanHoi()
        {
            InitializeComponent();
        }

        private void frmChiTietPhanHoi_Load(object sender, EventArgs e)
        {
            LoadThongTinVaPhanHoi(sdt);

            txtNgPhanHoi.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            txtNVPhanHoi.Text = "NV001"; 
        }
        private void LoadThongTinVaPhanHoi(string sodt)
        {
            string sql = @"
        SELECT kh.Sodienthoai, kh.Hovaten,
               ph.Phanhoi, ph.Traloi
        FROM Thongtinkhachhang kh
        LEFT JOIN (
            SELECT TOP 1 Sodienthoai, Phanhoi, Traloi
            FROM Phanhoikhachhang
            WHERE Sodienthoai = @sdt
            ORDER BY Ngaytao DESC
        ) ph ON kh.Sodienthoai = ph.Sodienthoai
        WHERE kh.Sodienthoai = @sdt";

            SqlParameter p = new SqlParameter("@sdt", sodt);
            DataTable dt = dc.GetData(sql, p);

            if (dt.Rows.Count > 0)
            {
                DataRow r = dt.Rows[0];
                txtPhone.Text = r["Sodienthoai"].ToString();
                txtName.Text = r["Hovaten"].ToString();
                txtPHKH.Text = r["Phanhoi"] != DBNull.Value ? r["Phanhoi"].ToString() : "";
                txtTraLoi.Text = r.Table.Columns.Contains("Traloi") && r["Traloi"] != DBNull.Value
                    ? r["Traloi"].ToString()
                    : "";
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin khách hàng!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sdt = txtPhone.Text.Trim();
            string hoTen = txtName.Text.Trim();
            string phanHoi = txtPHKH.Text.Trim();
            string traLoi = txtTraLoi.Text.Trim();
            string ngayPhanHoi = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string maNV = txtNVPhanHoi.Text.Trim();

            if (string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(hoTen) ||
                string.IsNullOrEmpty(phanHoi) || string.IsNullOrEmpty(traLoi))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin phản hồi!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            string sql = @"
        INSERT INTO Phanhoikhachhang (Sodienthoai, Phanhoi, Traloi, Nhanvienphanhoi, Ngaytao)
        VALUES (@sdt, @phanhoi, @traloi, @manv, @ngaytao)";

            using (SqlConnection conn = new SqlConnection(dc.connection))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@sdt", sdt);
                        cmd.Parameters.AddWithValue("@phanhoi", phanHoi);
                        cmd.Parameters.AddWithValue("@traloi", traLoi);
                        cmd.Parameters.AddWithValue("@manv", maNV);
                        cmd.Parameters.AddWithValue("@ngaytao", ngayPhanHoi);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Lưu phản hồi thành công!",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu phản hồi: " + ex.Message,
                                    "Lỗi",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
