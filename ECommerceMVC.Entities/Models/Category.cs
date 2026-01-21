namespace ECommerceMVC.Entities.Models
{
    public class Category : BaseClass
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string? Description { get; set; }
    }
}
