namespace BTL_LTTQ
{
    partial class frmSanpham
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelRoot = new System.Windows.Forms.Panel();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.cmbFilterStatus = new System.Windows.Forms.ComboBox();
            this.lblFilterStatus = new System.Windows.Forms.Label();
            this.cmbFilterPriceType = new System.Windows.Forms.ComboBox();
            this.lblFilterPrice = new System.Windows.Forms.Label();
            this.cmbFilterLoai = new System.Windows.Forms.ComboBox();
            this.lblFilterLoai = new System.Windows.Forms.Label();
            this.cmbFilterSize = new System.Windows.Forms.ComboBox();
            this.lblFilterSize = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panelProductInfo = new System.Windows.Forms.Panel();
            this.picProductImage = new System.Windows.Forms.PictureBox();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.lblImage = new System.Windows.Forms.Label();
            this.btnUploadImage = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.cmbColor = new System.Windows.Forms.ComboBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.cmbSize = new System.Windows.Forms.ComboBox();
            this.lblSize = new System.Windows.Forms.Label();
            this.cmbLoai = new System.Windows.Forms.ComboBox();
            this.lblLoai = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtSellingPrice = new System.Windows.Forms.TextBox();
            this.lblSellingPrice = new System.Windows.Forms.Label();
            this.txtImportPrice = new System.Windows.Forms.TextBox();
            this.lblImportPrice = new System.Windows.Forms.Label();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.lblProductCode = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.lblProductName = new System.Windows.Forms.Label();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelRoot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.grpFilter.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelProductInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panelRoot
            // 
            this.panelRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(72)))));
            this.panelRoot.Controls.Add(this.dgvProducts);
            this.panelRoot.Controls.Add(this.grpFilter);
            this.panelRoot.Controls.Add(this.panelButtons);
            this.panelRoot.Controls.Add(this.panelProductInfo);
            this.panelRoot.Controls.Add(this.lblTitle);
            this.panelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRoot.Location = new System.Drawing.Point(0, 0);
            this.panelRoot.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelRoot.Name = "panelRoot";
            this.panelRoot.Padding = new System.Windows.Forms.Padding(24, 25, 24, 25);
            this.panelRoot.Size = new System.Drawing.Size(1312, 720);
            this.panelRoot.TabIndex = 0;
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AllowUserToResizeRows = false;
            this.dgvProducts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProducts.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.dgvProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.Location = new System.Drawing.Point(12, 508);
            this.dgvProducts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvProducts.MultiSelect = false;
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.RowHeadersWidth = 51;
            this.dgvProducts.RowTemplate.Height = 28;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(1070, 187);
            this.dgvProducts.TabIndex = 4;
            this.dgvProducts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProducts_CellClick);
            // 
            // grpFilter
            // 
            this.grpFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFilter.BackColor = System.Drawing.Color.Transparent;
            this.grpFilter.Controls.Add(this.cmbFilterStatus);
            this.grpFilter.Controls.Add(this.lblFilterStatus);
            this.grpFilter.Controls.Add(this.cmbFilterPriceType);
            this.grpFilter.Controls.Add(this.lblFilterPrice);
            this.grpFilter.Controls.Add(this.cmbFilterLoai);
            this.grpFilter.Controls.Add(this.lblFilterLoai);
            this.grpFilter.Controls.Add(this.cmbFilterSize);
            this.grpFilter.Controls.Add(this.lblFilterSize);
            this.grpFilter.Controls.Add(this.txtSearch);
            this.grpFilter.Controls.Add(this.lblSearch);
            this.grpFilter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFilter.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpFilter.Location = new System.Drawing.Point(12, 390);
            this.grpFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Padding = new System.Windows.Forms.Padding(16, 14, 16, 14);
            this.grpFilter.Size = new System.Drawing.Size(1070, 102);
            this.grpFilter.TabIndex = 3;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Lọc sản phẩm";
            // 
            // cmbFilterStatus
            // 
            this.cmbFilterStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.cmbFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFilterStatus.ForeColor = System.Drawing.Color.White;
            this.cmbFilterStatus.FormattingEnabled = true;
            this.cmbFilterStatus.Location = new System.Drawing.Point(737, 52);
            this.cmbFilterStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbFilterStatus.Name = "cmbFilterStatus";
            this.cmbFilterStatus.Size = new System.Drawing.Size(150, 28);
            this.cmbFilterStatus.TabIndex = 10;
            // 
            // lblFilterStatus
            // 
            this.lblFilterStatus.AutoSize = true;
            this.lblFilterStatus.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblFilterStatus.Location = new System.Drawing.Point(742, 32);
            this.lblFilterStatus.Name = "lblFilterStatus";
            this.lblFilterStatus.Size = new System.Drawing.Size(78, 20);
            this.lblFilterStatus.TabIndex = 9;
            this.lblFilterStatus.Text = "Trạng thái:";
            // 
            // cmbFilterPriceType
            // 
            this.cmbFilterPriceType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.cmbFilterPriceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterPriceType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFilterPriceType.ForeColor = System.Drawing.Color.White;
            this.cmbFilterPriceType.FormattingEnabled = true;
            this.cmbFilterPriceType.Items.AddRange(new object[] {
            "",
            "Giá tăng dần",
            "Giá giảm dần"});
            this.cmbFilterPriceType.Location = new System.Drawing.Point(598, 52);
            this.cmbFilterPriceType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbFilterPriceType.Name = "cmbFilterPriceType";
            this.cmbFilterPriceType.Size = new System.Drawing.Size(120, 28);
            this.cmbFilterPriceType.TabIndex = 7;
            this.cmbFilterPriceType.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // lblFilterPrice
            // 
            this.lblFilterPrice.AutoSize = true;
            this.lblFilterPrice.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblFilterPrice.Location = new System.Drawing.Point(598, 32);
            this.lblFilterPrice.Name = "lblFilterPrice";
            this.lblFilterPrice.Size = new System.Drawing.Size(65, 20);
            this.lblFilterPrice.TabIndex = 6;
            this.lblFilterPrice.Text = "Sắp xếp:";
            // 
            // cmbFilterLoai
            // 
            this.cmbFilterLoai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.cmbFilterLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterLoai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFilterLoai.ForeColor = System.Drawing.Color.White;
            this.cmbFilterLoai.FormattingEnabled = true;
            this.cmbFilterLoai.Location = new System.Drawing.Point(438, 52);
            this.cmbFilterLoai.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbFilterLoai.Name = "cmbFilterLoai";
            this.cmbFilterLoai.Size = new System.Drawing.Size(150, 28);
            this.cmbFilterLoai.TabIndex = 5;
            this.cmbFilterLoai.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // lblFilterLoai
            // 
            this.lblFilterLoai.AutoSize = true;
            this.lblFilterLoai.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblFilterLoai.Location = new System.Drawing.Point(438, 32);
            this.lblFilterLoai.Name = "lblFilterLoai";
            this.lblFilterLoai.Size = new System.Drawing.Size(40, 20);
            this.lblFilterLoai.TabIndex = 4;
            this.lblFilterLoai.Text = "Loại:";
            // 
            // cmbFilterSize
            // 
            this.cmbFilterSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.cmbFilterSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFilterSize.ForeColor = System.Drawing.Color.White;
            this.cmbFilterSize.FormattingEnabled = true;
            this.cmbFilterSize.Location = new System.Drawing.Point(292, 52);
            this.cmbFilterSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbFilterSize.Name = "cmbFilterSize";
            this.cmbFilterSize.Size = new System.Drawing.Size(130, 28);
            this.cmbFilterSize.TabIndex = 3;
            this.cmbFilterSize.SelectedIndexChanged += new System.EventHandler(this.Filter_Changed);
            // 
            // lblFilterSize
            // 
            this.lblFilterSize.AutoSize = true;
            this.lblFilterSize.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblFilterSize.Location = new System.Drawing.Point(292, 32);
            this.lblFilterSize.Name = "lblFilterSize";
            this.lblFilterSize.Size = new System.Drawing.Size(39, 20);
            this.lblFilterSize.TabIndex = 2;
            this.lblFilterSize.Text = "Size:";
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.ForeColor = System.Drawing.Color.White;
            this.txtSearch.Location = new System.Drawing.Point(19, 52);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(250, 27);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblSearch.Location = new System.Drawing.Point(19, 30);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(141, 20);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Tìm kiếm sản phẩm:";
            // 
            // panelButtons
            // 
            this.panelButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelButtons.BackColor = System.Drawing.Color.Transparent;
            this.panelButtons.Controls.Add(this.btnExport);
            this.panelButtons.Controls.Add(this.btnRefresh);
            this.panelButtons.Controls.Add(this.btnDelete);
            this.panelButtons.Controls.Add(this.btnEdit);
            this.panelButtons.Controls.Add(this.btnAdd);
            this.panelButtons.Location = new System.Drawing.Point(1088, 86);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.panelButtons.Size = new System.Drawing.Size(200, 609);
            this.panelButtons.TabIndex = 2;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(72)))), ((int)(((byte)(98)))));
            this.btnExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(67)))), ((int)(((byte)(95)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(11, 260);
            this.btnExport.Margin = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(179, 46);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "Xuất File";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(72)))), ((int)(((byte)(98)))));
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(67)))), ((int)(((byte)(95)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(11, 207);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(179, 46);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(90)))), ((int)(((byte)(79)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(110)))), ((int)(((byte)(99)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(11, 154);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(179, 46);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(72)))), ((int)(((byte)(98)))));
            this.btnEdit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(67)))), ((int)(((byte)(95)))));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(11, 101);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(179, 46);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(72)))), ((int)(((byte)(98)))));
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(67)))), ((int)(((byte)(95)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(11, 48);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(179, 46);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panelProductInfo
            // 
            this.panelProductInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelProductInfo.BackColor = System.Drawing.Color.Transparent;
            this.panelProductInfo.Controls.Add(this.picProductImage);
            this.panelProductInfo.Controls.Add(this.txtImagePath);
            this.panelProductInfo.Controls.Add(this.lblImage);
            this.panelProductInfo.Controls.Add(this.btnUploadImage);
            this.panelProductInfo.Controls.Add(this.txtDescription);
            this.panelProductInfo.Controls.Add(this.lblDescription);
            this.panelProductInfo.Controls.Add(this.cmbColor);
            this.panelProductInfo.Controls.Add(this.lblColor);
            this.panelProductInfo.Controls.Add(this.cmbSize);
            this.panelProductInfo.Controls.Add(this.lblSize);
            this.panelProductInfo.Controls.Add(this.cmbLoai);
            this.panelProductInfo.Controls.Add(this.lblLoai);
            this.panelProductInfo.Controls.Add(this.txtQuantity);
            this.panelProductInfo.Controls.Add(this.lblQuantity);
            this.panelProductInfo.Controls.Add(this.txtSellingPrice);
            this.panelProductInfo.Controls.Add(this.lblSellingPrice);
            this.panelProductInfo.Controls.Add(this.txtImportPrice);
            this.panelProductInfo.Controls.Add(this.lblImportPrice);
            this.panelProductInfo.Controls.Add(this.txtProductCode);
            this.panelProductInfo.Controls.Add(this.lblProductCode);
            this.panelProductInfo.Controls.Add(this.txtProductName);
            this.panelProductInfo.Controls.Add(this.lblProductName);
            this.panelProductInfo.Controls.Add(this.cmbProduct);
            this.panelProductInfo.Controls.Add(this.lblProduct);
            this.panelProductInfo.Location = new System.Drawing.Point(24, 86);
            this.panelProductInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelProductInfo.Name = "panelProductInfo";
            this.panelProductInfo.Padding = new System.Windows.Forms.Padding(20);
            this.panelProductInfo.Size = new System.Drawing.Size(1048, 295);
            this.panelProductInfo.TabIndex = 1;
            // 
            // picProductImage
            // 
            this.picProductImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.picProductImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picProductImage.Location = new System.Drawing.Point(700, 80);
            this.picProductImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picProductImage.Name = "picProductImage";
            this.picProductImage.Size = new System.Drawing.Size(239, 200);
            this.picProductImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProductImage.TabIndex = 22;
            this.picProductImage.TabStop = false;
            // 
            // txtImagePath
            // 
            this.txtImagePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.txtImagePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtImagePath.ForeColor = System.Drawing.Color.White;
            this.txtImagePath.Location = new System.Drawing.Point(700, 50);
            this.txtImagePath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.Size = new System.Drawing.Size(239, 22);
            this.txtImagePath.TabIndex = 21;
            // 
            // lblImage
            // 
            this.lblImage.AutoSize = true;
            this.lblImage.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblImage.Location = new System.Drawing.Point(700, 30);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(62, 16);
            this.lblImage.TabIndex = 20;
            this.lblImage.Text = "Hình ảnh:";
            // 
            // btnUploadImage
            // 
            this.btnUploadImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.btnUploadImage.FlatAppearance.BorderSize = 0;
            this.btnUploadImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadImage.ForeColor = System.Drawing.Color.White;
            this.btnUploadImage.Location = new System.Drawing.Point(950, 50);
            this.btnUploadImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUploadImage.Name = "btnUploadImage";
            this.btnUploadImage.Size = new System.Drawing.Size(85, 22);
            this.btnUploadImage.TabIndex = 24;
            this.btnUploadImage.Text = "Chọn ảnh";
            this.btnUploadImage.UseVisualStyleBackColor = false;
            this.btnUploadImage.Click += new System.EventHandler(this.btnUploadImage_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.ForeColor = System.Drawing.Color.White;
            this.txtDescription.Location = new System.Drawing.Point(400, 281);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(279, 50);
            this.txtDescription.TabIndex = 19;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblDescription.Location = new System.Drawing.Point(400, 260);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(43, 16);
            this.lblDescription.TabIndex = 18;
            this.lblDescription.Text = "Mô tả:";
            // 
            // cmbColor
            // 
            this.cmbColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.cmbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbColor.ForeColor = System.Drawing.Color.White;
            this.cmbColor.FormattingEnabled = true;
            this.cmbColor.Location = new System.Drawing.Point(400, 220);
            this.cmbColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbColor.Name = "cmbColor";
            this.cmbColor.Size = new System.Drawing.Size(280, 24);
            this.cmbColor.TabIndex = 17;
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblColor.Location = new System.Drawing.Point(400, 199);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(61, 16);
            this.lblColor.TabIndex = 16;
            this.lblColor.Text = "Màu sắc:";
            // 
            // cmbSize
            // 
            this.cmbSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.cmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSize.ForeColor = System.Drawing.Color.White;
            this.cmbSize.FormattingEnabled = true;
            this.cmbSize.Location = new System.Drawing.Point(400, 160);
            this.cmbSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbSize.Name = "cmbSize";
            this.cmbSize.Size = new System.Drawing.Size(280, 24);
            this.cmbSize.TabIndex = 15;
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblSize.Location = new System.Drawing.Point(400, 140);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(36, 16);
            this.lblSize.TabIndex = 14;
            this.lblSize.Text = "Size:";
            // 
            // cmbLoai
            // 
            this.cmbLoai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.cmbLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbLoai.ForeColor = System.Drawing.Color.White;
            this.cmbLoai.FormattingEnabled = true;
            this.cmbLoai.Location = new System.Drawing.Point(400, 100);
            this.cmbLoai.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbLoai.Name = "cmbLoai";
            this.cmbLoai.Size = new System.Drawing.Size(280, 24);
            this.cmbLoai.TabIndex = 13;
            // 
            // lblLoai
            // 
            this.lblLoai.AutoSize = true;
            this.lblLoai.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblLoai.Location = new System.Drawing.Point(400, 80);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Size = new System.Drawing.Size(36, 16);
            this.lblLoai.TabIndex = 12;
            this.lblLoai.Text = "Loại:";
            // 
            // txtQuantity
            // 
            this.txtQuantity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.txtQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQuantity.ForeColor = System.Drawing.Color.White;
            this.txtQuantity.Location = new System.Drawing.Point(100, 281);
            this.txtQuantity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(279, 22);
            this.txtQuantity.TabIndex = 11;
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblQuantity.Location = new System.Drawing.Point(20, 281);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(63, 16);
            this.lblQuantity.TabIndex = 10;
            this.lblQuantity.Text = "Số lượng:";
            // 
            // txtSellingPrice
            // 
            this.txtSellingPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.txtSellingPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSellingPrice.ForeColor = System.Drawing.Color.White;
            this.txtSellingPrice.Location = new System.Drawing.Point(100, 220);
            this.txtSellingPrice.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSellingPrice.Name = "txtSellingPrice";
            this.txtSellingPrice.Size = new System.Drawing.Size(279, 22);
            this.txtSellingPrice.TabIndex = 9;
            // 
            // lblSellingPrice
            // 
            this.lblSellingPrice.AutoSize = true;
            this.lblSellingPrice.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblSellingPrice.Location = new System.Drawing.Point(20, 220);
            this.lblSellingPrice.Name = "lblSellingPrice";
            this.lblSellingPrice.Size = new System.Drawing.Size(57, 16);
            this.lblSellingPrice.TabIndex = 8;
            this.lblSellingPrice.Text = "Giá bán:";
            // 
            // txtImportPrice
            // 
            this.txtImportPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.txtImportPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtImportPrice.ForeColor = System.Drawing.Color.White;
            this.txtImportPrice.Location = new System.Drawing.Point(100, 160);
            this.txtImportPrice.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtImportPrice.Name = "txtImportPrice";
            this.txtImportPrice.Size = new System.Drawing.Size(279, 22);
            this.txtImportPrice.TabIndex = 7;
            // 
            // lblImportPrice
            // 
            this.lblImportPrice.AutoSize = true;
            this.lblImportPrice.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblImportPrice.Location = new System.Drawing.Point(20, 160);
            this.lblImportPrice.Name = "lblImportPrice";
            this.lblImportPrice.Size = new System.Drawing.Size(64, 16);
            this.lblImportPrice.TabIndex = 6;
            this.lblImportPrice.Text = "Giá nhập:";
            // 
            // txtProductCode
            // 
            this.txtProductCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.txtProductCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProductCode.ForeColor = System.Drawing.Color.White;
            this.txtProductCode.Location = new System.Drawing.Point(100, 100);
            this.txtProductCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(279, 22);
            this.txtProductCode.TabIndex = 5;
            // 
            // lblProductCode
            // 
            this.lblProductCode.AutoSize = true;
            this.lblProductCode.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblProductCode.Location = new System.Drawing.Point(20, 100);
            this.lblProductCode.Name = "lblProductCode";
            this.lblProductCode.Size = new System.Drawing.Size(58, 16);
            this.lblProductCode.TabIndex = 4;
            this.lblProductCode.Text = "Mã giày:";
            // 
            // txtProductName
            // 
            this.txtProductName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.txtProductName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProductName.ForeColor = System.Drawing.Color.White;
            this.txtProductName.Location = new System.Drawing.Point(100, 50);
            this.txtProductName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(279, 22);
            this.txtProductName.TabIndex = 3;
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblProductName.Location = new System.Drawing.Point(20, 50);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(63, 16);
            this.lblProductName.TabIndex = 2;
            this.lblProductName.Text = "Tên giày:";
            // 
            // cmbProduct
            // 
            this.cmbProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProduct.ForeColor = System.Drawing.Color.White;
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(100, 10);
            this.cmbProduct.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(280, 24);
            this.cmbProduct.TabIndex = 1;
            this.cmbProduct.SelectedIndexChanged += new System.EventHandler(this.cmbProduct_SelectedIndexChanged);
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblProduct.Location = new System.Drawing.Point(20, 10);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(72, 16);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "Hãng giày:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(24, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.lblTitle.Size = new System.Drawing.Size(410, 62);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý sản phẩm (Giày)";
            // 
            // frmSanpham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1312, 720);
            this.Controls.Add(this.panelRoot);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(1327, 752);
            this.Name = "frmSanpham";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý sản phẩm";
            this.Load += new System.EventHandler(this.frmSanpham_Load);
            this.panelRoot.ResumeLayout(false);
            this.panelRoot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelProductInfo.ResumeLayout(false);
            this.panelProductInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRoot;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelProductInfo;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.Label lblProductCode;
        private System.Windows.Forms.TextBox txtImportPrice;
        private System.Windows.Forms.Label lblImportPrice;
        private System.Windows.Forms.TextBox txtSellingPrice;
        private System.Windows.Forms.Label lblSellingPrice;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.ComboBox cmbLoai;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.ComboBox cmbSize;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.ComboBox cmbColor;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.PictureBox picProductImage;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.Label lblImage;
        private System.Windows.Forms.Button btnUploadImage;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ComboBox cmbFilterSize;
        private System.Windows.Forms.Label lblFilterSize;
        private System.Windows.Forms.ComboBox cmbFilterLoai;
        private System.Windows.Forms.Label lblFilterLoai;
        private System.Windows.Forms.ComboBox cmbFilterPriceType;
        private System.Windows.Forms.Label lblFilterPrice;
        private System.Windows.Forms.ComboBox cmbFilterStatus;
        private System.Windows.Forms.Label lblFilterStatus;
        private System.Windows.Forms.DataGridView dgvProducts;
    }
}
