using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BTL_LTTQ.BLL;
using ClosedXML.Excel;

namespace BTL_LTTQ.GUI
{
    public partial class frmHoaDon : Form
    {
        private SalesBLL _bll = new SalesBLL();
        private BTL_LTTQ.DTO.LoginResult _currentUser;

        // Biến toàn cục
        private string _maHDString = "";
        private string _ngayBan = "";
        private string _tenNV = "";
        private int _maNV = 0;
        private string _tenKH = "";
        private string _sdt = "";
        private string _diaChi = "";
        private decimal _tongTienSo = 0;

        private DataTable _dtChiTiet;
        private int _idHoaDonVuaLuu = 0;

        // Controls
        private TextBox txtMaHD, txtNgayBan, txtNhanVien, txtDiaChi, txtSDT;
        private ComboBox cboKhachHang;
        private Label lblTongTien;
        private NumericUpDown numGiamGia;
        private Button btnThemKhach, btnLuu, btnIn, btnHuy, btnSua;
        private DataGridView dgvChiTiet;
        private bool _isEditMode = false;

        // =================================================================================
        // CONSTRUCTOR 1: DÙNG CHO POS (Lập hóa đơn mới)
        // =================================================================================
        public frmHoaDon(DataTable gioHangTuPOS, BTL_LTTQ.DTO.LoginResult currentUser = null)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _dtChiTiet = gioHangTuPOS.Copy();

            SetupFullUI(); // Vẽ giao diện

            // Điền dữ liệu mặc định cho đơn mới
            // Tạo mã HĐ theo format: HDB_ddMMyyyy0xxx
            string datePart = DateTime.Now.ToString("ddMMyyyy");
            int sequence = GetNextInvoiceSequence(datePart);
            _maHDString = $"HDB_{datePart}0{sequence:D3}";
            _ngayBan = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            
            // Lấy thông tin nhân viên từ LoginResult
            if (_currentUser != null)
            {
                _tenNV = _currentUser.FullName;
                _maNV = _currentUser.EmployeeId;
            }
            else
            {
                _tenNV = "Không xác định";
                _maNV = 1; // Fallback
            }

            LoadInitData(isViewOnly: false); // Chế độ nhập liệu
            CalculateTotal();
        }

        // =================================================================================
        // CONSTRUCTOR 2: DÙNG CHO QUẢN LÝ (Xem lại hóa đơn cũ)
        // =================================================================================
        public frmHoaDon(int maHD, BTL_LTTQ.DTO.LoginResult currentUser = null)
        {
            InitializeComponent();
            _currentUser = currentUser;

            // Lấy dữ liệu từ SQL lên
            DataTable dtChung = _bll.GetInvoiceGeneral(maHD);
            DataTable dtChiTiet = _bll.GetInvoiceDetail(maHD);

            if (dtChung.Rows.Count > 0)
            {
                DataRow r = dtChung.Rows[0];
                _maHDString = r["MaHoaDon"].ToString();
                _ngayBan = Convert.ToDateTime(r["NgayLap"]).ToString("dd/MM/yyyy HH:mm");
                _tenNV = r["NhanVien"].ToString();
                _tenKH = r["KhachHang"].ToString();
                _sdt = r["SoDienThoai"].ToString();
                _diaChi = r["DiaChi"].ToString();
                _tongTienSo = r["ThanhToan"] != DBNull.Value ? Convert.ToDecimal(r["ThanhToan"]) : 0;
                _idHoaDonVuaLuu = maHD;
            }

            _dtChiTiet = dtChiTiet;

            SetupFullUI(); // Vẽ giao diện
            LoadInitData(isViewOnly: true); // Chế độ xem
        }

        private void frmHoaDon_Load(object sender, EventArgs e) { }

        // Lấy số thứ tự hóa đơn tiếp theo trong ngày
        private int GetNextInvoiceSequence(string datePart)
        {
            try
            {
                using (var dal = new BTL_LTTQ.DAL.DataProcesser())
                {
                    string sql = @"SELECT COUNT(*) + 1 
                                   FROM HoaDon 
                                   WHERE MaHoaDon LIKE @Pattern";
                    var result = dal.ExecuteQuery(sql, System.Data.CommandType.Text,
                        new System.Data.SqlClient.SqlParameter("@Pattern", $"HDB_{datePart}0%"));
                    if (result.Rows.Count > 0 && result.Rows[0][0] != DBNull.Value)
                    {
                        return Convert.ToInt32(result.Rows[0][0]);
                    }
                }
            }
            catch { }
            return 1;
        }

        // Hàm load dữ liệu chung
        private void LoadInitData(bool isViewOnly)
        {
            txtMaHD.Text = _maHDString;
            txtNgayBan.Text = _ngayBan;
            txtNhanVien.Text = _tenNV;

            try
            {
                cboKhachHang.DataSource = _bll.GetKhachHang();
                cboKhachHang.DisplayMember = "HoTen";
                cboKhachHang.ValueMember = "MaKH";

                if (isViewOnly)
                {
                    cboKhachHang.Text = _tenKH;
                    cboKhachHang.Enabled = false;
                    txtSDT.Text = _sdt;
                    txtDiaChi.Text = _diaChi;
                    lblTongTien.Text = _tongTienSo.ToString("N0") + " VNĐ";

                    btnLuu.Visible = false;
                    btnSua.Visible = true;
                    btnIn.Enabled = true;
                    btnThemKhach.Visible = false;
                    numGiamGia.Enabled = false;
                    dgvChiTiet.ReadOnly = true;
                }
                else
                {
                    cboKhachHang.SelectedIndex = -1;
                }
            }
            catch { }

            dgvChiTiet.DataSource = _dtChiTiet;
            if (dgvChiTiet.Columns.Contains("MaCTSP")) dgvChiTiet.Columns["MaCTSP"].Visible = false;
            if (dgvChiTiet.Columns.Contains("DonGia")) dgvChiTiet.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            if (dgvChiTiet.Columns.Contains("ThanhTien")) dgvChiTiet.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
        }

        private void CalculateTotal()
        {
            decimal tongTienHang = 0;
            foreach (DataRow r in _dtChiTiet.Rows) tongTienHang += Convert.ToDecimal(r["ThanhTien"]);

            decimal giamGia = tongTienHang * (numGiamGia.Value / 100);
            decimal thanhToan = tongTienHang - giamGia;

            lblTongTien.Text = thanhToan.ToString("N0") + " VNĐ";
        }

        private void CboKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedItem != null)
            {
                DataRowView drv = (DataRowView)cboKhachHang.SelectedItem;
                txtSDT.Text = drv["SoDienThoai"].ToString();
                // Lấy địa chỉ từ ComboBox
                txtDiaChi.Text = drv["DiaChi"] != DBNull.Value && !string.IsNullOrEmpty(drv["DiaChi"].ToString()) 
                    ? drv["DiaChi"].ToString() 
                    : "Khách tại quầy";
            }
        }

        // Tìm hoặc thêm khách hàng theo SĐT
        private void TimHoacThemKhachTheoSDT(string sdt)
        {
            if (string.IsNullOrWhiteSpace(sdt))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!");
                return;
            }

            try
            {
                var khachHang = _bll.GetKhachHang();
                foreach (DataRow row in khachHang.Rows)
                {
                    if (row["SoDienThoai"].ToString().Trim() == sdt.Trim())
                    {
                        // Tìm thấy, chọn khách hàng
                        cboKhachHang.SelectedValue = row["MaKH"];
                        MessageBox.Show($"Đã tìm thấy khách hàng: {row["HoTen"]}");
                        return;
                    }
                }

                // Không tìm thấy, hỏi có muốn thêm không
                if (MessageBox.Show($"Không tìm thấy khách hàng với SĐT: {sdt}\nBạn có muốn thêm khách hàng mới không?",
                    "Thêm khách hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Form f = new Form { Text = "Thêm khách mới", Size = new Size(350, 220), StartPosition = FormStartPosition.CenterParent, BackColor = Color.FromArgb(58, 60, 92), ForeColor = Color.White, FormBorderStyle = FormBorderStyle.FixedToolWindow };
                    Label l1 = new Label { Text = "Họ tên:", Location = new Point(20, 20), AutoSize = true };
                    TextBox t1 = new TextBox { Location = new Point(20, 45), Width = 280 };
                    Label l2 = new Label { Text = "SĐT:", Location = new Point(20, 85), AutoSize = true };
                    TextBox t2 = new TextBox { Location = new Point(20, 110), Width = 280, Text = sdt, ReadOnly = true };
                    Button b = new Button { Text = "LƯU", Location = new Point(150, 145), Width = 100, DialogResult = DialogResult.OK, BackColor = Color.OrangeRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

                    f.Controls.AddRange(new Control[] { l1, t1, l2, t2, b });
                    f.AcceptButton = b;

                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        if (string.IsNullOrWhiteSpace(t1.Text)) return;
                        int newID = _bll.QuickAddCustomer(t1.Text, sdt);
                        cboKhachHang.DataSource = _bll.GetKhachHang();
                        cboKhachHang.SelectedValue = newID;
                        MessageBox.Show("Đã thêm khách hàng: " + t1.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // --- LOGIC KHI BẤM NÚT (+) THÊM KHÁCH ---
        private void BtnThemKhach_Click(object sender, EventArgs e)
        {
            Form f = new Form { Text = "Thêm khách mới", Size = new Size(350, 220), StartPosition = FormStartPosition.CenterParent, BackColor = Color.FromArgb(58, 60, 92), ForeColor = Color.White, FormBorderStyle = FormBorderStyle.FixedToolWindow };
            Label l1 = new Label { Text = "Họ tên:", Location = new Point(20, 20), AutoSize = true };
            TextBox t1 = new TextBox { Location = new Point(20, 45), Width = 280 };
            Label l2 = new Label { Text = "SĐT:", Location = new Point(20, 85), AutoSize = true };
            TextBox t2 = new TextBox { Location = new Point(20, 110), Width = 280 };
            Button b = new Button { Text = "LƯU", Location = new Point(150, 145), Width = 100, DialogResult = DialogResult.OK, BackColor = Color.OrangeRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

            f.Controls.AddRange(new Control[] { l1, t1, l2, t2, b });
            f.AcceptButton = b;

            if (f.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(t1.Text)) return;
                try
                {
                    int newID = _bll.QuickAddCustomer(t1.Text, t2.Text);
                    cboKhachHang.DataSource = _bll.GetKhachHang();
                    cboKhachHang.SelectedValue = newID;
                    MessageBox.Show("Đã thêm khách hàng: " + t1.Text);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra khách hàng
            if (cboKhachHang.SelectedIndex == -1 || cboKhachHang.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng!");
                return;
            }

            // Kiểm tra có hàng trong giỏ
            if (_dtChiTiet == null || _dtChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Không có hàng để lưu!");
                return;
            }

            try
            {
                int maKH = Convert.ToInt32(cboKhachHang.SelectedValue);
                // Sử dụng mã NV từ LoginResult
                int maNV = _maNV > 0 ? _maNV : (_currentUser?.EmployeeId ?? 1);

                decimal tongHang = 0;
                foreach (DataRow r in _dtChiTiet.Rows) tongHang += Convert.ToDecimal(r["ThanhTien"]);
                decimal tienGiam = tongHang * (numGiamGia.Value / 100);
                decimal thanhToan = tongHang - tienGiam;

                if (_isEditMode && _idHoaDonVuaLuu > 0)
                {
                    // Cập nhật DataTable từ DataGridView trước
                    DataTable dtChiTietMoi = new DataTable();
                    dtChiTietMoi.Columns.Add("MaCTSP", typeof(int));
                    dtChiTietMoi.Columns.Add("TenSP", typeof(string));
                    dtChiTietMoi.Columns.Add("SoLuong", typeof(int));
                    dtChiTietMoi.Columns.Add("DonGia", typeof(decimal));
                    dtChiTietMoi.Columns.Add("GiamGia", typeof(int));
                    dtChiTietMoi.Columns.Add("ThanhTien", typeof(decimal));

                    foreach (DataGridViewRow row in dgvChiTiet.Rows)
                    {
                        if (row.IsNewRow) continue;
                        int maCTSP = Convert.ToInt32(row.Cells["MaCTSP"].Value);
                        int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                        decimal donGia = Convert.ToDecimal(row.Cells["DonGia"].Value);
                        int giamGiaSP = row.Cells["GiamGia"] != null && row.Cells["GiamGia"].Value != DBNull.Value 
                            ? Convert.ToInt32(row.Cells["GiamGia"].Value) : 0;
                        decimal thanhTien = Convert.ToDecimal(row.Cells["ThanhTien"].Value);

                        // Kiểm tra số lượng tồn kho
                        var sanPham = _bll.GetSanPhamBanHang();
                        bool found = false;
                        foreach (DataRow spRow in sanPham.Rows)
                        {
                            if (Convert.ToInt32(spRow["MaCTSP"]) == maCTSP)
                            {
                                int tonKho = Convert.ToInt32(spRow["SoLuongTon"]);
                                if (soLuong > tonKho)
                                {
                                    MessageBox.Show($"Sản phẩm {row.Cells["TenSP"].Value} chỉ còn {tonKho} trong kho!");
                                    return;
                                }
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            MessageBox.Show($"Không tìm thấy sản phẩm với mã {maCTSP}!");
                            return;
                        }

                        dtChiTietMoi.Rows.Add(maCTSP, row.Cells["TenSP"].Value, soLuong, donGia, giamGiaSP, thanhTien);
                    }

                    // Cập nhật hóa đơn
                    bool success = _bll.CapNhatHoaDon(_idHoaDonVuaLuu, maKH, tongHang, tienGiam, thanhToan, dtChiTietMoi);
                    if (success)
                    {
                        MessageBox.Show("Cập nhật hóa đơn thành công!");
                        _dtChiTiet = dtChiTietMoi;
                        _isEditMode = false;
                        btnLuu.Visible = false;
                        btnLuu.Text = "Lưu Hóa Đơn";
                        btnLuu.BackColor = Color.Green;
                        btnSua.Visible = true;
                        btnIn.Enabled = true;
                        cboKhachHang.Enabled = false;
                        numGiamGia.Enabled = false;
                        dgvChiTiet.ReadOnly = true;
                        btnThemKhach.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật hóa đơn! Vui lòng kiểm tra lại dữ liệu.");
                    }
                }
                else
                {
                    // Tạo hóa đơn mới
                    _idHoaDonVuaLuu = _bll.ThanhToan(maKH, maNV, tongHang, tienGiam, thanhToan, _dtChiTiet);

                    if (_idHoaDonVuaLuu > 0)
                    {
                        MessageBox.Show("Lưu thành công! Mã HĐ: " + _idHoaDonVuaLuu);
                        btnLuu.Enabled = false;
                        btnIn.Enabled = true;
                        cboKhachHang.Enabled = false;
                        numGiamGia.Enabled = false;
                        txtMaHD.Text = "HD" + _idHoaDonVuaLuu;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnIn_Click(object sender, EventArgs e)
        {
            if (_idHoaDonVuaLuu == 0) { MessageBox.Show("Phải Lưu hóa đơn trước khi in!"); return; }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xlsx)|*.xlsx";
            sfd.FileName = "HoaDon_" + txtMaHD.Text + ".xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("HoaDon");

                        // Tiêu đề
                        worksheet.Cell(1, 1).Value = "HÓA ĐƠN BÁN HÀNG";
                        worksheet.Cell(1, 1).Style.Font.Bold = true;
                        worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                        worksheet.Range(1, 1, 1, 4).Merge();

                        // Thông tin hóa đơn
                        worksheet.Cell(3, 1).Value = "Mã HĐ:";
                        worksheet.Cell(3, 2).Value = txtMaHD.Text;
                        worksheet.Cell(4, 1).Value = "Ngày:";
                        worksheet.Cell(4, 2).Value = txtNgayBan.Text;
                        worksheet.Cell(5, 1).Value = "Nhân viên:";
                        worksheet.Cell(5, 2).Value = txtNhanVien.Text;
                        worksheet.Cell(6, 1).Value = "Khách hàng:";
                        worksheet.Cell(6, 2).Value = cboKhachHang.Text;
                        worksheet.Cell(7, 1).Value = "SĐT:";
                        worksheet.Cell(7, 2).Value = txtSDT.Text;
                        worksheet.Cell(8, 1).Value = "Địa chỉ:";
                        worksheet.Cell(8, 2).Value = txtDiaChi.Text;

                        // Tiêu đề bảng
                        int rowStart = 10;
                        worksheet.Cell(rowStart, 1).Value = "Tên Hàng";
                        worksheet.Cell(rowStart, 2).Value = "Số Lượng";
                        worksheet.Cell(rowStart, 3).Value = "Đơn Giá";
                        worksheet.Cell(rowStart, 4).Value = "Thành Tiền";
                        worksheet.Range(rowStart, 1, rowStart, 4).Style.Font.Bold = true;
                        worksheet.Range(rowStart, 1, rowStart, 4).Style.Fill.BackgroundColor = XLColor.LightGray;

                        // Dữ liệu chi tiết
                        int row = rowStart + 1;
                        foreach (DataGridViewRow dgvRow in dgvChiTiet.Rows)
                        {
                            worksheet.Cell(row, 1).Value = dgvRow.Cells["TenSP"].Value?.ToString();
                            worksheet.Cell(row, 2).Value = Convert.ToInt32(dgvRow.Cells["SoLuong"].Value);
                            worksheet.Cell(row, 3).Value = Convert.ToDecimal(dgvRow.Cells["DonGia"].Value);
                            worksheet.Cell(row, 3).Style.NumberFormat.Format = "#,##0";
                            worksheet.Cell(row, 4).Value = Convert.ToDecimal(dgvRow.Cells["ThanhTien"].Value);
                            worksheet.Cell(row, 4).Style.NumberFormat.Format = "#,##0";
                            row++;
                        }

                        // Tổng tiền
                        worksheet.Cell(row, 3).Value = "TỔNG THANH TOÁN:";
                        worksheet.Cell(row, 3).Style.Font.Bold = true;
                        worksheet.Cell(row, 4).Value = lblTongTien.Text.Replace(" VNĐ", "").Replace(",", "");
                        worksheet.Cell(row, 4).Style.Font.Bold = true;
                        worksheet.Cell(row, 4).Style.Font.FontColor = XLColor.Red;
                        worksheet.Cell(row, 4).Style.NumberFormat.Format = "#,##0";

                        worksheet.Columns().AdjustToContents();
                        workbook.SaveAs(sfd.FileName);
                    }

                    MessageBox.Show("Xuất hóa đơn thành công!");
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất file: " + ex.Message);
                }
            }
        }

        private void SetupFullUI()
        {
            this.Text = "HÓA ĐƠN BÁN HÀNG";
            this.Size = new Size(900, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(45, 47, 72);
            this.ForeColor = Color.White;

            Label title = new Label { Text = "HÓA ĐƠN BÁN HÀNG", Font = new Font("Segoe UI", 24, FontStyle.Bold), ForeColor = Color.OrangeRed, Dock = DockStyle.Top, Height = 60, TextAlign = ContentAlignment.MiddleCenter };
            this.Controls.Add(title);

            GroupBox grpInfo = new GroupBox { Text = "Thông tin chung", Location = new Point(20, 70), Size = new Size(850, 210), ForeColor = Color.Gainsboro, Font = new Font("Segoe UI", 10) };
            this.Controls.Add(grpInfo);

            // Cột trái - Thông tin hóa đơn
            CreateLabel(grpInfo, "Mã hóa đơn:", 20, 35); txtMaHD = CreateTextBox(grpInfo, 120, 32, 200); txtMaHD.ReadOnly = true;
            CreateLabel(grpInfo, "Ngày bán:", 20, 70); txtNgayBan = CreateTextBox(grpInfo, 120, 67, 200); txtNgayBan.ReadOnly = true;
            CreateLabel(grpInfo, "Nhân viên:", 20, 105); txtNhanVien = CreateTextBox(grpInfo, 120, 102, 200); txtNhanVien.ReadOnly = true;

            // Cột phải - Thông tin khách hàng
            CreateLabel(grpInfo, "SĐT khách hàng:", 400, 35);
            TextBox txtSDTInput = CreateTextBox(grpInfo, 520, 32, 180);
            txtSDTInput.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter)
                {
                    TimHoacThemKhachTheoSDT(txtSDTInput.Text.Trim());
                }
            };
            grpInfo.Controls.Add(txtSDTInput);

            CreateLabel(grpInfo, "Khách hàng:", 400, 70);
            cboKhachHang = new ComboBox
            {
                Location = new Point(520, 67),
                Width = 180,
                DropDownStyle = ComboBoxStyle.DropDown,
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems
            };

            cboKhachHang.SelectedIndexChanged += CboKhachHang_SelectedIndexChanged;
            grpInfo.Controls.Add(cboKhachHang);

            btnThemKhach = new Button { Text = "+", Location = new Point(710, 66), Size = new Size(30, 23), BackColor = Color.OrangeRed, FlatStyle = FlatStyle.Flat, ForeColor = Color.White };
            btnThemKhach.Click += BtnThemKhach_Click;
            grpInfo.Controls.Add(btnThemKhach);

            CreateLabel(grpInfo, "SĐT:", 400, 105); txtSDT = CreateTextBox(grpInfo, 520, 102, 220);
            CreateLabel(grpInfo, "Địa chỉ:", 400, 140); txtDiaChi = CreateTextBox(grpInfo, 520, 137, 220);

            dgvChiTiet = new DataGridView { Location = new Point(20, 300), Size = new Size(850, 240), BackgroundColor = Color.FromArgb(58, 60, 92), BorderStyle = BorderStyle.None, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, AllowUserToAddRows = false, ReadOnly = true };
            dgvChiTiet.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(232, 90, 79);
            dgvChiTiet.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvChiTiet.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvChiTiet.DefaultCellStyle.BackColor = Color.FromArgb(45, 47, 72);
            dgvChiTiet.DefaultCellStyle.ForeColor = Color.White;
            dgvChiTiet.EnableHeadersVisualStyles = false;
            this.Controls.Add(dgvChiTiet);

            Label lblGiam = new Label { Text = "Giảm giá (%):", Location = new Point(20, 550), AutoSize = true, Font = new Font("Segoe UI", 10) };
            this.Controls.Add(lblGiam);
            numGiamGia = new NumericUpDown { Location = new Point(120, 547), Width = 80, Font = new Font("Segoe UI", 10) };
            numGiamGia.ValueChanged += (s, e) => CalculateTotal();
            this.Controls.Add(numGiamGia);

            lblTongTien = new Label { Text = "0 VNĐ", Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.Yellow, Location = new Point(600, 550), AutoSize = true };
            this.Controls.Add(lblTongTien);

            int btnY = 600;
            btnLuu = new Button { Text = "Lưu Hóa Đơn", Location = new Point(200, btnY), Size = new Size(130, 40), BackColor = Color.Green, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnLuu.Click += BtnLuu_Click;

            btnSua = new Button { Text = "Sửa Hóa Đơn", Location = new Point(340, btnY), Size = new Size(130, 40), BackColor = Color.Blue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Visible = false };
            btnSua.Click += BtnSua_Click;

            btnIn = new Button { Text = "In Hóa Đơn", Location = new Point(480, btnY), Size = new Size(130, 40), BackColor = Color.Orange, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Enabled = false };
            btnIn.Click += BtnIn_Click;

            btnHuy = new Button { Text = "Hủy / Đóng", Location = new Point(620, btnY), Size = new Size(130, 40), BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnHuy.Click += BtnHuy_Click;

            this.Controls.Add(btnLuu);
            this.Controls.Add(btnSua);
            this.Controls.Add(btnIn);
            this.Controls.Add(btnHuy);
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (_idHoaDonVuaLuu == 0)
            {
                MessageBox.Show("Không có hóa đơn để sửa!");
                return;
            }

            if (MessageBox.Show("Bạn có muốn sửa hóa đơn này không?\nLưu ý: Số lượng trong kho sẽ được điều chỉnh tự động.",
                "Xác nhận sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _isEditMode = true;
                
                // Cho phép chỉnh sửa
                cboKhachHang.Enabled = true;
                numGiamGia.Enabled = true;
                btnThemKhach.Visible = true;
                dgvChiTiet.ReadOnly = false;
                
                // Đổi nút
                btnSua.Visible = false;
                btnLuu.Visible = true;
                btnLuu.Text = "Cập nhật";
                btnLuu.BackColor = Color.Blue;
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            // Nếu đã lưu hóa đơn, hỏi có muốn xóa không
            if (_idHoaDonVuaLuu > 0)
            {
                if (MessageBox.Show("Bạn có muốn hủy hóa đơn này và cộng lại số lượng vào kho không?",
                    "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        if (_bll.HuyHoaDon(_idHoaDonVuaLuu))
                        {
                            MessageBox.Show("Đã hủy hóa đơn và cộng lại số lượng vào kho!");
                            this.DialogResult = DialogResult.Cancel;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Không thể hủy hóa đơn!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
            else
            {
                // Chưa lưu, chỉ đóng form
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void CreateLabel(Control p, string t, int x, int y) { p.Controls.Add(new Label { Text = t, Location = new Point(x, y), AutoSize = true }); }
        private TextBox CreateTextBox(Control p, int x, int y, int w) { var t = new TextBox { Location = new Point(x, y), Width = w }; p.Controls.Add(t); return t; }
    }
}