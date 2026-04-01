use OnlineSales
go

-- Tự động trừ tồn kho khi bán hàng
create or alter trigger trgAfterInsertOrderDetail
on OrderDetail
after insert
as
begin
	set NOCOUNT on;

	UPDATE Product
	set StockQuantity = Product.StockQuantity - i.Quantity
	from Product
	inner join inserted i on Product.ProductID = i.ProductID;

	if exists (select 1 from Product where StockQuantity < 0)
	BEGIN
        RAISERROR(N'Lỗi: Số lượng hàng trong kho không đủ để bán!', 16, 1);
        ROLLBACK TRANSACTION;
    END
end
go

-- Hoàn hàng tồn kho khi xóa chi tiết đơn hàng
create or alter trigger trgAfterDeleteOrderDetail
on OrderDetail
after DELETE
as
begin
	UPDATE Product
	set StockQuantity = Product.StockQuantity + d.Quantity
	from Product
	inner join deleted d on Product.ProductID = d.ProductID
end
go

-- Tính tổng tiền
create or alter trigger trgUpdateTotalAmount
on OrderDetail
after INSERT, UPDATE, DELETE
as
begin
	DECLARE @AffectedOrders TABLE (OrderID nvarchar(20))

	insert into @AffectedOrders 
	select OrderID from inserted 
	union 
	select OrderID from deleted;

	update Orders
	set TotalAmount = (
		select SUM(Quantity * UnitPrice)
		from OrderDetail
		where OrderDetail.OrderID = Orders.OrderID
	)
	where OrderID in (select OrderID from @AffectedOrders)

	update Orders set TotalAmount = 0 where TotalAmount is null
end
go
