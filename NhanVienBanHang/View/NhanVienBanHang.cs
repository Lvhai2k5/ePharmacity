using System;
using System.Drawing;
using System.Windows.Forms;

namespace NhanVienBanHang
{
    public partial class NhanVienBanHang : Form
    {
        private Button btnOrderHistory;
        private Button btnCreateCustomer;
        private Button btnEditCustomer;
        private Button btnCreateOrder;
        private Button btnExit;
        private Label lblTitle;
        private Panel pnlMain;
        private Panel pnlHeader;
        private Panel pnlMenu;

        public NhanVienBanHang()
        {
            InitializeComponent();
            SetupHoverEffects();
            TestDatabaseConnection();
        }

        private void TestDatabaseConnection()
        {
            try
            {
                var db = new DatabaseHelper.DatabaseConnection();
                if (!db.TestConnection())
                {
                    // Connection failed - show warning but allow app to continue
                    // User can still use the app, but database features won't work
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể kết nối database: {ex.Message}\n\nỨng dụng vẫn có thể chạy nhưng các chức năng database sẽ không hoạt động.", 
                              "Cảnh báo Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SetupHoverEffects()
        {
            AddHoverEffect(btnOrderHistory, Color.FromArgb(52, 152, 219));
            AddHoverEffect(btnCreateCustomer, Color.FromArgb(46, 204, 113));
            AddHoverEffect(btnEditCustomer, Color.FromArgb(155, 89, 182));
            AddHoverEffect(btnCreateOrder, Color.FromArgb(230, 126, 34));
            AddHoverEffect(btnExit, Color.FromArgb(231, 76, 60));
        }

        private void AddHoverEffect(Button button, Color originalColor)
        {
            button.MouseEnter += (s, e) => {
                button.BackColor = ControlPaint.Light(originalColor, 0.2f);
            };
            
            button.MouseLeave += (s, e) => {
                button.BackColor = originalColor;
            };
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                string useCaseId = button.Tag?.ToString();
                
                try
                {
                    switch (useCaseId)
                    {
                        case "UC001":
                            ShowOrderHistoryForm();
                            break;
                        case "UC002":
                            ShowCreateCustomerForm();
                            break;
                        case "UC003":
                            ShowCreateOrderForm();
                            break;
                        case "UC004":
                            ShowUpdateCustomerForm();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi mở form: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ShowOrderHistoryForm()
        {
            using (OrderHistoryForm form = new OrderHistoryForm())
            {
                form.ShowDialog();
            }
        }

        private void ShowCreateCustomerForm()
        {
            using (CreateCustomerForm form = new CreateCustomerForm())
            {
                form.ShowDialog();
            }
        }

        private void ShowCreateOrderForm()
        {
            using (CreateOrderForm form = new CreateOrderForm())
            {
                form.ShowDialog();
            }
        }

        private void ShowUpdateCustomerForm()
        {
            using (UpdateCustomerForm form = new UpdateCustomerForm())
            {
                form.ShowDialog();
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", 
                                                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}