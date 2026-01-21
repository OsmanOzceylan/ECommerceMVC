namespace ECommerceMVC.DataAccess.Queries
{
    public static class CustomerQueries
    {
        public static string GetCustomerByCustomerName = @"
            SELECT CustomerID, CustomerName, CustomerPassword, CustomerLastName, Email, Address, City, PostalCode, Country, Phone 
            FROM Customers 
            WHERE CustomerName = @CustomerName;
        ";
        public static string GetCustomerByCustomerID = @"
            SELECT CustomerID, CustomerName, CustomerPassword, CustomerLastName, Email, Address, City, PostalCode, Country, Phone 
            FROM Customers 
            WHERE CustomerID = @CustomerID;
        ";
        public static string GetCustomerInfo = @"
            SELECT CustomerID, CustomerName, CustomerPassword, CustomerLastName, Email, Address, City, PostalCode, Country, Phone 
            FROM Customers 
            WHERE CustomerName = @CustomerName AND CustomerPassword = @CustomerPassword;
        ";
        public static string CreateCustomer = @"
            INSERT INTO Customers (CustomerName, CustomerPassword, CustomerLastName, Email, Address, City, PostalCode, Country, Phone, IsActive, IsDeleted, CreatedTime)
            VALUES (@CustomerName, @CustomerPassword, @CustomerLastName, @Email, @Address, @City, @PostalCode, @Country, @Phone, 1, 0, GETDATE());
            SELECT CAST(SCOPE_IDENTITY() AS INT);
        ";
        public static string UpdateCustomer = @"
        UPDATE Customers
        SET CustomerName = @CustomerName,
            Address = @Address,
            City = @City,
            Country = @Country,
            Phone = @Phone,
            CustomerLastName = @CustomerLastName,
            Email = @Email,
            PostalCode = @PostalCode
        WHERE CustomerID = @CustomerID";
    }
}
