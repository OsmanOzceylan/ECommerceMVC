
namespace ECommerceMVC.Entities.Models
{
    public class Favorite : BaseClass
    {
        public int FavoriteID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }


        // joinden gelen
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImageUrl { get; set; }
    }
}
