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
            this.linkLabel1Login = new System.Windows.Forms.LinkLabel();
            this.linkLabelLogin = new System.Windows.Forms.LinkLabel();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
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
            this.panelRegister.Controls.Add(this.linkLabelLogin);
            this.panelRegister.Controls.Add(this.textBox4);
            this.panelRegister.Controls.Add(this.textBox3);
            this.panelRegister.Controls.Add(this.textBox2);
            this.panelRegister.Controls.Add(this.textBox1);
            this.panelRegister.Location = new System.Drawing.Point(117, 85);
            this.panelRegister.Name = "panelRegister";
            this.panelRegister.Size = new System.Drawing.Size(997, 500);
            this.panelRegister.TabIndex = 8;
            // 
            // panelForgotpassword
            // 
            this.panelForgotpassword.BackgroundImage = global::NhaThuoc.Properties.Resources._22;
            this.panelForgotpassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelForgotpassword.Controls.Add(this.linkLabel1Login);
            this.panelForgotpassword.Location = new System.Drawing.Point(215, 170);
            this.panelForgotpassword.Name = "panelForgotpassword";
            this.panelForgotpassword.Size = new System.Drawing.Size(1000, 500);
            this.panelForgotpassword.TabIndex = 5;
            this.panelForgotpassword.Paint += new System.Windows.Forms.PaintEventHandler(this.panelForgotpassword_Paint);
            // 
            // linkLabel1Login
            // 
            this.linkLabel1Login.AutoSize = true;
            this.linkLabel1Login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(204)))));
            this.linkLabel1Login.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.linkLabel1Login.Location = new System.Drawing.Point(712, 339);
            this.linkLabel1Login.Name = "linkLabel1Login";
            this.linkLabel1Login.Size = new System.Drawing.Size(99, 23);
            this.linkLabel1Login.TabIndex = 0;
            this.linkLabel1Login.TabStop = true;
            this.linkLabel1Login.Text = "Đăng nhập";
            this.linkLabel1Login.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1Login_LinkClicked);
            // 
            // linkLabelLogin
            // 
            this.linkLabelLogin.AutoSize = true;
            this.linkLabelLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.linkLabelLogin.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.linkLabelLogin.Location = new System.Drawing.Point(187, 335);
            this.linkLabelLogin.Name = "linkLabelLogin";
            this.linkLabelLogin.Size = new System.Drawing.Size(99, 23);
            this.linkLabelLogin.TabIndex = 4;
            this.linkLabelLogin.TabStop = true;
            this.linkLabelLogin.Text = "Đăng nhập";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(653, 335);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 22);
            this.textBox4.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(653, 255);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 22);
            this.textBox3.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(653, 181);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 22);
            this.textBox2.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(653, 112);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(849, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 39);
            this.button2.TabIndex = 7;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkboxHienmatkhau
            // 
            this.checkboxHienmatkhau.AutoSize = true;
            this.checkboxHienmatkhau.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.checkboxHienmatkhau.Location = new System.Drawing.Point(363, 342);
            this.checkboxHienmatkhau.Name = "checkboxHienmatkhau";
            this.checkboxHienmatkhau.Size = new System.Drawing.Size(114, 20);
            this.checkboxHienmatkhau.TabIndex = 6;
            this.checkboxHienmatkhau.Text = "Hiện mật khẩu";
            this.checkboxHienmatkhau.UseVisualStyleBackColor = false;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(363, 230);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(291, 22);
            this.txtUsername.TabIndex = 2;
            // 
            // linkLabelRegister
            // 
            this.linkLabelRegister.AutoSize = true;
            this.linkLabelRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(182)))), ((int)(((byte)(255)))));
            this.linkLabelRegister.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.linkLabelRegister.Location = new System.Drawing.Point(810, 395);
            this.linkLabelRegister.Name = "linkLabelRegister";
            this.linkLabelRegister.Size = new System.Drawing.Size(80, 23);
            this.linkLabelRegister.TabIndex = 5;
            this.linkLabelRegister.TabStop = true;
            this.linkLabelRegister.Text = "Đăng ký";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(363, 284);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(291, 22);
            this.txtPassword.TabIndex = 3;
            // 
            // linkLabelForgotpassword
            // 
            this.linkLabelForgotpassword.AutoSize = true;
            this.linkLabelForgotpassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(218)))), ((int)(((byte)(204)))));
            this.linkLabelForgotpassword.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.linkLabelForgotpassword.Location = new System.Drawing.Point(82, 395);
            this.linkLabelForgotpassword.Name = "linkLabelForgotpassword";
            this.linkLabelForgotpassword.Size = new System.Drawing.Size(138, 23);
            this.linkLabelForgotpassword.TabIndex = 4;
            this.linkLabelForgotpassword.TabStop = true;
            this.linkLabelForgotpassword.Text = "Quên mật khẩu";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(420, 405);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(175, 51);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.UseVisualStyleBackColor = true;
            // 
            // Homepage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1000, 500);
            this.Controls.Add(this.panelLogin);
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
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.LinkLabel linkLabelLogin;
        private System.Windows.Forms.Panel panelForgotpassword;
        private System.Windows.Forms.LinkLabel linkLabel1Login;
    }
}