using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NhaThuoc
{
    public partial class UpdateCustomerForm : Form
    {
        private DatabaseHelper.DatabaseConnection db;
        private string currentEmployeeId = "NV001"; // TODO: Get from login session
        private DataTable originalCustomersData;

        public UpdateCustomerForm()
        {
            InitializeComponent();
            db = new DatabaseHelper.DatabaseConnection();
            SetupInitialState();
            LoadAllCustomers();
            SetupPlaceholderText();
        }

        private void SetupInitialState()
        {
            pnlEditCustomer.Visible = false;
            
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.MultiSelect = false;
        }

        private void SetupPlaceholderText()
        {
            txtSearch.Enter += (s, e) => {
                if (txtSearch.Text == "Nhập số điện thoại khách hàng")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };
            txtSearch.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "Nhập số điện thoại khách hàng";
                    txtSearch.ForeColor = Color.Gray;
                }
            };
        }
        private void LoadAllCustomers()
        {
            try
            {
                originalCustomersData = db.GetAllCustomers();
                dgvCustomers.DataSource = originalCustomersData;
                
                if (dgvCustomers.Columns.Count == 0)
                {
                    SetupDataGridViewColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách khách hàng: {ex.Message}", 
                              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridViewColumns()
        {
            dgvCustomers.Columns.Clear();
            
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Hovaten",
                HeaderText = "Họ và tên",
                DataPropertyName = "Hovaten",
                Width = 250
            });
            
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Sodienthoai",
                HeaderText = "Số điện thoại",
                DataPropertyName = "Sodienthoai",
                Width = 150
            });
            
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Diemtichluy",
                HeaderText = "Điểm tích lũy",
                DataPropertyName = "Diemtichluy",
                Width = 120
            });
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txtSearch.Text.Trim();
                
                if (string.IsNullOrWhiteSpace(searchTerm) || 
                    searchTerm == "Nhập số điện thoại khách hàng")
                {
                    LoadAllCustomers();
                    return;
                }

                DataTable searchResult = db.SearchCustomers(searchTerm);
                dgvCustomers.DataSource = searchResult;
                
                if (searchResult.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy khách hàng nào phù hợp.", 
                                  "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", 
                              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];
                    
                    string fullName = row.Cells["Hovaten"].Value?.ToString();
                    string phoneNumber = row.Cells["Sodienthoai"].Value?.ToString();
                    string points = row.Cells["Diemtichluy"].Value?.ToString();
                    
                    txtEditFullName.Text = fullName;
                    txtEditPhoneNumber.Text = phoneNumber;
                    txtEditPoints.Text = points;
                    
                    pnlEditCustomer.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi chọn khách hàng: {ex.Message}", 
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateEditInput())
                return;
            try
            {
                string fullName = txtEditFullName.Text.Trim();
                string phoneNumber = txtEditPhoneNumber.Text.Trim();
                int points = int.Parse(txtEditPoints.Text.Trim());
                bool success = db.UpdateCustomer( phoneNumber, fullName, points);
                if (success)
                {
                    MessageBox.Show("Cập nhật thông tin khách hàng thành công!", 
                                  "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LoadAllCustomers();
                    pnlEditCustomer.Visible = false;
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật thông tin khách hàng.", 
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thông tin: {ex.Message}", 
                              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            pnlEditCustomer.Visible = false;
        }
        private bool ValidateEditInput()
        {
            if (string.IsNullOrWhiteSpace(txtEditFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEditFullName.Focus();
                return false;
            }
            if (txtEditFullName.Text.Trim().Length < 2)
            {
                MessageBox.Show("Họ và tên phải có ít nhất 2 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEditFullName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEditPhoneNumber.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEditPhoneNumber.Focus();
                return false;
            }

            string phoneNumber = txtEditPhoneNumber.Text.Trim();
            if (!IsValidVietnamesePhoneNumber(phoneNumber))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập số điện thoại Việt Nam (10-11 số).", 
                              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEditPhoneNumber.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEditPoints.Text))
            {
                MessageBox.Show("Vui lòng nhập điểm tích lũy.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEditPoints.Focus();
                return false;
            }
            if (!int.TryParse(txtEditPoints.Text.Trim(), out int points) || points < 0)
            {
                MessageBox.Show("Điểm tích lũy phải là số nguyên không âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEditPoints.Focus();
                return false;
            }

            return true;
        }
        private bool IsValidVietnamesePhoneNumber(string phoneNumber)
        {
            string pattern = @"^(0|\+84)[3-9]\d{8,9}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Nhập số điện thoại khách hàng")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }
        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Nhập số điện thoại khách hàng";
                txtSearch.ForeColor = Color.Gray;
            }
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
                this.Hide();
                NhanVienBanHang mainForm = new NhanVienBanHang();
                mainForm.Show();
            
        }

        private void UpdateCustomerForm_Load(object sender, EventArgs e)
        {

        }
    }
}
