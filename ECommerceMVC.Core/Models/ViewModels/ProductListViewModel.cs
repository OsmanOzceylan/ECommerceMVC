using ECommerceMVC.Core.Models.Response;

namespace ECommerceMVC.Core.Models.ViewModels
{
    public class ProductListViewModel
    {
        public List<ProductResponseModel> Products { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalProducts { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalProducts / PageSize);
        public List<CategoryResponseModel> Categories { get; set; } = new List<CategoryResponseModel>();
        public List<int> FavoriteIds { get; set; } = new List<int>();
    }
}
