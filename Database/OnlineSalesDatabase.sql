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

-- Nhà cung cấp
create table NhaCungCap(
	MaNhaCungCap nvarchar(20) primary key,
	TenNhaCungCap nvarchar(50),
	SoDienThoai nvarchar(15),
	DiaChiNhaCungCap nvarchar(50)
)
go

-- Kho hàng
create table Kho(
	MaKho nvarchar(20) primary key,
	TenKho nvarchar(100),
	DiaChi nvarchar(200)
)
go

-- Sản Phẩm
create table SanPham(
	MaSP nvarchar(20) primary key,
	TenSP nvarchar(100),
	GiaBan decimal(18,2) DEFAULT 0,
	MaLoaiSP nvarchar(20),
	MaNhaCungCap nvarchar(20),
	MaKho nvarchar(20),
	SoLuongHangTonKho int,
	constraint FK_SanPham_LoaiSP Foreign key (MaLoaiSP) REFERENCES LoaiSP(MaLoaiSP),
	constraint FK_SanPham_NhaCungCap Foreign key (MaNhaCungCap) REFERENCES NhaCungCap(MaNhaCungCap),
	constraint FK_SanPham_Kho Foreign key (MaKho) REFERENCES Kho(MaKho)
)
go



-- Khách Hàng
create table KhachHang(
	MaKH nvarchar(20) primary key,
	HoKhachHang nvarchar(50),
	TenKhachHang nvarchar(100),
	SoDienThoai nvarchar(15) unique,
	Email nvarchar(50) unique,
	DiaChi nvarchar(200)
)
go


-- Trạng thái đơn hàng
create table TrangThaiDonHang(
	MaTrangThai nvarchar(20) primary key,
	TenTrangThai nvarchar(100)
)
go


-- Đơn Hàng
create table DonHang(
	MaDonHang nvarchar(20) primary key,
	NgayDat DATETIME DEFAULT GETDATE(),
	TongTien DECIMAL(18,2) DEFAULT 0,
	MaKH nvarchar(20),
	MaTrangThai nvarchar(20),

	constraint FK_DonHang_KhachHang Foreign key (MaKH) REFERENCES KhachHang(MaKH),
	constraint FK_DonHang_TrangThai foreign key (MaTrangThai) REFERENCES TrangThaiDonHang(MaTrangThai)
)
go

-- Chi tiết đơn hàng
create table ChiTietDonHang(
	MaDH nvarchar(20),
	MaSP nvarchar(20),
	SoLuong int not null,
	DonGia DECIMAL(18,2) not null,
	
	constraint PK_ChiTietDonHang primary key (MaDH, MaSP),

	constraint FK_ChiTietDonHang_DonHang Foreign key (MaDH) REFERENCES DonHang(MaDonHang),
	constraint FK_ChiTietDonHang_SanPham Foreign key (MaSP) REFERENCES SanPham(MaSP)
)
go


-- Vận chuyển
create table VanChuyen(
	MaVanChuyen nvarchar(20) primary key,
	MaDonHang nvarchar(20),
	DonViVanChuyen nvarchar(100),
	NgayGiaoDuKien datetime,
	
	PhiVanChuyen decimal(18,2),
	constraint FK_VanChuyen_DonHang foreign key (MaDonHang) REFERENCES DonHang(MaDonHang)
)
go

-- Nhập dữ liệu
-- Loại sản phẩm
insert into LoaiSP values
('L01', N'Điện thoại'),
('L02', N'Laptop'),
('L03', N'Phụ kiện'),
('L04', N'Thiết bị gia dụng')
go

-- Nhà cung cấp
insert into NhaCungCap values
('NCC01', N'Samsung', '1800588889', N'Hà Nội'),
('NCC02', N'Apple Store', '18001192', N'TP. Hồ Chí Minh'),
('NCC03', N'Dell Store', '0903333333', N'Hồ Chí Minh'),
('NCC04', N'Sony Electronics', '0904444444', N'Cần Thơ')
go

-- Kho hàng
insert into Kho values
('K01', N'Kho Hà Nội', N'Hà Nội'),
('K02', N'Kho Hồ Chí Minh', N'Hồ Chí Minh')
go

-- Sản Phẩm
insert into SanPham values
('SP01', N'iPhone 14', 22000000, 'L01', 'NCC02', 'K01', 25),
('SP02', N'Samsung Galaxy S23', 18000000, 'L01', 'NCC01', 'K02', 36),
('SP03', N'Dell XPS 13', 35000000, 'L02', 'NCC03', 'K02', 5),
('SP04', N'Sony Headphone', 3000000, 'L03', 'NCC04', 'K01', 100),
('SP05', N'Sạc nhanh Samsung', 500000, 'L03', 'NCC01', 'K02', 136)
go

-- Khách Hàng
insert into KhachHang values
('KH01', N'Phạm', N'Minh Quân', '0123456789', 'pmq@gmail.com', N'TP. Hồ Chí Minh'),
('KH02', N'Lê', N'Khắc Tùng', '0987654321', 'lkt@gmail.com', N'Đồng Nai'),
('KH03', N'La', N'Anh Minh', '0999666333', 'anhminhla@gmail.com', N'Hồ Chí Minh')
go

-- Trạng thái đơn hàng
insert into TrangThaiDonHang values
('TT01', N'Chờ xác nhận'),
('TT02', N'Đang chuẩn bị hàng'),
('TT03', N'Đang giao hàng'),
('TT04', N'Đã giao'),
('TT05', N'Đã hủy')
go

-- Đơn hàng
insert into DonHang values
('DH01', GETDATE(), 25000000, 'KH01', 'TT04'),
('DH02', GETDATE(), 18000000, 'KH02', 'TT03'),
('DH03', GETDATE(), 35000000, 'KH03', 'TT02')
go

-- Chi tiết đơn hàng
insert into ChiTietDonHang values
('DH01', 'SP01', 1, 22000000),
('DH01', 'SP05', 1, 500000),
('DH02', 'SP02', 1, 18000000),
('DH03', 'SP03', 1, 35000000)
go

-- Vận chuyển
insert into VanChuyen values
('VC01', 'DH01', N'Giao Hàng Nhanh', '2024-06-10', 30000),
('VC02', 'DH02', N'Giao Hàng Tiết Kiệm', '2024-06-11', 25000),
('VC03', 'DH03', N'Viettel Post', '2024-06-12', 40000)
go