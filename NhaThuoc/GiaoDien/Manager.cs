using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NhaThuoc.GiaoDien
{
    public partial class Manager : Form
    {
        public string tam="-1";
        public string Hovaten;
        public string Sodienthoai;
        public string Manhanvien;
        public string chucnang=null;
        public string chon;
        public string tam1, tam2, tam3,  tam5;
        public DateTime tam4;
        DatabaseConnection db = new DatabaseConnection();
        DataTable dt;
        public Manager()
        {
            InitializeComponent();
            //HienThiTaiKhoan();
        }

        public Manager(string hoten,string sdt,string manv)
        {
            InitializeComponent();
            Sodienthoai = sdt;
            Manhanvien = manv;
            Hovaten = hoten;
            //HienThiTaiKhoan();
        }

        private void Manager_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            dataGridView.Visible = true;
            txtSodienthoai.Visible=false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (chucnang=="QuanLyNhanVien")
            {
                
                if (tam == "-1")
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string s = "exec XoaTaiKhoanNhanVien @sdt;";
                SqlParameter[] parameter = new SqlParameter[]
                {
                    new SqlParameter("@sdt",tam2)
                };
                db.GiveData(s, parameter);
                MessageBox.Show("Xóa tài khoản thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HienThiTaiKhoan();
            }
        }

        public void HienThiTaiKhoan()
        {
            string s1 = "select * from Taikhoan";
            dt = db.GiveDataNoParameter(s1);
            dataGridView.DataSource = dt;
        }

        private void btnQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            //ResetTatCaControl();
            chucnang = "QuanLyNhanVien";
            txtSodienthoai.Visible= false;
            btnThem.Enabled = true;
            btnThem.Visible = true;
            btnSua.Visible = true;
            btnXoa.Visible = true;
            panel1.Visible = false;
            dataGridView.Visible = true;
            HienThiTaiKhoan();



        }

        private void btnQuanLyDoanhThu_Click(object sender, EventArgs e)
        {
            this.Hide();
            Revenue revenue = new Revenue();
            revenue.ShowDialog(); // Show dạng modal
            this.Show(); // Khi Add đóng, quay lại Manager
            HienThiTaiKhoan(); // Refresh lại danh sách
        }

        private void btnQuanLyTruyCap_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            dataGridView.Visible = true;
            if (tam == "-1")
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xem quyền truy cập", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Hide();
            Role role = new Role(tam2);
            role.ShowDialog();
            this.Show();

        }

        private void btnThongBaoNhanVien_Click(object sender, EventArgs e)
        {
            chucnang = "Thongbao";
            panel1.Visible = true;
            dataGridView.Visible = false;

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage();
            homepage.Show();
            this.Hide();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (chucnang == "QuanLyNhanVien")
            {
                this.Hide(); 
                Add add = new Add();
                add.ShowDialog();
                this.Show(); 
                HienThiTaiKhoan(); 
            }
        }

        private void btnHoantac_Click(object sender, EventArgs e)
        {
            HienThiTaiKhoan();
        }

        private void btnGui_Click(object sender, EventArgs e)
        {
            if (chucnang == null)
                return;
            if (chucnang == "Thongbao")
            {
                string chude = txtChude.Text;
                string noidung = txtThongbao.Text;
                string nguoinhan = comboBox1.Text;
                if (chude == "" || noidung == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (chon == "Cá nhân")
                    nguoinhan = txtSodienthoai.Text;
                string s = "exec Luuthongbao @chude,@noidung,@nguoinhan";
                SqlParameter[] parameter = new SqlParameter[]
                   {
                    new SqlParameter("@chude",chude)
                    ,new SqlParameter("@noidung",noidung)
                    ,new SqlParameter("@nguoinhan",nguoinhan)
                   };
                db.GiveData(s, parameter);
                MessageBox.Show("Gửi thông báo thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtThongbao_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chon = comboBox1.SelectedItem.ToString();
            if (chon == "Cá nhân")
            {
                btnThem.Visible = false;
                btnSua.Visible = false;
                btnXoa.Visible = false;
                txtSodienthoai.Visible = true;
            }
            else
            {
                btnThem.Visible = true;
                btnSua.Visible = true;
                btnXoa.Visible = true;
                txtSodienthoai.Visible = false;
            }
        }

        private void btnThem_Enter(object sender, EventArgs e) => HienLoi(btnThem, "Thongbao");
        private void btnThem_Leave(object sender, EventArgs e) => TatLoi(btnThem);

        private void btnSua_Enter(object sender, EventArgs e) => HienLoi(btnSua, "Thongbao");
        private void btnSua_Leave(object sender, EventArgs e) => TatLoi(btnSua);

        private void btnXoa_Enter(object sender, EventArgs e) => HienLoi(btnXoa, "Thongbao");
        private void btnXoa_Leave(object sender, EventArgs e) => TatLoi(btnXoa);

        private void btnGui_Enter(object sender, EventArgs e) => HienLoi(btnGui, "QuanLyNhanVien");
        private void btnGui_Leave(object sender, EventArgs e) => TatLoi(btnGui);

        private void comboBox1_Enter(object sender, EventArgs e) => HienLoi(comboBox1, "QuanLyNhanVien");
        private void comboBox1_Leave(object sender, EventArgs e) => TatLoi(comboBox1);

        
        private void panelChucnang_Paint(object sender, PaintEventArgs e)
        {

        }
        

        private void btnThem_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void btnSua_MouseLeave(object sender, EventArgs e)
        {
            
        }



        private void HienLoi(Control ctrl, string requiredFunction)
        {
            errorProvider1.Clear();
            if (chucnang == requiredFunction || chucnang == null)
            {
                // Chỉ hiển thị cảnh báo, không vô hiệu hóa nút
                errorProvider1.SetError(ctrl, "Không có đặc quyền khi thực hiện chức năng!");
            }
        }

        private void TatLoi(Control ctrl)
        {
            // Xóa cảnh báo khi rời khỏi
            errorProvider1.Clear();
        }


        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tam = e.RowIndex.ToString();
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                tam1 = row.Cells["Hovaten"].Value.ToString();
                tam2 = row.Cells["Sodienthoai"].Value.ToString();
                tam3 = row.Cells["Matkhau"].Value.ToString();
                tam4 = Convert.ToDateTime(row.Cells["Ngaysinh"].Value);
                tam5 = row.Cells["Chucvu"].Value.ToString();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (chucnang == "QuanLyNhanVien")
            {
                if (tam=="-1")
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần sửa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                this.Hide(); 
                Add add = new Add(tam1,tam2,tam3,tam4,tam5);
                add.ShowDialog(); // Show dạng modal
                this.Show(); // Khi Add đóng, quay lại Manager
                HienThiTaiKhoan(); // Refresh lại danh sách
            }
        }
    }
}
