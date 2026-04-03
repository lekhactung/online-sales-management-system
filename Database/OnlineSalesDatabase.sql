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
	Password nvarchar(20) not null,
	Role nvarchar(20) CHECK (Role in ('Admin', 'Customer')),
	
	CustomerID nvarchar(20),
	AdminID nvarchar(20) unique,
	constraint fk_account_customer foreign key (CustomerID) REFERENCES Customer (CustomerID),
	constraint fk_account_admin foreign key (AdminID) REFERENCES Admin (AdminID),
	constraint CHK_Account_Owner CHECK(
		(Role = 'Customer' and CustomerID is not null and AdminID is not null)
		OR
		(Role = 'Admin' and AdminID is not null and CustomerID is not null)
	)
)
go

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




-- Product Category
insert into ProductCategory values
('L01', N'Điện thoại'),
('L02', N'Laptop'),
('L03', N'Phụ kiện'),
('L04', N'Thiết bị gia dụng')
go

-- Supplier
insert into Supplier values
('NCC01', N'Samsung', '1800588889', N'Hà Nội'),
('NCC02', N'Apple Store', '18001192', N'TP. Hồ Chí Minh'),
('NCC03', N'Dell Store', '0903333333', N'Hồ Chí Minh'),
('NCC04', N'Sony Electronics', '0904444444', N'Cần Thơ')
go

-- Warehouse
insert into Warehouse values
('K01', N'Kho Hà Nội', N'Hà Nội'),
('K02', N'Kho Hồ Chí Minh', N'Hồ Chí Minh')
go

-- Product
insert into Product values
('SP01', N'iPhone 14', 22000000, 'L01', 'NCC02', 'K01', 25),
('SP02', N'Samsung Galaxy S23', 18000000, 'L01', 'NCC01', 'K02', 36),
('SP03', N'Dell XPS 13', 35000000, 'L02', 'NCC03', 'K02', 5),
('SP04', N'Sony Headphone', 3000000, 'L03', 'NCC04', 'K01', 100),
('SP05', N'Sạc nhanh Samsung', 500000, 'L03', 'NCC01', 'K02', 136)
go

-- Customer
insert into Customer values
('KH01', N'Phạm', N'Minh Quân', '0123456789', 'pmq@gmail.com', N'TP. Hồ Chí Minh'),
('KH02', N'Lê', N'Khắc Tùng', '0987654321', 'lkt@gmail.com', N'Đồng Nai'),
('KH03', N'La', N'Anh Minh', '0999666333', 'anhminhla@gmail.com', N'Hồ Chí Minh')
go

-- Order Status
insert into OrderStatus values
('TT01', N'Chờ xác nhận'),
('TT02', N'Đang chuẩn bị hàng'),
('TT03', N'Đang giao hàng'),
('TT04', N'Đã giao'),
('TT05', N'Đã hủy')
go

-- Orders
insert into Orders values
('DH01', GETDATE(), 25000000, 'KH01', 'TT04'),
('DH02', GETDATE(), 18000000, 'KH02', 'TT03'),
('DH03', GETDATE(), 35000000, 'KH03', 'TT02')
go

-- Order Detail
insert into OrderDetail values
('DH01', 'SP01', 1, 22000000),
('DH01', 'SP05', 1, 500000),
('DH02', 'SP02', 1, 18000000),
('DH03', 'SP03', 1, 35000000)
go

-- Shipping
insert into Shipping values
('VC01', 'DH01', N'Giao Hàng Nhanh', '2024-06-10', 30000),
('VC02', 'DH02', N'Giao Hàng Tiết Kiệm', '2024-06-11', 25000),
('VC03', 'DH03', N'Viettel Post', '2024-06-12', 40000)
go