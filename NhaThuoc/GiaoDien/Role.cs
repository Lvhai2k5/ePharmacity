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

namespace NhaThuoc.GiaoDien
{
    public partial class Role : Form
    {
        public string Sodienthoai;
        DatabaseConnection db = new DatabaseConnection();
        public Role()
        {
            InitializeComponent();
        }

        public Role(string sdt)
        {
            InitializeComponent();
            Sodienthoai = sdt;
        }

        private void btnXacnhan_Click(object sender, EventArgs e)
        {
            if (txtMa.Text!= "ePharmacyAdmin")
            {
                MessageBox.Show("Vui lòng nhập đúng mã phân quyền chính xác", "Thông báo");
                return;
            }
            int x;
            if (radioButton1.Checked==true)
            {
                x = 1;
            }
            else if (radioButton2.Checked == true)
            {
                x = 2;
            }
            else if (radioButton3.Checked == true)
            {
                x = 3;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn chức năng muốn phân quyền", "Thông báo");
                return;
            }

            if (x == 1)
            {
                string s1 = "exec SuaPhanQuyen @sdt,@pq;";
                SqlParameter[] parameter1 = new SqlParameter[]
                    {
                        new SqlParameter("@sdt", Sodienthoai),
                        new SqlParameter("@pq", x)
                    };
                db.GiveData(s1, parameter1);
                MessageBox.Show("Phân quyền truy cập thành công", "Thông báo");
            }
            else if (x == 2)
            {
                string s1 = "exec SuaPhanQuyen @sdt,@pq;";
                SqlParameter[] parameter1 = new SqlParameter[]
                    {
                        new SqlParameter("@sdt", Sodienthoai),
                        new SqlParameter("@pq", x)
                    };
                db.GiveData(s1, parameter1);
                MessageBox.Show("Phân quyền truy cập thành công", "Thông báo");
            }
            else
            {
                this.Hide();
                Add add = new Add(Sodienthoai);
                add.ShowDialog();
                this.Show();
            }

        }

        private void Role_Load(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
