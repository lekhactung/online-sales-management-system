use master
go

create database OnlineSales;
go

use OnlineSales;
go

-- Product Category
create table ProductCategory(
	CategoryID nvarchar(20) primary key,
	CategoryName nvarchar(100)
)
go

-- Supplier
create table Supplier(
	SupplierID nvarchar(20) primary key,
	SupplierName nvarchar(50),
	Phone nvarchar(15),
	Address nvarchar(50)
)
go

-- Warehouse
create table Warehouse(
	WarehouseID nvarchar(20) primary key,
	WarehouseName nvarchar(100),
	Address nvarchar(200)
)
go

-- Product
create table Product(
	ProductID nvarchar(20) primary key,
	ProductName nvarchar(100),
	Price decimal(18,2) DEFAULT 0,
	CategoryID nvarchar(20),
	SupplierID nvarchar(20),
	WarehouseID nvarchar(20),
	StockQuantity int,

	constraint FK_Product_Category 
	foreign key (CategoryID) REFERENCES ProductCategory(CategoryID),

	constraint FK_Product_Supplier 
	foreign key (SupplierID) REFERENCES Supplier(SupplierID),

	constraint FK_Product_Warehouse 
	foreign key (WarehouseID) REFERENCES Warehouse(WarehouseID)
)
go

-- Admin
create table Admin(
	AdminID nvarchar(20) primary key,
	LastName nvarchar(50),
	FirstName nvarchar(50),
	Phone nvarchar(15) unique,
	Email nvarchar(50) unique
)
go


-- Customer
create table Customer(
	CustomerID nvarchar(20) primary key,
	LastName nvarchar(50),
	FirstName nvarchar(100),
	Phone nvarchar(15) unique,
	Email nvarchar(50) unique,
	Address nvarchar(200),
	AdminID nvarchar(20),
	
	constraint FK_Customer_Admin foreign key (AdminID) REFERENCES Admin (AdminID)
)
go


-- Account
create table Account(
	AccountID nvarchar(20) primary key,
	Username nvarchar(20) not null unique,
	Password nvarchar(255) not null,
	Role nvarchar(20) CHECK (Role in ('Admin', 'Customer')),
	
	CustomerID nvarchar(20),
	AdminID nvarchar(20),
	constraint fk_account_customer foreign key (CustomerID) REFERENCES Customer (CustomerID),
	constraint fk_account_admin foreign key (AdminID) REFERENCES Admin (AdminID),
	constraint CHK_Account_Owner CHECK(
		(Role = 'Customer' and CustomerID is not null and AdminID is null)
		OR
		(Role = 'Admin' and AdminID is not null and CustomerID is null)
	),
)
go

CREATE UNIQUE INDEX UX_Account_Admin
ON Account(AdminID)
WHERE AdminID IS NOT NULL;
GO


-- Order Status
create table OrderStatus(
	StatusID nvarchar(20) primary key,
	StatusName nvarchar(100)
)
go


-- Orders
create table Orders(
	OrderID nvarchar(20) primary key,
	OrderDate DATETIME DEFAULT GETDATE(),
	TotalAmount DECIMAL(18,2) DEFAULT 0,
	CustomerID nvarchar(20),
	StatusID nvarchar(20),

	constraint FK_Orders_Customer 
	foreign key (CustomerID) REFERENCES Customer(CustomerID),

	constraint FK_Orders_Status 
	foreign key (StatusID) REFERENCES OrderStatus(StatusID)
)
go

-- Order Detail
create table OrderDetail(
	OrderID nvarchar(20),
	ProductID nvarchar(20),
	Quantity int not null,
	UnitPrice DECIMAL(18,2) not null,

	constraint PK_OrderDetail primary key (OrderID, ProductID),

	constraint FK_OrderDetail_Order 
	foreign key (OrderID) REFERENCES Orders(OrderID),

	constraint FK_OrderDetail_Product 
	foreign key (ProductID) REFERENCES Product(ProductID)
)
go


-- Shipping
create table Shipping(
	ShippingID nvarchar(20) primary key,
	OrderID nvarchar(20),
	ShippingCompany nvarchar(100),
	EstimatedDeliveryDate datetime,
	ShippingFee decimal(18,2),

	constraint FK_Shipping_Order 
	foreign key (OrderID) REFERENCES Orders(OrderID)
)
go



-- ==========================================
-- 1. ADMIN
-- ==========================================
INSERT INTO Admin VALUES
('AD01', N'Phạm', N'Minh Quân', '0123456789', 'pmq_admin@gmail.com'),
('AD02', N'La', N'Anh Minh', '0999666333', 'aminh_admin@gmail.com'),
('AD03', N'Lê', N'Khắc Tùng', '0987654321', 'lkt_admin@gmail.com');
GO

-- ==========================================
-- 2. CUSTOMER (Được phân công cho các Admin)
-- ==========================================
INSERT INTO Customer VALUES
('KH01', N'Nguyễn', N'Thị Lan', '0901111111', 'lan.nguyen@gmail.com', N'Hà Nội', 'AD01'),
('KH02', N'Trần', N'Văn Bình', '0902222222', 'binh.tran@gmail.com', N'Đà Nẵng', 'AD01'),
('KH03', N'Lý', N'Hải', '0903333333', 'hai.ly@gmail.com', N'TP. Hồ Chí Minh', 'AD02'),
('KH04', N'Hoàng', N'Thu Thảo', '0904444444', 'thao.hoang@gmail.com', N'Cần Thơ', 'AD02'),
('KH05', N'Vũ', N'Đức Đam', '0905555555', 'dam.vu@gmail.com', N'Hải Phòng', 'AD03'),
('KH06', N'Đinh', N'Bảo Trâm', '0906666666', 'tram.dinh@gmail.com', N'Đồng Nai', 'AD03');
GO

-- ==========================================
-- 3. ACCOUNT (Tài khoản đăng nhập)
-- ==========================================
-- Tài khoản Admin (Mỗi Admin 1 tài khoản)
INSERT INTO Account VALUES
('ACC01', 'admin_quan', 'pass123', 'Admin', NULL, 'AD01'),
('ACC02', 'admin_minh', 'pass123', 'Admin', NULL, 'AD02'),
('ACC03', 'admin_tung', 'pass123', 'Admin', NULL, 'AD03');
GO

-- Tài khoản Customer (KH01 có 2 tài khoản để thể hiện 1 User nhiều Acc)
INSERT INTO Account VALUES
-- KH01 có 2 account
('ACC04', 'lannguyen_main', 'pass123', 'Customer', 'KH01', NULL),
('ACC05', 'lannguyen_sub', 'pass123', 'Customer', 'KH01', NULL),

-- các user khác
('ACC06', 'binhtran99', 'pass123', 'Customer', 'KH02', NULL),
('ACC07', 'lyhai_vp', 'pass123', 'Customer', 'KH03', NULL),
('ACC08', 'thaohoang22', 'pass123', 'Customer', 'KH04', NULL),
('ACC09', 'damvu_hp', 'pass123', 'Customer', 'KH05', NULL),
('ACC10', 'tramdinh_dn', 'pass123', 'Customer', 'KH06', NULL);
GO
-- ==========================================
-- 4. PRODUCT CATEGORY (Thêm Máy tính bảng)
-- ==========================================
INSERT INTO ProductCategory (CategoryID, CategoryName) VALUES
('L01', N'Điện thoại'),
('L02', N'Laptop'),
('L03', N'Phụ kiện'),
('L04', N'Thiết bị gia dụng'),
('L05', N'Máy tính bảng')
GO

-- ==========================================
-- 5. SUPPLIER (Thêm Asus, LG)
-- ==========================================
INSERT INTO Supplier (SupplierID, SupplierName, Phone, Address) VALUES
('NCC01', N'Samsung', '1800588889', N'Hà Nội'),
('NCC02', N'Apple Store', '18001192', N'TP. Hồ Chí Minh'),
('NCC03', N'Dell Store', '0903333333', N'Hồ Chí Minh'),
('NCC04', N'Sony Electronics', '0904444444', N'Cần Thơ'),
('NCC05', N'Asus VN', '18006588', N'TP. Hồ Chí Minh'),
('NCC06', N'LG Electronics', '18001509', N'Hải Phòng')
GO

-- ==========================================
-- 6. WAREHOUSE (Thêm Kho Đà Nẵng)
-- ==========================================
INSERT INTO Warehouse (WarehouseID, WarehouseName, Address) VALUES
('K01', N'Kho Hà Nội', N'Hà Nội'),
('K02', N'Kho Hồ Chí Minh', N'Hồ Chí Minh'),
('K03', N'Kho Đà Nẵng', N'Đà Nẵng')
GO

-- ==========================================
-- 7. PRODUCT (Thêm nhiều sản phẩm đa dạng)
-- ==========================================
INSERT INTO Product (ProductID, ProductName, Price, CategoryID, SupplierID, WarehouseID, StockQuantity) VALUES
('SP01', N'iPhone 14', 22000000, 'L01', 'NCC02', 'K01', 25),
('SP02', N'Samsung Galaxy S23', 18000000, 'L01', 'NCC01', 'K02', 36),
('SP03', N'Dell XPS 13', 35000000, 'L02', 'NCC03', 'K02', 5),
('SP04', N'Sony Headphone', 3000000, 'L03', 'NCC04', 'K01', 100),
('SP05', N'Sạc nhanh Samsung', 500000, 'L03', 'NCC01', 'K02', 136),
('SP06', N'iPad Pro M2', 28000000, 'L05', 'NCC02', 'K01', 20),
('SP07', N'Asus ROG Strix', 40000000, 'L02', 'NCC05', 'K02', 15),
('SP08', N'Tủ lạnh LG Inverter', 15000000, 'L04', 'NCC06', 'K03', 10),
('SP09', N'Ốp lưng iPhone 14', 200000, 'L03', 'NCC02', 'K01', 500),
('SP10', N'Chuột Logitech', 450000, 'L03', 'NCC04', 'K02', 200)
GO

-- ==========================================
-- 8. ORDER STATUS
-- ==========================================
INSERT INTO OrderStatus (StatusID, StatusName) VALUES
('TT01', N'Chờ xác nhận'),
('TT02', N'Đang chuẩn bị hàng'),
('TT03', N'Đang giao hàng'),
('TT04', N'Đã giao'),
('TT05', N'Đã hủy')
GO

-- ==========================================
-- 9. ORDERS
-- ==========================================
INSERT INTO Orders (OrderID, OrderDate, TotalAmount, CustomerID, StatusID) VALUES
('DH01', GETDATE(), 28200000, 'KH01', 'TT04'), -- iPad Pro + Ốp lưng
('DH02', GETDATE(), 40000000, 'KH02', 'TT03'), -- Asus ROG
('DH03', GETDATE(), 30000000, 'KH03', 'TT02'), -- 2 Tủ lạnh LG
('DH04', GETDATE(), 18500000, 'KH04', 'TT01'), -- Galaxy S23 + Sạc
('DH05', GETDATE(), 35450000, 'KH05', 'TT04'), -- Dell XPS + Chuột
('DH06', GETDATE(), 22000000, 'KH06', 'TT05')  -- Hủy đơn iPhone 14
GO

-- ==========================================
-- 10. ORDER DETAIL
-- ==========================================
INSERT INTO OrderDetail (OrderID, ProductID, Quantity, UnitPrice) VALUES
('DH01', 'SP06', 1, 28000000),
('DH01', 'SP09', 1, 200000),
('DH02', 'SP07', 1, 40000000),
('DH03', 'SP08', 2, 15000000),
('DH04', 'SP02', 1, 18000000),
('DH04', 'SP05', 1, 500000),
('DH05', 'SP03', 1, 35000000),
('DH05', 'SP10', 1, 450000),
('DH06', 'SP01', 1, 22000000)
GO

-- ==========================================
-- 11. SHIPPING
-- ==========================================
INSERT INTO Shipping (ShippingID, OrderID, ShippingCompany, EstimatedDeliveryDate, ShippingFee) VALUES
('VC01', 'DH01', N'Giao Hàng Nhanh', '2024-06-10', 30000),
('VC02', 'DH02', N'Giao Hàng Tiết Kiệm', '2024-06-11', 50000),
('VC03', 'DH03', N'Viettel Post', '2024-06-12', 150000),
('VC04', 'DH04', N'Ahamove', '2024-06-13', 25000),
('VC05', 'DH05', N'Giao Hàng Nhanh', '2024-06-09', 40000)
GO