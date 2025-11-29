namespace BTL_LTTQ
{
    partial class frmReport
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panelRoot = new System.Windows.Forms.Panel();
            this.bodyLayout = new System.Windows.Forms.TableLayoutPanel();
            this.chartRevenue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panelRight = new System.Windows.Forms.Panel();
            this.lblTopCustomers = new System.Windows.Forms.Label();
            this.dgvTopCustomers = new System.Windows.Forms.DataGridView();
            this.lblTopProducts = new System.Windows.Forms.Label();
            this.dgvTopProducts = new System.Windows.Forms.DataGridView();
            this.tableSummary = new System.Windows.Forms.TableLayoutPanel();
            this.panelRevenueCard = new System.Windows.Forms.Panel();
            this.panelRevenueAccent = new System.Windows.Forms.Panel();
            this.lblRevenueDelta = new System.Windows.Forms.Label();
            this.lblRevenueSub = new System.Windows.Forms.Label();
            this.lblRevenueValue = new System.Windows.Forms.Label();
            this.lblRevenueTitle = new System.Windows.Forms.Label();
            this.panelProfitCard = new System.Windows.Forms.Panel();
            this.panelProfitAccent = new System.Windows.Forms.Panel();
            this.lblProfitDelta = new System.Windows.Forms.Label();
            this.lblProfitSub = new System.Windows.Forms.Label();
            this.lblProfitValue = new System.Windows.Forms.Label();
            this.lblProfitTitle = new System.Windows.Forms.Label();
            this.panelOrderCard = new System.Windows.Forms.Panel();
            this.panelOrderAccent = new System.Windows.Forms.Panel();
            this.lblOrderDelta = new System.Windows.Forms.Label();
            this.lblOrderSub = new System.Windows.Forms.Label();
            this.lblOrderValue = new System.Windows.Forms.Label();
            this.lblOrderTitle = new System.Windows.Forms.Label();
            this.panelCustomerCard = new System.Windows.Forms.Panel();
            this.panelCustomerAccent = new System.Windows.Forms.Panel();
            this.lblCustomerDelta = new System.Windows.Forms.Label();
            this.lblCustomerSub = new System.Windows.Forms.Label();
            this.lblCustomerValue = new System.Windows.Forms.Label();
            this.lblCustomerTitle = new System.Windows.Forms.Label();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblCompareMode = new System.Windows.Forms.Label();
            this.cboCompareMode = new System.Windows.Forms.ComboBox();
            this.lblTopCount = new System.Windows.Forms.Label();
            this.nudTopCount = new System.Windows.Forms.NumericUpDown();
            this.lblGrouping = new System.Windows.Forms.Label();
            this.cboGrouping = new System.Windows.Forms.ComboBox();
            this.lblQuickRange = new System.Windows.Forms.Label();
            this.cboQuickRange = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panelRoot.SuspendLayout();
            this.bodyLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).BeginInit();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProducts)).BeginInit();
            this.tableSummary.SuspendLayout();
            this.panelRevenueCard.SuspendLayout();
            this.panelProfitCard.SuspendLayout();
            this.panelOrderCard.SuspendLayout();
            this.panelCustomerCard.SuspendLayout();
            this.grpFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopCount)).BeginInit();
            this.SuspendLayout();
            // 
            // panelRoot
            // 
            this.panelRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(72)))));
            this.panelRoot.Controls.Add(this.bodyLayout);
            this.panelRoot.Controls.Add(this.tableSummary);
            this.panelRoot.Controls.Add(this.grpFilter);
            this.panelRoot.Controls.Add(this.lblHeader);
            this.panelRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRoot.Location = new System.Drawing.Point(0, 0);
            this.panelRoot.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelRoot.Name = "panelRoot";
            this.panelRoot.Padding = new System.Windows.Forms.Padding(18, 15, 18, 15);
            this.panelRoot.Size = new System.Drawing.Size(960, 585);
            this.panelRoot.TabIndex = 0;
            // 
            // bodyLayout
            // 
            this.bodyLayout.ColumnCount = 2;
            this.bodyLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.bodyLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.bodyLayout.Controls.Add(this.chartRevenue, 0, 0);
            this.bodyLayout.Controls.Add(this.panelRight, 1, 0);
            this.bodyLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bodyLayout.Location = new System.Drawing.Point(18, 273);
            this.bodyLayout.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bodyLayout.Name = "bodyLayout";
            this.bodyLayout.RowCount = 1;
            this.bodyLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.bodyLayout.Size = new System.Drawing.Size(924, 297);
            this.bodyLayout.TabIndex = 3;
            // 
            // chartRevenue
            // 
            this.chartRevenue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(78)))));
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(72)))), ((int)(((byte)(98)))));
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(72)))), ((int)(((byte)(98)))));
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(52)))), ((int)(((byte)(78)))));
            chartArea1.Name = "ChartArea1";
            this.chartRevenue.ChartAreas.Add(chartArea1);
            this.chartRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.ForeColor = System.Drawing.Color.Gainsboro;
            legend1.Name = "Legend1";
            this.chartRevenue.Legends.Add(legend1);
            this.chartRevenue.Location = new System.Drawing.Point(8, 6);
            this.chartRevenue.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.chartRevenue.Name = "chartRevenue";
            this.chartRevenue.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Doanh thu";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(90)))), ((int)(((byte)(79)))));
            series2.Legend = "Legend1";
            series2.Name = "Lợi nhuận";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chartRevenue.Series.Add(series1);
            this.chartRevenue.Series.Add(series2);
            this.chartRevenue.Size = new System.Drawing.Size(538, 285);
            this.chartRevenue.TabIndex = 0;
            this.chartRevenue.Text = "chart1";
            // 
            // panelRight
            // 
            this.panelRight.BackColor = System.Drawing.Color.Transparent;
            this.panelRight.Controls.Add(this.lblTopCustomers);
            this.panelRight.Controls.Add(this.dgvTopCustomers);
            this.panelRight.Controls.Add(this.lblTopProducts);
            this.panelRight.Controls.Add(this.dgvTopProducts);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(554, 0);
            this.panelRight.Margin = new System.Windows.Forms.Padding(0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.panelRight.Size = new System.Drawing.Size(370, 297);
            this.panelRight.TabIndex = 1;
            // 
            // lblTopCustomers
            // 
            this.lblTopCustomers.AutoSize = true;
            this.lblTopCustomers.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTopCustomers.ForeColor = System.Drawing.Color.White;
            this.lblTopCustomers.Location = new System.Drawing.Point(10, 119);
            this.lblTopCustomers.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTopCustomers.Name = "lblTopCustomers";
            this.lblTopCustomers.Size = new System.Drawing.Size(136, 19);
            this.lblTopCustomers.TabIndex = 3;
            this.lblTopCustomers.Text = "Top khách hàng (5)";
            // 
            // dgvTopCustomers
            // 
            this.dgvTopCustomers.AllowUserToAddRows = false;
            this.dgvTopCustomers.AllowUserToDeleteRows = false;
            this.dgvTopCustomers.AllowUserToResizeRows = false;
            this.dgvTopCustomers.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.dgvTopCustomers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTopCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopCustomers.Location = new System.Drawing.Point(12, 136);
            this.dgvTopCustomers.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvTopCustomers.MultiSelect = false;
            this.dgvTopCustomers.Name = "dgvTopCustomers";
            this.dgvTopCustomers.ReadOnly = true;
            this.dgvTopCustomers.RowHeadersVisible = false;
            this.dgvTopCustomers.RowHeadersWidth = 51;
            this.dgvTopCustomers.RowTemplate.Height = 28;
            this.dgvTopCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopCustomers.Size = new System.Drawing.Size(336, 109);
            this.dgvTopCustomers.TabIndex = 2;
            // 
            // lblTopProducts
            // 
            this.lblTopProducts.AutoSize = true;
            this.lblTopProducts.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTopProducts.ForeColor = System.Drawing.Color.White;
            this.lblTopProducts.Location = new System.Drawing.Point(10, 8);
            this.lblTopProducts.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTopProducts.Name = "lblTopProducts";
            this.lblTopProducts.Size = new System.Drawing.Size(153, 19);
            this.lblTopProducts.TabIndex = 1;
            this.lblTopProducts.Text = "Top sản phẩm bán (5)";
            // 
            // dgvTopProducts
            // 
            this.dgvTopProducts.AllowUserToAddRows = false;
            this.dgvTopProducts.AllowUserToDeleteRows = false;
            this.dgvTopProducts.AllowUserToResizeRows = false;
            this.dgvTopProducts.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(82)))));
            this.dgvTopProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTopProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopProducts.Location = new System.Drawing.Point(12, 25);
            this.dgvTopProducts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvTopProducts.MultiSelect = false;
            this.dgvTopProducts.Name = "dgvTopProducts";
            this.dgvTopProducts.ReadOnly = true;
            this.dgvTopProducts.RowHeadersVisible = false;
            this.dgvTopProducts.RowHeadersWidth = 51;
            this.dgvTopProducts.RowTemplate.Height = 28;
            this.dgvTopProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopProducts.Size = new System.Drawing.Size(336, 106);
            this.dgvTopProducts.TabIndex = 0;
            // 
            // tableSummary
            // 
            this.tableSummary.ColumnCount = 4;
            this.tableSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableSummary.Controls.Add(this.panelRevenueCard, 0, 0);
            this.tableSummary.Controls.Add(this.panelProfitCard, 1, 0);
            this.tableSummary.Controls.Add(this.panelOrderCard, 2, 0);
            this.tableSummary.Controls.Add(this.panelCustomerCard, 3, 0);
            this.tableSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableSummary.Location = new System.Drawing.Point(18, 133);
            this.tableSummary.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableSummary.Name = "tableSummary";
            this.tableSummary.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.tableSummary.RowCount = 1;
            this.tableSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableSummary.Size = new System.Drawing.Size(924, 140);
            this.tableSummary.TabIndex = 2;
            // 
            // panelRevenueCard
            // 
            this.panelRevenueCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.panelRevenueCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRevenueCard.Controls.Add(this.panelRevenueAccent);
            this.panelRevenueCard.Controls.Add(this.lblRevenueDelta);
            this.panelRevenueCard.Controls.Add(this.lblRevenueSub);
            this.panelRevenueCard.Controls.Add(this.lblRevenueValue);
            this.panelRevenueCard.Controls.Add(this.lblRevenueTitle);
            this.panelRevenueCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRevenueCard.Location = new System.Drawing.Point(6, 7);
            this.panelRevenueCard.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.panelRevenueCard.Name = "panelRevenueCard";
            this.panelRevenueCard.Padding = new System.Windows.Forms.Padding(14, 11, 14, 11);
            this.panelRevenueCard.Size = new System.Drawing.Size(219, 128);
            this.panelRevenueCard.TabIndex = 0;
            // 
            // panelRevenueAccent
            // 
            this.panelRevenueAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(126)))), ((int)(((byte)(107)))));
            this.panelRevenueAccent.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRevenueAccent.Location = new System.Drawing.Point(14, 108);
            this.panelRevenueAccent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelRevenueAccent.Name = "panelRevenueAccent";
            this.panelRevenueAccent.Size = new System.Drawing.Size(189, 2);
            this.panelRevenueAccent.TabIndex = 4;
            // 
            // lblRevenueDelta
            // 
            this.lblRevenueDelta.AutoSize = true;
            this.lblRevenueDelta.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRevenueDelta.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblRevenueDelta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(192)))), ((int)(((byte)(215)))));
            this.lblRevenueDelta.Location = new System.Drawing.Point(14, 93);
            this.lblRevenueDelta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRevenueDelta.Name = "lblRevenueDelta";
            this.lblRevenueDelta.Size = new System.Drawing.Size(99, 15);
            this.lblRevenueDelta.TabIndex = 3;
            this.lblRevenueDelta.Text = "So với kỳ trước: --";
            // 
            // lblRevenueSub
            // 
            this.lblRevenueSub.AutoSize = true;
            this.lblRevenueSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRevenueSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRevenueSub.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblRevenueSub.Location = new System.Drawing.Point(14, 78);
            this.lblRevenueSub.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblRevenueSub.Name = "lblRevenueSub";
            this.lblRevenueSub.Size = new System.Drawing.Size(77, 15);
            this.lblRevenueSub.TabIndex = 2;
            this.lblRevenueSub.Text = "Giá trị TB: 0 đ";
            // 
            // lblRevenueValue
            // 
            this.lblRevenueValue.AutoSize = true;
            this.lblRevenueValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRevenueValue.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblRevenueValue.ForeColor = System.Drawing.Color.White;
            this.lblRevenueValue.Location = new System.Drawing.Point(14, 31);
            this.lblRevenueValue.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblRevenueValue.Name = "lblRevenueValue";
            this.lblRevenueValue.Size = new System.Drawing.Size(72, 47);
            this.lblRevenueValue.TabIndex = 1;
            this.lblRevenueValue.Text = "0 đ";
            this.lblRevenueValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRevenueTitle
            // 
            this.lblRevenueTitle.AutoSize = true;
            this.lblRevenueTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRevenueTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblRevenueTitle.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblRevenueTitle.Location = new System.Drawing.Point(14, 11);
            this.lblRevenueTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lblRevenueTitle.Name = "lblRevenueTitle";
            this.lblRevenueTitle.Size = new System.Drawing.Size(82, 20);
            this.lblRevenueTitle.TabIndex = 0;
            this.lblRevenueTitle.Text = "Doanh thu";
            // 
            // panelProfitCard
            // 
            this.panelProfitCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.panelProfitCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProfitCard.Controls.Add(this.panelProfitAccent);
            this.panelProfitCard.Controls.Add(this.lblProfitDelta);
            this.panelProfitCard.Controls.Add(this.lblProfitSub);
            this.panelProfitCard.Controls.Add(this.lblProfitValue);
            this.panelProfitCard.Controls.Add(this.lblProfitTitle);
            this.panelProfitCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProfitCard.Location = new System.Drawing.Point(237, 7);
            this.panelProfitCard.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.panelProfitCard.Name = "panelProfitCard";
            this.panelProfitCard.Padding = new System.Windows.Forms.Padding(14, 11, 14, 11);
            this.panelProfitCard.Size = new System.Drawing.Size(219, 128);
            this.panelProfitCard.TabIndex = 1;
            // 
            // panelProfitAccent
            // 
            this.panelProfitAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(176)))), ((int)(((byte)(107)))));
            this.panelProfitAccent.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelProfitAccent.Location = new System.Drawing.Point(14, 108);
            this.panelProfitAccent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelProfitAccent.Name = "panelProfitAccent";
            this.panelProfitAccent.Size = new System.Drawing.Size(189, 2);
            this.panelProfitAccent.TabIndex = 4;
            // 
            // lblProfitDelta
            // 
            this.lblProfitDelta.AutoSize = true;
            this.lblProfitDelta.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProfitDelta.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblProfitDelta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(192)))), ((int)(((byte)(215)))));
            this.lblProfitDelta.Location = new System.Drawing.Point(14, 93);
            this.lblProfitDelta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProfitDelta.Name = "lblProfitDelta";
            this.lblProfitDelta.Size = new System.Drawing.Size(99, 15);
            this.lblProfitDelta.TabIndex = 3;
            this.lblProfitDelta.Text = "So với kỳ trước: --";
            // 
            // lblProfitSub
            // 
            this.lblProfitSub.AutoSize = true;
            this.lblProfitSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProfitSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProfitSub.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblProfitSub.Location = new System.Drawing.Point(14, 78);
            this.lblProfitSub.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblProfitSub.Name = "lblProfitSub";
            this.lblProfitSub.Size = new System.Drawing.Size(64, 15);
            this.lblProfitSub.TabIndex = 2;
            this.lblProfitSub.Text = "Tỉ suất: 0%";
            // 
            // lblProfitValue
            // 
            this.lblProfitValue.AutoSize = true;
            this.lblProfitValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProfitValue.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblProfitValue.ForeColor = System.Drawing.Color.White;
            this.lblProfitValue.Location = new System.Drawing.Point(14, 31);
            this.lblProfitValue.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblProfitValue.Name = "lblProfitValue";
            this.lblProfitValue.Size = new System.Drawing.Size(72, 47);
            this.lblProfitValue.TabIndex = 1;
            this.lblProfitValue.Text = "0 đ";
            // 
            // lblProfitTitle
            // 
            this.lblProfitTitle.AutoSize = true;
            this.lblProfitTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProfitTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblProfitTitle.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblProfitTitle.Location = new System.Drawing.Point(14, 11);
            this.lblProfitTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lblProfitTitle.Name = "lblProfitTitle";
            this.lblProfitTitle.Size = new System.Drawing.Size(77, 20);
            this.lblProfitTitle.TabIndex = 0;
            this.lblProfitTitle.Text = "Lợi nhuận";
            // 
            // panelOrderCard
            // 
            this.panelOrderCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.panelOrderCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOrderCard.Controls.Add(this.panelOrderAccent);
            this.panelOrderCard.Controls.Add(this.lblOrderDelta);
            this.panelOrderCard.Controls.Add(this.lblOrderSub);
            this.panelOrderCard.Controls.Add(this.lblOrderValue);
            this.panelOrderCard.Controls.Add(this.lblOrderTitle);
            this.panelOrderCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOrderCard.Location = new System.Drawing.Point(468, 7);
            this.panelOrderCard.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.panelOrderCard.Name = "panelOrderCard";
            this.panelOrderCard.Padding = new System.Windows.Forms.Padding(14, 11, 14, 11);
            this.panelOrderCard.Size = new System.Drawing.Size(219, 128);
            this.panelOrderCard.TabIndex = 2;
            // 
            // panelOrderAccent
            // 
            this.panelOrderAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(181)))), ((int)(((byte)(207)))));
            this.panelOrderAccent.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOrderAccent.Location = new System.Drawing.Point(14, 108);
            this.panelOrderAccent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelOrderAccent.Name = "panelOrderAccent";
            this.panelOrderAccent.Size = new System.Drawing.Size(189, 2);
            this.panelOrderAccent.TabIndex = 4;
            // 
            // lblOrderDelta
            // 
            this.lblOrderDelta.AutoSize = true;
            this.lblOrderDelta.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOrderDelta.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblOrderDelta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(192)))), ((int)(((byte)(215)))));
            this.lblOrderDelta.Location = new System.Drawing.Point(14, 93);
            this.lblOrderDelta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOrderDelta.Name = "lblOrderDelta";
            this.lblOrderDelta.Size = new System.Drawing.Size(99, 15);
            this.lblOrderDelta.TabIndex = 3;
            this.lblOrderDelta.Text = "So với kỳ trước: --";
            // 
            // lblOrderSub
            // 
            this.lblOrderSub.AutoSize = true;
            this.lblOrderSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOrderSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblOrderSub.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblOrderSub.Location = new System.Drawing.Point(14, 78);
            this.lblOrderSub.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblOrderSub.Name = "lblOrderSub";
            this.lblOrderSub.Size = new System.Drawing.Size(95, 15);
            this.lblOrderSub.TabIndex = 2;
            this.lblOrderSub.Text = "Sản phẩm bán: 0";
            // 
            // lblOrderValue
            // 
            this.lblOrderValue.AutoSize = true;
            this.lblOrderValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOrderValue.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblOrderValue.ForeColor = System.Drawing.Color.White;
            this.lblOrderValue.Location = new System.Drawing.Point(14, 31);
            this.lblOrderValue.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblOrderValue.Name = "lblOrderValue";
            this.lblOrderValue.Size = new System.Drawing.Size(40, 47);
            this.lblOrderValue.TabIndex = 1;
            this.lblOrderValue.Text = "0";
            // 
            // lblOrderTitle
            // 
            this.lblOrderTitle.AutoSize = true;
            this.lblOrderTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOrderTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblOrderTitle.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblOrderTitle.Location = new System.Drawing.Point(14, 11);
            this.lblOrderTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lblOrderTitle.Name = "lblOrderTitle";
            this.lblOrderTitle.Size = new System.Drawing.Size(117, 20);
            this.lblOrderTitle.TabIndex = 0;
            this.lblOrderTitle.Text = "Đơn hàng đóng";
            // 
            // panelCustomerCard
            // 
            this.panelCustomerCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.panelCustomerCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCustomerCard.Controls.Add(this.panelCustomerAccent);
            this.panelCustomerCard.Controls.Add(this.lblCustomerDelta);
            this.panelCustomerCard.Controls.Add(this.lblCustomerSub);
            this.panelCustomerCard.Controls.Add(this.lblCustomerValue);
            this.panelCustomerCard.Controls.Add(this.lblCustomerTitle);
            this.panelCustomerCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCustomerCard.Location = new System.Drawing.Point(699, 7);
            this.panelCustomerCard.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.panelCustomerCard.Name = "panelCustomerCard";
            this.panelCustomerCard.Padding = new System.Windows.Forms.Padding(14, 11, 14, 11);
            this.panelCustomerCard.Size = new System.Drawing.Size(219, 128);
            this.panelCustomerCard.TabIndex = 3;
            // 
            // panelCustomerAccent
            // 
            this.panelCustomerAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(200)))), ((int)(((byte)(155)))));
            this.panelCustomerAccent.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCustomerAccent.Location = new System.Drawing.Point(14, 108);
            this.panelCustomerAccent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelCustomerAccent.Name = "panelCustomerAccent";
            this.panelCustomerAccent.Size = new System.Drawing.Size(189, 2);
            this.panelCustomerAccent.TabIndex = 4;
            // 
            // lblCustomerDelta
            // 
            this.lblCustomerDelta.AutoSize = true;
            this.lblCustomerDelta.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCustomerDelta.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblCustomerDelta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(192)))), ((int)(((byte)(215)))));
            this.lblCustomerDelta.Location = new System.Drawing.Point(14, 93);
            this.lblCustomerDelta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCustomerDelta.Name = "lblCustomerDelta";
            this.lblCustomerDelta.Size = new System.Drawing.Size(99, 15);
            this.lblCustomerDelta.TabIndex = 3;
            this.lblCustomerDelta.Text = "So với kỳ trước: --";
            // 
            // lblCustomerSub
            // 
            this.lblCustomerSub.AutoSize = true;
            this.lblCustomerSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCustomerSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCustomerSub.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblCustomerSub.Location = new System.Drawing.Point(14, 78);
            this.lblCustomerSub.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lblCustomerSub.Name = "lblCustomerSub";
            this.lblCustomerSub.Size = new System.Drawing.Size(79, 15);
            this.lblCustomerSub.TabIndex = 2;
            this.lblCustomerSub.Text = "Khách mua: 0";
            // 
            // lblCustomerValue
            // 
            this.lblCustomerValue.AutoSize = true;
            this.lblCustomerValue.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCustomerValue.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblCustomerValue.ForeColor = System.Drawing.Color.White;
            this.lblCustomerValue.Location = new System.Drawing.Point(14, 31);
            this.lblCustomerValue.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblCustomerValue.Name = "lblCustomerValue";
            this.lblCustomerValue.Size = new System.Drawing.Size(40, 47);
            this.lblCustomerValue.TabIndex = 1;
            this.lblCustomerValue.Text = "0";
            // 
            // lblCustomerTitle
            // 
            this.lblCustomerTitle.AutoSize = true;
            this.lblCustomerTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCustomerTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblCustomerTitle.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblCustomerTitle.Location = new System.Drawing.Point(14, 11);
            this.lblCustomerTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lblCustomerTitle.Name = "lblCustomerTitle";
            this.lblCustomerTitle.Size = new System.Drawing.Size(122, 20);
            this.lblCustomerTitle.TabIndex = 0;
            this.lblCustomerTitle.Text = "Tương tác khách";
            // 
            // grpFilter
            // 
            this.grpFilter.Controls.Add(this.btnExport);
            this.grpFilter.Controls.Add(this.lblCompareMode);
            this.grpFilter.Controls.Add(this.cboCompareMode);
            this.grpFilter.Controls.Add(this.lblTopCount);
            this.grpFilter.Controls.Add(this.nudTopCount);
            this.grpFilter.Controls.Add(this.lblGrouping);
            this.grpFilter.Controls.Add(this.cboGrouping);
            this.grpFilter.Controls.Add(this.lblQuickRange);
            this.grpFilter.Controls.Add(this.cboQuickRange);
            this.grpFilter.Controls.Add(this.btnRefresh);
            this.grpFilter.Controls.Add(this.lblTo);
            this.grpFilter.Controls.Add(this.lblFrom);
            this.grpFilter.Controls.Add(this.dtpTo);
            this.grpFilter.Controls.Add(this.dtpFrom);
            this.grpFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFilter.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpFilter.Location = new System.Drawing.Point(18, 15);
            this.grpFilter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Padding = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.grpFilter.Size = new System.Drawing.Size(924, 118);
            this.grpFilter.TabIndex = 1;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Thiết lập bộ lọc nâng cao";
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(190)))), ((int)(((byte)(138)))));
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(352, 80);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(116, 24);
            this.btnExport.TabIndex = 10;
            this.btnExport.Text = "Xuất báo cáo";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblCompareMode
            // 
            this.lblCompareMode.AutoSize = true;
            this.lblCompareMode.Location = new System.Drawing.Point(16, 67);
            this.lblCompareMode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCompareMode.Name = "lblCompareMode";
            this.lblCompareMode.Size = new System.Drawing.Size(114, 13);
            this.lblCompareMode.TabIndex = 9;
            this.lblCompareMode.Text = "Chế độ so sánh kỳ đối";
            // 
            // cboCompareMode
            // 
            this.cboCompareMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompareMode.FormattingEnabled = true;
            this.cboCompareMode.Location = new System.Drawing.Point(16, 82);
            this.cboCompareMode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboCompareMode.Name = "cboCompareMode";
            this.cboCompareMode.Size = new System.Drawing.Size(166, 21);
            this.cboCompareMode.TabIndex = 8;
            // 
            // lblTopCount
            // 
            this.lblTopCount.AutoSize = true;
            this.lblTopCount.Location = new System.Drawing.Point(556, 20);
            this.lblTopCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTopCount.Name = "lblTopCount";
            this.lblTopCount.Size = new System.Drawing.Size(128, 13);
            this.lblTopCount.TabIndex = 7;
            this.lblTopCount.Text = "Số dòng Top (3 - 15 mục)";
            // 
            // nudTopCount
            // 
            this.nudTopCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.nudTopCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudTopCount.ForeColor = System.Drawing.Color.White;
            this.nudTopCount.Location = new System.Drawing.Point(558, 36);
            this.nudTopCount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nudTopCount.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudTopCount.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudTopCount.Name = "nudTopCount";
            this.nudTopCount.Size = new System.Drawing.Size(68, 20);
            this.nudTopCount.TabIndex = 6;
            this.nudTopCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblGrouping
            // 
            this.lblGrouping.AutoSize = true;
            this.lblGrouping.Location = new System.Drawing.Point(420, 20);
            this.lblGrouping.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGrouping.Name = "lblGrouping";
            this.lblGrouping.Size = new System.Drawing.Size(82, 13);
            this.lblGrouping.TabIndex = 5;
            this.lblGrouping.Text = "Nhóm xu hướng";
            // 
            // cboGrouping
            // 
            this.cboGrouping.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(60)))), ((int)(((byte)(92)))));
            this.cboGrouping.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGrouping.ForeColor = System.Drawing.Color.White;
            this.cboGrouping.FormattingEnabled = true;
            this.cboGrouping.Location = new System.Drawing.Point(422, 35);
            this.cboGrouping.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboGrouping.Name = "cboGrouping";
            this.cboGrouping.Size = new System.Drawing.Size(121, 21);
            this.cboGrouping.TabIndex = 4;
            // 
            // lblQuickRange
            // 
            this.lblQuickRange.AutoSize = true;
            this.lblQuickRange.Location = new System.Drawing.Point(16, 20);
            this.lblQuickRange.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblQuickRange.Name = "lblQuickRange";
            this.lblQuickRange.Size = new System.Drawing.Size(78, 13);
            this.lblQuickRange.TabIndex = 3;
            this.lblQuickRange.Text = "Phạm vi nhanh";
            // 
            // cboQuickRange
            // 
            this.cboQuickRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboQuickRange.FormattingEnabled = true;
            this.cboQuickRange.Location = new System.Drawing.Point(16, 35);
            this.cboQuickRange.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboQuickRange.Name = "cboQuickRange";
            this.cboQuickRange.Size = new System.Drawing.Size(121, 21);
            this.cboQuickRange.TabIndex = 2;
            this.cboQuickRange.SelectedIndexChanged += new System.EventHandler(this.cboQuickRange_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(90)))), ((int)(((byte)(79)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(212, 80);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(128, 24);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Tải dữ liệu";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(283, 20);
            this.lblTo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(53, 13);
            this.lblTo.TabIndex = 3;
            this.lblTo.Text = "Đến ngày";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(149, 20);
            this.lblFrom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(46, 13);
            this.lblFrom.TabIndex = 2;
            this.lblFrom.Text = "Từ ngày";
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(285, 35);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(133, 20);
            this.dtpTo.TabIndex = 1;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(152, 35);
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(133, 20);
            this.dtpFrom.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(24, 21);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(269, 32);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Báo cáo & thống kê bán";
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(960, 585);
            this.Controls.Add(this.panelRoot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Báo cáo thống kê";
            this.Load += new System.EventHandler(this.frmReport_Load);
            this.panelRoot.ResumeLayout(false);
            this.panelRoot.PerformLayout();
            this.bodyLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProducts)).EndInit();
            this.tableSummary.ResumeLayout(false);
            this.panelRevenueCard.ResumeLayout(false);
            this.panelRevenueCard.PerformLayout();
            this.panelProfitCard.ResumeLayout(false);
            this.panelProfitCard.PerformLayout();
            this.panelOrderCard.ResumeLayout(false);
            this.panelOrderCard.PerformLayout();
            this.panelCustomerCard.ResumeLayout(false);
            this.panelCustomerCard.PerformLayout();
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelRoot;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.TableLayoutPanel tableSummary;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cboQuickRange;
        private System.Windows.Forms.Label lblQuickRange;
        private System.Windows.Forms.Label lblGrouping;
        private System.Windows.Forms.ComboBox cboGrouping;
        private System.Windows.Forms.NumericUpDown nudTopCount;
        private System.Windows.Forms.Label lblTopCount;
        private System.Windows.Forms.Label lblCompareMode;
        private System.Windows.Forms.ComboBox cboCompareMode;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel panelRevenueCard;
        private System.Windows.Forms.Panel panelProfitCard;
        private System.Windows.Forms.Panel panelOrderCard;
        private System.Windows.Forms.Panel panelCustomerCard;
        private System.Windows.Forms.Panel panelRevenueAccent;
        private System.Windows.Forms.Label lblRevenueTitle;
        private System.Windows.Forms.Label lblRevenueValue;
        private System.Windows.Forms.Label lblRevenueSub;
        private System.Windows.Forms.Label lblRevenueDelta;
        private System.Windows.Forms.Panel panelProfitAccent;
        private System.Windows.Forms.Label lblProfitSub;
        private System.Windows.Forms.Label lblProfitValue;
        private System.Windows.Forms.Label lblProfitTitle;
        private System.Windows.Forms.Label lblProfitDelta;
        private System.Windows.Forms.Panel panelOrderAccent;
        private System.Windows.Forms.Label lblOrderSub;
        private System.Windows.Forms.Label lblOrderValue;
        private System.Windows.Forms.Label lblOrderTitle;
        private System.Windows.Forms.Label lblOrderDelta;
        private System.Windows.Forms.Panel panelCustomerAccent;
        private System.Windows.Forms.Label lblCustomerSub;
        private System.Windows.Forms.Label lblCustomerValue;
        private System.Windows.Forms.Label lblCustomerTitle;
        private System.Windows.Forms.Label lblCustomerDelta;
        private System.Windows.Forms.TableLayoutPanel bodyLayout;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenue;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.DataGridView dgvTopCustomers;
        private System.Windows.Forms.DataGridView dgvTopProducts;
        private System.Windows.Forms.Label lblTopCustomers;
        private System.Windows.Forms.Label lblTopProducts;
    }
}

