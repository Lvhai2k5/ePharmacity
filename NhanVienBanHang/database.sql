-- ================================
-- Xóa database cũ nếu tồn tại
-- ================================


-- ================================
-- Tạo Database mới
-- ================================
CREATE DATABASE NhaThuoc;
GO
USE NhaThuoc;
GO

-- ================================
-- 1. Bảng Tài khoản
-- ================================
CREATE TABLE Taikhoan (
    Manguoidung nvarchar(255) primary key not null ,
    Hovaten NVARCHAR(255) NOT NULL,
    Sodienthoai NVARCHAR(20) NOT NULL UNIQUE,
    Matkhau NVARCHAR(255) NOT NULL,
    Ngaysinh DATE,
    Chucvu NVARCHAR(50) null
);

-- ================================
-- 2. Bảng Thông tin khách hàng
-- ================================
CREATE TABLE Thongtinkhachhang (
    Sodienthoai NVARCHAR(20) PRIMARY KEY,
    Hovaten NVARCHAR(255) NOT NULL,
    Diemtichluy INT DEFAULT 0,
    Manhanvien nvarchar(255) not null,
    CONSTRAINT FK_KH_NV FOREIGN KEY (Manhanvien) REFERENCES Taikhoan(Manguoidung)
);

-- ================================
-- 3. Bảng Nhóm thuốc
-- ================================
CREATE TABLE NhomThuoc (
    MaNhom INT PRIMARY KEY IDENTITY(1,1),
    TenNhom NVARCHAR(100) NOT NULL UNIQUE,
    MoTa NVARCHAR(255)
);

-- ================================
-- 4. Bảng Loại thuốc
-- ================================
CREATE TABLE LoaiThuoc (
    MaLoai INT PRIMARY KEY IDENTITY(1,1),
    TenLoai NVARCHAR(100) NOT NULL UNIQUE,
    MoTa NVARCHAR(255),
    MaNhom INT NOT NULL,
    CONSTRAINT FK_LoaiThuoc_Nhom FOREIGN KEY (MaNhom) REFERENCES NhomThuoc(MaNhom)
);

-- ================================
-- 5. Bảng Sản phẩm thuốc
-- ================================
CREATE TABLE Sanphamthuoc (
    ID INT PRIMARY KEY IDENTITY(1,1),
    MaLoai INT NOT NULL,
    Mahanghoa NVARCHAR(50),
    Mavach NVARCHAR(50),
    Tenhang NVARCHAR(255) NOT NULL,
    Soluong INT DEFAULT 0,
    Mathuoc NVARCHAR(50),
    Thanhphan NVARCHAR(255),
    Nhasanxuat NVARCHAR(255),
    Donggoi NVARCHAR(255),
    Gianhap DECIMAL(18,2) NOT NULL,   -- giá nhập
    Kho INT DEFAULT 0,
    Mota NVARCHAR(500),
    Lohang NVARCHAR(100),
    Ngayhethan DATE,
    Giaban DECIMAL(18,2) NOT NULL,     -- giá bán
    CONSTRAINT FK_SP_Loai FOREIGN KEY (MaLoai) REFERENCES LoaiThuoc(MaLoai)
);

-- ================================
-- 6. Bảng Đơn hàng
-- ================================
CREATE TABLE Donhang (
    Madonhang INT PRIMARY KEY IDENTITY(1,1),
    Sodienthoaikhachhang NVARCHAR(20) NOT NULL,
	Manhanvien nvarchar(255) not null,
    Ngaytaodon DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_DH_KH FOREIGN KEY (Sodienthoaikhachhang) REFERENCES Thongtinkhachhang(Sodienthoai)
);

-- ================================
-- 7. Bảng Chi tiết đơn hàng
-- ================================
CREATE TABLE Chitietdonhang (
    Madonhang INT NOT NULL,
Masanpham INT NOT NULL,
    Soluong INT NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,   -- giá bán tại thời điểm đặt
    Tongtiensanpham AS (Soluong * DonGia) PERSISTED, -- cột tính toán
    CONSTRAINT PK_CTDH PRIMARY KEY (Madonhang, Masanpham),
    CONSTRAINT FK_CTDH_DH FOREIGN KEY (Madonhang) REFERENCES Donhang(Madonhang) ON DELETE CASCADE,
    CONSTRAINT FK_CTDH_SP FOREIGN KEY (Masanpham) REFERENCES Sanphamthuoc(ID)
);

-- ================================
-- 8. Bảng Phản hồi khách hàng
-- ================================
CREATE TABLE Phanhoikhachhang (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Hovaten NVARCHAR(255) NOT NULL,
    Sodienthoai NVARCHAR(20) NOT NULL,
    Manhanvien nvarchar(255) NOT NULL,
    Phanhoi NVARCHAR(1000),
    Ngaytao DATETIME DEFAULT GETDATE(),
	Trangthai nvarchar(100),
    CONSTRAINT FK_PHKH_NV FOREIGN KEY (Manhanvien) REFERENCES Taikhoan(Manguoidung),
    CONSTRAINT FK_PHKH_KH FOREIGN KEY (Sodienthoai) REFERENCES Thongtinkhachhang(Sodienthoai)
);



-- ================================
-- DỮ LIỆU MẪU ĐÃ SỬA LỖI FOREIGN KEY
-- ================================

-- Xóa dữ liệu cũ nếu có (chỉ xóa dữ liệu thuốc)
DELETE FROM Sanphamthuoc;
DELETE FROM LoaiThuoc;
DELETE FROM NhomThuoc;

-- Reset IDENTITY columns (bắt đầu từ 1)
DBCC CHECKIDENT ('NhomThuoc', RESEED, 0);
DBCC CHECKIDENT ('LoaiThuoc', RESEED, 0);
DBCC CHECKIDENT ('Sanphamthuoc', RESEED, 0);

-- ================================
-- 1. Dữ liệu mẫu cho bảng NhomThuoc
-- ================================
INSERT INTO NhomThuoc (TenNhom, MoTa) VALUES
(N'Thuốc kháng sinh', N'Thuốc điều trị nhiễm khuẩn'),
(N'Thuốc giảm đau', N'Thuốc giảm đau, hạ sốt'),
(N'Thuốc tim mạch', N'Thuốc điều trị bệnh tim mạch'),
(N'Thuốc tiêu hóa', N'Thuốc điều trị bệnh tiêu hóa'),
(N'Thuốc hô hấp', N'Thuốc điều trị bệnh hô hấp'),
(N'Vitamin', N'Vitamin và khoáng chất'),
(N'Thuốc da liễu', N'Thuốc điều trị bệnh da'),
(N'Thuốc mắt', N'Thuốc nhỏ mắt'),
(N'Thuốc thần kinh', N'Thuốc điều trị bệnh thần kinh'),
(N'Dụng cụ y tế', N'Dụng cụ và thiết bị y tế');

-- ================================
-- 2. Dữ liệu mẫu cho bảng LoaiThuoc (Sử dụng SELECT để lấy MaNhom đúng)
-- ================================
INSERT INTO LoaiThuoc (TenLoai, MoTa, MaNhom) VALUES
-- Thuốc kháng sinh
(N'Penicillin', N'Kháng sinh nhóm penicillin', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc kháng sinh')),
(N'Cephalosporin', N'Kháng sinh nhóm cephalosporin', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc kháng sinh')),
(N'Macrolide', N'Kháng sinh nhóm macrolide', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc kháng sinh')),
(N'Quinolone', N'Kháng sinh nhóm quinolone', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc kháng sinh')),

-- Thuốc giảm đau
(N'NSAID', N'Thuốc chống viêm không steroid', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc giảm đau')),
(N'Paracetamol', N'Thuốc giảm đau, hạ sốt', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc giảm đau')),
(N'Opioid', N'Thuốc giảm đau opioid', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc giảm đau')),

-- Thuốc tim mạch
(N'Huyết áp', N'Thuốc điều trị huyết áp cao', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc tim mạch')),
(N'Tim mạch', N'Thuốc điều trị tim mạch', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc tim mạch')),

-- Thuốc tiêu hóa
(N'Kháng acid', N'Thuốc kháng acid dạ dày', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc tiêu hóa')),
(N'Nhuận tràng', N'Thuốc nhuận tràng', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc tiêu hóa')),
(N'Men tiêu hóa', N'Men tiêu hóa', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc tiêu hóa')),

-- Thuốc hô hấp
(N'Ho', N'Thuốc trị ho', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc hô hấp')),
(N'Hen suyễn', N'Thuốc trị hen suyễn', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc hô hấp')),

-- Vitamin
(N'Vitamin C', N'Vitamin C', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Vitamin')),
(N'Canxi', N'Bổ sung canxi', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Vitamin')),
(N'Vitamin B', N'Vitamin nhóm B', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Vitamin')),
(N'Vitamin D', N'Vitamin D', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Vitamin')),
(N'Multivitamin', N'Vitamin tổng hợp', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Vitamin')),

-- Thuốc da liễu
(N'Kháng nấm', N'Thuốc kháng nấm da', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc da liễu')),
(N'Kem dưỡng', N'Kem dưỡng da', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc da liễu')),

-- Thuốc mắt
(N'Nhỏ mắt', N'Thuốc nhỏ mắt', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc mắt')),

-- Thuốc thần kinh
(N'An thần', N'Thuốc an thần', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc thần kinh')),
(N'Chống trầm cảm', N'Thuốc chống trầm cảm', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Thuốc thần kinh')),

-- Dụng cụ y tế
(N'Băng gạc', N'Băng gạc y tế', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Dụng cụ y tế')),
(N'Thuốc sát trùng', N'Thuốc sát trùng', (SELECT MaNhom FROM NhomThuoc WHERE TenNhom = N'Dụng cụ y tế'));

-- ================================
-- 3. Dữ liệu mẫu cho bảng Sanphamthuoc (Sử dụng SELECT để lấy MaLoai đúng)
-- ================================
INSERT INTO Sanphamthuoc (MaLoai, Tenhang, Soluong, Gianhap, Giaban, Ngayhethan) VALUES
-- Penicillin - Thuốc kháng sinh
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Penicillin'), N'Amoxicillin 500mg', 100, 150000, 180000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Penicillin'), N'Amoxicillin 250mg', 80, 120000, 140000, '2025-11-30'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Penicillin'), N'Ampicillin 500mg', 60, 130000, 160000, '2025-10-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Penicillin'), N'Penicillin V 250mg', 90, 110000, 135000, '2025-12-15'),

-- Cephalosporin - Thuốc kháng sinh
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Cephalosporin'), N'Cephalexin 500mg', 75, 160000, 190000, '2025-11-30'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Cephalosporin'), N'Cefuroxime 250mg', 45, 180000, 220000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Cephalosporin'), N'Ceftriaxone 1g', 25, 250000, 300000, '2025-10-31'),

-- NSAID - Thuốc giảm đau
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'NSAID'), N'Ibuprofen 400mg', 120, 80000, 95000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'NSAID'), N'Aspirin 100mg', 200, 30000, 35000, '2025-11-30'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'NSAID'), N'Naproxen 250mg', 85, 90000, 110000, '2025-12-15'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'NSAID'), N'Diclofenac 50mg', 95, 75000, 90000, '2025-11-15'),

-- Paracetamol - Thuốc giảm đau
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Paracetamol'), N'Paracetamol 500mg', 200, 50000, 60000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Paracetamol'), N'Paracetamol 1000mg', 150, 70000, 85000, '2025-11-30'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Paracetamol'), N'Paracetamol 325mg', 180, 45000, 55000, '2025-12-15'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Paracetamol'), N'Paracetamol 650mg', 120, 60000, 72000, '2025-11-15'),

-- Huyết áp - Thuốc tim mạch
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Huyết áp'), N'Amlodipine 5mg', 70, 120000, 145000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Huyết áp'), N'Losartan 50mg', 65, 100000, 120000, '2025-11-30'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Huyết áp'), N'Enalapril 5mg', 55, 95000, 115000, '2025-12-15'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Huyết áp'), N'Metoprolol 50mg', 60, 110000, 135000, '2025-11-15'),

-- Vitamin C - Vitamin
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Vitamin C'), N'Vitamin C 1000mg', 150, 80000, 100000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Vitamin C'), N'Vitamin C 500mg', 180, 60000, 75000, '2025-11-30'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Vitamin C'), N'Vitamin C 2000mg', 100, 120000, 150000, '2025-12-15'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Vitamin C'), N'Vitamin C 250mg', 200, 45000, 55000, '2025-11-15'),

-- Canxi - Vitamin
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Canxi'), N'Calcium 500mg', 80, 100000, 120000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Canxi'), N'Calcium 600mg', 90, 110000, 135000, '2025-11-30'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Canxi'), N'Calcium + D3 1000mg', 75, 140000, 170000, '2025-12-15'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Canxi'), N'Calcium Carbonate 750mg', 85, 95000, 115000, '2025-11-15'),

-- Macrolide - Thuốc kháng sinh
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Macrolide'), N'Azithromycin 500mg', 50, 200000, 240000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Macrolide'), N'Clarithromycin 250mg', 65, 180000, 220000, '2025-11-30'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Macrolide'), N'Erythromycin 250mg', 70, 160000, 195000, '2025-12-15'),

-- Quinolone - Thuốc kháng sinh
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Quinolone'), N'Ciprofloxacin 500mg', 45, 220000, 270000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Quinolone'), N'Levofloxacin 500mg', 40, 240000, 290000, '2025-11-30'),

-- Opioid - Thuốc giảm đau
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Opioid'), N'Tramadol 50mg', 30, 150000, 180000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Opioid'), N'Codeine 30mg', 25, 120000, 145000, '2025-11-30'),

-- Tim mạch - Thuốc tim mạch
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Tim mạch'), N'Digoxin 0.25mg', 35, 130000, 160000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Tim mạch'), N'Warfarin 5mg', 40, 110000, 135000, '2025-11-30'),

-- Kháng acid - Thuốc tiêu hóa
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Kháng acid'), N'Omeprazole 20mg', 85, 90000, 110000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Kháng acid'), N'Ranitidine 150mg', 95, 70000, 85000, '2025-11-30'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Kháng acid'), N'Pantoprazole 40mg', 75, 100000, 120000, '2025-12-15'),

-- Nhuận tràng - Thuốc tiêu hóa
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Nhuận tràng'), N'Lactulose 15ml', 60, 80000, 95000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Nhuận tràng'), N'Bisacodyl 5mg', 70, 45000, 55000, '2025-11-30'),

-- Men tiêu hóa - Thuốc tiêu hóa
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Men tiêu hóa'), N'Pancreatin 25000', 80, 120000, 145000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Men tiêu hóa'), N'Lactase 10000', 65, 95000, 115000, '2025-11-30'),

-- Ho - Thuốc hô hấp
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Ho'), N'Codeine syrup 100ml', 55, 85000, 100000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Ho'), N'Dextromethorphan 15mg', 75, 60000, 72000, '2025-11-30'),

-- Hen suyễn - Thuốc hô hấp
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Hen suyễn'), N'Salbutamol 100mcg', 90, 70000, 85000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Hen suyễn'), N'Budesonide 200mcg', 85, 110000, 135000, '2025-11-30'),

-- Vitamin B - Vitamin
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Vitamin B'), N'B-Complex 60 viên', 100, 120000, 145000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Vitamin B'), N'B12 1000mcg', 85, 90000, 110000, '2025-11-30'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Vitamin B'), N'Folic Acid 5mg', 95, 50000, 60000, '2025-12-15'),

-- Vitamin D - Vitamin
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Vitamin D'), N'Vitamin D3 1000IU', 110, 80000, 95000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Vitamin D'), N'Vitamin D3 2000IU', 90, 100000, 120000, '2025-11-30'),

-- Multivitamin - Vitamin
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Multivitamin'), N'Centrum 60 viên', 75, 180000, 220000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Multivitamin'), N'One A Day 100 viên', 85, 150000, 180000, '2025-11-30'),

-- Kháng nấm - Thuốc da liễu
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Kháng nấm'), N'Clotrimazole 1% 20g', 60, 70000, 85000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Kháng nấm'), N'Ketoconazole 2% 60g', 45, 90000, 110000, '2025-11-30'),

-- Kem dưỡng - Thuốc da liễu
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Kem dưỡng'), N'Moisturizing Cream 100g', 80, 60000, 72000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Kem dưỡng'), N'Anti-aging Cream 50g', 65, 120000, 145000, '2025-11-30'),

-- Nhỏ mắt - Thuốc mắt
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Nhỏ mắt'), N'Eye Drops 10ml', 70, 80000, 95000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Nhỏ mắt'), N'Artificial Tears 15ml', 85, 50000, 60000, '2025-11-30'),

-- An thần - Thuốc thần kinh
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'An thần'), N'Lorazepam 1mg', 30, 100000, 120000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'An thần'), N'Diazepam 5mg', 35, 95000, 115000, '2025-11-30'),

-- Chống trầm cảm - Thuốc thần kinh
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Chống trầm cảm'), N'Fluoxetine 20mg', 40, 110000, 135000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Chống trầm cảm'), N'Sertraline 50mg', 45, 120000, 145000, '2025-11-30'),

-- Băng gạc - Dụng cụ y tế
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Băng gạc'), N'Medical Tape 5cm x 10m', 100, 25000, 30000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Băng gạc'), N'Bandage 10cm x 4.5m', 90, 35000, 42000, '2025-11-30'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Băng gạc'), N'Sterile Gauze 10x10cm', 120, 20000, 24000, '2025-12-15'),

-- Thuốc sát trùng - Dụng cụ y tế
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Thuốc sát trùng'), N'Iodine Solution 50ml', 75, 45000, 55000, '2025-12-31'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Thuốc sát trùng'), N'Hydrogen Peroxide 100ml', 85, 35000, 42000, '2025-11-30'),
((SELECT MaLoai FROM LoaiThuoc WHERE TenLoai = N'Thuốc sát trùng'), N'Alcohol 70% 250ml', 95, 30000, 36000, '2025-12-15');


-- ================================
-- KIỂM TRA DỮ LIỆU
-- ================================
SELECT 'NhomThuoc' as TableName, COUNT(*) as RecordCount FROM NhomThuoc
UNION ALL
SELECT 'LoaiThuoc', COUNT(*) FROM LoaiThuoc
UNION ALL
SELECT 'Sanphamthuoc', COUNT(*) FROM Sanphamthuoc;

-- Xem dữ liệu theo quan hệ
SELECT 
    nt.MaNhom,
    nt.TenNhom,
    COUNT(lt.MaLoai) as SoLoaiThuoc
FROM NhomThuoc nt
LEFT JOIN LoaiThuoc lt ON nt.MaNhom = lt.MaNhom
GROUP BY nt.MaNhom, nt.TenNhom
ORDER BY nt.MaNhom;

SELECT 
    lt.MaLoai,
    lt.TenLoai,
    nt.TenNhom,
    COUNT(sp.ID) as SoSanPham
FROM LoaiThuoc lt
INNER JOIN NhomThuoc nt ON lt.MaNhom = nt.MaNhom
LEFT JOIN Sanphamthuoc sp ON lt.MaLoai = sp.MaLoai
GROUP BY lt.MaLoai, lt.TenLoai, nt.TenNhom
ORDER BY lt.MaLoai;

PRINT 'Sample data inserted successfully with fixed foreign keys!';
