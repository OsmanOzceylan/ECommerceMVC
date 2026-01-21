namespace ECommerceMVC.DataAccess.Queries
{
    public class FavoriteQueries
    {
        public static string GetFavoritesByCustomerID = @"
        SELECT f.FavoriteID, f.CustomerID, f.ProductID, 
               p.ProductName, p.UnitPrice, p.ImageUrl
        FROM Favorites f
        JOIN Products p ON f.ProductID = p.ProductID
        WHERE f.CustomerID = @CustomerID";

        public static string AddToFavorites = @"
        INSERT INTO Favorites (CustomerID, ProductID)
        VALUES (@CustomerID, @ProductID)";

        public static string RemoveFromFavorites = @"
        DELETE FROM Favorites 
        WHERE CustomerID = @CustomerID AND ProductID = @ProductID";

        public static string ClearFavorites = @"
        DELETE FROM Favorites 
        WHERE CustomerID = @CustomerID";

        public static string FavoriteExists = @"
        SELECT COUNT(1)
        FROM Favorites
        WHERE CustomerID = @CustomerID AND ProductID = @ProductID";
    }
}
