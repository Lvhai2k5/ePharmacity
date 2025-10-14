-- ================================
-- STORED PROCEDURES VÀ FUNCTIONS
-- ================================
USE NhaThuoc;
GO

-- ================================
-- 1. STORED PROCEDURE: Lấy lịch sử đơn hàng
-- ================================
CREATE OR ALTER PROCEDURE sp_GetOrderHistory
    @SearchCriteria NVARCHAR(50) = '',
    @SearchValue NVARCHAR(255) = '',
    @FromDate DATETIME = NULL,
    @ToDate DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @SQL NVARCHAR(MAX);
    DECLARE @WHERE NVARCHAR(MAX) = ' WHERE 1=1';
    
    -- Xây dựng điều kiện WHERE động
    IF @SearchCriteria = 'madon' AND @SearchValue != ''
    BEGIN
        SET @WHERE = @WHERE + ' AND dh.Madonhang = ' + @SearchValue;
    END
    ELSE IF @SearchCriteria = 'sodienthoai' AND @SearchValue != ''
    BEGIN
        SET @WHERE = @WHERE + ' AND dh.Sodienthoaikhachhang LIKE ''%' + @SearchValue + '%''';
    END
    
    IF @FromDate IS NOT NULL
        SET @WHERE = @WHERE + ' AND dh.Ngaytaodon >= ''' + CONVERT(NVARCHAR, @FromDate, 120) + '''';
    
    IF @ToDate IS NOT NULL
        SET @WHERE = @WHERE + ' AND dh.Ngaytaodon <= ''' + CONVERT(NVARCHAR, DATEADD(DAY, 1, @ToDate), 120) + '''';
    
    SET @SQL = 'SELECT dh.Madonhang, dh.Sodienthoaikhachhang, kh.Hovaten as TenKhachHang,
                       dh.Ngaytaodon, nv.Hovaten as TenNhanVien,
                       ISNULL(SUM(ctdh.Tongtiensanpham), 0) as Tongtien
                FROM Donhang dh
                INNER JOIN Thongtinkhachhang kh ON dh.Sodienthoaikhachhang = kh.Sodienthoai
                INNER JOIN Taikhoan nv ON kh.Manhanvien = nv.Manguoidung
                LEFT JOIN Chitietdonhang ctdh ON dh.Madonhang = ctdh.Madonhang' + @WHERE +
               ' GROUP BY dh.Madonhang, dh.Sodienthoaikhachhang, kh.Hovaten, dh.Ngaytaodon, nv.Hovaten
                ORDER BY dh.Ngaytaodon DESC';
    
    EXEC sp_executesql @SQL;
END
GO


-- ================================
-- 2. STORED PROCEDURE: Lấy chi tiết đơn hàng
-- ================================
CREATE OR ALTER PROCEDURE sp_GetOrderDetails
    @OrderId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT ctdh.Madonhang, ctdh.Masanpham, sp.Tenhang, 
           ctdh.Soluong, ctdh.DonGia, ctdh.Tongtiensanpham
    FROM Chitietdonhang ctdh
    INNER JOIN Sanphamthuoc sp ON ctdh.Masanpham = sp.ID
    WHERE ctdh.Madonhang = @OrderId
    ORDER BY ctdh.Masanpham;
END
GO

-- ================================
-- 3. STORED PROCEDURE: Tạo đơn hàng mới
-- ================================
CREATE OR ALTER PROCEDURE sp_CreateOrder
    @CustomerPhone NVARCHAR(20),
    @EmployeeId NVARCHAR(255),
    @OrderId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra khách hàng có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM Thongtinkhachhang WHERE Sodienthoai = @CustomerPhone)
        BEGIN
            RAISERROR('Khách hàng không tồn tại trong hệ thống!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra nhân viên có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM Taikhoan WHERE Manguoidung = @EmployeeId)
        BEGIN
            RAISERROR('Nhân viên không tồn tại trong hệ thống!', 16, 1);
            RETURN;
        END
        
        -- Tạo đơn hàng mới
        INSERT INTO Donhang (Sodienthoaikhachhang, Manhanvien, Ngaytaodon)
        VALUES (@CustomerPhone, @EmployeeId, GETDATE());
        
        SET @OrderId = SCOPE_IDENTITY();
        
        COMMIT TRANSACTION;
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        THROW;
    END CATCH
END
GO

-- ================================
-- 4. STORED PROCEDURE: Thêm chi tiết đơn hàng
-- ================================
CREATE OR ALTER PROCEDURE sp_AddOrderDetail
    @OrderId INT,
    @ProductId INT,
    @Quantity INT,
    @Price DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra đơn hàng có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM Donhang WHERE Madonhang = @OrderId)
        BEGIN
            RAISERROR('Đơn hàng không tồn tại!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra sản phẩm có tồn tại và đủ số lượng không
        DECLARE @AvailableStock INT;
        SELECT @AvailableStock = Soluong FROM Sanphamthuoc WHERE ID = @ProductId;
        
        IF @AvailableStock IS NULL
        BEGIN
            RAISERROR('Sản phẩm không tồn tại!', 16, 1);
            RETURN;
        END
        
        IF @AvailableStock < @Quantity
        BEGIN
            RAISERROR('Không đủ số lượng sản phẩm trong kho!', 16, 1);
            RETURN;
        END
        
        -- Thêm chi tiết đơn hàng (UPSERT - cập nhật nếu đã có, thêm mới nếu chưa có)
        IF EXISTS (SELECT 1 FROM Chitietdonhang WHERE Madonhang = @OrderId AND Masanpham = @ProductId)
        BEGIN
            -- Cập nhật số lượng và giá
            UPDATE Chitietdonhang 
            SET Soluong = Soluong + @Quantity,
                DonGia = @Price
            WHERE Madonhang = @OrderId AND Masanpham = @ProductId;
        END
        ELSE
        BEGIN
            -- Thêm mới
            INSERT INTO Chitietdonhang (Madonhang, Masanpham, Soluong, DonGia)
            VALUES (@OrderId, @ProductId, @Quantity, @Price);
        END
        
        -- Cập nhật số lượng tồn kho (dùng cột Kho thay vì Soluong)
        UPDATE Sanphamthuoc 
        SET Kho = Kho - @Quantity
        WHERE ID = @ProductId;
        
        COMMIT TRANSACTION;
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        THROW;
    END CATCH
END
GO

-- ================================
-- 5. STORED PROCEDURE: Lấy danh sách sản phẩm có sẵn
-- ================================
CREATE OR ALTER PROCEDURE sp_GetAvailableProducts
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT sp.ID, sp.Tenhang,
           sp.Kho AS Kho,
           sp.Soluong as SoLuongTon, 
           sp.Giaban, sp.Ngayhethan, lt.TenLoai, nt.TenNhom
    FROM Sanphamthuoc sp
    INNER JOIN LoaiThuoc lt ON sp.MaLoai = lt.MaLoai
    INNER JOIN NhomThuoc nt ON lt.MaNhom = nt.MaNhom
    WHERE sp.Kho >= 0  -- Hiển thị cả sản phẩm có kho = 0
    ORDER BY sp.Kho DESC, sp.Tenhang;  -- Sắp xếp theo kho giảm dần, sau đó theo tên
END
GO

-- ================================
-- 6. STORED PROCEDURE: Kiểm tra số điện thoại khách hàng
-- ================================
CREATE OR ALTER PROCEDURE sp_CheckCustomerPhone
    @PhoneNumber NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Sodienthoai, Hovaten, Diemtichluy
    FROM Thongtinkhachhang
    WHERE Sodienthoai = @PhoneNumber;
END
GO

-- ================================
-- 7. STORED PROCEDURE: Tạo khách hàng mới
-- ================================
CREATE OR ALTER PROCEDURE sp_CreateCustomer
    @PhoneNumber NVARCHAR(20),
    @FullName NVARCHAR(255),
    @EmployeeId NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra số điện thoại đã tồn tại chưa
        IF EXISTS (SELECT 1 FROM Thongtinkhachhang WHERE Sodienthoai = @PhoneNumber)
        BEGIN
            RAISERROR('Số điện thoại đã tồn tại trong hệ thống!', 16, 1);
            RETURN;
        END
        
        -- Kiểm tra nhân viên có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM Taikhoan WHERE Manguoidung = @EmployeeId)
        BEGIN
            RAISERROR('Nhân viên không tồn tại trong hệ thống!', 16, 1);
            RETURN;
        END
        
        -- Tạo khách hàng mới
        INSERT INTO Thongtinkhachhang (Sodienthoai, Hovaten, Manhanvien)
        VALUES (@PhoneNumber, @FullName, @EmployeeId);
        
        COMMIT TRANSACTION;
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        THROW;
    END CATCH
END
GO

-- ================================
-- 8. FUNCTION: Tính tổng tiền đơn hàng
-- ================================
CREATE OR ALTER FUNCTION fn_CalculateOrderTotal(@OrderId INT)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @Total DECIMAL(18,2) = 0;
    
    SELECT @Total = ISNULL(SUM(Tongtiensanpham), 0)
    FROM Chitietdonhang
    WHERE Madonhang = @OrderId;
    
    RETURN @Total;
END
GO

-- ================================
-- 9. STORED PROCEDURE: Lấy thống kê đơn hàng
-- ================================
CREATE OR ALTER PROCEDURE sp_GetOrderStatistics
    @FromDate DATETIME = NULL,
    @ToDate DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @FromDate IS NULL SET @FromDate = DATEADD(MONTH, -1, GETDATE());
    IF @ToDate IS NULL SET @ToDate = GETDATE();
    
    SELECT 
        COUNT(*) as TotalOrders,
        SUM(dbo.fn_CalculateOrderTotal(dh.Madonhang)) as TotalRevenue,
        AVG(dbo.fn_CalculateOrderTotal(dh.Madonhang)) as AverageOrderValue,
        COUNT(DISTINCT dh.Sodienthoaikhachhang) as UniqueCustomers
    FROM Donhang dh
    WHERE dh.Ngaytaodon BETWEEN @FromDate AND @ToDate;
END
GO

-- ================================
-- 10. STORED PROCEDURE: Kiểm tra tồn kho sản phẩm
-- ================================
CREATE OR ALTER PROCEDURE sp_CheckProductStock
    @ProductId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT ID, Tenhang, Kho, Soluong as SoLuongTon, Giaban
    FROM Sanphamthuoc
    WHERE ID = @ProductId;
END
GO

-- ================================
-- 11. STORED PROCEDURE: Đảm bảo nhân viên mặc định tồn tại
-- ================================
CREATE OR ALTER PROCEDURE sp_EnsureDefaultEmployee
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM Taikhoan WHERE Manguoidung = 'NV001')
    BEGIN
        INSERT INTO Taikhoan (Manguoidung, Hovaten, Sodienthoai, Matkhau, Chucvu)
        VALUES ('NV001', N'Nhân viên mặc định', '0000000000', '123456', N'Nhân viên bán hàng');
        
        PRINT 'Đã tạo nhân viên mặc định NV001';
    END
    ELSE
    BEGIN
        PRINT 'Nhân viên mặc định NV001 đã tồn tại';
    END
END
GO

-- ================================
-- 12. STORED PROCEDURE: Xóa đơn hàng (nếu cần)
-- ================================
CREATE OR ALTER PROCEDURE sp_DeleteOrder
    @OrderId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Kiểm tra đơn hàng có tồn tại không
        IF NOT EXISTS (SELECT 1 FROM Donhang WHERE Madonhang = @OrderId)
        BEGIN
            RAISERROR('Đơn hàng không tồn tại!', 16, 1);
            RETURN;
        END
        
        -- Trả lại số lượng sản phẩm vào kho
        UPDATE sp 
        SET sp.Soluong = sp.Soluong + ctdh.Soluong
        FROM Sanphamthuoc sp
        INNER JOIN Chitietdonhang ctdh ON sp.ID = ctdh.Masanpham
        WHERE ctdh.Madonhang = @OrderId;
        
        -- Xóa đơn hàng (chi tiết sẽ tự động xóa do CASCADE)
        DELETE FROM Donhang WHERE Madonhang = @OrderId;
        
        COMMIT TRANSACTION;
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        THROW;
    END CATCH
END
GO

PRINT 'Tất cả stored procedures và functions đã được tạo thành công!';
