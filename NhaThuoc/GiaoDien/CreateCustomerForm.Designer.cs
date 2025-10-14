namespace NhaThuoc
{
    partial class CreateCustomerForm
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
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.lblPoints = new System.Windows.Forms.Label();
            this.lblPointsValue = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCheckExisting = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlCustomerInfo = new System.Windows.Forms.Panel();
            this.pnlExistingCustomer = new System.Windows.Forms.Panel();
            this.btnCreateNew = new System.Windows.Forms.Button();
            this.btnUseExisting = new System.Windows.Forms.Button();
            this.lblExistingCustomerInfo = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.pnlCustomerInfo.SuspendLayout();
            this.pnlExistingCustomer.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFullName
            // 
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFullName.ForeColor = System.Drawing.Color.Gray;
            this.txtFullName.Location = new System.Drawing.Point(304, 120);
            this.txtFullName.Margin = new System.Windows.Forms.Padding(4);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(360, 34);
            this.txtFullName.TabIndex = 0;
            this.txtFullName.Text = "Nhập họ và tên khách hàng";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneNumber.ForeColor = System.Drawing.Color.Gray;
            this.txtPhoneNumber.Location = new System.Drawing.Point(304, 37);
            this.txtPhoneNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(360, 34);
            this.txtPhoneNumber.TabIndex = 0;
            this.txtPhoneNumber.Text = "Nhập số điện thoại";
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblFullName.Location = new System.Drawing.Point(307, 93);
            this.lblFullName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(106, 23);
            this.lblFullName.TabIndex = 1;
            this.lblFullName.Text = "Họ và tên (*)";
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.AutoSize = true;
            this.lblPhoneNumber.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhoneNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPhoneNumber.Location = new System.Drawing.Point(301, 10);
            this.lblPhoneNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(133, 23);
            this.lblPhoneNumber.TabIndex = 1;
            this.lblPhoneNumber.Text = "Số điện thoại (*)";
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoints.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPoints.Location = new System.Drawing.Point(390, 170);
            this.lblPoints.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(110, 23);
            this.lblPoints.TabIndex = 1;
            this.lblPoints.Text = "Điểm tích lũy";
            // 
            // lblPointsValue
            // 
            this.lblPointsValue.AutoSize = true;
            this.lblPointsValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPointsValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblPointsValue.Location = new System.Drawing.Point(501, 170);
            this.lblPointsValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPointsValue.Name = "lblPointsValue";
            this.lblPointsValue.Size = new System.Drawing.Size(20, 23);
            this.lblPointsValue.TabIndex = 1;
            this.lblPointsValue.Text = "0";
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnCreate.FlatAppearance.BorderSize = 0;
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Location = new System.Drawing.Point(13, 343);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(179, 43);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Tạo tài khoản";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // btnCheckExisting
            // 
            this.btnCheckExisting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnCheckExisting.FlatAppearance.BorderSize = 0;
            this.btnCheckExisting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckExisting.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckExisting.ForeColor = System.Drawing.Color.White;
            this.btnCheckExisting.Location = new System.Drawing.Point(400, 70);
            this.btnCheckExisting.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheckExisting.Name = "btnCheckExisting";
            this.btnCheckExisting.Size = new System.Drawing.Size(120, 35);
            this.btnCheckExisting.TabIndex = 2;
            this.btnCheckExisting.Text = "Kiểm tra";
            this.btnCheckExisting.UseVisualStyleBackColor = false;
            this.btnCheckExisting.Click += new System.EventHandler(this.BtnCheckExisting_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblTitle.Location = new System.Drawing.Point(304, 9);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(417, 37);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "TẠO TÀI KHOẢN KHÁCH HÀNG";
            // 
            // pnlCustomerInfo
            // 
            this.pnlCustomerInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCustomerInfo.Controls.Add(this.pnlExistingCustomer);
            this.pnlCustomerInfo.Controls.Add(this.btnBack);
            this.pnlCustomerInfo.Controls.Add(this.lblPointsValue);
            this.pnlCustomerInfo.Controls.Add(this.lblPoints);
            this.pnlCustomerInfo.Controls.Add(this.lblFullName);
            this.pnlCustomerInfo.Controls.Add(this.lblPhoneNumber);
            this.pnlCustomerInfo.Controls.Add(this.txtFullName);
            this.pnlCustomerInfo.Controls.Add(this.txtPhoneNumber);
            this.pnlCustomerInfo.Controls.Add(this.btnCreate);
            this.pnlCustomerInfo.Location = new System.Drawing.Point(0, 60);
            this.pnlCustomerInfo.Margin = new System.Windows.Forms.Padding(4);
            this.pnlCustomerInfo.Name = "pnlCustomerInfo";
            this.pnlCustomerInfo.Size = new System.Drawing.Size(1016, 390);
            this.pnlCustomerInfo.TabIndex = 4;
            // 
            // pnlExistingCustomer
            // 
            this.pnlExistingCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlExistingCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlExistingCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlExistingCustomer.Controls.Add(this.btnCreateNew);
            this.pnlExistingCustomer.Controls.Add(this.btnUseExisting);
            this.pnlExistingCustomer.Controls.Add(this.lblExistingCustomerInfo);
            this.pnlExistingCustomer.Location = new System.Drawing.Point(13, 10);
            this.pnlExistingCustomer.Margin = new System.Windows.Forms.Padding(4);
            this.pnlExistingCustomer.Name = "pnlExistingCustomer";
            this.pnlExistingCustomer.Size = new System.Drawing.Size(983, 299);
            this.pnlExistingCustomer.TabIndex = 5;
            this.pnlExistingCustomer.Visible = false;
            this.pnlExistingCustomer.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlExistingCustomer_Paint);
            // 
            // btnCreateNew
            // 
            this.btnCreateNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnCreateNew.FlatAppearance.BorderSize = 0;
            this.btnCreateNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateNew.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateNew.ForeColor = System.Drawing.Color.White;
            this.btnCreateNew.Location = new System.Drawing.Point(730, 247);
            this.btnCreateNew.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateNew.Name = "btnCreateNew";
            this.btnCreateNew.Size = new System.Drawing.Size(140, 35);
            this.btnCreateNew.TabIndex = 2;
            this.btnCreateNew.Text = "Tạo mới";
            this.btnCreateNew.UseVisualStyleBackColor = false;
            this.btnCreateNew.Click += new System.EventHandler(this.BtnCreateNew_Click);
            // 
            // btnUseExisting
            // 
            this.btnUseExisting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnUseExisting.FlatAppearance.BorderSize = 0;
            this.btnUseExisting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUseExisting.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUseExisting.ForeColor = System.Drawing.Color.White;
            this.btnUseExisting.Location = new System.Drawing.Point(510, 247);
            this.btnUseExisting.Margin = new System.Windows.Forms.Padding(4);
            this.btnUseExisting.Name = "btnUseExisting";
            this.btnUseExisting.Size = new System.Drawing.Size(140, 35);
            this.btnUseExisting.TabIndex = 2;
            this.btnUseExisting.Text = "Sử dụng";
            this.btnUseExisting.UseVisualStyleBackColor = false;
            this.btnUseExisting.Click += new System.EventHandler(this.BtnUseExisting_Click);
            // 
            // lblExistingCustomerInfo
            // 
            this.lblExistingCustomerInfo.AutoSize = true;
            this.lblExistingCustomerInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExistingCustomerInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblExistingCustomerInfo.Location = new System.Drawing.Point(20, 20);
            this.lblExistingCustomerInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExistingCustomerInfo.Name = "lblExistingCustomerInfo";
            this.lblExistingCustomerInfo.Size = new System.Drawing.Size(207, 28);
            this.lblExistingCustomerInfo.TabIndex = 0;
            this.lblExistingCustomerInfo.Text = "Khách hàng đã tồn tại:";
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(869, 343);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(127, 43);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "← Quay lại";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // CreateCustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1014, 463);
            this.Controls.Add(this.pnlCustomerInfo);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnCheckExisting);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CreateCustomerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TẠO TÀI KHOẢN KHÁCH HÀNG";
            this.Load += new System.EventHandler(this.CreateCustomerForm_Load);
            this.pnlCustomerInfo.ResumeLayout(false);
            this.pnlCustomerInfo.PerformLayout();
            this.pnlExistingCustomer.ResumeLayout(false);
            this.pnlExistingCustomer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.Label lblPointsValue;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCheckExisting;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlCustomerInfo;
        private System.Windows.Forms.Panel pnlExistingCustomer;
        private System.Windows.Forms.Button btnCreateNew;
        private System.Windows.Forms.Button btnUseExisting;
        private System.Windows.Forms.Label lblExistingCustomerInfo;
        private System.Windows.Forms.Button btnBack;
    }
}