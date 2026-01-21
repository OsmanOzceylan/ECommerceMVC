namespace ECommerceMVC.DataAccess.Queries
{
    public static class ProductSqlQueries
    {
        public const string GetAllProduct = @"
            SELECT p.ProductID, p.ProductName, c.CategoryName, p.UnitPrice, p.ImageUrl, p.UnitsInStock
            FROM Products p
            LEFT JOIN Categories c ON p.CategoryID = c.CategoryID";

        public const string AddProduct = @"
            INSERT INTO Products
            (ProductID, ProductName, CategoryID, UnitPrice, UnitsInStock, ImageUrl)
            VALUES   
            (@ProductID, @ProductName, @CategoryID, @UnitPrice, @UnitsInStock, @ImageUrl)";

        public const string UpdateProduct = @"
            UPDATE Products SET
                ProductName = @ProductName,
                SupplierID = @SupplierID,
                CategoryID = @CategoryID,
                UnitPrice = @UnitPrice,
                UnitsInStock = @UnitsInStock,
                ImageUrl = @ImageUrl
            WHERE ProductID = @ProductID";

        public const string GetProductsByCategory = @"
            SELECT p.ProductID, p.ProductName, c.CategoryName, p.UnitPrice, p.ImageUrl, p.UnitsInStock
            FROM Products p
            LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
            WHERE p.CategoryID = @CategoryID";

        public const string GetTop5BestSellingProducts = @"
            SELECT TOP 5 
                p.ProductID, p.ProductName, c.CategoryName, p.UnitPrice, p.ImageUrl, p.UnitsInStock, SUM(od.Quantity) AS TotalSold 
            FROM Products p
            JOIN Categories c ON p.CategoryID = c.CategoryID
            JOIN [OrderDetails] od ON p.ProductID = od.ProductID
            GROUP BY p.ProductID, p.ProductName, c.CategoryName, p.UnitPrice, p.ImageUrl, p.UnitsInStock
            ORDER BY TotalSold DESC";

        public static string GetProductsByCategoryName = @"
            SELECT 
                p.ProductID, 
                p.ProductName, 
                p.UnitPrice, 
                p.ImageUrl,
                p.UnitsInStock,
                c.CategoryName 
            FROM Products p
            JOIN Categories c ON p.CategoryID = c.CategoryID
            WHERE c.CategoryName = @CategoryName";

        public static string CreateTempProductTable = @"
            CREATE TABLE #TempProducts (
                ProductID INT,
                ProductName NVARCHAR(50),
                CategoryID INT,
                UnitPrice DECIMAL(18, 2),
                UnitsInStock INT,
                ImageUrl NVARCHAR(500)
            )";

        public static string InsertTempProduct = @"
            INSERT INTO #TempProducts (ProductID, ProductName, CategoryID, UnitPrice, UnitsInStock, ImageUrl)
            SELECT ProductID, ProductName, CategoryID, UnitPrice, UnitsInStock, ImageUrl
            FROM Products";

        public static string GetProductById = @"
            SELECT p.ProductID, p.ProductName, c.CategoryName, p.UnitPrice, p.ImageUrl, p.UnitsInStock
            FROM Products p
            INNER JOIN Categories c ON p.CategoryID = c.CategoryID
            WHERE p.ProductID = @ProductID";
    }
}
