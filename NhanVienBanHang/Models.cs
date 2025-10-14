using System;

namespace NhanVienBanHang
{
    // Model cho thông tin khách hàng
    public class Customer
    {
        public string Sodienthoai { get; set; }
        public string Hovaten { get; set; }
        public int Diemtichluy { get; set; }
        public string Manhanvien { get; set; }
    }

    // Model cho đơn hàng
    public class Order
    {
        public int Madonhang { get; set; }
        public string Sodienthoaikhachhang { get; set; }
        public string Manhanvien { get; set; }
        public DateTime Ngaytaodon { get; set; }
        public string TenKhachHang { get; set; }
        public string TenNhanVien { get; set; }
    }

    // Model cho chi tiết đơn hàng
    public class OrderDetail
    {
        public int Madonhang { get; set; }
        public int Masanpham { get; set; }
        public string Tenhang { get; set; }
        public int Soluong { get; set; }
        public decimal DonGia { get; set; }
        public decimal Tongtiensanpham { get; set; }
    }

    // Model cho sản phẩm
    public class Product
    {
        public int ID { get; set; }
        public int MaLoai { get; set; }
        public string Tenhang { get; set; }
        public int Soluong { get; set; }
        public decimal Giaban { get; set; }
        public DateTime? Ngayhethan { get; set; }
        public string TenLoai { get; set; }
        public string TenNhom { get; set; }
        public string Mahanghoa { get; set; }
        public string Mavach { get; set; }
        public string Mathuoc { get; set; }
        public string Thanhphan { get; set; }
        public string Nhasanxuat { get; set; }
        public string Donggoi { get; set; }
        public decimal Gianhap { get; set; }
        public string Mota { get; set; }
        public string Lohang { get; set; }
    }

    // Model cho loại thuốc
    public class DrugCategory
    {
        public int MaLoai { get; set; }
        public string TenLoai { get; set; }
        public string MoTa { get; set; }
        public int MaNhom { get; set; }
        public string TenNhom { get; set; }
    }

    // Model cho nhóm thuốc
    public class DrugGroup
    {
        public int MaNhom { get; set; }
        public string TenNhom { get; set; }
        public string MoTa { get; set; }
    }

    // Model cho tài khoản nhân viên
    public class Employee
    {
        public string Manguoidung { get; set; }
        public string Hovaten { get; set; }
        public string Sodienthoai { get; set; }
        public string Matkhau { get; set; }
        public DateTime? Ngaysinh { get; set; }
        public string Chucvu { get; set; }
    }
}
