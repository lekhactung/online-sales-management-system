using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminAccount",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminAccount", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    StatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ProductDtoResults",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ProductPriceRangeResults",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    WarehouseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WarehouseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.WarehouseId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "OrderStatus",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SupplierId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    WarehouseId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StockQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "SupplierId");
                    table.ForeignKey(
                        name: "FK_Product_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "WarehouseId");
                });

            migrationBuilder.CreateTable(
                name: "Shipping",
                columns: table => new
                {
                    ShippingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShippingCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimatedDeliveryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ShippingFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipping", x => x.ShippingId);
                    table.ForeignKey(
                        name: "FK_Shipping_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderDetail_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AdminAccount",
                columns: new[] { "AdminId", "FullName", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "Super Administrator", "123456", "SuperAdmin", "superadmin" },
                    { 2, "Product Manager", "123456", "ProductAdmin", "productadmin" },
                    { 3, "Order Manager", "123456", "OrderAdmin", "orderadmin" },
                    { 4, "Customer Care", "123456", "CustomerAdmin", "customeradmin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductId",
                table: "OrderDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SupplierId",
                table: "Product",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_WarehouseId",
                table: "Product",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipping_OrderId",
                table: "Shipping",
                column: "OrderId",
                unique: true);

            migrationBuilder.Sql(@"
-- Tính tổng tiền sau thuế (VAT)
create or alter function funcCalculateTotalWithVAT(
	@Amount DECIMAL(18, 2),
	@VAT int
) returns decimal(18, 2)
as
begin
	return @Amount *(1 + @VAT / 100.0)
end
");

            migrationBuilder.Sql(@"
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
");

            migrationBuilder.Sql(@"
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
");

            migrationBuilder.Sql(@"
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
");

            migrationBuilder.Sql(@"
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
");

            migrationBuilder.Sql(@"
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
");

            migrationBuilder.Sql(@"
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
");

            migrationBuilder.Sql(@"
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
");

            migrationBuilder.Sql(@"
-- View Thống kê doanh thu theo sản phẩm
create or alter view viewProductRevenue
as
select p.ProductID, p.ProductName,
	sum(od.Quantity) as TotalSold,
	sum(od.Quantity * od.UnitPrice) as TotalRevenue
from Product p
inner join OrderDetail od on od.ProductID = p.ProductID
group by p.ProductID, p.ProductName
");

            migrationBuilder.Sql(@"
-- View thống kê doanh thu theo loại sản phẩm
create or alter view viewProductCategoryRevenue
as
select pc.CategoryID, pc.CategoryName,
	COUNT(CASE WHEN os.StatusName != N'Đã hủy' THEN od.ProductID END) as TotalProductsSold,
	ISNULL(SUM(CASE WHEN os.StatusName != N'Đã hủy' THEN od.Quantity ELSE 0 END), 0) as TotalQuantity,
	ISNULL(SUM(CASE WHEN os.StatusName != N'Đã hủy' THEN (od.Quantity * od.UnitPrice) ELSE 0 END), 0) as TotalRevenue
from ProductCategory pc
left join Product p on pc.CategoryID = p.CategoryID
left join OrderDetail od on od.ProductID = p.ProductID
left join Orders o on od.OrderID = o.OrderID
left join OrderStatus os on o.StatusID = os.StatusID
group by pc.CategoryID, pc.CategoryName
");

            migrationBuilder.Sql(@"
-- View báo cáo doanh số theo tháng
create view viewMonthlySalesReport
as
select
	YEAR(OrderDate) as SalesYear,
	MONTH(OrderDate) as SalesMonth,
	COUNT(OrderID) as TotalOrders,
	SUM(TotalAmount) as MonthlyRevenue
from Orders
group by YEAR(OrderDate), MONTH(OrderDate)
");

            migrationBuilder.Sql(@"
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
");

            migrationBuilder.Sql(@"
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
");

            migrationBuilder.Sql(@"
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
");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminAccount");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "ProductDtoResults");

            migrationBuilder.DropTable(
                name: "ProductPriceRangeResults");

            migrationBuilder.DropTable(
                name: "Shipping");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "OrderStatus");
        }
    }
}
