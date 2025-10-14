using NhaThuoc.GiaoDien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NhaThuoc
{
    public partial class Homepage : Form
    {
        DataTable dt;
        DatabaseConnection db = new DatabaseConnection();
        private Timer transitionTimer;
        private int transitionStep = 0;
        private const int TRANSITION_STEPS = 20;
        private const int TRANSITION_INTERVAL = 20;
        private bool isTransitioning = false;
        private string currentPanel = "login";
        private string targetPanel = "login";
        public Homepage()
        {
            InitializeComponent();
            this.Size = new Size(1000, 500);
            DatPlaceholderHover(txtUsername, "Vui lòng nhập số điện thoại");
            DatPlaceholderMatKhau(txtPassword, "Nhập mật khẩu", checkboxHienmatkhau);
            DatPlaceholderHover(txtHovatenRegister, "Vui lòng họ và tên");
            DatPlaceholderHover(txtTentaikhoanRegister, "Vui lòng nhập số điện thoại");
            DatPlaceholderHover(txtMatkhauRegister, "Vui lòng nhập mật khẩu");
            DatPlaceholderHover(txtChucvuRegister, "Vui lòng nhập chức vụ");
            DatPlaceholderHover(txtKhoiphuc, "Vui lòng nhập mã nhân viên");
            panelLogin.Size = new Size(1000, 500);
            panelRegister.Size = new Size(1000, 500);
            panelForgotpassword.Size = new Size(1000, 500);
            SetupTransition();
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.panelRegister);
            this.Controls.Add(this.panelForgotpassword);
            panelLogin.Location = new Point(0, 0);
            panelRegister.Location = new Point(this.Width, 0);
            panelForgotpassword.Location = new Point(-this.Width, 0);
            this.linkLabelRegister.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelRegister_LinkClicked);
            this.linkLabelForgotpassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelForgotpassword_LinkClicked);
            this.linkLabelLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelLogin_LinkClicked);
            this.linkLabel1Login.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1Login_LinkClicked);
            currentPanel = "login";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }
        


        private void SetupTransition()
        {
            transitionTimer = new Timer();
            transitionTimer.Interval = TRANSITION_INTERVAL;
            transitionTimer.Tick += TransitionTimer_Tick;
        }

        private void TransitionTimer_Tick(object sender, EventArgs e)
        {
            if (isTransitioning)
            {
                transitionStep++;
                float progress = (float)transitionStep / TRANSITION_STEPS;

                // Easing function (ease-in-out)
                float easedProgress = progress < 0.5f ?
                    2 * progress * progress :
                    1 - (float)Math.Pow(-2 * progress + 2, 2) / 2;

                if (targetPanel == "register")
                {
                    this.BackColor = ColorTranslator.FromHtml("#38b6ff");
                    // Slide login panel to left, register panel from right
                    panelLogin.Location = new Point((int)(-this.Width * easedProgress), 0);
                    panelRegister.Location = new Point((int)(this.Width * (1 - easedProgress)), 0);
                    
                }
                else if (targetPanel == "login")
                {
                    if (currentPanel == "register")
                    {
                        
                        //this.BackColor = ColorTranslator.FromHtml("#38b6ff");
                        // Slide register panel to right, login panel from left
                        panelRegister.Location = new Point((int)(this.Width * easedProgress), 0);
                        panelLogin.Location = new Point((int)(-this.Width * (1 - easedProgress)), 0);
                    }
                    else if (currentPanel == "forgot")
                    {
                        //panelLogin.Paint += panel1_Paint;
                        // this.BackColor = ColorTranslator.FromHtml("#f1dacc");
                        // Slide forgot panel to left, login panel from right
                        panelForgotpassword.Location = new Point((int)(-this.Width * easedProgress), 0);
                        panelLogin.Location = new Point((int)(this.Width * (1 - easedProgress)), 0);
                    }
                }
                else if (targetPanel == "forgot")
                {
                    this.BackColor = ColorTranslator.FromHtml("#f1dacc");
                    // Slide login panel to right, forgot panel from left
                    panelLogin.Location = new Point((int)(this.Width * easedProgress), 0);
                    panelForgotpassword.Location = new Point((int)(-this.Width * (1 - easedProgress)), 0);
                }

                if (transitionStep >= TRANSITION_STEPS)
                {
                    transitionTimer.Stop();
                    isTransitioning = false;
                    transitionStep = 0;
                    currentPanel = targetPanel;

                    // Reset panel positions
                    if (currentPanel == "login")
                    {
                        panelLogin.Location = new Point(0, 0);
                        panelRegister.Location = new Point(this.Width, 0);
                        panelForgotpassword.Location = new Point(-this.Width, 0);
                    }
                    else if (currentPanel == "register")
                    {
                        panelRegister.Location = new Point(0, 0);
                        panelLogin.Location = new Point(-this.Width, 0);
                        panelForgotpassword.Location = new Point(-this.Width, 0);
                    }
                    else if (currentPanel == "forgot")
                    {
                        panelForgotpassword.Location = new Point(0, 0);
                        panelLogin.Location = new Point(this.Width, 0);
                        panelRegister.Location = new Point(this.Width, 0);
                    }
                }
            }
        }

        private void StartTransition(string newPanel)
        {
            if (!isTransitioning && currentPanel != newPanel)
            {
                targetPanel = newPanel;
                isTransitioning = true;
                transitionStep = 0;
                transitionTimer.Start();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Y <= 40 && e.X >= this.Width - 40)
            {
                this.Close();
            }
            base.OnMouseClick(e);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1Login_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StartTransition("login");
        }

        private void linkLabelLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StartTransition("login");
        }

        private void linkLabelForgotpassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StartTransition("forgot");
        }

        private void linkLabelRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StartTransition("register");
        }

        private void Homepage_Load(object sender, EventArgs e)
        {
            
            txtPassword.UseSystemPasswordChar = true;
            panelLogin.Location = new Point(0, 0);
            panelRegister.Location = new Point(this.Width, 0);
            panelForgotpassword.Location = new Point(-this.Width, 0);
        }

        private void panelForgotpassword_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            //Kiểm tra tài khoản tồn tại hay không
            string s1 ="select * from TaiKhoan where Sodienthoai=@username and Matkhau=@password";
            SqlParameter[] parameter1 = new SqlParameter[]
            {
                new SqlParameter("@username",username),
                new SqlParameter("@password",password)
            };
            dt = db.GiveData(s1,parameter1);

            //trả kết quả cho màn hình chính
             if (dt == null)
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng vui lòng kiểm tra tài khoản","Thông báo");
                return;
            }

            //Lấy thông tin nhân viên
            string hoten = dt.Rows[0]["Hovaten"].ToString();
            string sdt = dt.Rows[0]["Sodienthoai"].ToString();
            string manv = dt.Rows[0]["Manguoidung"].ToString();
            string chucvu = dt.Rows[0]["Chucvu"].ToString();

            //Kiem tra trang thai tai khoan

            //dieu huong trang thai tai khoan
            string st= "select * from dbo.Bangtruycap(@sdt);";
            SqlParameter[] parameter2 = new SqlParameter[]
            {
                new SqlParameter("@sdt",sdt)
            };
            DataTable dt1 = db.GiveData(st, parameter2);
            if (dt1.Rows.Count > 0)
            {
                string trangthai = dt1.Rows[0]["Trangthai"].ToString();
                if (trangthai == "2")
                {
                    MessageBox.Show("Tài khoản của bạn đã bị khóa, vui lòng liên hệ quản lý để biết thêm chi tiết", "Thông báo");
                    return;
                }
            }


            //Mở form theo chức vụ
            if (chucvu == "QuanLy")
            {
                Manager manager = new Manager(hoten, sdt, manv);
                manager.Show();
                this.Hide();
            }
            else
            if (chucvu == "NhanVienChamSocKhachHang")
            {
                frmCSKH frmCSKH = new frmCSKH(hoten, sdt, manv);
                frmCSKH.Show();
                this.Hide();
            }
            else
             if (chucvu == "NhanVienBanHang")
            {
                NhanVienBanHang nhanVienBanHang = new NhanVienBanHang(hoten, sdt, manv);
                nhanVienBanHang.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Chức vụ không hợp lệ", "Thông báo");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string manhanvien = txtKhoiphuc.Text;
            string matkhau = null;

            string query = "SELECT dbo.Khoiphucmatkhau(@manhanvien)";
            SqlParameter[] parameters = new SqlParameter[]
                        {
                new SqlParameter("@manhanvien", manhanvien)
                        };
            DataTable dt = db.GiveData(query, parameters);

            if (dt.Rows.Count > 0)
            {
                matkhau = dt.Rows[0][0].ToString();
                MessageBox.Show("Mật khẩu của nhân viên có mã: " + manhanvien + " là: " + matkhau, "Thông báo");
                txtKhoiphuc.Text = "";
            }
            else
            {
                MessageBox.Show("Mã nhân viên không tồn tại", "Cảnh báo");
            }

        }

        private bool KiemTraDu18Tuoi(DateTime ngaysinh)
        {
            DateTime today = DateTime.Today; // ngày hiện tại (chỉ lấy phần ngày)
            int tuoi = today.Year - ngaysinh.Year;
            if (ngaysinh.Date > today.AddYears(-tuoi))
            {
                tuoi--;
            }
            return tuoi >= 18;
        }

        public void ClearRegisterFields()
        {
            txtHovatenRegister.Text = "";
            txtTentaikhoanRegister.Text = "";
            txtMatkhauRegister.Text = "";
            txtChucvuRegister.Text = "";
            dateTimePicker.Value = DateTime.Today;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string hovaten = txtHovatenRegister.Text;
            string sodienthoai = txtTentaikhoanRegister.Text;
            string matkhau = txtMatkhauRegister.Text;
            string chucvu = txtChucvuRegister.Text;
            DateTime ngaysinh = dateTimePicker.Value;

            //dk1
            if (!KiemTraDu18Tuoi(ngaysinh))
            {
                MessageBox.Show("Ngày sinh người dùng chưa hợp lệ!!!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //dk2
            if  (string.IsNullOrWhiteSpace(hovaten) || string.IsNullOrWhiteSpace(sodienthoai) || string.IsNullOrWhiteSpace(matkhau) || string.IsNullOrWhiteSpace(chucvu))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //dk3
            string s1 = "select * from dbo.KiemTraThongTinNhanVien(@sdt)";
            SqlParameter[] parameter1 = new SqlParameter[]
            {
                new SqlParameter("@sdt",sodienthoai)
            };
            DataTable dt = db.GiveData(s1, parameter1);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Số điện thoại đã tồn tại", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Thêm nhân viên
            string s2 = "exec ThemTaiKhoan @hovaten,@sdt,@matkhau,@ngaysinh,@chucvu;";
            SqlParameter[] parameter2 = new SqlParameter[]
            {
                new SqlParameter("@hovaten",hovaten),
                new SqlParameter("@sdt",sodienthoai),
                new SqlParameter("@matkhau",matkhau),
                new SqlParameter("@ngaysinh",ngaysinh),
                new SqlParameter("@chucvu",chucvu)
            };
            db.GiveData(s2, parameter2);
            ClearRegisterFields();
            MessageBox.Show("Thêm nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkboxHienmatkhau_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !checkboxHienmatkhau.Checked;
        }

        private void panelRegister_Paint(object sender, PaintEventArgs e)
        {

        }

        private Dictionary<TextBox, bool> trangThaiNhap = new Dictionary<TextBox, bool>();

        private void DatPlaceholderHover(TextBox txt, string placeholder)
        {
            // Hiển thị placeholder ban đầu
            txt.Text = placeholder;
            txt.ForeColor = Color.DimGray;
            trangThaiNhap[txt] = false;

            // Khi focus hoặc rê chuột vào
            txt.Enter += (s, e) =>
            {
                if (!trangThaiNhap[txt] && txt.Text == placeholder)
                {
                    txt.Clear();
                    txt.ForeColor = Color.Black;
                }
            };
            txt.MouseEnter += (s, e) =>
            {
                if (!trangThaiNhap[txt] && txt.Text == placeholder)
                {
                    txt.Clear();
                    txt.ForeColor = Color.Black;
                }
            };

            // ✅ Khi người dùng bắt đầu gõ (bất kỳ ký tự nào)
            txt.KeyPress += (s, e) =>
            {
                if (!trangThaiNhap[txt] && txt.Text == placeholder)
                {
                    txt.Clear();
                    txt.ForeColor = Color.Black;
                }
            };

            // Khi mất focus hoặc rời chuột ra
            txt.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    trangThaiNhap[txt] = false;
                    txt.Text = placeholder;
                    txt.ForeColor = Color.DimGray;
                }
            };
            txt.MouseLeave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    trangThaiNhap[txt] = false;
                    txt.Text = placeholder;
                    txt.ForeColor = Color.DimGray;
                }
            };

            // Khi nội dung thay đổi (để đánh dấu là đã nhập)
            txt.TextChanged += (s, e) =>
            {
                trangThaiNhap[txt] = !string.IsNullOrWhiteSpace(txt.Text) && txt.Text != placeholder;
            };
        }
        private void DatPlaceholderMatKhau(TextBox txt, string placeholder, CheckBox chk)
        {
            bool daNhap = false;
            txt.Text = placeholder;
            txt.ForeColor = Color.DimGray;
            txt.UseSystemPasswordChar = false;

            txt.Enter += (s, e) =>
            {
                if (!daNhap && txt.Text == placeholder)
                {
                    txt.Clear();
                    txt.ForeColor = Color.Black;
                    txt.UseSystemPasswordChar = !chk.Checked;
                }
            };
            txt.KeyPress += (s, e) =>
            {
                if (!daNhap && txt.Text == placeholder)
                {
                    txt.Clear();
                    txt.ForeColor = Color.Black;
                    txt.UseSystemPasswordChar = !chk.Checked;
                }
            };
            txt.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    daNhap = false;
                    txt.UseSystemPasswordChar = false;
                    txt.Text = placeholder;
                    txt.ForeColor = Color.DimGray;
                }
            };
            chk.CheckedChanged += (s, e) =>
            {
                if (daNhap)
                {
                    txt.UseSystemPasswordChar = !chk.Checked;
                }
                else
                {
                    txt.UseSystemPasswordChar = false;
                }
            };
            txt.TextChanged += (s, e) =>
            {
                daNhap = !string.IsNullOrWhiteSpace(txt.Text) && txt.Text != placeholder;
            };
        }


    }
}
