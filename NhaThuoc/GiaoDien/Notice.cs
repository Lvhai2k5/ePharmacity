using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NhaThuoc.GiaoDien
{
    public partial class Notice : Form
    {
        public string Sodienthoai;
        public string tam="-1";
        public string tam1, tam2;
        DatabaseConnection db = new DatabaseConnection();
        public Notice()
        {
            InitializeComponent();
        }
        public Notice(string sdt)
        {
            InitializeComponent();
            Sodienthoai = sdt;
        }


        private void Notice_Load(object sender, EventArgs e)
        {
            HienThiAll();
        }
        public void HienThiAll()
        {
            string s = "select * from dbo.Nhanthongbao(@sdt)";
            SqlParameter[] parameters = new SqlParameter[]
                        {
                new SqlParameter("@sdt", Sodienthoai)
                        };
            DataTable dt = db.GiveData(s, parameters);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chủ đề:"+tam1 + "\nNội dung:" + tam2, "Nội dung thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tam = e.RowIndex.ToString();
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                tam1 = row.Cells["Chude"].Value.ToString();
                tam2 = row.Cells["Noidung"].Value.ToString();
                
            }
        }
    }
}
