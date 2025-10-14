using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data;

namespace NhaThuoc
{
    public partial class CreateOrderForm : Form
    {
        private DatabaseHelper.DatabaseConnection db;
        private List<OrderItem> orderItems;
        private string currentEmployeeId = "NV001"; // TODO: Get from login session
        private string currentCustomerPhone = "";
        private int pendingRedeemPoints = 0; 

        public CreateOrderForm()
        {
            InitializeComponent();
            db = new DatabaseHelper.DatabaseConnection();
            orderItems = new List<OrderItem>();
            db.EnsureDefaultEmployeeExists();  
            SetupDataGridViews();
            LoadProducts();
        }
        private void SetupDataGridViews()
        {
            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductName",
                HeaderText = "Tên sản phẩm",
                DataPropertyName = "ProductName",
                Width = 250
            });

            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "Số lượng",
                DataPropertyName = "Quantity",
                Width = 80
            });

            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UnitPrice",
                HeaderText = "Đơn giá",
                DataPropertyName = "UnitPrice",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });

            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalPrice",
                HeaderText = "Thành tiền",
                DataPropertyName = "TotalPrice",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });

            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stock",
                HeaderText = "Tồn kho",
                DataPropertyName = "Stock",
                Width = 80
            });
        }
        private void LoadProducts()
        {
            try
            {
                DataTable products = db.GetAvailableProducts();
                cmbProducts.Items.Clear();
                
                foreach (DataRow row in products.Rows)
                {
                    int stock = 0;
                    if (row.Table.Columns.Contains("Kho") && row["Kho"] != DBNull.Value)
                        stock = Convert.ToInt32(row["Kho"]);
                    else if (row.Table.Columns.Contains("SoLuongTon") && row["SoLuongTon"] != DBNull.Value)
                        stock = Convert.ToInt32(row["SoLuongTon"]);
                    string stockStatus = stock > 0 ? $"Còn: {stock}" : "HẾT HÀNG";
                    string displayText = $"{row["Tenhang"]} - {stockStatus} - Giá: {Convert.ToDecimal(row["Giaban"]):N0} VNĐ";
                    
                    ProductItem item = new ProductItem
                    {
                        ProductId = Convert.ToInt32(row["ID"]),
                        ProductName = row["Tenhang"].ToString(),
                        Stock = stock,
                        Price = Convert.ToDecimal(row["Giaban"]),
                        DisplayText = displayText
                    };
                    
                    cmbProducts.Items.Add(item);
                }

                if (cmbProducts.Items.Count > 0)
                    cmbProducts.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedItem is ProductItem product)
            {
                txtProductPrice.Text = product.Price.ToString("N0");
                txtProductStock.Text = product.Stock > 0 ? product.Stock.ToString() : "HẾT HÀNG";
                
                btnAddProduct.Enabled = product.Stock > 0;
            }
        }

        private void BtnSelectCustomer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string phoneNumber = txtCustomerPhone.Text.Trim();
                var customerData = db.GetCustomerByPhone(phoneNumber);
                
                if (customerData.Rows.Count > 0)
                {
                    var row = customerData.Rows[0];
                    txtCustomerName.Text = row["Hovaten"].ToString();
                    currentCustomerPhone = phoneNumber;
                    MessageBox.Show("Đã tìm thấy khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng với số điện thoại này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCustomerName.Clear();
                    currentCustomerPhone = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCreateNewCustomer_Click(object sender, EventArgs e)
        {
            using (CreateCustomerForm customerForm = new CreateCustomerForm())
            {
                if (customerForm.ShowDialog() == DialogResult.OK)
                {
                    txtCustomerPhone.Text = customerForm.CustomerPhone;
                    txtCustomerName.Text = customerForm.CustomerName;
                    currentCustomerPhone = customerForm.CustomerPhone;
                    MessageBox.Show("Đã tạo khách hàng mới thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedItem is ProductItem product)
            {
                int quantity = (int)nudQuantity.Value;
                int actualStock = db.GetProductStock(product.ProductId);
                if (actualStock <= 0)
                {
                    MessageBox.Show($"Sản phẩm '{product.ProductName}' đã hết hàng!", 
                                  "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (quantity > actualStock)
                {
                    MessageBox.Show($"Số lượng vượt quá tồn kho. Tồn kho hiện tại: {actualStock}", 
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var existingItem = orderItems.FirstOrDefault(x => x.ProductId == product.ProductId);
                if (existingItem != null)
                {
                    if (existingItem.Quantity + quantity > actualStock)
                    {
                        MessageBox.Show($"Tổng số lượng vượt quá tồn kho. Tồn kho hiện tại: {actualStock}", 
                                      "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    existingItem.Quantity += quantity;
                    existingItem.TotalPrice = existingItem.Quantity * existingItem.UnitPrice;
                }
                else
                {
                    OrderItem newItem = new OrderItem
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        Quantity = quantity,
                        UnitPrice = product.Price,
                        TotalPrice = quantity * product.Price,
                        Stock = actualStock
                    };
                    
                    orderItems.Add(newItem);
                }
                RefreshOrderItems();
                CalculateTotal();
            }
        }

        private void BtnRemoveProduct_Click(object sender, EventArgs e)
        {
            if (dgvOrderItems.SelectedRows.Count > 0)
            {
                int productId = Convert.ToInt32(dgvOrderItems.SelectedRows[0].Cells["ProductId"].Value);
                orderItems.RemoveAll(x => x.ProductId == productId);
                RefreshOrderItems();
                CalculateTotal();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (orderItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa tất cả sản phẩm khỏi đơn hàng?", 
                                                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    orderItems.Clear();
                    RefreshOrderItems();
                    CalculateTotal();
                }
            }
        }
        private void RefreshOrderItems()
        {
            dgvOrderItems.DataSource = null;
            dgvOrderItems.DataSource = orderItems;
        }

        private void CalculateTotal()
        {
            decimal total = orderItems.Sum(x => x.TotalPrice);
            lblTotalValue.Text = $"{total:N0} VNĐ";
        }

        private void BtnPrintInvoice_Click(object sender, EventArgs e)
        {
            decimal total = CalculateTotalAmount();
            decimal payable = Math.Max(0, total - pendingRedeemPoints);
            var itemsSnapshot = orderItems.Select(x => new OrderItem
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                TotalPrice = x.TotalPrice,
                Stock = x.Stock
            }).ToList();
            PrintInvoicePreview(itemsSnapshot, txtCustomerName.Text, currentCustomerPhone, payable, pendingRedeemPoints);
        }
        private void BtnPay_Click(object sender, EventArgs e)
        {
            if (orderItems.Count == 0)
            {
                MessageBox.Show("Chưa có sản phẩm để thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = CalculateTotalAmount();
            decimal payable = Math.Max(0, total - pendingRedeemPoints);
            DialogResult confirm = MessageBox.Show($"Xác nhận thanh toán số tiền {payable:N0} VNĐ?",
                                                  "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                string snapshotName = txtCustomerName.Text;
                string snapshotPhone = currentCustomerPhone;
                var itemsSnapshot = orderItems.Select(x => new OrderItem
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    TotalPrice = x.TotalPrice,
                    Stock = x.Stock
                }).ToList();

                int orderId = FinalizeOrder(payable);
                if (orderId > 0)
                {
                    if (pendingRedeemPoints > 0)
                    {
                        db.RedeemCustomerPoints(snapshotPhone, pendingRedeemPoints);
                    }
                    PrintInvoicePreview(itemsSnapshot, snapshotName, snapshotPhone, payable, pendingRedeemPoints);
                    pendingRedeemPoints = 0;
                    ResetForm();
                }
            }
        }
        private void BtnRedeemPoints_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentCustomerPhone))
            {
                MessageBox.Show("Vui lòng chọn khách hàng trước khi áp điểm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int currentPoints = db.GetCustomerPoints(currentCustomerPhone);
                if (currentPoints <= 0)
                {
                    MessageBox.Show("Khách hàng chưa có điểm để áp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                decimal total = CalculateTotalAmount();
                int maxRedeemable = (int)Math.Min(currentPoints, (int)total);

            string input = PromptInput(
                    $"Điểm hiện có: {currentPoints}\nTổng tiền: {total:N0} VNĐ\nNhập số điểm muốn dùng (tối đa {maxRedeemable}):",
                    "Áp điểm tích lũy", Math.Min(1000, maxRedeemable).ToString());

                if (int.TryParse(input, out int redeem) && redeem > 0)
                {
                    if (redeem > maxRedeemable)
                    {
                        MessageBox.Show("Số điểm vượt quá giới hạn có thể áp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    pendingRedeemPoints = redeem;
                    decimal newTotal = total - redeem;
                    lblTotalValue.Text = $"{newTotal:N0} VNĐ";

                    MessageBox.Show($"Đã áp {redeem} điểm (chưa trừ vào tài khoản). Số tiền tạm tính: {newTotal:N0} VNĐ\nĐiểm sẽ chỉ bị trừ sau khi thanh toán thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi áp điểm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int FinalizeOrder(decimal payable)
        {
            if (string.IsNullOrWhiteSpace(currentCustomerPhone) || orderItems.Count == 0)
                return -1;

            try
            {
                List<string> insufficientStockItems;
                bool stockAvailable = db.CheckStockAvailability(orderItems, out insufficientStockItems);
                if (!stockAvailable)
                {
                    string stockMessage = "Không đủ hàng trong kho cho các sản phẩm sau:\n\n" + string.Join("\n", insufficientStockItems);
                    MessageBox.Show(stockMessage, "Không đủ hàng trong kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                int orderId = db.CreateOrder(currentCustomerPhone, currentEmployeeId);
                foreach (var item in orderItems)
                {
                    db.AddOrderDetail(orderId, item.ProductId, item.Quantity, item.UnitPrice);
                    int newStock = item.Stock - item.Quantity;
                    db.UpdateProductStock(item.ProductId, newStock);
                }

                int pointsEarned = db.CalculatePointsFromOrder(payable);
                if (pointsEarned > 0)
                {
                    db.AddPointsToCustomer(currentCustomerPhone, pointsEarned);
                }

                MessageBox.Show($"Thanh toán thành công!\nTổng thanh toán: {payable:N0} VNĐ\nĐiểm tích lũy: +{pointsEarned}",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return orderId;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thanh toán: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }private void PrintInvoicePreview(List<OrderItem> items, string customerName, string customerPhone, decimal payable, int redeemPoints)
{
    System.Drawing.Printing.PrintDocument doc = new System.Drawing.Printing.PrintDocument();

    doc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A5", 583, 827); 
    doc.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(40, 40, 30, 30);

    doc.PrintPage += (s, e) =>
    {
        float y = 30;
        int left = 40;
        int right = 550;
        int colSL = left + 220;
        int colPrice = right - 150;
        int colAmount = right - 20;
        StringFormat sfRight = new StringFormat { Alignment = StringAlignment.Far };
        StringFormat sfCenter = new StringFormat { Alignment = StringAlignment.Center };

        Font titleFont = new Font("Segoe UI", 14, FontStyle.Bold);
        Font headerFont = new Font("Segoe UI", 11, FontStyle.Bold);
        Font normalFont = new Font("Segoe UI", 10);
        Font boldFont = new Font("Segoe UI", 10, FontStyle.Bold);

        try
        {
            Image logo = Image.FromFile(@"C:\Users\ASUS\source\repos\NhanVienBanHang\View\maQR.jpg");
            e.Graphics.DrawImage(logo, left + ((right - left) / 2) - 35, y, 70, 70);
        }
        catch { }

        e.Graphics.DrawString("CỬA HÀNG ePHARMA", titleFont, Brushes.Black, new RectangleF(left, y + 75, right - left, 25), sfCenter);
        y += 105;
        e.Graphics.DrawString("HÓA ĐƠN BÁN HÀNG", headerFont, Brushes.Black, new RectangleF(left, y, right - left, 25), sfCenter);
        y += 35;

        // === THÔNG TIN KHÁCH & NHÂN VIÊN ===
        e.Graphics.DrawString($"Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm}", normalFont, Brushes.Black, left, y); y += 18;
        e.Graphics.DrawString($"Nhân viên: {currentEmployeeId}", normalFont, Brushes.Black, left, y); y += 18;

        if (!string.IsNullOrWhiteSpace(customerName))
        {
            e.Graphics.DrawString($"Khách hàng: {customerName}", normalFont, Brushes.Black, left, y); y += 18;
        }
        if (!string.IsNullOrWhiteSpace(customerPhone))
        {
            e.Graphics.DrawString($"SĐT: {customerPhone}", normalFont, Brushes.Black, left, y); y += 18;
            try
            {
                int currentPoints = db.GetCustomerPoints(customerPhone);
                e.Graphics.DrawString($"Điểm hiện có: {currentPoints}", normalFont, Brushes.Black, left, y); y += 18;
            }
            catch { }
        }

        y += 5;
        e.Graphics.DrawLine(Pens.Black, left, y, right, y);
        y += 8;

        e.Graphics.DrawString("Tên sản phẩm", boldFont, Brushes.Black, left, y);
        e.Graphics.DrawString("SL", boldFont, Brushes.Black, colSL, y, sfRight);
        e.Graphics.DrawString("Đơn giá", boldFont, Brushes.Black, colPrice, y, sfRight);
        e.Graphics.DrawString("Thành tiền", boldFont, Brushes.Black, colAmount, y, sfRight);
        y += 18;
        e.Graphics.DrawLine(Pens.Black, left, y, right, y);
        y += 6;

        decimal total = 0;
        foreach (var it in items)
        {
            total += it.TotalPrice;
            string name = it.ProductName;

            float nameWidth = colSL - (left + 5);
            var words = name.Split(' ');
            string line = "";
            foreach (var word in words)
            {
                string test = (line.Length > 0 ? line + " " : "") + word;
                if (e.Graphics.MeasureString(test, normalFont).Width > nameWidth)
                {
                    e.Graphics.DrawString(line, normalFont, Brushes.Black, left, y);
                    y += 16;
                    line = word;
                }
                else
                    line = test;
            }
            e.Graphics.DrawString(line, normalFont, Brushes.Black, left, y);

            e.Graphics.DrawString(it.Quantity.ToString(), normalFont, Brushes.Black, colSL, y, sfRight);
            e.Graphics.DrawString(it.UnitPrice.ToString("N0"), normalFont, Brushes.Black, colPrice, y, sfRight);
            e.Graphics.DrawString(it.TotalPrice.ToString("N0"), normalFont, Brushes.Black, colAmount, y, sfRight);
            y += 20;
        }

        e.Graphics.DrawLine(Pens.Black, left, y, right, y);
        y += 8;
        e.Graphics.DrawString($"Tổng cộng: {total:N0} đ", boldFont, Brushes.Black, colAmount, y, sfRight);
        y += 20;
        if (redeemPoints > 0)
        {
            e.Graphics.DrawString($"Giảm điểm: -{redeemPoints:N0} đ", normalFont, Brushes.Black, colAmount, y, sfRight);
            y += 20;
        }
        e.Graphics.DrawString($"THANH TOÁN: {payable:N0} đ", headerFont, Brushes.Black, colAmount, y, sfRight);
        y += 25;

        e.Graphics.DrawLine(Pens.Black, left, y, right, y);
        y += 20;

        try
        {
            Image qrImage = Image.FromFile(@"C:\Users\ASUS\source\repos\NhanVienBanHang\View\maQR.jpg"); // 👉 thay đường dẫn QR thật
            
            int qrSize = 250;
            int qrX = left + ((right - left) / 2) - (qrSize / 2);

            Font qrTitle = new Font("Segoe UI", 10, FontStyle.Bold);
            e.Graphics.DrawString("THANH TOÁN QUA VIETQR", qrTitle, Brushes.Black,
                new RectangleF(left, y, right - left, 20), sfCenter);
            y += 20;

            e.Graphics.DrawImage(qrImage, qrX, y, qrSize, qrSize);
            e.Graphics.DrawRectangle(Pens.Gray, qrX - 3, y - 3, qrSize + 6, qrSize + 6);
            y += qrSize + 15;

            Font noteFont = new Font("Segoe UI", 8, FontStyle.Italic);
            e.Graphics.DrawString("Quét mã bằng ứng dụng ngân hàng để thanh toán", noteFont, Brushes.Gray,
                new RectangleF(left, y, right - left, 15), sfCenter);
            y += 20;
        }
        catch
        {
            e.Graphics.DrawString("[Không thể tải mã QR thanh toán]", normalFont, Brushes.Black, left, y);
            y += 20;
        }

        e.Graphics.DrawLine(Pens.Black, left, y, right, y);
        y += 8;

        Font footerFont = new Font("Segoe UI", 9, FontStyle.Bold);
        e.Graphics.DrawString("Cảm ơn quý khách đã mua hàng tại ePharma!", footerFont, Brushes.Black,
            new RectangleF(left, y, right - left, 18), sfCenter);
        y += 15;

        e.Graphics.DrawString(" Địa chỉ: 1 Võ Văn Ngân, Thủ Đức", normalFont, Brushes.Gray,
            new RectangleF(left, y, right - left, 17), sfCenter);
        y += 12;

        e.Graphics.DrawString("Hẹn gặp lại quý khách!", normalFont, Brushes.Black,
            new RectangleF(left, y, right - left, 17), sfCenter);
    };

    PrintPreviewDialog preview = new PrintPreviewDialog();
    preview.Document = doc;
    preview.Width = 800;
    preview.Height = 600;
    preview.ShowDialog();
}



        private string PromptInput(string message, string title, string defaultValue)
        {
            Form prompt = new Form()
            {
                Width = 420,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterParent
            };
            Label textLabel = new Label() { Left = 10, Top = 10, Width = 380, Height = 60, Text = message };
            TextBox inputBox = new TextBox() { Left = 10, Top = 80, Width = 380, Text = defaultValue };
            Button confirmation = new Button() { Text = "OK", Left = 220, Width = 80, Top = 110, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "Hủy", Left = 310, Width = 80, Top = 110, DialogResult = DialogResult.Cancel };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.AcceptButton = confirmation;
            prompt.CancelButton = cancel;
            return prompt.ShowDialog(this) == DialogResult.OK ? inputBox.Text : string.Empty;
        }

        private decimal CalculateTotalAmount()
        {
            return orderItems.Sum(x => x.TotalPrice);
        }

        private void BtnCreateOrder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentCustomerPhone))
            {
                MessageBox.Show("Vui lòng chọn hoặc tạo khách hàng trước khi tạo đơn hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (orderItems.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một sản phẩm vào đơn hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                List<string> insufficientStockItems;
                bool stockAvailable = db.CheckStockAvailability(orderItems, out insufficientStockItems);
                
                if (!stockAvailable)
                {
                    string stockMessage = "Không đủ hàng trong kho cho các sản phẩm sau:\n\n";
                    foreach (var item in insufficientStockItems)
                    {
                        stockMessage += $"• {item}\n";
                    }
                    stockMessage += "\nVui lòng điều chỉnh số lượng hoặc liên hệ nhà cung cấp.";
                    
                    MessageBox.Show(stockMessage, "Không đủ hàng trong kho", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn tạo đơn hàng này?", 
                                                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int orderId = db.CreateOrder(currentCustomerPhone, currentEmployeeId);
                    foreach (var item in orderItems)
                    {
                        db.AddOrderDetail(orderId, item.ProductId, item.Quantity, item.UnitPrice);
                        int newStock = item.Stock - item.Quantity;
                        db.UpdateProductStock(item.ProductId, newStock);
                    }
                    decimal totalAmount = CalculateTotalAmount();
                    int pointsEarned = db.CalculatePointsFromOrder(totalAmount);
                    if (pointsEarned > 0)
                    {
                        bool pointsAdded = db.AddPointsFromOrder(currentCustomerPhone, totalAmount);
                        if (pointsAdded)
                        {
                            MessageBox.Show($"Tạo đơn hàng thành công!\n" +
                                          $"Mã đơn hàng: {orderId}\n" +
                                          $"Tổng tiền: {lblTotalValue.Text}\n" +
                                          $"Điểm tích lũy: +{pointsEarned} điểm", 
                                          "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Tạo đơn hàng thành công!\n" +
                                          $"Mã đơn hàng: {orderId}\n" +
                                          $"Tổng tiền: {lblTotalValue.Text}\n" +
                                          $"Lưu ý: Không thể cộng điểm tích lũy", 
                                          "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Tạo đơn hàng thành công!\nMã đơn hàng: {orderId}\nTổng tiền: {lblTotalValue.Text}", 
                                      "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo đơn hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (orderItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy? Tất cả thông tin đơn hàng sẽ bị mất.", 
                                                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }
        private void ResetForm()
        {
            txtCustomerPhone.Clear();
            txtCustomerName.Clear();
            currentCustomerPhone = "";
            orderItems.Clear();
            RefreshOrderItems();
            CalculateTotal();
            LoadProducts(); 
        }
        private class ProductItem
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int Stock { get; set; }
            public decimal Price { get; set; }
            public string DisplayText { get; set; }
            public override string ToString() => DisplayText;
        }
       
        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            NhanVienBanHang mainForm = new NhanVienBanHang();
            mainForm.Show();
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public class OrderItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int Stock { get; set; }
    }
}