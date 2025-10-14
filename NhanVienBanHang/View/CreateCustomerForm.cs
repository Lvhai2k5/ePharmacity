using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NhanVienBanHang
{
    public partial class CreateCustomerForm : Form
    {
        private DatabaseHelper.DatabaseConnection db;
        private string currentEmployeeId = "NV001"; // TODO: Get from login session

        public string CustomerName { get; private set; }
        public string CustomerPhone { get; private set; }
        public int CustomerPoints { get; private set; }

        public CreateCustomerForm()
        {
            InitializeComponent();
            db = new DatabaseHelper.DatabaseConnection();
            SetupPlaceholderText();
            SetupInitialState();
        }

        private void SetupInitialState()
        {
            pnlExistingCustomer.Visible = false;
            pnlCustomerInfo.Visible = true;
        }

        private void SetupPlaceholderText()
        {
            txtFullName.Enter += (s, e) => {
                if (txtFullName.Text == "Nhập họ và tên khách hàng")
                {
                    txtFullName.Text = "";
                    txtFullName.ForeColor = Color.Black;
                }
            };
            txtFullName.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    txtFullName.Text = "Nhập họ và tên khách hàng";
                    txtFullName.ForeColor = Color.Gray;
                }
            };

            txtPhoneNumber.Enter += (s, e) => {
                if (txtPhoneNumber.Text == "Nhập số điện thoại")
                {
                    txtPhoneNumber.Text = "";
                    txtPhoneNumber.ForeColor = Color.Black;
                }
            };
            txtPhoneNumber.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
                {
                    txtPhoneNumber.Text = "Nhập số điện thoại";
                    txtPhoneNumber.ForeColor = Color.Gray;
                }
            };
        }

        private void BtnCheckExisting_Click(object sender, EventArgs e)
        {
            if (!ValidatePhoneNumber())
                return;

            try
            {
                string phoneNumber = txtPhoneNumber.Text.Trim();
                
                if (db.IsPhoneNumberExists(phoneNumber))
                {
                    ShowExistingCustomerPanel(phoneNumber);
                }
                else
                {
                    MessageBox.Show("Số điện thoại chưa được sử dụng. Bạn có thể tạo tài khoản mới.", 
                                  "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kiểm tra số điện thoại: {ex.Message}", 
                              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUseExisting_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn sử dụng khách hàng này?", 
                                                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string phoneNumber = txtPhoneNumber.Text.Trim();
                var customerData = db.CheckCustomerPhone(phoneNumber);
                if (customerData.Rows.Count > 0)
                {
                    var row = customerData.Rows[0];
                    CustomerName = row["Hovaten"].ToString();
                    CustomerPhone = phoneNumber;
                    CustomerPoints = Convert.ToInt32(row["Diemtichluy"]);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void BtnCreateNew_Click(object sender, EventArgs e)
        {
            pnlExistingCustomer.Visible = false;
            pnlCustomerInfo.Visible = true;
            txtPhoneNumber.Clear();
            txtFullName.Clear();
            txtPhoneNumber.Focus();
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                string fullName = txtFullName.Text.Trim();
                string phoneNumber = txtPhoneNumber.Text.Trim();

                // Đảm bảo nhân viên mặc định tồn tại
                db.EnsureDefaultEmployeeExists();

                // Kiểm tra lại số điện thoại trước khi tạo
                if (db.IsPhoneNumberExists(phoneNumber))
                {
                    // Hiển thị panel khách hàng đã tồn tại để cho phép "Sử dụng" hoặc "Tạo mới"
                    MessageBox.Show("Số điện thoại đã được sử dụng. Bạn có thể sử dụng khách hàng này hoặc tạo mới.",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowExistingCustomerPanel(phoneNumber);
                    return;
                }

                db.CreateCustomer(phoneNumber, fullName, currentEmployeeId);
                CustomerName = fullName;
                CustomerPhone = phoneNumber;
                CustomerPoints = 0;

                MessageBox.Show($"Tạo tài khoản thành công!\n" +
                              $"Tên: {fullName}\n" +
                              $"SĐT: {phoneNumber}\n" +
                              $"Điểm tích lũy: 0", 
                              "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo tài khoản: {ex.Message}", 
                              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowExistingCustomerPanel(string phoneNumber)
        {
            var customerData = db.CheckCustomerPhone(phoneNumber);
            if (customerData.Rows.Count > 0)
            {
                var row = customerData.Rows[0];
                string customerName = row["Hovaten"].ToString();
                int points = Convert.ToInt32(row["Diemtichluy"]);

                lblExistingCustomerInfo.Text =
                    "Khách hàng đã tồn tại:" + Environment.NewLine +
                    $"Tên: {customerName}" + Environment.NewLine +
                    $"SĐT: {phoneNumber}" + Environment.NewLine +
                    $"Điểm tích lũy: {points} điểm";

                // Hiển thị panel khách hàng đã tồn tại nằm trong pnlCustomerInfo
                pnlCustomerInfo.Visible = true;
                pnlExistingCustomer.Visible = true;
                pnlExistingCustomer.BringToFront();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateInput()
        {
            // Validate full name
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || txtFullName.Text == "Nhập họ và tên khách hàng")
            {
                MessageBox.Show("Vui lòng nhập họ và tên khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (txtFullName.Text.Trim().Length < 2)
            {
                MessageBox.Show("Họ và tên phải có ít nhất 2 ký tự.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            // Validate phone number
            if (!ValidatePhoneNumber())
                return false;

            return true;
        }

        private bool ValidatePhoneNumber()
        {
            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text) || txtPhoneNumber.Text == "Nhập số điện thoại")
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhoneNumber.Focus();
                return false;
            }

            string phoneNumber = txtPhoneNumber.Text.Trim();
            
            if (!IsValidVietnamesePhoneNumber(phoneNumber))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập số điện thoại Việt Nam (10-11 số).", 
                              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhoneNumber.Focus();
                return false;
            }

            return true;
        }

        private bool IsValidVietnamesePhoneNumber(string phoneNumber)
        {
            string pattern = @"^(0|\+84)[3-9]\d{8,9}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            NhanVienBanHang mainForm = new NhanVienBanHang();
            mainForm.Show();
        }

        private void CreateCustomerForm_Load(object sender, EventArgs e)
        {

        }
    }
}