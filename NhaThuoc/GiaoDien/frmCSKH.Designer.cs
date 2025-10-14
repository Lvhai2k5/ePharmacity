namespace NhaThuoc
{
    partial class frmCSKH
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.Exitbtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnPhanHoiKH = new System.Windows.Forms.Button();
            this.btnLSDH = new System.Windows.Forms.Button();
            this.btnInfoKH = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel_phu = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.Exitbtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 46);
            this.panel1.TabIndex = 12;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(12, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(217, 28);
            this.label6.TabIndex = 8;
            this.label6.Text = "Chăm sóc khách hàng";
            // 
            // Exitbtn
            // 
            this.Exitbtn.BackColor = System.Drawing.Color.Red;
            this.Exitbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exitbtn.Location = new System.Drawing.Point(960, 12);
            this.Exitbtn.Name = "Exitbtn";
            this.Exitbtn.Size = new System.Drawing.Size(28, 24);
            this.Exitbtn.TabIndex = 0;
            this.Exitbtn.Text = "x";
            this.Exitbtn.UseVisualStyleBackColor = false;
            this.Exitbtn.Click += new System.EventHandler(this.Exitbtn_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.btnPhanHoiKH);
            this.panel2.Controls.Add(this.btnLSDH);
            this.panel2.Controls.Add(this.btnInfoKH);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(260, 454);
            this.panel2.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.button1.Location = new System.Drawing.Point(33, 280);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(196, 41);
            this.button1.TabIndex = 3;
            this.button1.Text = "Thông báo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnPhanHoiKH
            // 
            this.btnPhanHoiKH.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnPhanHoiKH.Location = new System.Drawing.Point(33, 215);
            this.btnPhanHoiKH.Name = "btnPhanHoiKH";
            this.btnPhanHoiKH.Size = new System.Drawing.Size(196, 41);
            this.btnPhanHoiKH.TabIndex = 2;
            this.btnPhanHoiKH.Text = "Phản hồi KH";
            this.btnPhanHoiKH.UseVisualStyleBackColor = true;
            this.btnPhanHoiKH.Click += new System.EventHandler(this.btnPhanHoiKH_Click);
            // 
            // btnLSDH
            // 
            this.btnLSDH.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnLSDH.Location = new System.Drawing.Point(33, 136);
            this.btnLSDH.Name = "btnLSDH";
            this.btnLSDH.Size = new System.Drawing.Size(196, 41);
            this.btnLSDH.TabIndex = 1;
            this.btnLSDH.Text = "Lịch sử đơn hàng";
            this.btnLSDH.UseVisualStyleBackColor = true;
            this.btnLSDH.Click += new System.EventHandler(this.btnLSDH_Click);
            // 
            // btnInfoKH
            // 
            this.btnInfoKH.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnInfoKH.Location = new System.Drawing.Point(33, 59);
            this.btnInfoKH.Name = "btnInfoKH";
            this.btnInfoKH.Size = new System.Drawing.Size(196, 41);
            this.btnInfoKH.TabIndex = 0;
            this.btnInfoKH.Text = "Thông tin KH";
            this.btnInfoKH.UseVisualStyleBackColor = true;
            this.btnInfoKH.Click += new System.EventHandler(this.btnInfoKH_Click);
            // 
            // panel_phu
            // 
            this.panel_phu.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_phu.Location = new System.Drawing.Point(260, 46);
            this.panel_phu.Name = "panel_phu";
            this.panel_phu.Size = new System.Drawing.Size(740, 454);
            this.panel_phu.TabIndex = 14;
            this.panel_phu.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_phu_Paint);
            // 
            // frmCSKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 500);
            this.Controls.Add(this.panel_phu);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCSKH";
            this.Text = "frmEmployees";
            this.Load += new System.EventHandler(this.frmCSKH_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Exitbtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnInfoKH;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnPhanHoiKH;
        private System.Windows.Forms.Button btnLSDH;
        private System.Windows.Forms.Panel panel_phu;
        private System.Windows.Forms.Button button1;
    }
}