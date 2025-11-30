using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BTL_LTTQ.BLL;

namespace BTL_LTTQ.GUI
{
    public partial class frmBanHang : Form
    {
        private SalesBLL _bll = new SalesBLL();
        private DataTable _dtGioHang;
        private BTL_LTTQ.DTO.LoginResult _currentUser;
        private DataTable _dtSanPham; // Store all products for filtering

        public frmBanHang(BTL_LTTQ.DTO.LoginResult currentUser = null)
        {
            _currentUser = currentUser;
            InitializeComponent();
            this.Load += frmBanHang_Load;
            InitGioHang();
        }

        public frmBanHang() : this(null)
        {
        }

        private void InitGioHang()
        {
            _dtGioHang = new DataTable();
            _dtGioHang.Columns.Add("MaCTSP", typeof(int));
            _dtGioHang.Columns.Add("TenSP", typeof(string));
            _dtGioHang.Columns.Add("SoLuong", typeof(int));
            _dtGioHang.Columns.Add("DonGia", typeof(decimal));
            _dtGioHang.Columns.Add("GiamGia", typeof(int));
            _dtGioHang.Columns.Add("ThanhTien", typeof(decimal));

            dgvGioHang.DataSource = _dtGioHang;
            
            // Set HeaderText tiếng Việt có dấu
            SetGridViewHeaders();
            
            CalculateTotal();
        }
        
        private void SetGridViewHeaders()
        {
            if (dgvGioHang.Columns["MaCTSP"] != null) 
                dgvGioHang.Columns["MaCTSP"].Visible = false;
            
            if (dgvGioHang.Columns["TenSP"] != null)
                dgvGioHang.Columns["TenSP"].HeaderText = "Tên sản phẩm";
            
            if (dgvGioHang.Columns["SoLuong"] != null)
                dgvGioHang.Columns["SoLuong"].HeaderText = "Số lượng";
            
            if (dgvGioHang.Columns["DonGia"] != null)
                dgvGioHang.Columns["DonGia"].HeaderText = "Đơn giá";
            
            if (dgvGioHang.Columns["GiamGia"] != null)
                dgvGioHang.Columns["GiamGia"].HeaderText = "Giảm giá (%)";
            
            if (dgvGioHang.Columns["ThanhTien"] != null)
                dgvGioHang.Columns["ThanhTien"].HeaderText = "Thành tiền";
        }

        private void CalculateTotal()
        {
            decimal total = 0;
            foreach (DataRow row in _dtGioHang.Rows)
            {
                total += Convert.ToDecimal(row["ThanhTien"]);
            }
            lblTongTien.Text = $"Tổng tiền: {total:N0} VNĐ";
        }

        private void frmBanHang_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadProductCards();
        }

        private void LoadData()
        {
            _dtSanPham = _bll.GetSanPhamBanHang();
            
            // Đảm bảo HeaderText được set lại sau khi load
            SetGridViewHeaders();
        }

        private void LoadProductCards(string searchText = "")
        {
            pnlProducts.Controls.Clear();

            if (_dtSanPham == null || _dtSanPham.Rows.Count == 0) return;

            DataRow[] filteredRows;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                filteredRows = _dtSanPham.Select();
            }
            else
            {
                string filter = $"TenHienThi LIKE '%{searchText.Replace("'", "''")}%'";
                filteredRows = _dtSanPham.Select(filter);
            }

            foreach (DataRow row in filteredRows)
            {
                Panel card = CreateProductCard(row);
                pnlProducts.Controls.Add(card);
            }
        }

        private Panel CreateProductCard(DataRow product)
        {
            Panel card = new Panel
            {
                Width = 180,
                Height = 200,
                Margin = new Padding(10),
                BackColor = Color.FromArgb(231, 111, 81),
                Cursor = Cursors.Hand
            };

            // Product Image Placeholder
            PictureBox picProduct = new PictureBox
            {
                Width = 160,
                Height = 120,
                Location = new Point(10, 10),
                BackColor = Color.White,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = LoadProductImage(product)
            };

            // Product Name
            Label lblName = new Label
            {
                Width = 160,
                Location = new Point(10, 135),
                Height = 35,
                Text = $"[{product["MaCTSP"]}] {product["TenHienThi"]}",  // ← Hiển thị MaCTSP để debug
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Product Price
            Label lblPrice = new Label
            {
                Width = 160,
                Location = new Point(10, 170),
                Height = 25,
                Text = $"{Convert.ToDecimal(product["GiaBan"]):N0} VNĐ",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 221, 89),
                TextAlign = ContentAlignment.MiddleCenter
            };

            card.Controls.Add(picProduct);
            card.Controls.Add(lblName);
            card.Controls.Add(lblPrice);

            // Click event for entire card
            card.Click += (s, e) => ProductCard_Click(product);
            picProduct.Click += (s, e) => ProductCard_Click(product);
            lblName.Click += (s, e) => ProductCard_Click(product);
            lblPrice.Click += (s, e) => ProductCard_Click(product);

            // Hover effect
            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(252, 125, 116);
            card.MouseLeave += (s, e) => card.BackColor = Color.FromArgb(231, 111, 81);

            return card;
        }

        private Image LoadProductImage(DataRow product)
        {
            try
            {
                // Lấy đường dẫn ảnh từ database
                string imagePath = product["HinhAnhChung"] != DBNull.Value 
                    ? product["HinhAnhChung"].ToString() 
                    : "";
                
                if (string.IsNullOrWhiteSpace(imagePath))
                {
                    // Nếu không có ảnh trong database, dùng placeholder
                    return GeneratePlaceholderImage(product["TenHienThi"].ToString());
                }
                
                // Chuyển đổi relative path thành full path
                string fullPath = GetFullImagePath(imagePath);
                
                // Kiểm tra file có tồn tại không
                if (System.IO.File.Exists(fullPath))
                {
                    // Load image vào memory để tránh file lock
                    using (var fileStream = new System.IO.FileStream(fullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
                    {
                        var memoryStream = new System.IO.MemoryStream();
                        fileStream.CopyTo(memoryStream);
                        return Image.FromStream(memoryStream);
                    }
                }
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, không hiển thị message để tránh làm phiền người dùng
                System.Diagnostics.Debug.WriteLine($"Error loading image: {ex.Message}");
            }
            
            // Fallback to placeholder if anything goes wrong
            return GeneratePlaceholderImage(product["TenHienThi"].ToString());
        }
        
        private string GetFullImagePath(string relativeOrFullPath)
        {
            if (string.IsNullOrWhiteSpace(relativeOrFullPath))
                return string.Empty;

            // Nếu đã là full path, return ngay
            if (System.IO.Path.IsPathRooted(relativeOrFullPath))
                return relativeOrFullPath;

            // Convert relative -> full
            string projectRoot = System.IO.Path.GetFullPath(
                System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));
            return System.IO.Path.Combine(projectRoot, relativeOrFullPath);
        }

        private Image GeneratePlaceholderImage(string productName)
        {
            // Create a simple placeholder image with product initial
            Bitmap bmp = new Bitmap(160, 120);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(42, 157, 143));
                
                // Draw product initial or icon
                string initial = productName.Length > 0 ? productName.Substring(0, 1).ToUpper() : "?";
                using (Font font = new Font("Segoe UI", 48F, FontStyle.Bold))
                {
                    SizeF textSize = g.MeasureString(initial, font);
                    PointF point = new PointF((160 - textSize.Width) / 2, (120 - textSize.Height) / 2);
                    g.DrawString(initial, font, Brushes.White, point);
                }
            }
            return bmp;
        }

        private void ProductCard_Click(DataRow product)
        {
            int tonKho = Convert.ToInt32(product["SoLuongTon"]);
            if (tonKho <= 0)
            {
                MessageBox.Show("Sản phẩm này hiện đã hết hàng!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Show dialog to input quantity and discount
            using (Form dialog = new Form())
            {
                dialog.Text = "Thêm vào giỏ hàng";
                dialog.Size = new Size(350, 250);
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;
                dialog.BackColor = Color.FromArgb(55, 57, 82);

                Label lblProduct = new Label
                {
                    Text = product["TenHienThi"].ToString(),
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = Color.White,
                    Location = new Point(20, 20),
                    AutoSize = true
                };

                Label lblSL = new Label
                {
                    Text = $"Số lượng (Tồn: {tonKho}):",
                    ForeColor = Color.Gainsboro,
                    Location = new Point(20, 60),
                    AutoSize = true
                };

                NumericUpDown numSL = new NumericUpDown
                {
                    Location = new Point(20, 85),
                    Width = 290,
                    Minimum = 1,
                    Maximum = tonKho,
                    Value = 1
                };

                Label lblGG = new Label
                {
                    Text = "Giảm giá (%):",
                    ForeColor = Color.Gainsboro,
                    Location = new Point(20, 115),
                    AutoSize = true
                };

                NumericUpDown numGG = new NumericUpDown
                {
                    Location = new Point(20, 140),
                    Width = 290,
                    Maximum = 100,
                    Value = 0
                };

                Button btnOK = new Button
                {
                    Text = "Thêm vào giỏ",
                    Location = new Point(20, 175),
                    Width = 140,
                    Height = 35,
                    BackColor = Color.FromArgb(38, 166, 91),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    DialogResult = DialogResult.OK
                };
                btnOK.FlatAppearance.BorderSize = 0;

                Button btnCancel = new Button
                {
                    Text = "Hủy",
                    Location = new Point(170, 175),
                    Width = 140,
                    Height = 35,
                    BackColor = Color.Gray,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F),
                    DialogResult = DialogResult.Cancel
                };
                btnCancel.FlatAppearance.BorderSize = 0;

                dialog.Controls.AddRange(new Control[] { 
                    lblProduct, lblSL, numSL, lblGG, numGG, btnOK, btnCancel 
                });

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    AddToCart(product, (int)numSL.Value, (int)numGG.Value);
                }
            }
        }

        private void AddToCart(DataRow product, int quantity, int discount)
        {
            int maCTSP = Convert.ToInt32(product["MaCTSP"]);
            string tenSP = product["TenHienThi"].ToString();
            decimal giaBan = Convert.ToDecimal(product["GiaBan"]);

            decimal tienGiam = (giaBan * quantity) * discount / 100;
            decimal thanhTien = (giaBan * quantity) - tienGiam;

            // Check if product already in cart with same discount
            foreach (DataRow r in _dtGioHang.Rows)
            {
                if ((int)r["MaCTSP"] == maCTSP && (int)r["GiamGia"] == discount)
                {
                    r["SoLuong"] = (int)r["SoLuong"] + quantity;
                    r["ThanhTien"] = (decimal)r["ThanhTien"] + thanhTien;
                    CalculateTotal();
                    return;
                }
            }

            _dtGioHang.Rows.Add(maCTSP, tenSP, quantity, giaBan, discount, thanhTien);
            CalculateTotal();
        }

        private void DgvGioHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (MessageBox.Show("Xóa sản phẩm này khỏi giỏ hàng?", "Xác nhận", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _dtGioHang.Rows.RemoveAt(e.RowIndex);
                    CalculateTotal();
                }
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (_dtGioHang.Rows.Count == 0) 
            { 
                MessageBox.Show("Giỏ hàng trống!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return; 
            }

            frmHoaDon f = new frmHoaDon(_dtGioHang, _currentUser);
            f.ShowDialog();

            if (f.DialogResult == DialogResult.OK)
            {
                _dtGioHang.Rows.Clear();
                CalculateTotal();
                // Reload dữ liệu sản phẩm từ database để cập nhật số lượng tồn kho
                LoadData();
                LoadProductCards(txtSearch.Text);
            }
        }

        // Search functionality
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.ForeColor == Color.Gray) return; // Ignore placeholder text
            
            LoadProductCards(txtSearch.Text);
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Tìm kiếm sản phẩm...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Tìm kiếm sản phẩm...";
                txtSearch.ForeColor = Color.Gray;
            }
        }
    }
}
