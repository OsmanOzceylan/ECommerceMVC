
namespace ECommerceMVC.Entities.Models
{
    public class BaseClass
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime ModifiedTime { get; set; }
        public int ModifiedUserId { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int? DeletedUserId { get; set; }
    }
}
