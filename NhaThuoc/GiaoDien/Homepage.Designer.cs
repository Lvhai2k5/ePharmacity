namespace NhaThuoc
{
    partial class Homepage
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
            this.panelLogin = new System.Windows.Forms.Panel();
            this.panelRegister = new System.Windows.Forms.Panel();
            this.panelForgotpassword = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnQuenmatkhau = new System.Windows.Forms.Button();
            this.txtKhoiphuc = new System.Windows.Forms.TextBox();
            this.linkLabel1Login = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.linkLabelLogin = new System.Windows.Forms.LinkLabel();
            this.txtChucvuRegister = new System.Windows.Forms.TextBox();
            this.txtMatkhauRegister = new System.Windows.Forms.TextBox();
            this.txtTentaikhoanRegister = new System.Windows.Forms.TextBox();
            this.txtHovatenRegister = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.checkboxHienmatkhau = new System.Windows.Forms.CheckBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.linkLabelRegister = new System.Windows.Forms.LinkLabel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.linkLabelForgotpassword = new System.Windows.Forms.LinkLabel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.panelLogin.SuspendLayout();
            this.panelRegister.SuspendLayout();
            this.panelForgotpassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLogin
            // 
            this.panelLogin.BackgroundImage = global::NhaThuoc.Properties.Resources.n1;
            this.panelLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelLogin.Controls.Add(this.panelRegister);
            this.panelLogin.Controls.Add(this.button2);
            this.panelLogin.Controls.Add(this.checkboxHienmatkhau);
            this.panelLogin.Controls.Add(this.txtUsername);
            this.panelLogin.Controls.Add(this.linkLabelRegister);
            this.panelLogin.Controls.Add(this.txtPassword);
            this.panelLogin.Controls.Add(this.linkLabelForgotpassword);
            this.panelLogin.Controls.Add(this.btnLogin);
            this.panelLogin.Location = new System.Drawing.Point(-5, 3);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(1000, 500);
            this.panelLogin.TabIndex = 6;
            this.panelLogin.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panelRegister
            // 
            this.panelRegister.BackgroundImage = global::NhaThuoc.Properties.Resources._33;
            this.panelRegister.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelRegister.Controls.Add(this.panelForgotpassword);
            this.panelRegister.Controls.Add(this.button1);
            this.panelRegister.Controls.Add(this.dateTimePicker);
            this.panelRegister.Controls.Add(this.linkLabelLogin);
            this.panelRegister.Controls.Add(this.txtChucvuRegister);
            this.panelRegister.Controls.Add(this.txtMatkhauRegister);
            this.panelRegister.Controls.Add(this.txtTentaikhoanRegister);
            this.panelRegister.Controls.Add(this.txtHovatenRegister);
            this.panelRegister.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.panelRegister.Location = new System.Drawing.Point(3, 0);
            this.panelRegister.Name = "panelRegister";
            this.panelRegister.Size = new System.Drawing.Size(997, 500);
            this.panelRegister.TabIndex = 8;
            this.panelRegister.Paint += new System.Windows.Forms.PaintEventHandler(this.panelRegister_Paint);
            // 
            // panelForgotpassword
            // 
            this.panelForgotpassword.BackgroundImage = global::NhaThuoc.Properties.Resources._22;
            this.panelForgotpassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelForgotpassword.Controls.Add(this.label1);
            this.panelForgotpassword.Controls.Add(this.btnQuenmatkhau);
            this.panelForgotpassword.Controls.Add(this.txtKhoiphuc);
            this.panelForgotpassword.Controls.Add(this.linkLabel1Login);
            this.panelForgotpassword.Location = new System.Drawing.Point(3, 0);
            this.panelForgotpassword.Name = "panelForgotpassword";
            this.panelForgotpassword.Size = new System.Drawing.Size(1000, 500);
            this.panelForgotpassword.TabIndex = 5;
            this.panelForgotpassword.Paint += new System.Windows.Forms.PaintEventHandler(this.panelForgotpassword_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(384, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Vui lòng mã nhân viên để khôi phục mật khẩu";
            // 
            // btnQuenmatkhau
            // 
            this.btnQuenmatkhau.Location = new System.Drawing.Point(188, 300);
            this.btnQuenmatkhau.Name = "btnQuenmatkhau";
            this.btnQuenmatkhau.Size = new System.Drawing.Size(134, 58);
            this.btnQuenmatkhau.TabIndex = 3;
            this.btnQuenmatkhau.Text = "Gửi yêu cầu";
            this.btnQuenmatkhau.UseVisualStyleBackColor = true;
            this.btnQuenmatkhau.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtKhoiphuc
            // 
            this.txtKhoiphuc.Location = new System.Drawing.Point(51, 227);
            this.txtKhoiphuc.Name = "txtKhoiphuc";
            this.txtKhoiphuc.Size = new System.Drawing.Size(409, 30);
            this.txtKhoiphuc.TabIndex = 1;
            this.txtKhoiphuc.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // linkLabel1Login
            // 
            this.linkLabel1Login.AutoSize = true;
            this.linkLabel1Login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(204)))));
            this.linkLabel1Login.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.linkLabel1Login.LinkColor = System.Drawing.Color.Black;
            this.linkLabel1Login.Location = new System.Drawing.Point(712, 339);
            this.linkLabel1Login.Name = "linkLabel1Login";
            this.linkLabel1Login.Size = new System.Drawing.Size(99, 23);
            this.linkLabel1Login.TabIndex = 0;
            this.linkLabel1Login.TabStop = true;
            this.linkLabel1Login.Text = "Đăng nhập";
            this.linkLabel1Login.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1Login_LinkClicked);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(589, 375);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 34);
            this.button1.TabIndex = 7;
            this.button1.Text = "Đăng ký";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(539, 312);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(253, 30);
            this.dateTimePicker.TabIndex = 6;
            // 
            // linkLabelLogin
            // 
            this.linkLabelLogin.AutoSize = true;
            this.linkLabelLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.linkLabelLogin.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.linkLabelLogin.LinkColor = System.Drawing.Color.Black;
            this.linkLabelLogin.Location = new System.Drawing.Point(187, 335);
            this.linkLabelLogin.Name = "linkLabelLogin";
            this.linkLabelLogin.Size = new System.Drawing.Size(99, 23);
            this.linkLabelLogin.TabIndex = 4;
            this.linkLabelLogin.TabStop = true;
            this.linkLabelLogin.Text = "Đăng nhập";
            // 
            // txtChucvuRegister
            // 
            this.txtChucvuRegister.Location = new System.Drawing.Point(539, 256);
            this.txtChucvuRegister.Name = "txtChucvuRegister";
            this.txtChucvuRegister.Size = new System.Drawing.Size(253, 30);
            this.txtChucvuRegister.TabIndex = 3;
            // 
            // txtMatkhauRegister
            // 
            this.txtMatkhauRegister.Location = new System.Drawing.Point(539, 192);
            this.txtMatkhauRegister.Name = "txtMatkhauRegister";
            this.txtMatkhauRegister.Size = new System.Drawing.Size(253, 30);
            this.txtMatkhauRegister.TabIndex = 2;
            // 
            // txtTentaikhoanRegister
            // 
            this.txtTentaikhoanRegister.Location = new System.Drawing.Point(539, 132);
            this.txtTentaikhoanRegister.Name = "txtTentaikhoanRegister";
            this.txtTentaikhoanRegister.Size = new System.Drawing.Size(253, 30);
            this.txtTentaikhoanRegister.TabIndex = 1;
            // 
            // txtHovatenRegister
            // 
            this.txtHovatenRegister.Location = new System.Drawing.Point(539, 71);
            this.txtHovatenRegister.Name = "txtHovatenRegister";
            this.txtHovatenRegister.Size = new System.Drawing.Size(253, 30);
            this.txtHovatenRegister.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(521, 367);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(133, 51);
            this.button2.TabIndex = 7;
            this.button2.Text = "Thoát";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkboxHienmatkhau
            // 
            this.checkboxHienmatkhau.AutoSize = true;
            this.checkboxHienmatkhau.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.checkboxHienmatkhau.Location = new System.Drawing.Point(363, 309);
            this.checkboxHienmatkhau.Name = "checkboxHienmatkhau";
            this.checkboxHienmatkhau.Size = new System.Drawing.Size(155, 27);
            this.checkboxHienmatkhau.TabIndex = 6;
            this.checkboxHienmatkhau.Text = "Hiện mật khẩu";
            this.checkboxHienmatkhau.UseVisualStyleBackColor = false;
            this.checkboxHienmatkhau.CheckedChanged += new System.EventHandler(this.checkboxHienmatkhau_CheckedChanged);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(363, 191);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(291, 30);
            this.txtUsername.TabIndex = 2;
            // 
            // linkLabelRegister
            // 
            this.linkLabelRegister.AutoSize = true;
            this.linkLabelRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.linkLabelRegister.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.linkLabelRegister.LinkColor = System.Drawing.Color.Black;
            this.linkLabelRegister.Location = new System.Drawing.Point(810, 395);
            this.linkLabelRegister.Name = "linkLabelRegister";
            this.linkLabelRegister.Size = new System.Drawing.Size(80, 23);
            this.linkLabelRegister.TabIndex = 5;
            this.linkLabelRegister.TabStop = true;
            this.linkLabelRegister.Text = "Đăng ký";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(363, 248);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(291, 30);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // linkLabelForgotpassword
            // 
            this.linkLabelForgotpassword.AutoSize = true;
            this.linkLabelForgotpassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(204)))));
            this.linkLabelForgotpassword.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.linkLabelForgotpassword.LinkColor = System.Drawing.Color.Black;
            this.linkLabelForgotpassword.Location = new System.Drawing.Point(82, 395);
            this.linkLabelForgotpassword.Name = "linkLabelForgotpassword";
            this.linkLabelForgotpassword.Size = new System.Drawing.Size(138, 23);
            this.linkLabelForgotpassword.TabIndex = 4;
            this.linkLabelForgotpassword.TabStop = true;
            this.linkLabelForgotpassword.Text = "Quên mật khẩu";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(350, 367);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(146, 51);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // Homepage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1000, 500);
            this.Controls.Add(this.panelLogin);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Homepage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.panelRegister.ResumeLayout(false);
            this.panelRegister.PerformLayout();
            this.panelForgotpassword.ResumeLayout(false);
            this.panelForgotpassword.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.LinkLabel linkLabelForgotpassword;
        private System.Windows.Forms.LinkLabel linkLabelRegister;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.CheckBox checkboxHienmatkhau;
        private System.Windows.Forms.Panel panelRegister;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtChucvuRegister;
        private System.Windows.Forms.TextBox txtMatkhauRegister;
        private System.Windows.Forms.TextBox txtTentaikhoanRegister;
        private System.Windows.Forms.TextBox txtHovatenRegister;
        private System.Windows.Forms.LinkLabel linkLabelLogin;
        private System.Windows.Forms.Panel panelForgotpassword;
        private System.Windows.Forms.LinkLabel linkLabel1Login;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnQuenmatkhau;
        private System.Windows.Forms.TextBox txtKhoiphuc;
        private System.Windows.Forms.Label label1;
    }
}