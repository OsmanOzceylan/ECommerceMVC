namespace ECommerceMVC.DataAccess.Queries
{
    public class OrderQueries
    {
        public const string CreateOrder = @"
            INSERT INTO Orders (CustomerID, OrderDate )
            VALUES (@CustomerID, @OrderDate);
            SELECT CAST(SCOPE_IDENTITY() as int);"; // otomatik artan ıd değeri alır.


        public const string CreateOrderDetail = @"
            INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice)
            VALUES (@OrderID, @ProductID, @Quantity, @UnitPrice)";
        public const string CreateOrderInfo = @"
            INSERT INTO OrderInfo 
            (OrderID, FirstName, LastName, Email, Address, City, PostalCode, PhoneNumber, CardNumber, CardHolderName, CVV)
            VALUES (@OrderID, @FirstName, @LastName, @Email, @Address, @City, @PostalCode, @PhoneNumber, @CardNumber, @CardHolderName, @CVV)";
    }
}
