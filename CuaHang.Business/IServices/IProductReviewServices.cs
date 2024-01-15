using CuaHang.Models.Pagination;
using CuaHang.Models.Requests.Product_Review;
using CuaHang.Models.RequestsModels.Product_Review;
using CuaHang.Models.Responses;
using CuaHang.Models.ResponsesModels.ProductReview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHang.Business.IServices
{
    public interface IProductReviewServices 
    {
        Task<ProductReviewResponseModel> ThemReview(CreateProductReviewModels request);
        Task<ProductReviewResponseModel> SuaReview(int id, UpdateProductReviewModels request);
        Task XoaReview(int id);
        Task<IQueryable<ProductReviewResponseModel>> HienThiDSReviewID(GetPageProductReviewRequestModels input);
        IQueryable<ProductReviewResponseModel> HienThiDSReview(GetPageProductReviewRequestModels input);

    }
}
