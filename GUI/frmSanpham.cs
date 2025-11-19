using System;
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

                // Load sản phẩm
                var productsTable = _productService.GetSanPham();
                if (productsTable != null && productsTable.Rows.Count > 0)
                {
                    cmbProduct.DataSource = productsTable;
                    cmbProduct.DisplayMember = "TenGiay";
                    cmbProduct.ValueMember = "MaSP";
                }

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
                    MessageBox.Show("Không có sản phẩm nào trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                DataPropertyName = "MauSac",
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
            dgvProducts.Columns["MaSKU"].FillWeight      = 80;
            dgvProducts.Columns["TenGiay"].FillWeight    = 180;
            dgvProducts.Columns["Size"].FillWeight       = 40;
            dgvProducts.Columns["Mau"].FillWeight        = 70;
            dgvProducts.Columns["Loai"].FillWeight       = 90;
            dgvProducts.Columns["GiaNhap"].FillWeight    = 70;
            dgvProducts.Columns["GiaBan"].FillWeight     = 70;
            dgvProducts.Columns["SoLuongTon"].FillWeight = 50;
            dgvProducts.Columns["HinhAnh"].FillWeight    = 110;
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
            cmbProduct.SelectedValue = GetMaSPFromRow(row);
            txtProductName.Text = row.Cells["TenGiay"].Value?.ToString() ?? "";
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

            txtImagePath.Text = row.Cells["HinhAnh"].Value?.ToString() ?? "";
            LoadProductImage(txtImagePath.Text);
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
            if (string.IsNullOrWhiteSpace(imagePath))
            {
                picProductImage.Image = null;
                return;
            }

            try
            {
                if (File.Exists(imagePath))
                {
                    picProductImage.Image = Image.FromFile(imagePath);
                }
                else
                {
                    picProductImage.Image = null;
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

                    LoadProducts();      // reload lưới chi tiết
                    LoadComboBoxes();    // reload combobox Sản phẩm để có sản phẩm mới
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
                    MessageBox.Show("Sửa sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
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

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            try
            {
                if (_productService.DeleteProduct(_currentMaCTSP))
                {
                    MessageBox.Show("Xóa sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Xóa sản phẩm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            LoadProducts();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx|CSV Files|*.csv",
                    FileName = $"DanhSachSanPham_{DateTime.Now:yyyyMMdd}.xlsx"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // TODO: Implement export functionality
                    MessageBox.Show("Chức năng xuất file đang được phát triển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
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
                var selectedValue = cmbProduct.SelectedValue;
                int maSP;
                
                // Convert SelectedValue to int safely
                if (selectedValue is int intValue)
                {
                    maSP = intValue;
                }
                else if (selectedValue is long longValue)
                {
                    maSP = (int)longValue;
                }
                else if (int.TryParse(selectedValue.ToString(), out var parsedValue))
                {
                    maSP = parsedValue;
                }
                else
                {
                    return;
                }

                var products = _productService.GetSanPham();
                if (products == null || products.Rows.Count == 0)
                    return;

                var selectedProduct = products.AsEnumerable()
                    .FirstOrDefault(r => 
                    {
                        var rowMaSP = r["MaSP"];
                        if (rowMaSP == DBNull.Value)
                            return false;
                        
                        if (rowMaSP is int rowInt)
                            return rowInt == maSP;
                        if (rowMaSP is long rowLong)
                            return (int)rowLong == maSP;
                        if (int.TryParse(rowMaSP.ToString(), out var rowParsed))
                            return rowParsed == maSP;
                        return false;
                    });

                if (selectedProduct != null)
                {
                    var tenGiay = selectedProduct["TenGiay"];
                    txtProductName.Text = tenGiay == DBNull.Value ? "" : tenGiay.ToString();
                }
            }
            catch (Exception ex)
            {
                // Silently handle errors to avoid disrupting user experience
                // The error might occur during form initialization
            }
        }

        private void ApplyFilters()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var searchText = txtSearch.Text.Trim();
                int? maSize = null;
                int? maLoai = null;
                string priceFilter = null;
                decimal? priceValue = null;

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

                if (cmbFilterPriceType.SelectedIndex > 0 && !string.IsNullOrWhiteSpace(txtFilterPrice.Text))
                {
                    priceFilter = cmbFilterPriceType.SelectedIndex == 1 ? "GiaNhap" : "GiaBan";
                    var priceText = txtFilterPrice.Text.Replace(",", "").Replace(".", "").Trim();
                    if (decimal.TryParse(priceText, out var price))
                        priceValue = price;
                }

                var products = _productService.SearchProducts(searchText, maSize, maLoai, priceFilter, priceValue);
                BindDataGridView(products);
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
            
            int maSP = 0;
            if (cmbProduct.SelectedValue != null)
            {
                if (cmbProduct.SelectedValue is int intSP)
                    maSP = intSP;
                else if (cmbProduct.SelectedValue is long longSP)
                    maSP = (int)longSP;
                else if (int.TryParse(cmbProduct.SelectedValue.ToString(), out var parsedSP))
                    maSP = parsedSP;
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
            if (cmbProduct.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtProductCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã giày!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (cmbProduct.Items.Count > 0)
                cmbProduct.SelectedIndex = 0;
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

        private void picProductImage_Click(object sender, EventArgs e)
        {

        }
    }
}
