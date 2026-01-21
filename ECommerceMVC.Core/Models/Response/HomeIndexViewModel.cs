namespace ECommerceMVC.Core.Models.Response
{
    public class HomeIndexViewModel
    {
        public List<ProductResponseModel> TopSellingProducts { get; set; }
        public List<ProductResponseModel> FavoriteProducts { get; set; }
        public List<ProductResponseModel> AllProducts { get; set; }
        public List<CategoryResponseModel> Categories { get; set; } = new();
        public List<int> FavoriteIds { get; set; } = new List<int>();
        // Pagination
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalProducts { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalProducts / PageSize);
    }
}
