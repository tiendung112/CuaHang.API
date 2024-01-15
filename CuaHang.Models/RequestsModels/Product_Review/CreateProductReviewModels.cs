namespace CuaHang.Models.Requests.Product_Review
{
    public class CreateProductReviewModels
    {
        public int productID { get; set; }

        public string? content_rated { get; set; }
        public int point_evaluation { get; set; }
        public string? content_seen { get; set; }
       
    }
}
