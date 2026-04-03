use OnlineSales
go

-- View xem thông tin đơn hàng
create or alter view viewOrderInformation
as
SELECT
	o.OrderID, o.OrderDate,
	c.LastName + ' ' + c.FirstName as CustomerName,
	p.ProductName,
	od.Quantity,
	od.UnitPrice,
	(od.Quantity * od.UnitPrice) as SubTotal,
	os.StatusName
FROM Orders o
inner join Customer c on o.CustomerID = c.CustomerID
inner join OrderDetail od on o.OrderID = od.OrderID
inner join Product p on od.ProductID = p.ProductID
inner join OrderStatus os on o.StatusID = os.StatusID
go

select * from viewOrderInformation
go

-- View Thống kê doanh thu theo sản phẩm
create or alter view viewProductRevenue
as
select p.ProductID, p.ProductName,
	sum(od.Quantity) as TotalSold,
	sum(od.Quantity * od.UnitPrice) as TotalRevenue
from Product p
inner join OrderDetail od on od.ProductID = p.ProductID
group by p.ProductID, p.ProductName
go

select * from viewProductRevenue
go

-- View thống kê doanh thu theo loại sản phẩm
create or alter view viewProductCategoryRevenue
as
select pc.CategoryID, pc.CategoryName,
	count(od.ProductID) as TotalProductsSold,
	sum(od.Quantity) as TotalQuantity,
	sum(od.Quantity * od.UnitPrice) as TotalRevenue
from ProductCategory pc
left join Product p on pc.CategoryID = p.CategoryID
left join OrderDetail od on od.ProductID = p.ProductID
group by pc.CategoryID, pc.CategoryName
go

select * from viewProductCategoryRevenue
go

-- View báo cáo doanh số theo tháng
create or alter view viewMonthlySalesReport
as
select
	YEAR(OrderDate) as SalesYear,
	MONTH(OrderDate) as SalesMonth,
	COUNT(OrderID) as TotalOrders,
	SUM(TotalAmount) as MonthlyRevenue
from Orders
group by YEAR(OrderDate), MONTH(OrderDate)
go

select * from viewMonthlySalesReport
go

-- View lọc danh sách hàng sắp hết < 10 sản phẩm
create or alter view viewLowStockQuantityAlert
as
select
	p.ProductID, p.ProductName, p.StockQuantity,
	wh.WarehouseName,
	sp.SupplierName, sp.Phone as SupplierPhone,
	dbo.funcGetProductStatus(p.ProductID) as ProductStatus
from Product p
inner join Warehouse wh on p.WarehouseID = wh.WarehouseID
inner join Supplier sp on sp.SupplierID = p.SupplierID
go

select * from viewLowStockQuantityAlert
go

-- View xem tình trạng giao hàng
create or alter view viewShippingStatusSummary
as
select o.OrderID, o.OrderDate,
	os.StatusName,
	sh.ShippingCompany, sh.EstimatedDeliveryDate, sh.ShippingFee,
	c.LastName + ' ' + C.FirstName as CustomerName
from Orders o
inner join OrderStatus os on o.StatusID = os.StatusID
left join Shipping sh on o.OrderID = sh.OrderID
inner join Customer c on c.CustomerID = o.CustomerID;
go

select * from viewShippingStatusSummary
go

-- Chi tiết trạng thái kho và nhà cung cấp
create or alter view viewInventoryReport
as
select
	p.ProductID, 
    p.ProductName, 
    p.StockQuantity,
    c.CategoryName,
    s.SupplierName,
    w.WarehouseName,
	CASE 
        WHEN p.StockQuantity = 0 THEN N'Hết hàng'
        WHEN p.StockQuantity <= 5 THEN N'Cần nhập gấp'
        ELSE N'Bình thường'
    END AS StockStatus
from Product p
LEFT JOIN ProductCategory c ON p.CategoryID = c.CategoryID
LEFT JOIN Supplier s ON p.SupplierID = s.SupplierID
LEFT JOIN Warehouse w ON p.WarehouseID = w.WarehouseID
go

select * from viewInventoryReport
go