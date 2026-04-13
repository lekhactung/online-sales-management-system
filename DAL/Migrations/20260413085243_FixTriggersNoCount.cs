using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixTriggersNoCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
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
");

            migrationBuilder.Sql(@"
create or alter trigger trgAfterDeleteOrderDetail
on OrderDetail
after DELETE
as
begin
	set NOCOUNT on;

	UPDATE Product
	set StockQuantity = Product.StockQuantity + d.Quantity
	from Product
	inner join deleted d on Product.ProductID = d.ProductID
end
");

            migrationBuilder.Sql(@"
create or alter trigger trgUpdateTotalAmount
on OrderDetail
after INSERT, UPDATE, DELETE
as
begin
	set NOCOUNT on;

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
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
