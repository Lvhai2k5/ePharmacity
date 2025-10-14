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
    public partial class frmInfoKH : Form
    {
        DataConnection dc = new DataConnection();
        public frmInfoKH()
        {
            InitializeComponent();
        }
        private void LoadAllCustomers()
        {
            string sql = @"
                        SELECT  kh.Hovaten AS HoTen,
                                kh.Sodienthoai AS SoDienThoai,
                                kh.Diemtichluy AS DiemTichLuy
                        FROM Thongtinkhachhang kh";
                        
            DataTable dt = dc.GetData(sql);
            dgvInfoKH.DataSource = dt;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadAllCustomers();
                return;
            }

            string sql = @"
                        SELECT  kh.Hovaten AS HoTen,
                                kh.Sodienthoai AS SoDienThoai,
                                kh.Diemtichluy AS DiemTichLuy
                        FROM Thongtinkhachhang kh
                        WHERE kh.Sodienthoai LIKE @kw + '%'
                            OR kh.Hovaten LIKE N'%' + @kw + '%'";

            SqlParameter param = new SqlParameter("@kw", keyword);
            DataTable dt = dc.GetData(sql, param);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("❌ Không tìm thấy thông tin khách hàng phù hợp.",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            dgvInfoKH.DataSource = dt;
        }

        private void frmInfoKH_Load(object sender, EventArgs e)
        {
            LoadAllCustomers();
        }


        private void dgvInfoKH_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string sodt = dgvInfoKH.Rows[e.RowIndex].Cells["SoDienThoai"].Value.ToString();
                frmInfoChiTietKH frm = new frmInfoChiTietKH(sodt);
                frm.ShowDialog();
            }
        }
    }
}
