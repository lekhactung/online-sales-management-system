-- Tính tổng tiền sau thuế (VAT)
create function funcCalculateTotalWithVAT(
	@Amount DECIMAL(18, 2),
	@VAT int
) returns decimal(18, 2)
as
begin
	return @Amount + (@Amount + @VAT / 100)
end
go

-- Kiểm tra hàng tồn kho
create function funcGetProductStatus (
	@productID nvarchar(20)
) returns nvarchar(100)
as
begin
	declare @quantity int
	declare @status nvarchar(100)

	select @quantity = StockQuantity from Product where @productID = ProductID

	if @quantity > 10
		set @status = N'Còn hàng'
	else if @quantity > 0
		set @status = N'Sắp hết hàng'
	else
		set @status = N'Hết hàng'

		return @status
end
go

select ProductName, dbo.funcGetProductStatus(ProductID) as TinhTrang
from Product
go

-- Lấy danh đơn hàng của một khách hàng
create function funcGetOrdersByCustomer(
	@customerID nvarchar(20)
) returns table
as
return (
	select
		o.OrderID, o.OrderDate, o.TotalAmount,
		os.StatusName
	from Orders o
	inner join OrderStatus os on o.StatusID = os.StatusID
	where o.CustomerID = @customerID
)
go

select * from funcGetOrdersByCustomer(N'KH01')
go

-- Tìm kiếm sản phẩm theo khoảng giá
create function funcSearchProductByPrice(
	@minPrice DECIMAL(18,2),
	@maxPrice DECIMAL(18,2)
) returns table
as
return(
	select ProductID, ProductName, Price, StockQuantity
	from Product
	where Price between @minPrice and @maxPrice
)
go

select * from funcSearchProductByPrice (120000, 2000000)
go