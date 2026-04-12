use OnlineSales
go

--====================== KHÁCH HÀNG ==========================
-- Thêm khách hàng
CREATE OR ALTER PROC spAddCustomer(
    @CustomerID nvarchar(20),
    @LastName nvarchar(50),
    @FirstName nvarchar(100),
    @Phone nvarchar(15),
    @Email nvarchar(50),
    @Address nvarchar(200)
)
AS
BEGIN
    IF @CustomerID IS NULL OR @CustomerID = ''
        BEGIN
            RAISERROR(N'Mã khách hàng không được để trống!', 16, 1);
            RETURN;
        END

    ELSE IF EXISTS (SELECT 1 FROM Customer WHERE CustomerID = @CustomerID)
        BEGIN
            RAISERROR(N'Mã khách hàng này đã tồn tại!', 16, 1);
            RETURN;
        END

    ELSE
        INSERT INTO Customer (CustomerID, LastName, FirstName, Phone, Email, Address) 
        VALUES (@CustomerID, @LastName, @FirstName, @Phone, @Email, @Address);
    
        PRINT N'Thêm khách hàng thành công';
END;
GO

-- Xóa Khách Hàng
CREATE OR ALTER PROC spDeleteCustomer(
    @CustomerID nvarchar(20)
)
AS
BEGIN
    SET NOCOUNT ON;

    IF @CustomerID IS NULL OR @CustomerID = ''
    BEGIN
        RAISERROR(N'Mã khách hàng không được để trống!', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Customer WHERE CustomerID = @CustomerID)
    BEGIN
        RAISERROR(N'Không tìm thấy khách hàng!', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        DELETE FROM OrderDetail 
        WHERE OrderID IN (SELECT OrderID FROM Orders WHERE CustomerID = @CustomerID);

        DELETE FROM Shipping 
        WHERE OrderID IN (SELECT OrderID FROM Orders WHERE CustomerID = @CustomerID);

        DELETE FROM Orders 
        WHERE CustomerID = @CustomerID;

        DELETE FROM Customer 
        WHERE CustomerID = @CustomerID;

        COMMIT TRANSACTION;
        PRINT N'Đã xóa thành công khách hàng và toàn bộ lịch sử giao dịch liên quan.';

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        
        DECLARE @Err nvarchar(4000) = ERROR_MESSAGE();
        RAISERROR(N'Lỗi khi xóa dữ liệu: %s', 16, 1, @Err);
    END CATCH
END;
GO


-- Sửa khách hàng
CREATE OR ALTER PROC spUpdateCustomer(
    @CustomerID nvarchar(20),
    @LastName nvarchar(50),
    @FirstName nvarchar(100),
    @Phone nvarchar(15),
    @Email nvarchar(50),
    @Address nvarchar(200)
)
AS 
BEGIN
    IF @CustomerID IS NULL OR @CustomerID = ''
        BEGIN
            RAISERROR(N'Mã khách hàng không được để trống!', 16, 1);
            RETURN;
        END

    ELSE IF NOT EXISTS (SELECT 1 FROM Customer WHERE CustomerID = @CustomerID)
        BEGIN
            RAISERROR(N'Không tìm thấy khách hàng!', 16, 1)
            RETURN
        END
    ELSE
        BEGIN
            update Customer
            set LastName = @LastName, FirstName = @FirstName, Email = @Email, Phone = @Phone, Address = @Address
            where CustomerID = @CustomerID
        END
END
go

--======================= LOẠI SẢN PHẨM =====================
-- Thêm Loại sản phẩm
CREATE OR ALTER PROC spAddCategory(
    @CategoryID nvarchar(20),
    @CategoryName nvarchar(100)
)
AS
BEGIN
    IF @CategoryID IS NULL OR @CategoryID = ''
    BEGIN
        RAISERROR(N'Mã loại không được để trống!', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM ProductCategory WHERE CategoryID = @CategoryID)
    BEGIN
        RAISERROR(N'Mã loại này đã tồn tại!', 16, 1);
        RETURN;
    END

    INSERT INTO ProductCategory (CategoryID, CategoryName) VALUES (@CategoryID, @CategoryName);
    PRINT N'Thêm loại sản phẩm thành công';
END;
GO

-- Sửa Loại sản phẩm
CREATE OR ALTER PROC spUpdateCategory(
    @CategoryID nvarchar(20),
    @CategoryName nvarchar(100)
)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM ProductCategory WHERE CategoryID = @CategoryID)
    BEGIN
        RAISERROR(N'Không tìm thấy loại sản phẩm!', 16, 1);
        RETURN;
    END

    UPDATE ProductCategory SET CategoryName = @CategoryName WHERE CategoryID = @CategoryID;
    PRINT N'Cập nhật loại sản phẩm thành công';
END;
GO

-- Xóa Loại sản phẩm (Xóa luôn các sản phẩm thuộc loại này)
CREATE OR ALTER PROC spDeleteCategory(
    @CategoryID nvarchar(20)
)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Xóa chi tiết đơn hàng của các sản phẩm thuộc loại này
        DELETE FROM OrderDetail WHERE ProductID IN (SELECT ProductID FROM Product WHERE CategoryID = @CategoryID);
        
        -- Xóa các sản phẩm thuộc loại này
        DELETE FROM Product WHERE CategoryID = @CategoryID;
        
        -- Cuối cùng xóa loại sản phẩm
        DELETE FROM ProductCategory WHERE CategoryID = @CategoryID;

        COMMIT TRANSACTION;
        PRINT N'Đã xóa loại sản phẩm và các dữ liệu liên quan thành công.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @Err nvarchar(4000) = ERROR_MESSAGE();
        RAISERROR(@Err, 16, 1);
    END CATCH
END;
GO

--========================= SẢN PHẨM ========================== 
-- Thêm Sản phẩm
CREATE OR ALTER PROC spAddProduct(
    @ProductID nvarchar(20),
    @ProductName nvarchar(100),
    @Price decimal(18,2),
    @CategoryID nvarchar(20),
    @SupplierID nvarchar(20),
    @WarehouseID nvarchar(20),
    @StockQuantity int
)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Product WHERE ProductID = @ProductID)
    BEGIN
        RAISERROR(N'Mã sản phẩm đã tồn tại!', 16, 1);
        RETURN;
    END

    INSERT INTO Product (ProductID, ProductName, Price, CategoryID, SupplierID, WarehouseID, StockQuantity)
    VALUES (@ProductID, @ProductName, @Price, @CategoryID, @SupplierID, @WarehouseID, @StockQuantity);
    PRINT N'Thêm sản phẩm thành công';
END;
GO

-- Xóa Sản phẩm (Xóa sạch dấu vết trong đơn hàng)
CREATE OR ALTER PROC spDeleteProduct(
    @ProductID nvarchar(20)
)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Xóa trong chi tiết đơn hàng trước
        DELETE FROM OrderDetail WHERE ProductID = @ProductID;

        -- Sau đó xóa sản phẩm
        DELETE FROM Product WHERE ProductID = @ProductID;

        COMMIT TRANSACTION;
        PRINT N'Đã xóa sản phẩm và lịch sử bán hàng liên quan thành công.';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

--========================= THÔNG TIN ===========================
CREATE OR ALTER PROC spGetAllProducts
AS
BEGIN
    SELECT p.ProductID, p.ProductName, p.Price, p.StockQuantity, p.CategoryID,
        c.CategoryName, s.SupplierName, w.WarehouseName
    FROM Product p
    LEFT JOIN ProductCategory c ON p.CategoryID = c.CategoryID
    LEFT JOIN Supplier s ON p.SupplierID = s.SupplierID
    LEFT JOIN Warehouse w ON p.WarehouseID = w.WarehouseID;
END
go

-- Xem chi tiết đơn hàng của một khách hàng
CREATE OR ALTER PROC spGetOrdersByCustomer(
    @CustomerID nvarchar(20)
)
AS
BEGIN
    SELECT o.OrderID, o.OrderDate, o.TotalAmount, st.StatusName
    FROM Orders o
    JOIN OrderStatus st ON o.StatusID = st.StatusID
    WHERE o.CustomerID = @CustomerID;
END;
GO