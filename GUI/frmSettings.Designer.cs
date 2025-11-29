namespace BTL_LTTQ
{
    partial class frmSettings
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

            if (disposing)
            {
                // Stop and dispose the timer
                if (_hideScrollbarTimer != null)
                {
                    _hideScrollbarTimer.Stop();
                    _hideScrollbarTimer.Dispose();
                    _hideScrollbarTimer = null;
                }
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelPasswordSection = new System.Windows.Forms.Panel();
            this.lblPasswordSectionTitle = new System.Windows.Forms.Label();
            this.btnCancelPassword = new System.Windows.Forms.Button();
            this.btnSavePassword = new System.Windows.Forms.Button();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelProfileSection = new System.Windows.Forms.Panel();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.btnUploadAvatar = new System.Windows.Forms.Button();
            this.btnSaveProfile = new System.Windows.Forms.Button();
            this.txtHireDate = new System.Windows.Forms.TextBox();
            this.lblHireDate = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblProfileSectionTitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelPasswordSection.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelProfileSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(72)))));
            this.panelMain.Controls.Add(this.panelContent);
            this.panelMain.Controls.Add(this.panelHeader);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(24);
            this.panelMain.Size = new System.Drawing.Size(1180, 900);
            this.panelMain.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.panelRight);
            this.panelContent.Controls.Add(this.panelLeft);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(24, 114);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1132, 762);
            this.panelContent.TabIndex = 1;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.panelPasswordSection);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(580, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.panelRight.Size = new System.Drawing.Size(552, 762);
            this.panelRight.TabIndex = 1;
            // 
            // panelPasswordSection
            // 
            this.panelPasswordSection.AutoScroll = true;
            this.panelPasswordSection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(56)))), ((int)(((byte)(82)))));
            this.panelPasswordSection.Controls.Add(this.lblPasswordSectionTitle);
            this.panelPasswordSection.Controls.Add(this.btnCancelPassword);
            this.panelPasswordSection.Controls.Add(this.btnSavePassword);
            this.panelPasswordSection.Controls.Add(this.txtConfirmPassword);
            this.panelPasswordSection.Controls.Add(this.lblConfirmPassword);
            this.panelPasswordSection.Controls.Add(this.txtNewPassword);
            this.panelPasswordSection.Controls.Add(this.lblNewPassword);
            this.panelPasswordSection.Controls.Add(this.txtOldPassword);
            this.panelPasswordSection.Controls.Add(this.lblOldPassword);
            this.panelPasswordSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPasswordSection.Location = new System.Drawing.Point(8, 0);
            this.panelPasswordSection.Name = "panelPasswordSection";
            this.panelPasswordSection.Padding = new System.Windows.Forms.Padding(24);
            this.panelPasswordSection.Size = new System.Drawing.Size(544, 762);
            this.panelPasswordSection.TabIndex = 0;
            // 
            // lblPasswordSectionTitle
            // 
            this.lblPasswordSectionTitle.AutoSize = true;
            this.lblPasswordSectionTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPasswordSectionTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblPasswordSectionTitle.ForeColor = System.Drawing.Color.White;
            this.lblPasswordSectionTitle.Location = new System.Drawing.Point(24, 24);
            this.lblPasswordSectionTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.lblPasswordSectionTitle.Name = "lblPasswordSectionTitle";
            this.lblPasswordSectionTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.lblPasswordSectionTitle.Size = new System.Drawing.Size(140, 48);
            this.lblPasswordSectionTitle.TabIndex = 8;
            this.lblPasswordSectionTitle.Text = "Đổi mật khẩu";
            // 
            // btnCancelPassword
            // 
            this.btnCancelPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.btnCancelPassword.FlatAppearance.BorderSize = 0;
            this.btnCancelPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelPassword.ForeColor = System.Drawing.Color.White;
            this.btnCancelPassword.Location = new System.Drawing.Point(190, 330);
            this.btnCancelPassword.Name = "btnCancelPassword";
            this.btnCancelPassword.Size = new System.Drawing.Size(140, 40);
            this.btnCancelPassword.TabIndex = 4;
            this.btnCancelPassword.Text = "Hủy";
            this.btnCancelPassword.UseVisualStyleBackColor = false;
            this.btnCancelPassword.Click += new System.EventHandler(this.btnCancelPassword_Click);
            // 
            // btnSavePassword
            // 
            this.btnSavePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(106)))), ((int)(((byte)(148)))));
            this.btnSavePassword.FlatAppearance.BorderSize = 0;
            this.btnSavePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSavePassword.ForeColor = System.Drawing.Color.White;
            this.btnSavePassword.Location = new System.Drawing.Point(30, 330);
            this.btnSavePassword.Name = "btnSavePassword";
            this.btnSavePassword.Size = new System.Drawing.Size(145, 40);
            this.btnSavePassword.TabIndex = 3;
            this.btnSavePassword.Text = "Đổi mật khẩu";
            this.btnSavePassword.UseVisualStyleBackColor = false;
            this.btnSavePassword.Click += new System.EventHandler(this.btnSavePassword_Click);
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfirmPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.txtConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtConfirmPassword.ForeColor = System.Drawing.Color.White;
            this.txtConfirmPassword.Location = new System.Drawing.Point(30, 270);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '●';
            this.txtConfirmPassword.Size = new System.Drawing.Size(484, 27);
            this.txtConfirmPassword.TabIndex = 2;
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblConfirmPassword.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblConfirmPassword.Location = new System.Drawing.Point(30, 240);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(165, 19);
            this.lblConfirmPassword.TabIndex = 5;
            this.lblConfirmPassword.Text = "Xác nhận mật khẩu mới";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.txtNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNewPassword.ForeColor = System.Drawing.Color.White;
            this.txtNewPassword.Location = new System.Drawing.Point(30, 185);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '●';
            this.txtNewPassword.Size = new System.Drawing.Size(484, 27);
            this.txtNewPassword.TabIndex = 1;
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNewPassword.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblNewPassword.Location = new System.Drawing.Point(30, 155);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(101, 19);
            this.lblNewPassword.TabIndex = 3;
            this.lblNewPassword.Text = "Mật khẩu mới";
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOldPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.txtOldPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOldPassword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtOldPassword.ForeColor = System.Drawing.Color.White;
            this.txtOldPassword.Location = new System.Drawing.Point(30, 105);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.PasswordChar = '●';
            this.txtOldPassword.Size = new System.Drawing.Size(484, 27);
            this.txtOldPassword.TabIndex = 0;
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblOldPassword.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblOldPassword.Location = new System.Drawing.Point(30, 75);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(90, 19);
            this.lblOldPassword.TabIndex = 0;
            this.lblOldPassword.Text = "Mật khẩu cũ";
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.panelProfileSection);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.panelLeft.Size = new System.Drawing.Size(580, 762);
            this.panelLeft.TabIndex = 0;
            // 
            // panelProfileSection
            // 
            this.panelProfileSection.AutoScroll = true;
            this.panelProfileSection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(56)))), ((int)(((byte)(82)))));
            this.panelProfileSection.Controls.Add(this.picAvatar);
            this.panelProfileSection.Controls.Add(this.btnUploadAvatar);
            this.panelProfileSection.Controls.Add(this.btnSaveProfile);
            this.panelProfileSection.Controls.Add(this.txtHireDate);
            this.panelProfileSection.Controls.Add(this.lblHireDate);
            this.panelProfileSection.Controls.Add(this.txtAddress);
            this.panelProfileSection.Controls.Add(this.lblAddress);
            this.panelProfileSection.Controls.Add(this.txtEmail);
            this.panelProfileSection.Controls.Add(this.lblEmail);
            this.panelProfileSection.Controls.Add(this.txtPhone);
            this.panelProfileSection.Controls.Add(this.lblPhone);
            this.panelProfileSection.Controls.Add(this.txtFullName);
            this.panelProfileSection.Controls.Add(this.lblFullName);
            this.panelProfileSection.Controls.Add(this.lblProfileSectionTitle);
            this.panelProfileSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProfileSection.Location = new System.Drawing.Point(0, 0);
            this.panelProfileSection.Name = "panelProfileSection";
            this.panelProfileSection.Padding = new System.Windows.Forms.Padding(24);
            this.panelProfileSection.Size = new System.Drawing.Size(572, 762);
            this.panelProfileSection.TabIndex = 0;
            // 
            // picAvatar
            // 
            this.picAvatar.BackColor = System.Drawing.Color.Gray;
            this.picAvatar.Location = new System.Drawing.Point(30, 62);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(90, 90);
            this.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAvatar.TabIndex = 11;
            this.picAvatar.TabStop = false;
            // 
            // btnUploadAvatar
            // 
            this.btnUploadAvatar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(106)))), ((int)(((byte)(148)))));
            this.btnUploadAvatar.FlatAppearance.BorderSize = 0;
            this.btnUploadAvatar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadAvatar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnUploadAvatar.ForeColor = System.Drawing.Color.White;
            this.btnUploadAvatar.Location = new System.Drawing.Point(130, 124);
            this.btnUploadAvatar.Name = "btnUploadAvatar";
            this.btnUploadAvatar.Size = new System.Drawing.Size(95, 28);
            this.btnUploadAvatar.TabIndex = 12;
            this.btnUploadAvatar.Text = "Tải ảnh";
            this.btnUploadAvatar.UseVisualStyleBackColor = false;
            this.btnUploadAvatar.Click += new System.EventHandler(this.btnUploadAvatar_Click);
            // 
            // btnSaveProfile
            // 
            this.btnSaveProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(106)))), ((int)(((byte)(148)))));
            this.btnSaveProfile.FlatAppearance.BorderSize = 0;
            this.btnSaveProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveProfile.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnSaveProfile.ForeColor = System.Drawing.Color.White;
            this.btnSaveProfile.Location = new System.Drawing.Point(30, 555);
            this.btnSaveProfile.Name = "btnSaveProfile";
            this.btnSaveProfile.Size = new System.Drawing.Size(180, 40);
            this.btnSaveProfile.TabIndex = 5;
            this.btnSaveProfile.Text = "Lưu thông tin";
            this.btnSaveProfile.UseVisualStyleBackColor = false;
            this.btnSaveProfile.Click += new System.EventHandler(this.btnSaveProfile_Click);
            // 
            // txtHireDate
            // 
            this.txtHireDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHireDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.txtHireDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHireDate.Enabled = false;
            this.txtHireDate.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtHireDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.txtHireDate.Location = new System.Drawing.Point(30, 503);
            this.txtHireDate.Name = "txtHireDate";
            this.txtHireDate.ReadOnly = true;
            this.txtHireDate.Size = new System.Drawing.Size(512, 27);
            this.txtHireDate.TabIndex = 13;
            // 
            // lblHireDate
            // 
            this.lblHireDate.Location = new System.Drawing.Point(0, 0);
            this.lblHireDate.Name = "lblHireDate";
            this.lblHireDate.Size = new System.Drawing.Size(100, 23);
            this.lblHireDate.TabIndex = 14;
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtAddress.ForeColor = System.Drawing.Color.White;
            this.txtAddress.Location = new System.Drawing.Point(30, 417);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(512, 45);
            this.txtAddress.TabIndex = 4;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAddress.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblAddress.Location = new System.Drawing.Point(30, 391);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(54, 19);
            this.lblAddress.TabIndex = 6;
            this.lblAddress.Text = "Địa chỉ";
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtEmail.ForeColor = System.Drawing.Color.White;
            this.txtEmail.Location = new System.Drawing.Point(30, 344);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(512, 27);
            this.txtEmail.TabIndex = 3;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblEmail.Location = new System.Drawing.Point(30, 318);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(45, 19);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email";
            // 
            // txtPhone
            // 
            this.txtPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPhone.ForeColor = System.Drawing.Color.White;
            this.txtPhone.Location = new System.Drawing.Point(30, 271);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(512, 27);
            this.txtPhone.TabIndex = 2;
            this.txtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhone_KeyPress);
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPhone.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblPhone.Location = new System.Drawing.Point(30, 245);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(97, 19);
            this.lblPhone.TabIndex = 2;
            this.lblPhone.Text = "Số điện thoại";
            // 
            // txtFullName
            // 
            this.txtFullName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFullName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.txtFullName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtFullName.ForeColor = System.Drawing.Color.White;
            this.txtFullName.Location = new System.Drawing.Point(30, 198);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(512, 27);
            this.txtFullName.TabIndex = 1;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFullName.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblFullName.Location = new System.Drawing.Point(30, 172);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(54, 19);
            this.lblFullName.TabIndex = 0;
            this.lblFullName.Text = "Họ tên";
            // 
            // lblProfileSectionTitle
            // 
            this.lblProfileSectionTitle.AutoSize = true;
            this.lblProfileSectionTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProfileSectionTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblProfileSectionTitle.ForeColor = System.Drawing.Color.White;
            this.lblProfileSectionTitle.Location = new System.Drawing.Point(24, 24);
            this.lblProfileSectionTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.lblProfileSectionTitle.Name = "lblProfileSectionTitle";
            this.lblProfileSectionTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.lblProfileSectionTitle.Size = new System.Drawing.Size(184, 48);
            this.lblProfileSectionTitle.TabIndex = 0;
            this.lblProfileSectionTitle.Text = "Thông tin cá nhân";
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.btnLogout);
            this.panelHeader.Controls.Add(this.lblSubtitle);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(24, 24);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1132, 90);
            this.panelHeader.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(992, 25);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(140, 40);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "🚪 Đăng xuất";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblSubtitle.Location = new System.Drawing.Point(4, 56);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(365, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Quản lý thông tin cá nhân và thay đổi mật khẩu tài khoản";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(117, 41);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Cài đặt";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1180, 900);
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cài đặt";
            this.panelMain.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panelPasswordSection.ResumeLayout(false);
            this.panelPasswordSection.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelProfileSection.ResumeLayout(false);
            this.panelProfileSection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelProfileSection;
        private System.Windows.Forms.Label lblProfileSectionTitle;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtHireDate;
        private System.Windows.Forms.Label lblHireDate;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Button btnUploadAvatar;
        private System.Windows.Forms.Button btnSaveProfile;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelPasswordSection;
        private System.Windows.Forms.Label lblPasswordSectionTitle;
        private System.Windows.Forms.Button btnCancelPassword;
        private System.Windows.Forms.Button btnSavePassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.TextBox txtOldPassword;
        private System.Windows.Forms.Label lblOldPassword;
    }
}
