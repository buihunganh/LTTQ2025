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
using Excel = Microsoft.Office.Interop.Excel;

namespace BTL_LTTQ
{
    public partial class frmSanpham : Form
    {
        private readonly ProductService _productService;
        private int _currentMaCTSP;
        private bool _isLoadingComboBoxes = false;
        private string _currentSelectedBrand = null;

        public frmSanpham()
        {
            InitializeComponent();
            _productService = IsInDesignMode() ? null : new ProductService();
        }

        private void frmSanpham_Load(object sender, EventArgs e)
        {
            if (IsInDesignMode()) return;
            LoadComboBoxes();
            LoadProducts();
            ResetForm();
            SetupAutoSKUGeneration();
        }

        private void SetupAutoSKUGeneration()
        {
        }

        private void btnGenerateSKU_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên giày!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return;
            }

            int? maSize = GetSelectedValueAsInt(cmbSize);
            int? maMau = GetSelectedValueAsInt(cmbColor);

            if (!maSize.HasValue)
            {
                MessageBox.Show("Vui lòng chọn size!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbSize.Focus();
                return;
            }

            if (!maMau.HasValue)
            {
                MessageBox.Show("Vui lòng chọn màu sắc!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbColor.Focus();
                return;
            }

            GenerateSKU();
        }

        private void LoadComboBoxes()
        {
            try
            {
                _isLoadingComboBoxes = true;
                var allProducts = _productService.GetAllProducts();
                var brands = allProducts
                    .Select(p => p.TenGiay?.Split(' ').FirstOrDefault())
                    .Where(b => !string.IsNullOrWhiteSpace(b))
                    .Distinct()
                    .OrderBy(b => b)
                    .ToList();

                var brandsTable = new DataTable();
                brandsTable.Columns.Add("TenHang", typeof(string));
                var allBrandRow = brandsTable.NewRow();
                allBrandRow["TenHang"] = "Tất cả";
                brandsTable.Rows.Add(allBrandRow);
                foreach (var brand in brands)
                {
                    var row = brandsTable.NewRow();
                    row["TenHang"] = brand;
                    brandsTable.Rows.Add(row);
                }

                cmbProduct.DataSource = brandsTable;
                cmbProduct.DisplayMember = "TenHang";
                cmbProduct.ValueMember = "TenHang";

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

                var mauTable = _productService.GetMauSac();
                if (mauTable != null && mauTable.Rows.Count > 0)
                {
                    cmbColor.DataSource = mauTable.Copy();
                    cmbColor.DisplayMember = "TenMau";
                    cmbColor.ValueMember = "MaMau";
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
                var allProducts = _productService.GetAllProducts();
                var filteredProducts = allProducts
                    .Where(p => p.TenGiay?.StartsWith(brandPrefix, StringComparison.OrdinalIgnoreCase) == true)
                    .ToList();
                BindDataGridView(filteredProducts);
            }
            catch (Exception ex)
            {
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
                DataPropertyName = "GiaBan",
                HeaderText = "Giá bán",
                Name = "GiaBan",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
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

            dgvProducts.DataSource = products;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgvProducts.Columns["MaSKU"].FillWeight = 80;
            dgvProducts.Columns["TenGiay"].FillWeight = 180;
            dgvProducts.Columns["Size"].FillWeight = 40;
            dgvProducts.Columns["Mau"].FillWeight = 70;
            dgvProducts.Columns["Loai"].FillWeight = 90;
            dgvProducts.Columns["GiaBan"].FillWeight = 70;
            dgvProducts.Columns["HinhAnh"].FillWeight = 110;

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
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var row = dgvProducts.Rows[e.RowIndex];
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

            var tenGiay = row.Cells["TenGiay"].Value?.ToString() ?? "";
            txtProductName.Text = tenGiay;
            string brandName = tenGiay?.Split(' ').FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(brandName))
            {
                try
                {
                    var wasLoading = _isLoadingComboBoxes;
                    _isLoadingComboBoxes = true;
                    cmbProduct.SelectedValue = brandName;
                    _currentSelectedBrand = brandName;
                    _isLoadingComboBoxes = wasLoading;
                }
                catch { }
            }

            txtProductCode.Text = row.Cells["MaSKU"].Value?.ToString() ?? "";
            cmbSize.SelectedValue = GetMaSizeFromRow(row);
            cmbColor.SelectedValue = GetMaMauFromRow(row);
            cmbLoai.SelectedValue = GetMaLoaiFromRow(row);

            if (row.Cells["GiaBan"].Value != null && decimal.TryParse(row.Cells["GiaBan"].Value.ToString(), out var giaBan))
                txtSellingPrice.Text = giaBan.ToString("N0");
            else
                txtSellingPrice.Clear();

            string imagePath = row.Cells["HinhAnh"].Value?.ToString() ?? "";
            txtImagePath.Text = GetRelativeImagePath(imagePath);
            LoadProductImage(imagePath);
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
                string fullPath = GetFullImagePath(imagePath);
                if (File.Exists(fullPath))
                {
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

                if (!string.IsNullOrWhiteSpace(tenGiay) && maLoai > 0)
                {
                    var moTa = txtDescription.Text.Trim();
                    maSpBase = _productService.EnsureBaseProduct(tenGiay, maLoai, moTa);
                }

                var product = CreateProductFromForm();
                if (maSpBase > 0)
                    product.MaSP = maSpBase;

                if (_productService.AddProduct(product))
                {
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadProductsWithCurrentBrand();
                    LoadComboBoxes();
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
                    if (!string.IsNullOrWhiteSpace(txtImagePath.Text) && product.MaCTSP > 0)
                    {
                        _productService.UpdateProductImage(product.MaCTSP, txtImagePath.Text);
                    }
                    MessageBox.Show("Sửa sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                var (success, imagePath) = _productService.DeleteProduct(_currentMaCTSP);
                if (success)
                {
                    if (!string.IsNullOrWhiteSpace(imagePath))
                    {
                        try
                        {
                            if (picProductImage.Image != null)
                            {
                                picProductImage.Image.Dispose();
                                picProductImage.Image = null;
                            }

                            string fullPath = imagePath;
                            if (!Path.IsPathRooted(imagePath))
                            {
                                string projectRoot = Directory.GetParent(Application.StartupPath).Parent.FullName;
                                fullPath = Path.Combine(projectRoot, imagePath);
                            }

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
            ReloadProductsWithCurrentBrand();
        }

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
            if (dgvProducts.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel|*.xlsx",
                FileName = "SanPham_" + DateTime.Now.ToString("ddMMyy")
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ExportToExcel(sfd.FileName);
            }
        }

        private void ExportToExcel(string filePath)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                Cursor = Cursors.WaitCursor;
                excelApp = new Excel.Application();
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                workbook = excelApp.Workbooks.Add();
                worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                worksheet.Name = "SanPham";

                int visibleColCount = 0;
                foreach (DataGridViewColumn col in dgvProducts.Columns)
                {
                    if (col.Visible && col.Name != "MaCTSP")
                        visibleColCount++;
                }

                string lastCol = GetExcelColumnName(visibleColCount);
                Excel.Range titleRange = worksheet.Range["A1", $"{lastCol}1"];
                titleRange.Merge();
                titleRange.Value2 = "DANH SÁCH SẢN PHẨM";
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 16;
                titleRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(232, 90, 79));
                titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ReleaseObject(titleRange);

                int headerRow = 3;
                int colIndex = 1;
                foreach (DataGridViewColumn col in dgvProducts.Columns)
                {
                    if (col.Visible && col.Name != "MaCTSP")
                    {
                        worksheet.Cells[headerRow, colIndex] = col.HeaderText;
                        colIndex++;
                    }
                }

                Excel.Range headerRange = worksheet.Range[worksheet.Cells[headerRow, 1], worksheet.Cells[headerRow, visibleColCount]];
                headerRange.Font.Bold = true;
                headerRange.Font.Size = 11;
                headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ReleaseObject(headerRange);

                int row = headerRow + 1;
                foreach (DataGridViewRow dgvRow in dgvProducts.Rows)
                {
                    if (dgvRow.IsNewRow) continue;
                    colIndex = 1;
                    foreach (DataGridViewColumn col in dgvProducts.Columns)
                    {
                        if (col.Visible && col.Name != "MaCTSP")
                        {
                            var value = dgvRow.Cells[col.Index].Value;
                            if (value != null && value != DBNull.Value)
                            {
                                worksheet.Cells[row, colIndex] = value.ToString();

                                if (col.Name == "GiaBan")
                                {
                                    Excel.Range cellRange = (Excel.Range)worksheet.Cells[row, colIndex];
                                    cellRange.NumberFormat = "#,##0";
                                    cellRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                                    ReleaseObject(cellRange);
                                }
                            }
                            colIndex++;
                        }
                    }
                    row++;
                }

                if (row > headerRow + 1)
                {
                    Excel.Range dataRange = worksheet.Range[worksheet.Cells[headerRow, 1], worksheet.Cells[row - 1, visibleColCount]];
                    dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    dataRange.Borders.Weight = Excel.XlBorderWeight.xlThin;
                    ReleaseObject(dataRange);
                }

                worksheet.Columns.AutoFit();
                worksheet.UsedRange.WrapText = false;

                for (int i = 1; i <= visibleColCount; i++)
                {
                    Excel.Range col = (Excel.Range)worksheet.Columns[i];
                    col.ColumnWidth = Math.Max((double)col.ColumnWidth * 1.1, 12);
                    ReleaseObject(col);
                }

                workbook.SaveAs(filePath);
                Cursor = Cursors.Default;

                MessageBox.Show("Xuất file thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Lỗi khi xuất file:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (worksheet != null) ReleaseObject(worksheet);
                if (workbook != null)
                {
                    workbook.Close(false);
                    ReleaseObject(workbook);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    ReleaseObject(excelApp);
                }
            }
        }

        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch
            {
                obj = null;
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
            if (_isLoadingComboBoxes || cmbProduct.SelectedValue == null) return;

            try
            {
                string selectedBrand = cmbProduct.SelectedValue?.ToString()?.Trim();
                if (string.IsNullOrWhiteSpace(selectedBrand) || selectedBrand == "Tất cả")
                {
                    _currentSelectedBrand = null;
                }
                else
                {
                    _currentSelectedBrand = selectedBrand;
                }
                
                ApplyFilters();
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
                var searchText = txtSearch.Text.Trim();
                int? maSize = GetSelectedValueAsInt(cmbFilterSize);
                int? maLoai = GetSelectedValueAsInt(cmbFilterLoai);

                List<ChiTietSanPhamDTO> products;
                if (!string.IsNullOrWhiteSpace(_currentSelectedBrand) && _currentSelectedBrand != "Tất cả" && _currentSelectedBrand != "-1")
                {
                    var allProducts = _productService.GetAllProducts();
                    products = allProducts
                        .Where(p => p.TenGiay?.StartsWith(_currentSelectedBrand, StringComparison.OrdinalIgnoreCase) == true)
                        .ToList();
                    var filteredProducts = products.AsQueryable();

                    if (!string.IsNullOrWhiteSpace(searchText))
                    {
                        var searchLower = searchText.ToLower();
                        filteredProducts = filteredProducts.Where(p =>
                            (p.TenGiay != null && p.TenGiay.ToLower().Contains(searchLower)) ||
                            (p.MaSKU != null && p.MaSKU.ToLower().Contains(searchLower)) ||
                            (p.TenLoai != null && p.TenLoai.ToLower().Contains(searchLower)));
                    }

                    if (maSize.HasValue && maSize.Value > 0)
                    {
                        filteredProducts = filteredProducts.Where(p => p.MaSize == maSize.Value);
                    }
                    if (maLoai.HasValue && maLoai.Value > 0)
                    {
                        string tenLoaiFilter = null;
                        var loaiTable = _productService.GetLoaiGiay();
                        if (loaiTable != null && loaiTable.Rows.Count > 0)
                        {
                            var loaiRow = loaiTable.AsEnumerable()
                                .FirstOrDefault(r => Convert.ToInt32(r["MaLoai"]) == maLoai.Value);
                            if (loaiRow != null)
                            {
                                tenLoaiFilter = loaiRow["TenLoai"].ToString();
                            }
                        }
                        
                        if (!string.IsNullOrWhiteSpace(tenLoaiFilter))
                        {
                            filteredProducts = filteredProducts.Where(p => p.TenLoai == tenLoaiFilter);
                        }
                    }
                    var priceSort = cmbFilterPriceType.Text.Trim();
                    if (priceSort == "Giá tăng dần")
                        filteredProducts = filteredProducts.OrderBy(p => p.GiaBan);
                    else if (priceSort == "Giá giảm dần")
                        filteredProducts = filteredProducts.OrderByDescending(p => p.GiaBan);

                    products = filteredProducts.ToList();
                }
                else
                {
                    products = _productService.SearchProducts(searchText, maSize, maLoai, null, null);

                    var priceSort = cmbFilterPriceType.Text.Trim();
                    if (priceSort == "Giá tăng dần")
                        products = products.OrderBy(p => p.GiaBan).ToList();
                    else if (priceSort == "Giá giảm dần")
                        products = products.OrderByDescending(p => p.GiaBan).ToList();
                }

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

        private void GenerateSKU()
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
                return;
            
            int? maSize = GetSelectedValueAsInt(cmbSize);
            int? maMau = GetSelectedValueAsInt(cmbColor);
            
            if (!maSize.HasValue || !maMau.HasValue)
                return;

            string brandName = cmbProduct.SelectedValue?.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(brandName) || brandName == "Tất cả")
                brandName = txtProductName.Text.Trim().Split(' ').FirstOrDefault();

            if (string.IsNullOrWhiteSpace(brandName)) return;

            string brandPrefix = GetBrandPrefix(brandName);
            string modelCode = GetModelCode(txtProductName.Text.Trim());
            string size = cmbSize.Text?.Trim() ?? "";
            string colorCode = GetColorCode(cmbColor.Text?.Trim() ?? "");

            if (!string.IsNullOrWhiteSpace(brandPrefix) && !string.IsNullOrWhiteSpace(modelCode) && 
                !string.IsNullOrWhiteSpace(size) && !string.IsNullOrWhiteSpace(colorCode))
            {
                txtProductCode.Text = $"{brandPrefix}-{modelCode}-{size}-{colorCode}";
            }
        }

        private string GetBrandPrefix(string brandName)
        {
            if (string.IsNullOrWhiteSpace(brandName)) return "";
            
            brandName = brandName.ToUpper().Trim();
            
            string[] words = brandName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length == 0) return "";
            
            string firstWord = words[0];
            
            if (firstWord.Length >= 3)
                return firstWord.Substring(0, 3);
            else if (firstWord.Length == 2)
                return firstWord;
            else if (words.Length > 1 && words[1].Length >= 2)
                return firstWord + words[1].Substring(0, Math.Min(2, words[1].Length));
            else
                return firstWord;
        }

        private string GetModelCode(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName)) return "SP";
            
            string[] words = productName.ToUpper().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length < 2) return "SP";
            
            string fullText = productName.ToUpper();
            if (fullText.Contains("FORCE") && fullText.Contains("1")) return "AF1";
            if (fullText.Contains("TRIPLE") && fullText.Contains("S")) return "TS";
            if (fullText.Contains("HIGH") && fullText.Contains("ANKLE")) return "HA";
            if (fullText.Contains("AIR MAX")) return "AMX";
            if (fullText.Contains("AIR")) return "AIR";
            
            for (int i = 1; i < words.Length && i < 3; i++)
            {
                string word = words[i];
                if (word.Length >= 2 && word.All(char.IsLetter))
                    return word.Length >= 3 ? word.Substring(0, 3) : word;
            }
            
            return words.Length > 1 ? (words[1].Length >= 3 ? words[1].Substring(0, 3) : words[1]) : "SP";
        }

        private string GetColorCode(string colorName)
        {
            if (string.IsNullOrWhiteSpace(colorName)) return "";
            
            colorName = colorName.ToUpper();
            var colorMap = new Dictionary<string, string>
            {
                { "TRẮNG", "WHT" }, { "WHITE", "WHT" }, { "WHT", "WHT" },
                { "ĐEN", "BLK" }, { "BLACK", "BLK" }, { "BLK", "BLK" },
                { "ĐỎ", "RED" }, { "RED", "RED" },
                { "XANH", "BLU" }, { "BLUE", "BLU" },
                { "VÀNG", "YLW" }, { "YELLOW", "YLW" },
                { "HỒNG", "PNK" }, { "PINK", "PNK" },
                { "XÁM", "GRY" }, { "GRAY", "GRY" }, { "GREY", "GRY" },
                { "NÂU", "BRN" }, { "BROWN", "BRN" },
                { "CAM", "ORG" }, { "ORANGE", "ORG" },
                { "TÍM", "PRP" }, { "PURPLE", "PRP" },
                { "XANH LÁ", "GRN" }, { "GREEN", "GRN" }
            };
            
            foreach (var kvp in colorMap)
            {
                if (colorName.Contains(kvp.Key))
                    return kvp.Value;
            }
            
            return colorName.Length >= 3 ? colorName.Substring(0, 3) : colorName;
        }

        private ChiTietSanPhamDTO CreateProductFromForm()
        {
            var giaBanText = txtSellingPrice.Text.Replace(",", "").Replace(".", "").Trim();
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
                catch { }
            }

            int maSize = GetSelectedValueAsInt(cmbSize) ?? 0;
            int maMau = GetSelectedValueAsInt(cmbColor) ?? 0;

            return new ChiTietSanPhamDTO
            {
                MaSP = maSP,
                MaSize = maSize,
                MaMau = maMau,
                MaSKU = txtProductCode.Text.Trim(),
                GiaNhap = 0,
                GiaBan = decimal.Parse(giaBanText),
                SoLuongTon = 0,
                HinhAnhChung = txtImagePath.Text.Trim(),
                TrangThai = true
            };
        }

        private bool ValidateInput()
        {
            var brandName = cmbProduct.SelectedValue?.ToString();
            bool isAllSelected = string.IsNullOrWhiteSpace(brandName) || brandName == "Tất cả" || brandName == "-1";

            if (isAllSelected)
            {
                if (string.IsNullOrWhiteSpace(txtProductName.Text.Trim()))
                {
                    MessageBox.Show("Khi chọn 'Tất cả', vui lòng nhập tên sản phẩm!\nTên hãng giày sẽ được lấy từ từ đầu tiên của tên sản phẩm.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtProductName.Focus();
                    return false;
                }
            }
            else if (cmbProduct.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn hãng giày!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
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

            var giaBanText = txtSellingPrice.Text.Replace(",", "").Replace(".", "").Trim();
            if (string.IsNullOrWhiteSpace(giaBanText) || !decimal.TryParse(giaBanText, out var giaBan) || giaBan <= 0)
            {
                MessageBox.Show("Giá bán không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSellingPrice.Focus();
                return false;
            }

            return true;
        }

        private int? GetSelectedValueAsInt(ComboBox comboBox)
        {
            if (comboBox.SelectedValue == null) return null;
            if (comboBox.SelectedValue is int intValue) return intValue;
            if (comboBox.SelectedValue is long longValue) return (int)longValue;
            if (int.TryParse(comboBox.SelectedValue.ToString(), out var parsed))
            {
                return parsed == -1 ? null : (int?)parsed;
            }
            return null;
        }

        private void ResetForm()
        {
            _currentMaCTSP = 0;
            txtProductName.Clear();
            txtProductCode.Clear();
            txtSellingPrice.Clear();
            txtDescription.Clear();
            txtImagePath.Clear();
            picProductImage.Image = null;
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

        private string GetRelativeImagePath(string fullOrRelativePath)
        {
            if (string.IsNullOrWhiteSpace(fullOrRelativePath)) return string.Empty;
            if (!Path.IsPathRooted(fullOrRelativePath)) return fullOrRelativePath;

            string marker = Path.Combine("Resources", "Images", "Products");
            int index = fullOrRelativePath.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
            if (index >= 0) return fullOrRelativePath.Substring(index);

            marker = "Resources/Images/Products";
            index = fullOrRelativePath.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
            if (index >= 0) return fullOrRelativePath.Substring(index).Replace("/", "\\");

            return fullOrRelativePath;
        }

        private string GetFullImagePath(string relativeOrFullPath)
        {
            if (string.IsNullOrWhiteSpace(relativeOrFullPath)) return string.Empty;
            if (Path.IsPathRooted(relativeOrFullPath)) return relativeOrFullPath;
            string projectRoot = Directory.GetParent(Application.StartupPath).Parent.FullName;
            return Path.Combine(projectRoot, relativeOrFullPath);
        }

        private void picProductImage_Click(object sender, EventArgs e) { }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            try
            {
                string brandName = cmbProduct.SelectedValue?.ToString()?.Trim();
                if (string.IsNullOrWhiteSpace(brandName) || brandName == "Tất cả")
                {
                    string productName = txtProductName.Text.Trim();
                    if (!string.IsNullOrWhiteSpace(productName))
                    {
                        brandName = productName.Split(' ').FirstOrDefault();
                    }
                }

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
                        string brandFolderName = brandName.ToLower();
                        string projectRoot = Directory.GetParent(Application.StartupPath).Parent.FullName;
                        string resourcesPath = Path.Combine(projectRoot, "Resources", "Images", "Products", brandFolderName);

                        if (!Directory.Exists(resourcesPath))
                        {
                            Directory.CreateDirectory(resourcesPath);
                        }

                        var existingFiles = Directory.GetFiles(resourcesPath, $"{brandFolderName}*.*");
                        int nextNumber = 1;

                        if (existingFiles.Length > 0)
                        {
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

                        string extension = Path.GetExtension(openFileDialog.FileName);
                        string uniqueFileName = $"{brandFolderName}{nextNumber}{extension}";
                        string destinationPath = Path.Combine(resourcesPath, uniqueFileName);

                        File.Copy(openFileDialog.FileName, destinationPath, true);
                        txtImagePath.Text = GetRelativeImagePath(destinationPath);
                        LoadProductImage(destinationPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn ảnh:\n{ex.Message}\n\nChi tiết: {ex.StackTrace}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
