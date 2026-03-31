use master
go

create database OnlineSales;
go

use OnlineSales;
go

-- Loại SP
create table LoaiSP(
	MaLoaiSP nvarchar(20) primary key,
	TenLoaiSP nvarchar(100)
)
go

-- Sản Phẩm
create table SanPham(
	MaSP nvarchar(20) primary key,
	TenSP nvarchar(100),
	GiaBan decimal(18,2) DEFAULT 0,
	MaLoaiSP nvarchar(20),
	
	constraint FK_SanPham_LoaiSP Foreign key (MaLoaiSP) REFERENCES LoaiSP(MaLoaiSP)
)
go

-- Khách Hàng
create table KhachHang(
	MaKH nvarchar(20) primary key,
	HoKhachHang nvarchar(50),
	TenKhachHang nvarchar(100),
	SoDienThoai nvarchar(15),
	DiaChi nvarchar(200)
)
go

-- Đơn Hàng
create table DonHang(
	MaDonHang nvarchar(20) primary key,
	NgayDat DATETIME DEFAULT GETDATE(),
	TongTien DECIMAL(18,2) DEFAULT 0,
	MaKH nvarchar(20),
	
	constraint FK_DonHang_KhachHang Foreign key (MaKH) REFERENCES KhachHang(MaKH)
)
go

create table ChiTietDonHang(
	MaChiTietDonHang nvarchar(20) primary key,
	MaDH nvarchar(20),
	MaSP nvarchar(20),
	SoLuong int not null,
	DonGia DECIMAL(18,2) not null,
	
	constraint FK_ChiTietDonHang_DonHang Foreign key (MaDH) REFERENCES DonHang(MaDonHang),
	constraint FK_ChiTietDonHang_SanPham Foreign key (MaSP) REFERENCES SanPham(MaSP)
)
go