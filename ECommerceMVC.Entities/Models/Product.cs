namespace ECommerceMVC.Entities.Models
{
    public class Product : BaseClass
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public decimal UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public string CategoryName { get; set; }
        public int TotalSold { get; set; }
        public string? ImageUrl { get; set; }
    }
}
