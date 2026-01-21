
namespace ECommerceMVC.Entities.Models
{
    public class Order : BaseClass
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
