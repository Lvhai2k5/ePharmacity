using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaThuoc
{
    internal class DatabaseHelper
    {
        public class DatabaseConnection
        {
            public string connection = "Data Source=LEVUHAI;Initial Catalog=NhaThuocDB;User ID=sa;Password=123;Integrated Security=True";
            public bool TestConnection()
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connection))
                    {
                        conn.Open();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi kết nối database:\n{ex.Message}\n\n" +
                                  "Vui lòng kiểm tra:\n" +
                                  "1. SQL Server đang chạy\n" +
                                  "2. Database 'NhaThuocDB' đã được tạo\n" +
                                  "3. Chạy script CreateDatabaseOnly.sql", 
                                  "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            public DataTable GetData(string query, params SqlParameter[] parameters)
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                cmd.Parameters.AddRange(parameters);
                        
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
                }
            }
            public int ExecuteNonQuery(string query, params SqlParameter[] parameters)
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);
                        
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            public object ExecuteScalar(string query, params SqlParameter[] parameters)
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);
                        
                        return cmd.ExecuteScalar();
                    }
                }
            }
            public bool IsPhoneNumberExists(string phoneNumber)
            {
                string query = "SELECT COUNT(*) FROM Thongtinkhachhang WHERE Sodienthoai = @phone";
                SqlParameter param = new SqlParameter("@phone", phoneNumber);
                int count = Convert.ToInt32(ExecuteScalar(query, param));
                return count > 0;
            }
            public DataTable GetCustomerByPhone(string phoneNumber)
            {
                string query = @"SELECT * FROM Thongtinkhachhang 
                               WHERE Sodienthoai = @phone";
                SqlParameter param = new SqlParameter("@phone", phoneNumber);
                return GetData(query, param);
            }
            public DataTable GetOrderHistory(string searchCriteria = "", string searchValue = "", DateTime? fromDate = null, DateTime? toDate = null)
            {
                string storedProcedure = "sp_GetOrderHistory";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@SearchCriteria", searchCriteria ?? ""),
                    new SqlParameter("@SearchValue", searchValue ?? ""),
                    new SqlParameter("@FromDate", fromDate.HasValue ? (object)fromDate.Value : DBNull.Value),
                    new SqlParameter("@ToDate", toDate.HasValue ? (object)toDate.Value : DBNull.Value)
                };

                return GetDataFromStoredProcedure(storedProcedure, parameters);
            }
            public DataTable GetOrderDetails(int orderId)
            {
                string storedProcedure = "sp_GetOrderDetails";
                SqlParameter param = new SqlParameter("@OrderId", orderId);
                return GetDataFromStoredProcedure(storedProcedure, param);
            }
            public DataTable GetAvailableProducts()
            {
                string storedProcedure = "sp_GetAvailableProducts";
                return GetDataFromStoredProcedure(storedProcedure);
            }
            public int GetProductStock(int productId)
            {
                DataTable result = GetDataFromStoredProcedure("sp_CheckProductStock", 
                    new SqlParameter("@ProductId", productId));
                
                if (result.Rows.Count > 0)
                {
                    if (result.Columns.Contains("Kho") && result.Rows[0]["Kho"] != DBNull.Value)
                        return Convert.ToInt32(result.Rows[0]["Kho"]);
                    if (result.Columns.Contains("SoLuongTon") && result.Rows[0]["SoLuongTon"] != DBNull.Value)
                        return Convert.ToInt32(result.Rows[0]["SoLuongTon"]);
                    return 0;
                }
                return 0;
            }
            public int CreateOrder(string customerPhone, string employeeId)
            {
                SqlParameter orderIdParam = new SqlParameter("@OrderId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                
                SqlParameter[] parameters = {
                    new SqlParameter("@CustomerPhone", customerPhone),
                    new SqlParameter("@EmployeeId", employeeId),
                    orderIdParam
                };
                
                ExecuteStoredProcedure("sp_CreateOrder", parameters);
                
                return Convert.ToInt32(orderIdParam.Value);
            }
            public void AddOrderDetail(int orderId, int productId, int quantity, decimal price)
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@OrderId", orderId),
                    new SqlParameter("@ProductId", productId),
                    new SqlParameter("@Quantity", quantity),
                    new SqlParameter("@Price", price)
                };
                
                ExecuteStoredProcedure("sp_AddOrderDetail", parameters);
            }
            public void UpdateProductStock(int productId, int newQuantity)
            {
                string query = "UPDATE Sanphamthuoc SET Soluong = @quantity WHERE ID = @productId";
                SqlParameter[] parameters = {
                    new SqlParameter("@quantity", newQuantity),
                    new SqlParameter("@productId", productId)
                };
                
                ExecuteNonQuery(query, parameters);
            }
            public bool IsEmployeeExists(string employeeId)
            {
                string query = "SELECT COUNT(*) FROM Taikhoan WHERE Manguoidung = @employeeId";
                SqlParameter param = new SqlParameter("@employeeId", employeeId);
                int count = Convert.ToInt32(ExecuteScalar(query, param));
                return count > 0;
            }
            public bool ValidateDatabaseIntegrity()
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connection))
                    {
                        conn.Open();
                    }
                    EnsureDefaultEmployeeExists();
                    string[] requiredTables = { "Taikhoan", "Thongtinkhachhang", "Donhang", "Sanphamthuoc" };
                    foreach (string table in requiredTables)
                    {
                        string checkQuery = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{table}'";
                        int count = Convert.ToInt32(ExecuteScalar(checkQuery));
                        if (count == 0)
                        {
                            throw new Exception($"Bảng {table} không tồn tại trong database.");
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Database validation failed: {ex.Message}");
                    return false;
                }
            }
            public DataTable GetDataFromStoredProcedure(string storedProcedureName, params SqlParameter[] parameters)
            {
                DataTable dataTable = new DataTable();
                
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        
                        conn.Open();
                        
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                
                return dataTable;
            }
            private void ExecuteStoredProcedure(string storedProcedureName, params SqlParameter[] parameters)
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            public void EnsureDefaultEmployeeExists()
            {
                try
                {
                    ExecuteStoredProcedure("sp_EnsureDefaultEmployee");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error ensuring default employee: {ex.Message}");
                }
            }
            public void CreateCustomer(string phoneNumber, string fullName, string employeeId)
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@PhoneNumber", phoneNumber),
                    new SqlParameter("@FullName", fullName),
                    new SqlParameter("@EmployeeId", employeeId)
                };
                
                ExecuteStoredProcedure("sp_CreateCustomer", parameters);
            }
            public DataTable CheckCustomerPhone(string phoneNumber)
            {
                return GetDataFromStoredProcedure("sp_CheckCustomerPhone", 
                    new SqlParameter("@PhoneNumber", phoneNumber));
            }
            public DataTable GetOrderStatistics(DateTime? fromDate = null, DateTime? toDate = null)
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@FromDate", fromDate.HasValue ? (object)fromDate.Value : DBNull.Value),
                    new SqlParameter("@ToDate", toDate.HasValue ? (object)toDate.Value : DBNull.Value)
                };
                
                return GetDataFromStoredProcedure("sp_GetOrderStatistics", parameters);
            }
            public DataTable GetAllCustomers()
            {
                string query = @"
                    SELECT Hovaten, Sodienthoai, Diemtichluy
                    FROM Thongtinkhachhang 
                    ORDER BY Sodienthoai DESC";
                return GetData(query);
            }
            public DataTable SearchCustomers(string searchTerm)
            {
                string query = @"
                    SELECT Hovaten, Sodienthoai, Diemtichluy
                    FROM Thongtinkhachhang 
                    WHERE Sodienthoai LIKE @SearchTerm 
                       OR Hovaten LIKE @SearchTerm
                    ORDER BY Sodienthoai DESC";
                
                SqlParameter[] parameters = {
                    new SqlParameter("@SearchTerm", "%" + searchTerm + "%")
                };
                
                return GetData(query, parameters);
            }
            public bool UpdateCustomer(string phoneNumber, string fullName, int points)
            {
                try
                {
                    string updateQuery = @"
                        UPDATE Thongtinkhachhang 
                        SET Hovaten = @FullName, 
                            Diemtichluy = @Points
                        WHERE Sodienthoai = @PhoneNumber";
                    
                    SqlParameter[] updateParams = {
                        new SqlParameter("@PhoneNumber", phoneNumber),
                        new SqlParameter("@FullName", fullName),
                        new SqlParameter("@Points", points)
                    };
                    
                    int rowsAffected = ExecuteNonQuery(updateQuery, updateParams);
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật khách hàng: {ex.Message}", 
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            private void ExecuteQuery(string query, params SqlParameter[] parameters)
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            public int CalculatePointsFromOrder(decimal orderValue)
            {
                return (int)Math.Floor(orderValue / 1000);
            }
            public bool AddPointsToCustomer(string phoneNumber, int points)
            {
                try
                {
                    string query = @"
                        UPDATE Thongtinkhachhang 
                        SET Diemtichluy = Diemtichluy + @Points
                        WHERE Sodienthoai = @PhoneNumber";
                    
                    SqlParameter[] parameters = {
                        new SqlParameter("@PhoneNumber", phoneNumber),
                        new SqlParameter("@Points", points)
                    };
                    
                    ExecuteQuery(query, parameters);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cộng điểm tích lũy: {ex.Message}", 
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool AddPointsFromOrder(string phoneNumber, decimal orderValue)
            {
                int points = CalculatePointsFromOrder(orderValue);
                if (points > 0)
                {
                    return AddPointsToCustomer(phoneNumber, points);
                }
                return true; 
            }
            public int GetCustomerPoints(string phoneNumber)
            {
                try
                {
                    string query = "SELECT Diemtichluy FROM Thongtinkhachhang WHERE Sodienthoai = @PhoneNumber";
                    SqlParameter param = new SqlParameter("@PhoneNumber", phoneNumber);
                    object result = ExecuteScalar(query, param);
                    return result != null ? Convert.ToInt32(result) : 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lấy điểm tích lũy: {ex.Message}", 
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }
            public bool RedeemCustomerPoints(string phoneNumber, int points)
            {
                try
                {
                    // Đảm bảo không trừ quá số điểm hiện có
                    string query = @"
                        UPDATE Thongtinkhachhang 
                        SET Diemtichluy = Diemtichluy - @Points
                        WHERE Sodienthoai = @PhoneNumber AND Diemtichluy >= @Points";

                    SqlParameter[] parameters = {
                        new SqlParameter("@PhoneNumber", phoneNumber),
                        new SqlParameter("@Points", points)
                    };

                    int affected = ExecuteNonQuery(query, parameters);
                    if (affected == 0)
                    {
                        MessageBox.Show("Không đủ điểm để áp dụng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi trừ điểm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            public bool CheckStockAvailability(List<OrderItem> orderItems, out List<string> insufficientStockItems)
            {
                insufficientStockItems = new List<string>();
                bool hasInsufficientStock = false;

                foreach (var item in orderItems)
                {
                    int currentStock = GetProductStock(item.ProductId);
                    if (currentStock < item.Quantity)
                    {
                        insufficientStockItems.Add($"{item.ProductName}: Cần {item.Quantity}, Có {currentStock}");
                        hasInsufficientStock = true;
                    }
                }
                return !hasInsufficientStock;
            }
        }
    }
}
