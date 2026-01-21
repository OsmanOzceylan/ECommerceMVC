namespace ECommerceMVC.Core.Models.Request
{
    public class OrderRequest
    {
        public class OrderDetailRequest
        {
            public int ProductID { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
        }

        public class OrderRequestModel
        {
            public int CustomerID { get; set; }
            public DateTime OrderDate { get; set; }
        }

        public class DeliveryInfo
        {
            public string Address { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public string Phone { get; set; }
        }
    }
}
