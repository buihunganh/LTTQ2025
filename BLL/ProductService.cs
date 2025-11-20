using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using BTL_LTTQ.DAL;
using BTL_LTTQ.DTO;

namespace BTL_LTTQ.BLL
{
    public class ProductService
    {
        public List<ChiTietSanPhamDTO> GetAllProducts()
        {
            try
            {
                const string sql = @"
                    SELECT 
                        ctsp.MaCTSP,
                        ctsp.MaSP,
                        sp.TenGiay,
                        ctsp.MaSize,
                        sz.KichCo,
                        ctsp.MaMau,
                        ms.TenMau,
                        ctsp.MaSKU,
                        ctsp.GiaNhap,
                        ctsp.GiaBan,
                        ctsp.SoLuongTon,
                        ctsp.TrangThai,
                        sp.HinhAnhChung,
                        lg.TenLoai
                    FROM ChiTietSanPham ctsp
                    INNER JOIN SanPham sp ON ctsp.MaSP = sp.MaSP
                    INNER JOIN SizeGiay sz ON ctsp.MaSize = sz.MaSize
                    INNER JOIN MauSac ms ON ctsp.MaMau = ms.MaMau
                    INNER JOIN LoaiGiay lg ON sp.MaLoai = lg.MaLoai
                    WHERE ctsp.TrangThai = 1 AND sp.TrangThai = 1
                    ORDER BY sp.TenGiay, sz.KichCo";

                using (var db = new DataProcesser())
                {
                    var table = db.ExecuteQuery(sql);
                    return ConvertToProductList(table);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách sản phẩm: {ex.Message}", ex);
            }
        }

        public List<ChiTietSanPhamDTO> SearchProducts(string searchText, int? maSize, int? maLoai, string priceFilter, decimal? priceValue)
        {
            try
            {
                var conditions = new List<string> { "ctsp.TrangThai = 1 AND sp.TrangThai = 1" };
                var parameters = new List<SqlParameter>();

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    conditions.Add("(sp.TenGiay LIKE @searchText OR ctsp.MaSKU LIKE @searchText OR lg.TenLoai LIKE @searchText)");
                    parameters.Add(new SqlParameter("@searchText", SqlDbType.NVarChar) { Value = $"%{searchText}%" });
                }

                if (maSize.HasValue && maSize.Value > 0)
                {
                    conditions.Add("ctsp.MaSize = @maSize");
                    parameters.Add(new SqlParameter("@maSize", SqlDbType.Int) { Value = maSize.Value });
                }

                if (maLoai.HasValue && maLoai.Value > 0)
                {
                    conditions.Add("sp.MaLoai = @maLoai");
                    parameters.Add(new SqlParameter("@maLoai", SqlDbType.Int) { Value = maLoai.Value });
                }

                if (!string.IsNullOrWhiteSpace(priceFilter) && priceValue.HasValue && priceValue.Value > 0)
                {
                    if (priceFilter == "GiaNhap")
                    {
                        conditions.Add("ctsp.GiaNhap <= @priceValue");
                    }
                    else if (priceFilter == "GiaBan")
                    {
                        conditions.Add("ctsp.GiaBan <= @priceValue");
                    }
                    parameters.Add(new SqlParameter("@priceValue", SqlDbType.Decimal) { Value = priceValue.Value });
                }

                var whereClause = string.Join(" AND ", conditions);
                var sql = $@"
                    SELECT 
                        ctsp.MaCTSP,
                        ctsp.MaSP,
                        sp.TenGiay,
                        ctsp.MaSize,
                        sz.KichCo,
                        ctsp.MaMau,
                        ms.TenMau,
                        ctsp.MaSKU,
                        ctsp.GiaNhap,
                        ctsp.GiaBan,
                        ctsp.SoLuongTon,
                        ctsp.TrangThai,
                        sp.HinhAnhChung,
                        lg.TenLoai
                    FROM ChiTietSanPham ctsp
                    INNER JOIN SanPham sp ON ctsp.MaSP = sp.MaSP
                    INNER JOIN SizeGiay sz ON ctsp.MaSize = sz.MaSize
                    INNER JOIN MauSac ms ON ctsp.MaMau = ms.MaMau
                    INNER JOIN LoaiGiay lg ON sp.MaLoai = lg.MaLoai
                    WHERE {whereClause}
                    ORDER BY sp.TenGiay, sz.KichCo";

                using (var db = new DataProcesser())
                {
                    var table = db.ExecuteQuery(sql, CommandType.Text, parameters.Count > 0 ? parameters.ToArray() : null);
                    return ConvertToProductList(table);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tìm kiếm sản phẩm: {ex.Message}", ex);
            }
        }

        public DataTable GetLoaiGiay()
        {
            try
            {
                const string sql = "SELECT MaLoai, TenLoai FROM LoaiGiay WHERE TrangThai = 1 ORDER BY TenLoai";
                using (var db = new DataProcesser())
                {
                    return db.ExecuteQuery(sql);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách loại giày: {ex.Message}", ex);
            }
        }

        public DataTable GetSizeGiay()
        {
            try
            {
                const string sql = "SELECT MaSize, KichCo FROM SizeGiay WHERE TrangThai = 1 ORDER BY CAST(KichCo AS INT)";
                using (var db = new DataProcesser())
                {
                    return db.ExecuteQuery(sql);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách size giày: {ex.Message}", ex);
            }
        }

        public DataTable GetMauSac()
        {
            try
            {
                const string sql = "SELECT MaMau, TenMau FROM MauSac WHERE TrangThai = 1 ORDER BY TenMau";
                using (var db = new DataProcesser())
                {
                    return db.ExecuteQuery(sql);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách màu sắc: {ex.Message}", ex);
            }
        }

        public DataTable GetSanPham()
        {
            try
            {
                const string sql = "SELECT MaSP, TenGiay FROM SanPham WHERE TrangThai = 1 ORDER BY TenGiay";
                using (var db = new DataProcesser())
                {
                    return db.ExecuteQuery(sql);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách sản phẩm: {ex.Message}", ex);
            }
        }

        public DataTable GetThuongHieu()
        {
            try
            {
                const string sql = "SELECT MaThuongHieu, TenThuongHieu FROM ThuongHieu WHERE TrangThai = 1 ORDER BY TenThuongHieu";
                using (var db = new DataProcesser())
                {
                    return db.ExecuteQuery(sql);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách thương hiệu: {ex.Message}", ex);
            }
        }

        public List<ChiTietSanPhamDTO> GetProductsByBrand(string brandName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(brandName))
                {
                    return GetAllProducts();
                }

                // Lọc sản phẩm theo thương hiệu - sử dụng LTRIM/RTRIM để tránh vấn đề khoảng trắng
                const string sql = @"
                    SELECT 
                        ctsp.MaCTSP,
                        ctsp.MaSP,
                        sp.TenGiay,
                        ctsp.MaSize,
                        sz.KichCo,
                        ctsp.MaMau,
                        ms.TenMau,
                        ctsp.MaSKU,
                        ctsp.GiaNhap,
                        ctsp.GiaBan,
                        ctsp.SoLuongTon,
                        ctsp.TrangThai,
                        sp.HinhAnhChung,
                        lg.TenLoai
                    FROM ChiTietSanPham ctsp
                    INNER JOIN SanPham sp ON ctsp.MaSP = sp.MaSP
                    INNER JOIN SizeGiay sz ON ctsp.MaSize = sz.MaSize
                    INNER JOIN MauSac ms ON ctsp.MaMau = ms.MaMau
                    INNER JOIN LoaiGiay lg ON sp.MaLoai = lg.MaLoai
                    INNER JOIN ThuongHieu th ON sp.MaThuongHieu = th.MaThuongHieu
                    WHERE ctsp.TrangThai = 1 AND sp.TrangThai = 1 
                          AND LTRIM(RTRIM(th.TenThuongHieu)) = LTRIM(RTRIM(@brandName))
                    ORDER BY sp.TenGiay, sz.KichCo";

                using (var db = new DataProcesser())
                {
                    var parameters = new[]
                    {
                        new SqlParameter("@brandName", SqlDbType.NVarChar) { Value = brandName.Trim() }
                    };
                    
                    System.Diagnostics.Debug.WriteLine($"GetProductsByBrand SQL - brandName: '{brandName.Trim()}'");
                    
                    var table = db.ExecuteQuery(sql, CommandType.Text, parameters);
                    var result = ConvertToProductList(table);
                    
                    System.Diagnostics.Debug.WriteLine($"GetProductsByBrand returned {result.Count} products");
                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetProductsByBrand: {ex.Message}");
                throw new Exception($"Lỗi khi lấy danh sách sản phẩm theo hãng: {ex.Message}", ex);
            }
        }

        public bool AddProduct(ChiTietSanPhamDTO product)
        {
            try
            {
                using (var db = new DataProcesser())
                {
                    const string variantSql = @"
                        SELECT MaCTSP, SoLuongTon 
                        FROM ChiTietSanPham 
                        WHERE MaSP = @MaSP AND MaSize = @MaSize AND MaMau = @MaMau";

                    var variantParams = new[]
                    {
                        new SqlParameter("@MaSP", SqlDbType.Int) { Value = product.MaSP },
                        new SqlParameter("@MaSize", SqlDbType.Int) { Value = product.MaSize },
                        new SqlParameter("@MaMau", SqlDbType.Int) { Value = product.MaMau }
                    };

                    var table = db.ExecuteQuery(variantSql, CommandType.Text, variantParams);

                    // Nếu đã có đúng biến thể (Sản phẩm + Size + Màu) => cộng thêm số lượng
                    if (table != null && table.Rows.Count > 0)
                    {
                        var row = table.Rows[0];
                        var maCTSP = Convert.ToInt32(row["MaCTSP"]);
                        var currentQty = row["SoLuongTon"] == DBNull.Value ? 0 : Convert.ToInt32(row["SoLuongTon"]);

                        const string updateSql = @"
                            UPDATE ChiTietSanPham
                            SET SoLuongTon = @NewSoLuong,
                                GiaNhap = @GiaNhap,
                                GiaBan = @GiaBan,
                                MaSKU = @MaSKU,
                                TrangThai = 1
                            WHERE MaCTSP = @MaCTSP";

                        var updateParams = new[]
                        {
                            new SqlParameter("@NewSoLuong", SqlDbType.Int) { Value = currentQty + product.SoLuongTon },
                            new SqlParameter("@GiaNhap", SqlDbType.Decimal) { Value = product.GiaNhap },
                            new SqlParameter("@GiaBan", SqlDbType.Decimal) { Value = product.GiaBan },
                            new SqlParameter("@MaSKU", SqlDbType.VarChar, 100) { Value = product.MaSKU },
                            new SqlParameter("@MaCTSP", SqlDbType.Int) { Value = maCTSP }
                        };

                        return db.ExecuteNonQuery(updateSql, CommandType.Text, updateParams) > 0;
                    }

                    // Chưa có biến thể này => kiểm tra SKU rồi insert mới
                    const string checkSkuSql = "SELECT COUNT(*) FROM ChiTietSanPham WHERE MaSKU = @MaSKU";
                    var checkParam = new SqlParameter("@MaSKU", SqlDbType.VarChar, 100) { Value = product.MaSKU };
                    var count = db.ExecuteScalar(checkSkuSql, CommandType.Text, checkParam);
                    if (Convert.ToInt32(count) > 0)
                        throw new Exception($"Mã SKU '{product.MaSKU}' đã tồn tại trong hệ thống!");

                    const string insertSql = @"
                        INSERT INTO ChiTietSanPham (MaSP, MaSize, MaMau, MaSKU, GiaNhap, GiaBan, SoLuongTon, TrangThai)
                        VALUES (@MaSP, @MaSize, @MaMau, @MaSKU, @GiaNhap, @GiaBan, @SoLuongTon, @TrangThai)";

                    var parameters = new[]
                    {
                        new SqlParameter("@MaSP", SqlDbType.Int) { Value = product.MaSP },
                        new SqlParameter("@MaSize", SqlDbType.Int) { Value = product.MaSize },
                        new SqlParameter("@MaMau", SqlDbType.Int) { Value = product.MaMau },
                        new SqlParameter("@MaSKU", SqlDbType.VarChar, 100) { Value = product.MaSKU },
                        new SqlParameter("@GiaNhap", SqlDbType.Decimal) { Value = product.GiaNhap },
                        new SqlParameter("@GiaBan", SqlDbType.Decimal) { Value = product.GiaBan },
                        new SqlParameter("@SoLuongTon", SqlDbType.Int) { Value = product.SoLuongTon },
                        new SqlParameter("@TrangThai", SqlDbType.Bit) { Value = product.TrangThai }
                    };

                    return db.ExecuteNonQuery(insertSql, CommandType.Text, parameters) > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm sản phẩm: {ex.Message}", ex);
            }
        }



        public bool UpdateProduct(ChiTietSanPhamDTO product)
        {
            try
            {
                // Kiểm tra MaSKU đã tồn tại ở sản phẩm khác chưa
                const string checkSql = "SELECT COUNT(*) FROM ChiTietSanPham WHERE MaSKU = @MaSKU AND MaCTSP != @MaCTSP";
                using (var db = new DataProcesser())
                {
                    var checkParams = new[]
                    {
                        new SqlParameter("@MaSKU", SqlDbType.VarChar, 100) { Value = product.MaSKU },
                        new SqlParameter("@MaCTSP", SqlDbType.Int) { Value = product.MaCTSP }
                    };
                    var count = db.ExecuteScalar(checkSql, CommandType.Text, checkParams);
                    if (Convert.ToInt32(count) > 0)
                    {
                        throw new Exception($"Mã SKU '{product.MaSKU}' đã tồn tại ở sản phẩm khác!");
                    }

                    const string sql = @"
                        UPDATE ChiTietSanPham 
                        SET MaSP = @MaSP, MaSize = @MaSize, MaMau = @MaMau, MaSKU = @MaSKU,
                            GiaNhap = @GiaNhap, GiaBan = @GiaBan, SoLuongTon = @SoLuongTon, TrangThai = @TrangThai
                        WHERE MaCTSP = @MaCTSP";

                    var parameters = new[]
                    {
                        new SqlParameter("@MaCTSP", SqlDbType.Int) { Value = product.MaCTSP },
                        new SqlParameter("@MaSP", SqlDbType.Int) { Value = product.MaSP },
                        new SqlParameter("@MaSize", SqlDbType.Int) { Value = product.MaSize },
                        new SqlParameter("@MaMau", SqlDbType.Int) { Value = product.MaMau },
                        new SqlParameter("@MaSKU", SqlDbType.VarChar, 100) { Value = product.MaSKU },
                        new SqlParameter("@GiaNhap", SqlDbType.Decimal) { Value = product.GiaNhap },
                        new SqlParameter("@GiaBan", SqlDbType.Decimal) { Value = product.GiaBan },
                        new SqlParameter("@SoLuongTon", SqlDbType.Int) { Value = product.SoLuongTon },
                        new SqlParameter("@TrangThai", SqlDbType.Bit) { Value = product.TrangThai }
                    };

                    return db.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật sản phẩm: {ex.Message}", ex);
            }
        }

        public bool UpdateProductImage(int maSP, string hinhAnhChung)
        {
            try
            {
                const string sql = "UPDATE SanPham SET HinhAnhChung = @HinhAnhChung WHERE MaSP = @MaSP";
                
                using (var db = new DataProcesser())
                {
                    var parameters = new[]
                    {
                        new SqlParameter("@HinhAnhChung", SqlDbType.NVarChar) { Value = (object)hinhAnhChung ?? DBNull.Value },
                        new SqlParameter("@MaSP", SqlDbType.Int) { Value = maSP }
                    };
                    
                    return db.ExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật hình ảnh sản phẩm: {ex.Message}", ex);
            }
        }

        public (bool success, string imagePath) DeleteProduct(int maCTSP)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"=== DeleteProduct called for MaCTSP: {maCTSP} ===");
                
                // Kiểm tra sản phẩm có đang được sử dụng trong hóa đơn không
                const string checkSql = @"
                    SELECT COUNT(*) 
                    FROM ChiTietHoaDon cthd 
                    INNER JOIN HoaDon hd ON cthd.MaHD = hd.MaHD
                    WHERE cthd.MaCTSP = @MaCTSP AND hd.TrangThai != N'Hủy'";

                using (var db = new DataProcesser())
                {
                    var checkParam = new SqlParameter("@MaCTSP", SqlDbType.Int) { Value = maCTSP };
                    var count = db.ExecuteScalar(checkSql, CommandType.Text, checkParam);
                    int invoiceCount = Convert.ToInt32(count);
                    System.Diagnostics.Debug.WriteLine($"Product in active invoices: {invoiceCount}");
                    
                    if (invoiceCount > 0)
                    {
                        throw new Exception("Không thể xóa sản phẩm này vì đã có trong hóa đơn!");
                    }

                    // Lấy MaSP và đường dẫn ảnh từ bảng SanPham trước khi xóa
                    const string getInfoSql = @"
                        SELECT sp.MaSP, sp.HinhAnhChung 
                        FROM ChiTietSanPham ctsp
                        INNER JOIN SanPham sp ON ctsp.MaSP = sp.MaSP
                        WHERE ctsp.MaCTSP = @MaCTSP";
                    var getInfoParam = new SqlParameter("@MaCTSP", SqlDbType.Int) { Value = maCTSP };
                    var infoTable = db.ExecuteQuery(getInfoSql, CommandType.Text, getInfoParam);
                    
                    if (infoTable.Rows.Count == 0)
                    {
                        throw new Exception("Không tìm thấy sản phẩm!");
                    }
                    
                    int maSP = Convert.ToInt32(infoTable.Rows[0]["MaSP"]);
                    string imagePath = infoTable.Rows[0]["HinhAnhChung"] != DBNull.Value 
                        ? infoTable.Rows[0]["HinhAnhChung"].ToString() 
                        : string.Empty;
                    System.Diagnostics.Debug.WriteLine($"MaSP: {maSP}, Image path: '{imagePath}'");

                    // Xóa biến thể sản phẩm từ ChiTietSanPham (hard delete)
                    const string deleteCTSPSql = "DELETE FROM ChiTietSanPham WHERE MaCTSP = @MaCTSP";
                    var deleteCTSPParam = new SqlParameter("@MaCTSP", SqlDbType.Int) { Value = maCTSP };
                    System.Diagnostics.Debug.WriteLine($"Executing DELETE from ChiTietSanPham: {deleteCTSPSql}");
                    int rowsAffected = db.ExecuteNonQuery(deleteCTSPSql, CommandType.Text, deleteCTSPParam);
                    System.Diagnostics.Debug.WriteLine($"Rows affected in ChiTietSanPham: {rowsAffected}");
                    
                    if (rowsAffected == 0)
                    {
                        System.Diagnostics.Debug.WriteLine("No rows deleted from ChiTietSanPham!");
                        return (false, imagePath);
                    }

                    // Kiểm tra xem còn biến thể nào khác của sản phẩm này không
                    const string checkVariantsSql = "SELECT COUNT(*) FROM ChiTietSanPham WHERE MaSP = @MaSP";
                    var checkVariantsParam = new SqlParameter("@MaSP", SqlDbType.Int) { Value = maSP };
                    var remainingVariants = db.ExecuteScalar(checkVariantsSql, CommandType.Text, checkVariantsParam);
                    int variantCount = Convert.ToInt32(remainingVariants);
                    System.Diagnostics.Debug.WriteLine($"Remaining variants for MaSP {maSP}: {variantCount}");

                    // Nếu không còn biến thể nào, xóa luôn sản phẩm gốc từ bảng SanPham
                    if (variantCount == 0)
                    {
                        const string deleteSPSql = "DELETE FROM SanPham WHERE MaSP = @MaSP";
                        var deleteSPParam = new SqlParameter("@MaSP", SqlDbType.Int) { Value = maSP };
                        System.Diagnostics.Debug.WriteLine($"No variants left, deleting from SanPham: {deleteSPSql}");
                        int spRowsAffected = db.ExecuteNonQuery(deleteSPSql, CommandType.Text, deleteSPParam);
                        System.Diagnostics.Debug.WriteLine($"Rows affected in SanPham: {spRowsAffected}");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Other variants exist, keeping SanPham record");
                    }
                    
                    System.Diagnostics.Debug.WriteLine("=== DeleteProduct completed successfully ===");
                    return (true, imagePath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR in DeleteProduct: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                throw new Exception($"Lỗi khi xóa sản phẩm: {ex.Message}", ex);
            }
        }

        public int EnsureBaseProduct(string tenGiay, int maLoai, string moTa, string hinhAnhChung)
        {
            if (string.IsNullOrWhiteSpace(tenGiay))
                throw new ArgumentException("Tên giày không được để trống", nameof(tenGiay));

            using (var db = new DataProcesser())
            {
                const string selectSql = @"
                        SELECT MaSP 
                        FROM SanPham 
                        WHERE TenGiay = @TenGiay AND MaLoai = @MaLoai AND TrangThai = 1";

                var selectParams = new[]
                {
                        new SqlParameter("@TenGiay", SqlDbType.NVarChar, 200) { Value = tenGiay },
                        new SqlParameter("@MaLoai", SqlDbType.Int) { Value = maLoai }
                    };

                var table = db.ExecuteQuery(selectSql, CommandType.Text, selectParams);
                if (table != null && table.Rows.Count > 0)
                {
                    var maSpObj = table.Rows[0]["MaSP"];
                    if (maSpObj != DBNull.Value)
                        return Convert.ToInt32(maSpObj);
                }

                const string insertSql = @"
                        INSERT INTO SanPham (TenGiay, MaLoai, MaThuongHieu, MoTa, HinhAnhChung, TrangThai)
                        VALUES (@TenGiay, @MaLoai, @MaThuongHieu, @MoTa, @HinhAnhChung, 1);
                        SELECT SCOPE_IDENTITY();";

                var insertParams = new[]
                {
                        new SqlParameter("@TenGiay", SqlDbType.NVarChar, 200) { Value = tenGiay },
                        new SqlParameter("@MaLoai", SqlDbType.Int) { Value = maLoai },
                        new SqlParameter("@MaThuongHieu", SqlDbType.Int) { Value = 1 }, // tạm mặc định 1
                        new SqlParameter("@MoTa", SqlDbType.NVarChar) { Value = (object)moTa ?? DBNull.Value },
                        new SqlParameter("@HinhAnhChung", SqlDbType.NVarChar) { Value = (object)hinhAnhChung ?? DBNull.Value }
                    };

                var newId = db.ExecuteScalar(insertSql, CommandType.Text, insertParams);
                return Convert.ToInt32(newId);
            }
        }

        private List<ChiTietSanPhamDTO> ConvertToProductList(DataTable table)
        {
            if (table == null || table.Rows.Count == 0)
                return new List<ChiTietSanPhamDTO>();

            return table.AsEnumerable().Select(row => new ChiTietSanPhamDTO
            {
                MaCTSP = row["MaCTSP"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaCTSP"]),
                MaSP = row["MaSP"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaSP"]),
                TenGiay = row["TenGiay"] == DBNull.Value ? "" : Convert.ToString(row["TenGiay"]),
                MaSize = row["MaSize"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaSize"]),
                KichCo = row["KichCo"] == DBNull.Value ? "" : Convert.ToString(row["KichCo"]),
                MaMau = row["MaMau"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaMau"]),
                TenMau = row["TenMau"] == DBNull.Value ? "" : Convert.ToString(row["TenMau"]),
                MaSKU = row["MaSKU"] == DBNull.Value ? "" : Convert.ToString(row["MaSKU"]),
                GiaNhap = row["GiaNhap"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GiaNhap"]),
                GiaBan = row["GiaBan"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GiaBan"]),
                SoLuongTon = row["SoLuongTon"] == DBNull.Value ? 0 : Convert.ToInt32(row["SoLuongTon"]),
                TrangThai = row["TrangThai"] == DBNull.Value ? true : Convert.ToBoolean(row["TrangThai"]),
                HinhAnhChung = row["HinhAnhChung"] == DBNull.Value ? "" : Convert.ToString(row["HinhAnhChung"]),
                TenLoai = row["TenLoai"] == DBNull.Value ? "" : Convert.ToString(row["TenLoai"])
            }).ToList();
        }
    }
}

