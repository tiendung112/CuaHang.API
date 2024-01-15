using CuaHang.Models.Requests;

namespace CuaHang.Models.RequestsModels.Product_Review
{
    public class GetPageProductReviewRequestModels : BasePaginationInpu
    {
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public int? productID { get; set; }

    }
}
