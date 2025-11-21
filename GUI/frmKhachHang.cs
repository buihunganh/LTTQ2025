using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BTL_LTTQ.BLL;
using ClosedXML.Excel;

namespace BTL_LTTQ.GUI
{
    public partial class frmKhachHang : Form
    {
        private KhachHangBLL bll = new KhachHangBLL();

        private readonly Color COLOR_BG = Color.FromArgb(45, 47, 72);
        private readonly Color COLOR_CARD = Color.FromArgb(58, 60, 92);
        private readonly Color COLOR_INPUT = Color.FromArgb(34, 37, 57);
        private readonly Color COLOR_ACCENT = Color.FromArgb(255, 111, 97);
        private readonly Color COLOR_SUCCESS = Color.FromArgb(0, 176, 155);
        private readonly Color COLOR_INFO = Color.FromArgb(86, 127, 232);
        private readonly Color COLOR_WARNING = Color.FromArgb(255, 173, 92);
        private readonly Color COLOR_DANGER = Color.FromArgb(232, 90, 79);
        private readonly Color COLOR_SECONDARY = Color.FromArgb(78, 80, 120);
        private readonly Color COLOR_TEXT = Color.White;
        private readonly Color COLOR_SUBTEXT = Color.Gainsboro;

        public frmKhachHang()
        {
            InitializeComponent();
            ApplyModernTheme();
            InitFilter(); 
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            if (cmbLocHang.Items.Count > 0) cmbLocHang.SelectedIndex = 0; 
        }

        private void InitFilter()
        {
            cmbLocHang.Items.Clear();
            cmbLocHang.Items.Add("Tất cả");
            cmbLocHang.Items.Add("Mới");
            cmbLocHang.Items.Add("Thành viên");
            cmbLocHang.Items.Add("Bạc");
            cmbLocHang.Items.Add("Vàng");
            cmbLocHang.Items.Add("Kim cương");

        }

        private void LoadData(string keyword = "")
        {
            string rankFilter = "";
            if (cmbLocHang.SelectedIndex > 0) 
            {
                rankFilter = cmbLocHang.SelectedItem.ToString();
            }

            dgvKhachHang.DataSource = bll.Search(keyword, rankFilter);

            dgvKhachHang.Columns["MaKH"].HeaderText = "Mã KH";
            dgvKhachHang.Columns["HoTen"].HeaderText = "Tên Khách Hàng";
            dgvKhachHang.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
            dgvKhachHang.Columns["TongChiTieu"].HeaderText = "Tổng Chi Tiêu";
            dgvKhachHang.Columns["TongChiTieu"].DefaultCellStyle.Format = "N0";
            dgvKhachHang.Columns["HangThanhVien"].HeaderText = "Hạng Thành Viên";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }

        private void cmbLocHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvKhachHang.Rows[e.RowIndex];
                txtMaKH.Text = row.Cells["MaKH"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtChiTieu.Text = string.Format("{0:N0} VND", row.Cells["TongChiTieu"].Value);
                txtHang.Text = row.Cells["HangThanhVien"].Value.ToString();
                txtChiTieu.ReadOnly = true;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtChiTieu.ReadOnly = false;

            if (string.IsNullOrWhiteSpace(txtHoTen.Text)) { MessageBox.Show("Nhập tên!"); return; }
            string cleanMoney = txtChiTieu.Text.Replace(" VND", "").Replace(",", "").Replace(".", "").Trim();
            decimal tongTien = 0;
            if(!string.IsNullOrEmpty(cleanMoney))
            {
                decimal.TryParse(cleanMoney, out tongTien);
            }
            if (bll.Add(txtHoTen.Text, txtSDT.Text, tongTien))
            {
                MessageBox.Show("Thêm thành công! Hạng thành viên đã được tính");
                LoadData();
                btnLamMoi_Click(null, null);
            }
            else MessageBox.Show("Lỗi: Thiếu thông tin.");
        }

        private void btnLuu_Click(object sender, EventArgs e) 
        {
            txtChiTieu.ReadOnly = false;
            if (string.IsNullOrEmpty(txtMaKH.Text)) return;
            if (bll.Edit(int.Parse(txtMaKH.Text), txtHoTen.Text, txtSDT.Text))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadData(txtSearch.Text);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text)) return;
            if (MessageBox.Show("Xóa khách hàng này?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (bll.Delete(int.Parse(txtMaKH.Text)))
                {
                    MessageBox.Show("Đã xóa.");
                    LoadData(txtSearch.Text);
                    btnLamMoi_Click(null, null);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaKH.Text = "";
            txtHoTen.Text = "";
            txtSDT.Text = "";
            txtChiTieu.Text = "";
            txtHang.Text = "";
            txtSearch.Text = "";
            if (cmbLocHang.Items.Count > 0) cmbLocHang.SelectedIndex = 0;
            LoadData();
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "Excel|*.xlsx", FileName = "KhachHang_" + DateTime.Now.ToString("ddMMyy") };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (var wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("KhachHang");
                    ws.Cell(1, 1).InsertTable(dgvKhachHang.DataSource as DataTable);
                    ws.Columns().AdjustToContents();
                    wb.SaveAs(sfd.FileName);
                }
                MessageBox.Show("Xuất file thành công!");
                System.Diagnostics.Process.Start(sfd.FileName);
            }
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text)) { MessageBox.Show("Vui lòng chọn khách hàng."); return; }
            MessageBox.Show($"Lịch sử mua hàng của: {txtHoTen.Text}\n(Chức năng này chờ module Hóa Đơn)");
        }

        private void txtChiTieu_TextChanged(object sender, EventArgs e)
        {

        }

        #region UI Helpers
        private void ApplyModernTheme()
        {
            this.DoubleBuffered = true;
            this.Font = new Font("Century Gothic", 10F, FontStyle.Regular);

            panelRoot.BackColor = COLOR_BG;
            panelRoot.Padding = new Padding(32, 28, 32, 32);

            lblTitle.Font = new Font("Century Gothic", 24F, FontStyle.Bold);
            lblTitle.ForeColor = COLOR_ACCENT;
            lblTitle.Padding = new Padding(0, 0, 0, 20);

            panelInfo.BackColor = COLOR_CARD;
            panelInfo.Padding = new Padding(24);
            panelInfo.BorderStyle = BorderStyle.None;

            panelButtons.BackColor = Color.FromArgb(49, 51, 78);
            panelButtons.Padding = new Padding(12, 24, 12, 24);

            grpFilter.BackColor = COLOR_CARD;
            grpFilter.ForeColor = COLOR_SUBTEXT;
            grpFilter.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            grpFilter.Padding = new Padding(18, 20, 18, 15);

            StyleTextBox(txtHoTen);
            StyleTextBox(txtSDT);
            StyleTextBox(txtChiTieu, true);
            StyleTextBox(txtHang, true);
            StyleTextBox(txtSearch);

            StyleCombo(cmbLocHang);

            StyleButton(btnThem, "➕  Thêm khách", COLOR_ACCENT);
            StyleButton(btnLuu, "💾  Lưu thay đổi", COLOR_INFO);
            StyleButton(btnXoa, "🗑  Xóa khách", COLOR_DANGER);
            StyleButton(btnLamMoi, "⟳  Làm mới", COLOR_SECONDARY);
            StyleButton(btnXuatFile, "📤  Xuất Excel", COLOR_WARNING);
            StyleButton(btnLichSu, "📜  Lịch sử mua hàng", COLOR_SUCCESS, 220);

            StyleDataGridView();
        }

        private void StyleTextBox(TextBox txt, bool isReadOnly = false)
        {
            txt.BackColor = COLOR_INPUT;
            txt.ForeColor = COLOR_TEXT;
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = new Font("Century Gothic", 10F, FontStyle.Regular);
            txt.ReadOnly = isReadOnly;
            txt.Margin = new Padding(0, 6, 0, 6);
            txt.Padding = new Padding(6, 4, 6, 4);
        }

        private void StyleCombo(ComboBox combo)
        {
            combo.FlatStyle = FlatStyle.Flat;
            combo.BackColor = COLOR_INPUT;
            combo.ForeColor = COLOR_TEXT;
            combo.Font = new Font("Century Gothic", 10F, FontStyle.Regular);
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void StyleButton(Button button, string text, Color color, int width = 180)
        {
            button.Text = text;
            button.Width = width;
            button.Height = 44;
            button.BackColor = color;
            button.ForeColor = Color.White;
            button.Font = new Font("Century Gothic", 10F, FontStyle.Bold);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            button.Margin = new Padding(0, 6, 0, 6);
            button.Padding = new Padding(8, 4, 8, 4);
        }

        private void StyleDataGridView()
        {
            dgvKhachHang.BackgroundColor = COLOR_CARD;
            dgvKhachHang.BorderStyle = BorderStyle.None;
            dgvKhachHang.EnableHeadersVisualStyles = false;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.BackColor = COLOR_ACCENT;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.ForeColor = COLOR_TEXT;
            dgvKhachHang.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10.5F, FontStyle.Bold);
            dgvKhachHang.ColumnHeadersHeight = 48;
            dgvKhachHang.DefaultCellStyle.BackColor = COLOR_BG;
            dgvKhachHang.DefaultCellStyle.ForeColor = COLOR_TEXT;
            dgvKhachHang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(78, 80, 110);
            dgvKhachHang.DefaultCellStyle.SelectionForeColor = COLOR_TEXT;
            dgvKhachHang.RowTemplate.Height = 42;
            dgvKhachHang.GridColor = Color.FromArgb(70, 72, 105);
        }
        #endregion
    }
}