using System;
using System.Drawing;
using System.Text;
using System.IO;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Data;

namespace NhanVienBanHang
{
    public partial class OrderHistoryForm : Form
    {
        private DatabaseHelper.DatabaseConnection db;

        public OrderHistoryForm()
        {
            InitializeComponent();
            db = new DatabaseHelper.DatabaseConnection();
            SetupDataGridViews();
            LoadInitialData();
        }

        private void SetupDataGridViews()
        {
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Madonhang",
                HeaderText = "Mã đơn hàng",
                DataPropertyName = "Madonhang",
                Width = 100
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Sodienthoaikhachhang",
                HeaderText = "Số điện thoại",
                DataPropertyName = "Sodienthoaikhachhang",
                Width = 120
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenKhachHang",
                HeaderText = "Tên khách hàng",
                DataPropertyName = "TenKhachHang",
                Width = 200
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Ngaytaodon",
                HeaderText = "Ngày tạo đơn",
                DataPropertyName = "Ngaytaodon",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenNhanVien",
                HeaderText = "Nhân viên",
                DataPropertyName = "TenNhanVien",
                Width = 150
            });

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tongtien",
                HeaderText = "Tổng tiền",
                DataPropertyName = "Tongtien",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });

            // Configure columns for order details
            dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Madonhang",
                HeaderText = "Mã đơn hàng",
                DataPropertyName = "Madonhang",
                Width = 100
            });

            dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Masanpham",
                HeaderText = "Mã sản phẩm",
                DataPropertyName = "Masanpham",
                Width = 100
            });

            dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tenhang",
                HeaderText = "Tên sản phẩm",
                DataPropertyName = "Tenhang",
                Width = 200
            });

            dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Soluong",
                HeaderText = "Số lượng",
                DataPropertyName = "Soluong",
                Width = 80
            });

            dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DonGia",
                HeaderText = "Đơn giá",
                DataPropertyName = "DonGia",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });

            dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tongtiensanpham",
                HeaderText = "Thành tiền",
                DataPropertyName = "Tongtiensanpham",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });
        }

        private void LoadInitialData()
        {
            try
            {
                DataTable orders = db.GetDataFromStoredProcedure("sp_GetOrderHistory");
                dgvOrders.DataSource = orders;
                lblTotalOrders.Text = $"Tổng số đơn hàng: {orders.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu ban đầu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchCriteria = cmbSearchCriteria.SelectedItem?.ToString() ?? "";
                string searchValue = txtSearchValue.Text.Trim();
                if (searchCriteria == "Mã đơn hàng" && !string.IsNullOrEmpty(searchValue))
                {
                    if (!int.TryParse(searchValue, out int orderId))
                    {
                        MessageBox.Show("Mã đơn hàng phải là số nguyên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                dgvOrderDetails.DataSource = null;

                DateTime? fromDate = dtpFromDate.Checked ? dtpFromDate.Value.Date : (DateTime?)null;
                DateTime? toDate = dtpToDate.Checked ? dtpToDate.Value.Date.AddDays(1).AddTicks(-1) : (DateTime?)null; // Kết thúc ngày 23:59:59

                DataTable orders = db.GetDataFromStoredProcedure("sp_GetOrderHistory", 
                    new System.Data.SqlClient.SqlParameter("@SearchCriteria", GetSearchCriteriaValue(searchCriteria)),
                    new System.Data.SqlClient.SqlParameter("@SearchValue", searchValue),
                    new System.Data.SqlClient.SqlParameter("@FromDate", fromDate ?? (object)DBNull.Value),
                    new System.Data.SqlClient.SqlParameter("@ToDate", toDate ?? (object)DBNull.Value));

                dgvOrders.DataSource = orders;
                lblTotalOrders.Text = $"Tổng số đơn hàng: {orders.Rows.Count}";

                if (orders.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy đơn hàng nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetSearchCriteriaValue(string displayText)
        {
            switch (displayText)
            {
                case "Mã đơn hàng": return "madon";
                case "Mã khách hàng": return "makhach";
                case "Số điện thoại": return "sodienthoai";
                default: return "";
            }
        }

        private void DgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count > 0)
            {
                int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["Madonhang"].Value);
                LoadOrderDetails(orderId);
            }
        }

        private void LoadOrderDetails(int orderId)
        {
            try
            {
                DataTable details = db.GetDataFromStoredProcedure("sp_GetOrderDetails",
                    new System.Data.SqlClient.SqlParameter("@OrderId", orderId));

                dgvOrderDetails.DataSource = details;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết đơn hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrders.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PrintDialog printDialog = new PrintDialog();
                PrintDocument printDocument = new PrintDocument();

                printDocument.PrintPage += (s, ev) =>
                {
                    string content = GenerateReportContent();
                    Font font = new Font("Arial", 10);
                    ev.Graphics.DrawString(content, font, Brushes.Black, ev.MarginBounds);
                };

                printDialog.Document = printDocument;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi in báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrders.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                    FileName = $"LichSuDonHang_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportToCSV(saveDialog.FileName);
                    MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GenerateReportContent()
        {
            StringBuilder content = new StringBuilder();
            content.AppendLine("=== BÁO CÁO LỊCH SỬ ĐƠN HÀNG ===");
            content.AppendLine($"Ngày tạo báo cáo: {DateTime.Now:dd/MM/yyyy HH:mm}");
            content.AppendLine();

            foreach (DataGridViewRow row in dgvOrders.Rows)
            {
                content.AppendLine($"Mã đơn hàng: {row.Cells["Madonhang"].Value}");
                content.AppendLine($"Khách hàng: {row.Cells["TenKhachHang"].Value}");
                content.AppendLine($"Số điện thoại: {row.Cells["Sodienthoaikhachhang"].Value}");
                content.AppendLine($"Ngày tạo: {row.Cells["Ngaytaodon"].Value}");
                content.AppendLine($"Tổng tiền: {row.Cells["Tongtien"].Value:N0} VNĐ");
                content.AppendLine("---");
            }

            return content.ToString();
        }

        private void ExportToCSV(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                for (int i = 0; i < dgvOrders.Columns.Count; i++)
                {
                    writer.Write(dgvOrders.Columns[i].HeaderText);
                    if (i < dgvOrders.Columns.Count - 1)
                        writer.Write(",");
                }
                writer.WriteLine();
                foreach (DataGridViewRow row in dgvOrders.Rows)
                {
                    for (int i = 0; i < dgvOrders.Columns.Count; i++)
                    {
                        writer.Write(row.Cells[i].Value?.ToString() ?? "");
                        if (i < dgvOrders.Columns.Count - 1)
                            writer.Write(",");
                    }
                    writer.WriteLine();
                }
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchValue.Clear();
                cmbSearchCriteria.SelectedIndex = -1;
                dtpFromDate.Checked = false;
                dtpToDate.Checked = false;
                DataTable orders = db.GetDataFromStoredProcedure("sp_GetOrderHistory");
                dgvOrders.DataSource = orders;
                dgvOrderDetails.DataSource = null;
                lblTotalOrders.Text = $"Tổng số đơn hàng: {orders.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi làm mới dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            NhanVienBanHang mainForm = new NhanVienBanHang();
            mainForm.Show();
        }

        private void pnlSearch_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
