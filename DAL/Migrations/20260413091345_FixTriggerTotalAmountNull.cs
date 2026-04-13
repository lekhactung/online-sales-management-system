using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixTriggerTotalAmountNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
	set TotalAmount = ISNULL((
		select SUM(Quantity * UnitPrice)
		from OrderDetail
		where OrderDetail.OrderID = Orders.OrderID
	), 0)
	where OrderID in (select OrderID from @AffectedOrders)
end
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
