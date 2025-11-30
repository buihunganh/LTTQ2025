using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BTL_LTTQ.BLL;
using BTL_LTTQ.DTO;

namespace BTL_LTTQ
{
    public partial class frmSanpham : Form
    {
        private readonly ProductService _productService;
        private int _currentMaCTSP;
        private bool _isLoadingComboBoxes = false;
        private string _currentSelectedBrand = null; // Lưu hãng hiện tại được chọn

        public frmSanpham()
        {
            InitializeComponent();
            _productService = IsInDesignMode() ? null : new ProductService();
        }

        private void frmSanpham_Load(object sender, EventArgs e)
        {
            if (IsInDesignMode())
            {
                return;
            }

            LoadComboBoxes();
            LoadProducts();
            ResetForm();
        }

        private void LoadComboBoxes()
        {
            try
            {
                _isLoadingComboBoxes = true;

                // Load hãng giày (từ tên sản phẩm - từ đầu tiên)
                var allProducts = _productService.GetAllProducts();
                var brands = allProducts
                    .Select(p => p.TenGiay?.Split(' ').FirstOrDefault())
                    .Where(b => !string.IsNullOrWhiteSpace(b))
                    .Distinct()
                    .OrderBy(b => b)
                    .ToList();

                var brandsTable = new DataTable();
                brandsTable.Columns.Add("TenHang", typeof(string));

                // Thêm option "Tất cả"
                var allBrandRow = brandsTable.NewRow();
                allBrandRow["TenHang"] = "Tất cả";
                brandsTable.Rows.Add(allBrandRow);

                // Thêm các hãng
                foreach (var brand in brands)
                {
                    var row = brandsTable.NewRow();
                    row["TenHang"] = brand;
                    brandsTable.Rows.Add(row);
                }

                cmbProduct.DataSource = brandsTable;
                cmbProduct.DisplayMember = "TenHang";
                cmbProduct.ValueMember = "TenHang";

                // Load loại giày
                var loaiTable = _productService.GetLoaiGiay();
                if (loaiTable != null && loaiTable.Rows.Count > 0)
                {
                    cmbLoai.DataSource = loaiTable.Copy();
                    cmbLoai.DisplayMember = "TenLoai";
                    cmbLoai.ValueMember = "MaLoai";

                    var loaiFilterTable = loaiTable.Copy();
                    var allLoaiRow = loaiFilterTable.NewRow();
                    allLoaiRow["MaLoai"] = -1;
                    allLoaiRow["TenLoai"] = "Tất cả";
                    loaiFilterTable.Rows.InsertAt(allLoaiRow, 0);
                    cmbFilterLoai.DataSource = loaiFilterTable;
                    cmbFilterLoai.DisplayMember = "TenLoai";
                    cmbFilterLoai.ValueMember = "MaLoai";
                }

                // Load size giày
                var sizeTable = _productService.GetSizeGiay();
                if (sizeTable != null && sizeTable.Rows.Count > 0)
                {
                    cmbSize.DataSource = sizeTable.Copy();
                    cmbSize.DisplayMember = "KichCo";
                    cmbSize.ValueMember = "MaSize";

                    var sizeFilterTable = sizeTable.Copy();
                    var allSizeRow = sizeFilterTable.NewRow();
                    allSizeRow["MaSize"] = -1;
                    allSizeRow["KichCo"] = "Tất cả";
                    sizeFilterTable.Rows.InsertAt(allSizeRow, 0);
                    cmbFilterSize.DataSource = sizeFilterTable;
                    cmbFilterSize.DisplayMember = "KichCo";
                    cmbFilterSize.ValueMember = "MaSize";
                }

                // Load màu sắc
                var mauTable = _productService.GetMauSac();
                if (mauTable != null && mauTable.Rows.Count > 0)
                {
                    cmbColor.DataSource = mauTable.Copy();
                    cmbColor.DisplayMember = "TenMau";
                    cmbColor.ValueMember = "MaMau";
                }

                // Load trạng thái filter
                if (cmbFilterStatus.Items.Count == 0)
                {
                    cmbFilterStatus.Items.Add("Tất cả");
                    cmbFilterStatus.Items.Add("Đang kinh doanh");
                    cmbFilterStatus.Items.Add("Ngừng kinh doanh");
                    cmbFilterStatus.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}\n\nChi tiết: {ex.InnerException?.Message ?? ex.ToString()}",
                    "Lỗi kết nối database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoadingComboBoxes = false;
            }
        }

        private void LoadProducts()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var products = _productService.GetAllProducts();
                BindDataGridView(products);

                if (products == null || products.Count == 0)
                {
                    // Không hiển thị message nếu đang filter
                    // MessageBox.Show("Không có sản phẩm nào trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách sản phẩm: {ex.Message}\n\nChi tiết: {ex.InnerException?.Message ?? ex.ToString()}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LoadProductsByBrandPrefix(string brandPrefix)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                System.Diagnostics.Debug.WriteLine($"LoadProductsByBrandPrefix called with brandPrefix: '{brandPrefix}'");
                
                // Lấy tất cả sản phẩm và filter theo prefix
                var allProducts = _productService.GetAllProducts();
                var filteredProducts = allProducts
                    .Where(p => p.TenGiay?.StartsWith(brandPrefix, StringComparison.OrdinalIgnoreCase) == true)
                    .ToList();
                
                System.Diagnostics.Debug.WriteLine($"LoadProductsByBrandPrefix found {filteredProducts.Count} products");
                BindDataGridView(filteredProducts);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in LoadProductsByBrandPrefix: {ex.Message}");
                MessageBox.Show($"Lỗi khi lọc sản phẩm theo hãng: {ex.Message}\n\nChi tiết: {ex.InnerException?.Message ?? ex.ToString()}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void BindDataGridView(System.Collections.Generic.List<ChiTietSanPhamDTO> products)
        {
            dgvProducts.DataSource = null;
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Columns.Clear();

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaSKU",
                HeaderText = "Mã giày",
                Name = "MaSKU",
                Width = 120
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenGiay",
                HeaderText = "Tên giày",
                Name = "TenGiay",
                Width = 200
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "KichCo",
                HeaderText = "Size",
                Name = "Size",
                Width = 60
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenMau",
                HeaderText = "Màu",
                Name = "Mau",
                Width = 100
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenLoai",
                HeaderText = "Loại",
                Name = "Loai",
                Width = 120
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GiaNhap",
                HeaderText = "Giá nhập",
                Name = "GiaNhap",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GiaBan",
                HeaderText = "Giá bán",
                Name = "GiaBan",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoLuongTon",
                HeaderText = "Số lượng",
                Name = "SoLuongTon",
                Width = 80
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HinhAnhChung",
                HeaderText = "Hình ảnh",
                Name = "HinhAnh",
                Width = 150
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaCTSP",
                HeaderText = "MaCTSP",
                Name = "MaCTSP",
                Visible = false
            });

            // Gán dữ liệu
            dgvProducts.DataSource = products;

            // ====== AUTO SIZE ĐỂ HẾT THANH CUỘN NGANG ======
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Điều chỉnh tỉ lệ rộng từng cột (tùy ý)
            dgvProducts.Columns["MaSKU"].FillWeight = 80;
            dgvProducts.Columns["TenGiay"].FillWeight = 180;
            dgvProducts.Columns["Size"].FillWeight = 40;
            dgvProducts.Columns["Mau"].FillWeight = 70;
            dgvProducts.Columns["Loai"].FillWeight = 90;
            dgvProducts.Columns["GiaNhap"].FillWeight = 70;
            dgvProducts.Columns["GiaBan"].FillWeight = 70;
            dgvProducts.Columns["SoLuongTon"].FillWeight = 50;
            dgvProducts.Columns["HinhAnh"].FillWeight = 110;
            // ================================================

            // Style như cũ
            dgvProducts.DefaultCellStyle.ForeColor = Color.White;
            dgvProducts.DefaultCellStyle.BackColor = Color.FromArgb(55, 57, 82);
            dgvProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(50, 52, 78);
            dgvProducts.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(58, 60, 92);
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.GridColor = Color.FromArgb(70, 72, 98);
            dgvProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(102, 106, 148);
            dgvProducts.DefaultCellStyle.SelectionForeColor = Color.White;
        }


        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvProducts.Rows[e.RowIndex];

            // Safely convert MaCTSP
            var maCTSPValue = row.Cells["MaCTSP"].Value;
            if (maCTSPValue != null)
            {
                if (maCTSPValue is int intValue)
                    _currentMaCTSP = intValue;
                else if (maCTSPValue is long longValue)
                    _currentMaCTSP = (int)longValue;
                else if (int.TryParse(maCTSPValue.ToString(), out var parsed))
                    _currentMaCTSP = parsed;
                else
                    _currentMaCTSP = 0;
            }
            else
            {
                _currentMaCTSP = 0;
            }

            // Load thông tin vào form
            // Lấy tên hãng từ tên sản phẩm (bắt đầu với tên hãng)
            var tenGiay = row.Cells["TenGiay"].Value?.ToString() ?? "";
            txtProductName.Text = tenGiay;

            // Tự động chọn hãng dựa trên tên sản phẩm (từ đầu tiên)
            string brandName = tenGiay?.Split(' ').FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(brandName))
            {
                try
                {
                    // Tạm thời tắt event để tránh trigger lại khi set SelectedValue
                    var wasLoading = _isLoadingComboBoxes;
                    _isLoadingComboBoxes = true;
                    cmbProduct.SelectedValue = brandName;
                    _currentSelectedBrand = brandName; // Cập nhật hãng hiện tại
                    _isLoadingComboBoxes = wasLoading;
                }
                catch
                {
                    // Nếu không tìm thấy trong ComboBox, không làm gì
                }
            }

            txtProductCode.Text = row.Cells["MaSKU"].Value?.ToString() ?? "";
            cmbSize.SelectedValue = GetMaSizeFromRow(row);
            cmbColor.SelectedValue = GetMaMauFromRow(row);
            cmbLoai.SelectedValue = GetMaLoaiFromRow(row);

            if (row.Cells["GiaNhap"].Value != null && decimal.TryParse(row.Cells["GiaNhap"].Value.ToString(), out var giaNhap))
                txtImportPrice.Text = giaNhap.ToString("N0");
            else
                txtImportPrice.Clear();

            if (row.Cells["GiaBan"].Value != null && decimal.TryParse(row.Cells["GiaBan"].Value.ToString(), out var giaBan))
                txtSellingPrice.Text = giaBan.ToString("N0");
            else
                txtSellingPrice.Clear();

            if (row.Cells["SoLuongTon"].Value != null && int.TryParse(row.Cells["SoLuongTon"].Value.ToString(), out var soLuong))
                txtQuantity.Text = soLuong.ToString();
            else
                txtQuantity.Clear();

            string imagePath = row.Cells["HinhAnh"].Value?.ToString() ?? "";
            txtImagePath.Text = GetRelativeImagePath(imagePath);  // Hiển thị relative path
            LoadProductImage(imagePath);  // Load bằng path gốc từ DB
        }

        private int GetMaSPFromRow(DataGridViewRow row)
        {
            try
            {
                var products = _productService.GetSanPham();
                if (products == null || products.Rows.Count == 0)
                    return -1;

                var tenGiay = row.Cells["TenGiay"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(tenGiay))
                    return -1;

                var product = products.AsEnumerable()
                    .FirstOrDefault(r =>
                    {
                        var rowTenGiay = r["TenGiay"];
                        return rowTenGiay != DBNull.Value && rowTenGiay.ToString() == tenGiay;
                    });

                if (product == null)
                    return -1;

                var maSP = product["MaSP"];
                if (maSP == DBNull.Value)
                    return -1;

                if (maSP is int intValue)
                    return intValue;
                if (maSP is long longValue)
                    return (int)longValue;
                if (int.TryParse(maSP.ToString(), out var parsed))
                    return parsed;

                return -1;
            }
            catch
            {
                return -1;
            }
        }

        private int GetMaSizeFromRow(DataGridViewRow row)
        {
            try
            {
                var sizes = _productService.GetSizeGiay();
                if (sizes == null || sizes.Rows.Count == 0)
                    return -1;

                var kichCo = row.Cells["Size"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(kichCo))
                    return -1;

                var size = sizes.AsEnumerable()
                    .FirstOrDefault(r =>
                    {
                        var rowKichCo = r["KichCo"];
                        return rowKichCo != DBNull.Value && rowKichCo.ToString() == kichCo;
                    });

                if (size == null)
                    return -1;

                var maSize = size["MaSize"];
                if (maSize == DBNull.Value)
                    return -1;

                if (maSize is int intValue)
                    return intValue;
                if (maSize is long longValue)
                    return (int)longValue;
                if (int.TryParse(maSize.ToString(), out var parsed))
                    return parsed;

                return -1;
            }
            catch
            {
                return -1;
            }
        }

        private int GetMaMauFromRow(DataGridViewRow row)
        {
            try
            {
                var maus = _productService.GetMauSac();
                if (maus == null || maus.Rows.Count == 0)
                    return -1;

                var tenMau = row.Cells["Mau"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(tenMau))
                    return -1;

                var mau = maus.AsEnumerable()
                    .FirstOrDefault(r =>
                    {
                        var rowTenMau = r["TenMau"];
                        return rowTenMau != DBNull.Value && rowTenMau.ToString() == tenMau;
                    });

                if (mau == null)
                    return -1;

                var maMau = mau["MaMau"];
                if (maMau == DBNull.Value)
                    return -1;

                if (maMau is int intValue)
                    return intValue;
                if (maMau is long longValue)
                    return (int)longValue;
                if (int.TryParse(maMau.ToString(), out var parsed))
                    return parsed;

                return -1;
            }
            catch
            {
                return -1;
            }
        }

        private int GetMaLoaiFromRow(DataGridViewRow row)
        {
            try
            {
                var loais = _productService.GetLoaiGiay();
                if (loais == null || loais.Rows.Count == 0)
                    return -1;

                var tenLoai = row.Cells["Loai"].Value?.ToString();
                if (string.IsNullOrWhiteSpace(tenLoai))
                    return -1;

                var loai = loais.AsEnumerable()
                    .FirstOrDefault(r =>
                    {
                        var rowTenLoai = r["TenLoai"];
                        return rowTenLoai != DBNull.Value && rowTenLoai.ToString() == tenLoai;
                    });

                if (loai == null)
                    return -1;

                var maLoai = loai["MaLoai"];
                if (maLoai == DBNull.Value)
                    return -1;

                if (maLoai is int intValue)
                    return intValue;
                if (maLoai is long longValue)
                    return (int)longValue;
                if (int.TryParse(maLoai.ToString(), out var parsed))
                    return parsed;

                return -1;
            }
            catch
            {
                return -1;
            }
        }

        private void LoadProductImage(string imagePath)
        {
            // Dispose of old image first
            if (picProductImage.Image != null)
            {
                picProductImage.Image.Dispose();
                picProductImage.Image = null;
            }

            if (string.IsNullOrWhiteSpace(imagePath))
            {
                return;
            }

            try
            {
                // Convert sang full path nếu cần
                string fullPath = GetFullImagePath(imagePath);
                
                if (File.Exists(fullPath))
                {
                    // Load image into memory to avoid file locking
                    using (var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        var memoryStream = new MemoryStream();
                        fileStream.CopyTo(memoryStream);
                        picProductImage.Image = Image.FromStream(memoryStream);
                    }
                }
            }
            catch
            {
                picProductImage.Image = null;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                int maLoai = 0;
                if (cmbLoai.SelectedValue != null)
                {
                    if (cmbLoai.SelectedValue is int intLoai)
                        maLoai = intLoai;
                    else if (cmbLoai.SelectedValue is long longLoai)
                        maLoai = (int)longLoai;
                    else
                        int.TryParse(cmbLoai.SelectedValue.ToString(), out maLoai);
                }

                var tenGiay = txtProductName.Text.Trim();
                int maSpBase = 0;

                // Nếu có tên giày + loại => đảm bảo có bản ghi trong bảng SanPham
                if (!string.IsNullOrWhiteSpace(tenGiay) && maLoai > 0)
                {
                    var moTa = txtDescription.Text.Trim();
                    var hinhAnh = txtImagePath.Text.Trim();
                    maSpBase = _productService.EnsureBaseProduct(tenGiay, maLoai, moTa, hinhAnh);
                }

                var product = CreateProductFromForm();
                if (maSpBase > 0)
                    product.MaSP = maSpBase;

                if (_productService.AddProduct(product))
                {
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reload theo hãng hiện tại (nếu có)
                    ReloadProductsWithCurrentBrand();
                    LoadComboBoxes();    // reload combobox hãng để có hãng mới
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Thêm sản phẩm thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"✗ EXCEPTION in btnAdd_Click: {ex.GetType().Name}");
                System.Diagnostics.Debug.WriteLine($"Message: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"InnerException: {ex.InnerException.Message}");
                }
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_currentMaCTSP == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput())
                return;

            try
            {
                var product = CreateProductFromForm();
                product.MaCTSP = _currentMaCTSP;
                if (_productService.UpdateProduct(product))
                {
                    // Update image if changed
                    if (!string.IsNullOrWhiteSpace(txtImagePath.Text) && product.MaSP > 0)
                    {
                        _productService.UpdateProductImage(product.MaSP, txtImagePath.Text);
                    }
                    
                    MessageBox.Show("Sửa sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Reload theo hãng hiện tại (nếu có)
                    ReloadProductsWithCurrentBrand();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Sửa sản phẩm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_currentMaCTSP == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?\nẢnh của sản phẩm cũng sẽ bị xóa.", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            try
            {
                // Delete product from database and get its image path
                var (success, imagePath) = _productService.DeleteProduct(_currentMaCTSP);
                
                if (success)
                {
                    // Delete image file if it exists
                    if (!string.IsNullOrWhiteSpace(imagePath))
                    {
                        try
                        {
                            // CRITICAL: Dispose image first to release file lock!
                            if (picProductImage.Image != null)
                            {
                                picProductImage.Image.Dispose();
                                picProductImage.Image = null;
                            }

                            // Convert to full path if needed
                            string fullPath = imagePath;
                            if (!Path.IsPathRooted(imagePath))
                            {
                                string projectRoot = Directory.GetParent(Application.StartupPath).Parent.FullName;
                                fullPath = Path.Combine(projectRoot, imagePath);
                            }

                            // Delete file if exists
                            if (File.Exists(fullPath))
                            {
                                File.Delete(fullPath);
                            }
                        }
                        catch (Exception imgEx)
                        {
                            MessageBox.Show($"Đã xóa sản phẩm nhưng không xóa được ảnh: {imgEx.Message}", 
                                "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                    MessageBox.Show("Xóa sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadProductsWithCurrentBrand();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Xóa sản phẩm thất bại! (success = false)", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetForm();
            // Reload theo hãng hiện tại (nếu có)
            ReloadProductsWithCurrentBrand();
        }
        
        /// <summary>
        /// Reload sản phẩm theo hãng hiện tại đang được chọn
        /// </summary>
        private void ReloadProductsWithCurrentBrand()
        {
            if (!string.IsNullOrWhiteSpace(_currentSelectedBrand) && 
                _currentSelectedBrand != "Tất cả" && 
                _currentSelectedBrand != "-1")
            {
                LoadProductsByBrandPrefix(_currentSelectedBrand);
            }
            else
            {
                LoadProducts();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra có dữ liệu không
                if (dgvProducts.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var saveDialog = new SaveFileDialog
                {
                    Filter = "CSV Files|*.csv",
                    FileName = $"DanhSachSanPham_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                    Title = "Xuất dữ liệu ra file CSV"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    try
                    {
                        // Xuất ra CSV
                        using (var writer = new StreamWriter(saveDialog.FileName, false, System.Text.Encoding.UTF8))
                        {
                            // Ghi header
                            var headers = new List<string>();
                            foreach (DataGridViewColumn col in dgvProducts.Columns)
                            {
                                // Bỏ qua cột ẩn và cột MaCTSP
                                if (col.Visible && col.Name != "MaCTSP")
                                {
                                    headers.Add($"\"{col.HeaderText}\"");
                                }
                            }
                            writer.WriteLine(string.Join(",", headers));

                            // Ghi dữ liệu
                            foreach (DataGridViewRow row in dgvProducts.Rows)
                            {
                                if (row.IsNewRow) continue;

                                var cells = new List<string>();
                                foreach (DataGridViewColumn col in dgvProducts.Columns)
                                {
                                    if (col.Visible && col.Name != "MaCTSP")
                                    {
                                        var value = row.Cells[col.Index].Value?.ToString() ?? "";
                                        // Escape dấu ngoặc kép và bọc trong quotes
                                        value = $"\"{value.Replace("\"", "\"\"")}\"";
                                        cells.Add(value);
                                    }
                                }
                                writer.WriteLine(string.Join(",", cells));
                            }
                        }

                        Cursor = Cursors.Default;

                        // Thông báo thành công và hỏi có muốn mở file không
                        var result = MessageBox.Show(
                            $"Xuất file thành công!\n\nĐường dẫn:\n{saveDialog.FileName}\n\nBạn có muốn mở file không?",
                            "Thành công",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                        if (result == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(saveDialog.FileName);
                        }
                    }
                    catch (Exception exportEx)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show($"Lỗi khi xuất file:\n{exportEx.Message}", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void Filter_Changed(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tránh xử lý event trong quá trình load dữ liệu
            if (_isLoadingComboBoxes)
                return;

            if (cmbProduct.SelectedValue == null)
                return;

            try
            {
                // Lấy tên hãng từ SelectedValue
                string selectedBrand = cmbProduct.SelectedValue?.ToString()?.Trim();

                // Debug: Hiển thị giá trị để kiểm tra
                System.Diagnostics.Debug.WriteLine($"Selected Brand: '{selectedBrand}' | SelectedIndex: {cmbProduct.SelectedIndex}");

                // Nếu chọn "Tất cả" hoặc không có giá trị, load tất cả sản phẩm
                if (string.IsNullOrWhiteSpace(selectedBrand) || selectedBrand == "Tất cả")
                {
                    _currentSelectedBrand = null; // Reset filter hãng
                    System.Diagnostics.Debug.WriteLine("Loading all products (Tất cả selected)");
                    LoadProducts();
                }
                else
                {
                    // Lưu hãng hiện tại được chọn
                    _currentSelectedBrand = selectedBrand;
                    System.Diagnostics.Debug.WriteLine($"Filtering by brand prefix: '{selectedBrand}'");
                    // Lọc sản phẩm theo prefix của hãng
                    LoadProductsByBrandPrefix(selectedBrand);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc sản phẩm theo hãng: {ex.Message}\n\nChi tiết: {ex.InnerException?.Message ?? ex.ToString()}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilters()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // Nếu có hãng được chọn, áp dụng filter theo hãng trước
                if (!string.IsNullOrWhiteSpace(_currentSelectedBrand) && 
                    _currentSelectedBrand != "Tất cả" && 
                    _currentSelectedBrand != "-1")
                {
                    // Lấy danh sách sản phẩm theo hãng trước
                    var productsByBrand = _productService.GetProductsByBrand(_currentSelectedBrand);
                    
                    // Sau đó áp dụng các filter khác (search, size, loại) trên danh sách đã filter theo hãng
                    var searchText = txtSearch.Text.Trim();
                    int? maSize = null;
                    int? maLoai = null;
                    
                    if (cmbFilterSize.SelectedValue != null)
                    {
                        int sizeValue;
                        if (cmbFilterSize.SelectedValue is int intSize)
                            sizeValue = intSize;
                        else if (cmbFilterSize.SelectedValue is long longSize)
                            sizeValue = (int)longSize;
                        else if (int.TryParse(cmbFilterSize.SelectedValue.ToString(), out var parsedSize))
                            sizeValue = parsedSize;
                        else
                            sizeValue = -1;

                        if (sizeValue != -1)
                            maSize = sizeValue;
                    }

                    if (cmbFilterLoai.SelectedValue != null)
                    {
                        int loaiValue;
                        if (cmbFilterLoai.SelectedValue is int intLoai)
                            loaiValue = intLoai;
                        else if (cmbFilterLoai.SelectedValue is long longLoai)
                            loaiValue = (int)longLoai;
                        else if (int.TryParse(cmbFilterLoai.SelectedValue.ToString(), out var parsedLoai))
                            loaiValue = parsedLoai;
                        else
                            loaiValue = -1;

                        if (loaiValue != -1)
                            maLoai = loaiValue;
                    }

                    // Filter từ danh sách đã filter theo hãng
                    var filteredProducts = productsByBrand.AsQueryable();
                    
                    if (!string.IsNullOrWhiteSpace(searchText))
                    {
                        var searchLower = searchText.ToLower();
                        filteredProducts = filteredProducts.Where(p => 
                            (p.TenGiay != null && p.TenGiay.ToLower().Contains(searchLower)) ||
                            (p.MaSKU != null && p.MaSKU.ToLower().Contains(searchLower)) ||
                            (p.TenLoai != null && p.TenLoai.ToLower().Contains(searchLower))
                        );
                    }
                    
                    if (maSize.HasValue && maSize.Value > 0)
                    {
                        filteredProducts = filteredProducts.Where(p => p.MaSize == maSize.Value);
                    }
                    
                    if (maLoai.HasValue && maLoai.Value > 0)
                    {
                        // Cần thêm logic filter theo loại nếu có
                        // Tạm thời bỏ qua vì cần query lại DB
                    }

                    // Sắp xếp theo giá nếu được chọn
                    var priceSort = cmbFilterPriceType.Text.Trim();
                    if (priceSort == "Giá tăng dần")
                    {
                        filteredProducts = filteredProducts.OrderBy(p => p.GiaBan);
                    }
                    else if (priceSort == "Giá giảm dần")
                    {
                        filteredProducts = filteredProducts.OrderByDescending(p => p.GiaBan);
                    }

                    BindDataGridView(filteredProducts.ToList());
                }
                else
                {
                    // Không có filter hãng, dùng SearchProducts như cũ
                    var searchText = txtSearch.Text.Trim();
                    int? maSize = null;
                    int? maLoai = null;

                    if (cmbFilterSize.SelectedValue != null)
                    {
                        int sizeValue;
                        if (cmbFilterSize.SelectedValue is int intSize)
                            sizeValue = intSize;
                        else if (cmbFilterSize.SelectedValue is long longSize)
                            sizeValue = (int)longSize;
                        else if (int.TryParse(cmbFilterSize.SelectedValue.ToString(), out var parsedSize))
                            sizeValue = parsedSize;
                        else
                            sizeValue = -1;

                        if (sizeValue != -1)
                            maSize = sizeValue;
                    }

                    if (cmbFilterLoai.SelectedValue != null)
                    {
                        int loaiValue;
                        if (cmbFilterLoai.SelectedValue is int intLoai)
                            loaiValue = intLoai;
                        else if (cmbFilterLoai.SelectedValue is long longLoai)
                            loaiValue = (int)longLoai;
                        else if (int.TryParse(cmbFilterLoai.SelectedValue.ToString(), out var parsedLoai))
                            loaiValue = parsedLoai;
                        else
                            loaiValue = -1;

                        if (loaiValue != -1)
                            maLoai = loaiValue;
                    }

                    var products = _productService.SearchProducts(searchText, maSize, maLoai, null, null);
                    
                    // Sắp xếp theo giá nếu được chọn
                    var priceSort = cmbFilterPriceType.Text.Trim();
                    if (priceSort == "Giá tăng dần")
                    {
                        products = products.OrderBy(p => p.GiaBan).ToList();
                    }
                    else if (priceSort == "Giá giảm dần")
                    {
                        products = products.OrderByDescending(p => p.GiaBan).ToList();
                    }
                    
                    BindDataGridView(products);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc: {ex.Message}\n\nChi tiết: {ex.InnerException?.Message ?? ex.ToString()}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private ChiTietSanPhamDTO CreateProductFromForm()
        {
            var giaNhapText = txtImportPrice.Text.Replace(",", "").Replace(".", "").Trim();
            var giaBanText = txtSellingPrice.Text.Replace(",", "").Replace(".", "").Trim();

            // Lấy MaSP từ tên sản phẩm (tìm trong database)
            int maSP = 0;
            var tenGiay = txtProductName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(tenGiay))
            {
                try
                {
                    var products = _productService.GetSanPham();
                    if (products != null && products.Rows.Count > 0)
                    {
                        var product = products.AsEnumerable()
                            .FirstOrDefault(r =>
                            {
                                var rowTenGiay = r["TenGiay"];
                                return rowTenGiay != DBNull.Value &&
                                       rowTenGiay.ToString().Equals(tenGiay, StringComparison.OrdinalIgnoreCase);
                            });

                        if (product != null)
                        {
                            var maSPObj = product["MaSP"];
                            if (maSPObj != DBNull.Value)
                            {
                                if (maSPObj is int intValue)
                                    maSP = intValue;
                                else if (maSPObj is long longValue)
                                    maSP = (int)longValue;
                                else if (int.TryParse(maSPObj.ToString(), out var parsed))
                                    maSP = parsed;
                            }
                        }
                    }
                }
                catch
                {
                    // Nếu không tìm thấy, maSP = 0 (sẽ được tạo mới trong EnsureBaseProduct)
                }
            }

            int maSize = 0;
            if (cmbSize.SelectedValue != null)
            {
                if (cmbSize.SelectedValue is int intSize)
                    maSize = intSize;
                else if (cmbSize.SelectedValue is long longSize)
                    maSize = (int)longSize;
                else if (int.TryParse(cmbSize.SelectedValue.ToString(), out var parsedSize))
                    maSize = parsedSize;
            }

            int maMau = 0;
            if (cmbColor.SelectedValue != null)
            {
                if (cmbColor.SelectedValue is int intMau)
                    maMau = intMau;
                else if (cmbColor.SelectedValue is long longMau)
                    maMau = (int)longMau;
                else if (int.TryParse(cmbColor.SelectedValue.ToString(), out var parsedMau))
                    maMau = parsedMau;
            }

            return new ChiTietSanPhamDTO
            {
                MaSP = maSP,
                MaSize = maSize,
                MaMau = maMau,
                MaSKU = txtProductCode.Text.Trim(),
                GiaNhap = decimal.Parse(giaNhapText),
                GiaBan = decimal.Parse(giaBanText),
                SoLuongTon = string.IsNullOrWhiteSpace(txtQuantity.Text) ? 0 : int.Parse(txtQuantity.Text),
                TrangThai = true
            };
        }

        private bool ValidateInput()
    {
        // Allow "Tất cả" if product name is entered (brand will be extracted from product name)
        var brandName = cmbProduct.SelectedValue?.ToString();
        bool isAllSelected = string.IsNullOrWhiteSpace(brandName) || brandName == "Tất cả" || brandName == "-1";
        
        // If "Tất cả" is selected, we need product name to extract brand
        if (isAllSelected)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text.Trim()))
            {
                MessageBox.Show("Khi chọn 'Tất cả', vui lòng nhập tên sản phẩm!\nTên hãng giày sẽ được lấy từ từ đầu tiên của tên sản phẩm.", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return false;
            }
            // Brand will be extracted from product name, so this is OK
        }
        else // If a specific brand is selected, ensure it's valid
        {
            if (cmbProduct.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn hãng giày!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        if (string.IsNullOrWhiteSpace(txtProductName.Text.Trim()))
        {
            MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtProductName.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtProductCode.Text.Trim()))
        {
            MessageBox.Show("Vui lòng nhập mã SKU!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtProductCode.Focus();
            return false;
        }
            if (cmbSize.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn size!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbColor.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn màu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            var giaNhapText = txtImportPrice.Text.Replace(",", "").Replace(".", "").Trim();
            if (string.IsNullOrWhiteSpace(giaNhapText) || !decimal.TryParse(giaNhapText, out var giaNhap) || giaNhap <= 0)
            {
                MessageBox.Show("Giá nhập không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtImportPrice.Focus();
                return false;
            }

            var giaBanText = txtSellingPrice.Text.Replace(",", "").Replace(".", "").Trim();
            if (string.IsNullOrWhiteSpace(giaBanText) || !decimal.TryParse(giaBanText, out var giaBan) || giaBan <= 0)
            {
                MessageBox.Show("Giá bán không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSellingPrice.Focus();
                return false;
            }

            if (giaBan < giaNhap)
            {
                MessageBox.Show("Giá bán phải lớn hơn hoặc bằng giá nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtQuantity.Text) || !int.TryParse(txtQuantity.Text, out var soLuong) || soLuong < 0)
            {
                MessageBox.Show("Số lượng không hợp lệ! Vui lòng nhập số >= 0", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return false;
            }

            return true;
        }

        private void ResetForm()
        {
            _currentMaCTSP = 0;
            txtProductName.Clear();
            txtProductCode.Clear();
            txtImportPrice.Clear();
            txtSellingPrice.Clear();
            txtQuantity.Clear();
            txtDescription.Clear();
            txtImagePath.Clear();
            picProductImage.Image = null;
            // Không reset ComboBox hãng giày để giữ filter hiện tại
            // if (cmbProduct.Items.Count > 0)
            //     cmbProduct.SelectedIndex = 0;
            if (cmbSize.Items.Count > 0)
                cmbSize.SelectedIndex = 0;
            if (cmbColor.Items.Count > 0)
                cmbColor.SelectedIndex = 0;
            if (cmbLoai.Items.Count > 0)
                cmbLoai.SelectedIndex = 0;
        }

        private static bool IsInDesignMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
                   System.Windows.Forms.Application.ExecutablePath.IndexOf("devenv.exe", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Chuyển đổi full path thành relative path bắt đầu từ Resources\Images\Products\
        /// </summary>
        private string GetRelativeImagePath(string fullOrRelativePath)
        {
            if (string.IsNullOrWhiteSpace(fullOrRelativePath))
                return string.Empty;

            // Nếu đã là relative path, return ngay
            if (!Path.IsPathRooted(fullOrRelativePath))
                return fullOrRelativePath;

            // Tìm phần "Resources\Images\Products"
            string marker = Path.Combine("Resources", "Images", "Products");
            int index = fullOrRelativePath.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
            
            if (index >= 0)
            {
                return fullOrRelativePath.Substring(index);
            }

            // Thử với forward slashes
            marker = "Resources/Images/Products";
            index = fullOrRelativePath.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
            
            if (index >= 0)
            {
                return fullOrRelativePath.Substring(index).Replace("/", "\\");
            }

            // Không tìm thấy, return nguyên bản
            return fullOrRelativePath;
        }

        /// <summary>
        /// Chuyển đổi relative path thành full path
        /// </summary>
        private string GetFullImagePath(string relativeOrFullPath)
        {
            if (string.IsNullOrWhiteSpace(relativeOrFullPath))
                return string.Empty;

            // Nếu đã là full path, return ngay
            if (Path.IsPathRooted(relativeOrFullPath))
                return relativeOrFullPath;

            // Convert relative -> full
            string projectRoot = Directory.GetParent(Application.StartupPath).Parent.FullName;
            return Path.Combine(projectRoot, relativeOrFullPath);
        }

        private void picProductImage_Click(object sender, EventArgs e)
        {

        }

        private void btnUploadImage_Click(object sender, EventArgs e)
    {
        try
        {
            // Get brand from selected ComboBox OR from product name
            string brandName = cmbProduct.SelectedValue?.ToString()?.Trim();
            
            // If brand not selected or "Tất cả", try to get from product name
            if (string.IsNullOrWhiteSpace(brandName) || brandName == "Tất cả")
            {
                string productName = txtProductName.Text.Trim();
                if (!string.IsNullOrWhiteSpace(productName))
                {
                    // Extract first word from product name as brand
                    brandName = productName.Split(' ').FirstOrDefault();
                    System.Diagnostics.Debug.WriteLine($"Auto-extracted brand from product name: {brandName}");
                }
            }
            
            // Final validation
            if (string.IsNullOrWhiteSpace(brandName) || brandName == "Tất cả")
            {
                MessageBox.Show("Vui lòng chọn hãng giày hoặc nhập tên sản phẩm trước khi upload ảnh!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Chọn hình ảnh sản phẩm";
                
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Create brand-specific folder: Resources/Images/Products/{brand}/
                    string brandFolderName = brandName.ToLower();
                    
                    // Navigate to project root (2 levels up from bin/Debug)
                    string projectRoot = Directory.GetParent(Application.StartupPath).Parent.FullName;
                    string resourcesPath = Path.Combine(projectRoot, "Resources", "Images", "Products", brandFolderName);
                    
                    System.Diagnostics.Debug.WriteLine($"StartupPath: {Application.StartupPath}");
                    System.Diagnostics.Debug.WriteLine($"ProjectRoot: {projectRoot}");
                    System.Diagnostics.Debug.WriteLine($"ResourcesPath: {resourcesPath}");
                    
                    // Create directory if not exists
                    if (!Directory.Exists(resourcesPath))
                    {
                        System.Diagnostics.Debug.WriteLine("Creating directory...");
                        Directory.CreateDirectory(resourcesPath);
                        System.Diagnostics.Debug.WriteLine($"Directory created: {resourcesPath}");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Directory already exists");
                    }
                    
                    // Find next available number by checking existing files
                    var existingFiles = Directory.GetFiles(resourcesPath, $"{brandFolderName}*.*");
                    int nextNumber = 1;
                    
                    if (existingFiles.Length > 0)
                    {
                        // Extract numbers from existing filenames and find max
                        var existingNumbers = existingFiles
                            .Select(f => Path.GetFileNameWithoutExtension(f))
                            .Where(name => name.StartsWith(brandFolderName))
                            .Select(name => name.Substring(brandFolderName.Length))
                            .Where(numStr => int.TryParse(numStr, out _))
                            .Select(numStr => int.Parse(numStr))
                            .ToList();
                        
                        if (existingNumbers.Any())
                        {
                            nextNumber = existingNumbers.Max() + 1;
                        }
                    }
                    
                    // Generate filename: brandname{number}.ext (e.g., nike1.jpg, nike4.jpg)
                    string extension = Path.GetExtension(openFileDialog.FileName);
                    string uniqueFileName = $"{brandFolderName}{nextNumber}{extension}";
                    string destinationPath = Path.Combine(resourcesPath, uniqueFileName);
                    
                    System.Diagnostics.Debug.WriteLine($"Existing files: {existingFiles.Length}, Next number: {nextNumber}");
                    System.Diagnostics.Debug.WriteLine($"Copying from: {openFileDialog.FileName}");
                    System.Diagnostics.Debug.WriteLine($"Copying to: {destinationPath}");
                    
                    // Copy file to destination
                    File.Copy(openFileDialog.FileName, destinationPath, true);
                    
                    System.Diagnostics.Debug.WriteLine("File copied successfully");
                    
                    // Hiển thị relative path trong UI
                    txtImagePath.Text = GetRelativeImagePath(destinationPath);
                    LoadProductImage(destinationPath);  // Load bằng full path
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error: {ex.ToString()}");
            MessageBox.Show($"Lỗi khi chọn ảnh:\n{ex.Message}\n\nChi tiết: {ex.StackTrace}", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    }
}
