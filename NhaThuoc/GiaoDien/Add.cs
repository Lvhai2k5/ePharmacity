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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace NhaThuoc.GiaoDien
{
    public partial class Add : Form
    {
        public string Hovaten;
        public string Sodienthoai;
        public string Matkhau;
        public string Chucvu;
        public DateTime Ngaysinh;
        public int th = 0;
        DatabaseConnection db = new DatabaseConnection();
        DataTable dt;
        public Add()
        {
            InitializeComponent();

        }
        public Add(string a)
        {
            th = 3;
            InitializeComponent();
            Sodienthoai = a;
            HienThiData();
            HienThiAll();
            ThietLapCanhBao();
        }


        public Add(string hovaten, string sodienthoai, string matkhau, DateTime ngaysinh, string chucvu)
        {
            InitializeComponent();
            th = 2;
            txtSodienthoai.ReadOnly = true;
            txtSodienthoai.BackColor = Color.LightGray;
            txtChucVu.ReadOnly = true;
            txtChucVu.BackColor = Color.LightGray;
            Sodienthoai = sodienthoai;
            Hovaten = hovaten;
            Matkhau = matkhau;
            Chucvu = chucvu;
            Ngaysinh = ngaysinh;
            HienThiAll();

        }
        //public Add(string hoten, string sdt, string manv)
        //{
        //    InitializeComponent();

        //    //HienThiAll();
        //}

        public void HienThiData()
        {
            string s = "select * from dbo.KiemTraThongTinNhanVien(@sdt)";
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@sdt",Sodienthoai)
            };
            dt = db.GiveData(s, parameter);
            if (dt.Rows.Count > 0)
            {
                Hovaten = dt.Rows[0]["Hovaten"].ToString();
                Matkhau = dt.Rows[0]["Matkhau"].ToString();
                Chucvu = dt.Rows[0]["Chucvu"].ToString();
                Ngaysinh = Convert.ToDateTime(dt.Rows[0]["Ngaysinh"]);
            }
        }

        public void ThietLapCanhBao()
        {
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider1.SetIconAlignment(txtHovaten, ErrorIconAlignment.MiddleRight);
            errorProvider1.SetIconAlignment(txtSodienthoai, ErrorIconAlignment.MiddleRight);
            errorProvider1.SetIconAlignment(txtMatkhau, ErrorIconAlignment.MiddleRight);
            errorProvider1.SetIconAlignment(dateTimePicker, ErrorIconAlignment.MiddleRight);

        }
        public void HienThiAll()
        {
            txtHovaten.Text = Hovaten;
            txtSodienthoai.Text = Sodienthoai;
            txtMatkhau.Text = Matkhau;
            txtChucVu.Text = Chucvu;
            dateTimePicker.Value = Ngaysinh;
        }

        private void Add_Load(object sender, EventArgs e)
        {

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
            txtHovaten.Text = "";
            txtSodienthoai.Text = "";
            txtMatkhau.Text = "";
            txtChucVu.Text = "";
            dateTimePicker.Value = DateTime.Today;
        }
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (th == 2)
            {
                string s= "exec ChinhSuaTaiKhoan @hovaten,@matkhau,@ngaysinh,@sodienthoai;";
                SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@hovaten",txtHovaten.Text),
                    new SqlParameter("@matkhau",txtMatkhau.Text),
                    new SqlParameter("@ngaysinh",dateTimePicker.Value)
                    ,new SqlParameter("@sodienthoai",txtSodienthoai.Text)
                    };
                db.GiveData(s,parameter);
                MessageBox.Show("Chỉnh sửa thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string hovaten = txtHovaten.Text;
                string sodienthoai = txtSodienthoai.Text;
                string matkhau = txtMatkhau.Text;
                string chucvu = txtChucVu.Text;
                DateTime ngaysinh = dateTimePicker.Value;

                //dk1
                if (!KiemTraDu18Tuoi(ngaysinh))
                {
                    MessageBox.Show("Ngày sinh người dùng chưa hợp lệ!!!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //dk2
                if (string.IsNullOrWhiteSpace(hovaten) || string.IsNullOrWhiteSpace(sodienthoai) || string.IsNullOrWhiteSpace(matkhau) || string.IsNullOrWhiteSpace(chucvu))
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
                dt = db.GiveData(s1, parameter1);
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
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSodienthoai_Enter(object sender, EventArgs e)
        {
            if (th == 2)
                errorProvider1.SetError(txtSodienthoai, "Không được phép thay đổi dữ liệu này!");
            if (th == 3)
                errorProvider1.SetError(txtSodienthoai, "Không được phép thay đổi dữ liệu này!");
        }

        private void txtSodienthoai_Leave(object sender, EventArgs e)
        {
            if (th==2)
            errorProvider1.SetError(txtSodienthoai, "");
            if (th ==3)
                errorProvider1.SetError(txtSodienthoai, "");
        }

        private void txtChucVu_Leave(object sender, EventArgs e)
        {
            if (th == 2)
                errorProvider1.SetError(txtChucVu, "");
            if (th == 3)
                errorProvider1.SetError(txtChucVu, "");
        }

        private void txtChucVu_Enter(object sender, EventArgs e)
        {
            if (th == 2)
                errorProvider1.SetError(txtChucVu, "Không được phép thay đổi dữ liệu này!");
            if (th == 3)
                errorProvider1.SetError(txtChucVu, "Không được phép thay đổi dữ liệu này!");
        }

        private void txtHovaten_Enter(object sender, EventArgs e)
        {
            if (th == 3)
                errorProvider1.SetError(txtHovaten, "Vui lòng nhập họ và tên");
        }

        private void txtHovaten_Leave(object sender, EventArgs e)
        {
            if (th==3)
                errorProvider1.SetError(txtHovaten, "");
        }

        private void txtMatkhau_Leave(object sender, EventArgs e)
        {
            if (th == 3)
                errorProvider1.SetError(txtMatkhau, "");
        }

        private void txtMatkhau_Enter(object sender, EventArgs e)
        {
            if (th == 3)
                errorProvider1.SetError(txtMatkhau, "Vui lòng nhập mật khẩu");
        }

        private void dateTimePicker_Leave(object sender, EventArgs e)
        {
            if (th == 3)
                errorProvider1.SetError(dateTimePicker, "");
        }

        private void dateTimePicker_Enter(object sender, EventArgs e)
        {
            if (th == 3)
                errorProvider1.SetError(dateTimePicker, "Vui lòng chọn ngày sinh");
        }
    }
}
