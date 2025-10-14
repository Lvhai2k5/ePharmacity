using System;
using System.Drawing;
using System.Windows.Forms;

namespace NhaThuoc
{
    public static class TextBoxHelper
    {
        /// <summary>
        /// Tạo TextBox với placeholder text
        /// </summary>
        /// <param name="placeholderText">Text hiển thị khi trống</param>
        /// <param name="location">Vị trí</param>
        /// <param name="size">Kích thước</param>
        /// <param name="font">Font chữ</param>
        /// <returns>TextBox đã được cấu hình</returns>
        public static TextBox CreatePlaceholderTextBox(string placeholderText, Point location, Size size, Font font = null)
        {
            TextBox textBox = new TextBox
            {
                Text = placeholderText,
                ForeColor = Color.Gray,
                Location = location,
                Size = size,
                Font = font ?? new Font("Segoe UI", 9)
            };

            // Xử lý sự kiện Enter (khi focus vào)
            textBox.Enter += (s, e) => {
                if (textBox.Text == placeholderText)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            // Xử lý sự kiện Leave (khi rời khỏi focus)
            textBox.Leave += (s, e) => {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholderText;
                    textBox.ForeColor = Color.Gray;
                }
            };

            return textBox;
        }

        /// <summary>
        /// Kiểm tra TextBox có chứa placeholder text không
        /// </summary>
        /// <param name="textBox">TextBox cần kiểm tra</param>
        /// <param name="placeholderText">Placeholder text</param>
        /// <returns>True nếu chứa placeholder text</returns>
        public static bool IsPlaceholderText(TextBox textBox, string placeholderText)
        {
            return textBox.Text == placeholderText;
        }

        /// <summary>
        /// Lấy giá trị thực của TextBox (loại bỏ placeholder text)
        /// </summary>
        /// <param name="textBox">TextBox cần lấy giá trị</param>
        /// <param name="placeholderText">Placeholder text</param>
        /// <returns>Giá trị thực hoặc chuỗi rỗng</returns>
        public static string GetRealValue(TextBox textBox, string placeholderText)
        {
            return textBox.Text == placeholderText ? "" : textBox.Text.Trim();
        }

        /// <summary>
        /// Kiểm tra TextBox có giá trị thực không (không phải placeholder)
        /// </summary>
        /// <param name="textBox">TextBox cần kiểm tra</param>
        /// <param name="placeholderText">Placeholder text</param>
        /// <returns>True nếu có giá trị thực</returns>
        public static bool HasRealValue(TextBox textBox, string placeholderText)
        {
            return !string.IsNullOrWhiteSpace(textBox.Text) && textBox.Text != placeholderText;
        }
    }
}

