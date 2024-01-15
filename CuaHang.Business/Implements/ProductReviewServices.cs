using CuaHang.Business.IServices;
using CuaHang.Common;
using CuaHang.Models.Entities;
using CuaHang.Models.Requests.Product_Review;
using CuaHang.Models.RequestsModels.Product_Review;
using CuaHang.Models.ResponsesModels.ProductReview;
using CuaHang.Repositories.Repo.IRepo;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHang.Business.Implements
{
    public class ProductReviewServices : BaseServices, IProductReviewServices
    {
        private readonly IRepoBase<Product> repoProduct;
        private readonly IRepoBase<Product_type> repoProductType;
        private readonly IRepoBase<Product_review> repoProductReview;
        private readonly IRepoBase<TaiKhoan> repoTK;
        private readonly IRepoBase<NguoiDung> repoUser;
        private readonly IHttpContextAccessor contextAccessor;
        public ProductReviewServices(IRepoBase<Product> _repoProduct,
            IRepoBase<Product_type> _repoProductType,
            IRepoBase<Product_review> _repoProductReview,
            IRepoBase<TaiKhoan> _repoTK,
            IRepoBase<NguoiDung> _repoUser,
            IHttpContextAccessor _contextAccessor
            )
        {
            repoProduct = _repoProduct;
            repoProductType = _repoProductType;
            repoProductReview = _repoProductReview;
            repoTK = _repoTK;
            repoUser = _repoUser;
            contextAccessor = _contextAccessor;
        }

        public async Task<IQueryable<ProductReviewResponseModel>> HienThiDSReviewID( GetPageProductReviewRequestModels input)
        {
            int userid = int.Parse(contextAccessor.HttpContext.User.FindFirst("Id").Value);
            var tk = await repoTK.GetAsync(x => x.Id == userid && !x.IsDeleted) ?? throw new Exception("Tài khoản không tồn tại");
            var user = await repoUser.GetAsync(x => x.TaiKhoanId == tk.Id && !x.IsDeleted) ?? throw new Exception("Người dùng không tồn tại");
            var query = repoProductReview.GetQueryable(x => x.IsDeleted == false && x.userID == user.Id);
            query = ApplySearchAndFilter(query, input);
            query.OrderByDescending(x => x.created_at);
            var data = query.Select(x => new ProductReviewResponseModel(x));
            var result = Utilities.ApplyPaging(data, input.PageSize, input.PageNo);
            return result;
        }

        private IQueryable<Product_review> ApplySearchAndFilter(IQueryable<Product_review> query, GetPageProductReviewRequestModels input)
        {
            // Filter
            if (input.TuNgay.HasValue)
            {
                query = query.Where(record => record.created_at >=input.TuNgay);
            }

            if (input.DenNgay.HasValue)
            {
                query = query.Where(record => record.created_at <= input.DenNgay);
            }

            if (input.productID.HasValue)
            {
                query = query.Where(record => record.productID == input.productID);
            }

            return query;
        }

        public IQueryable<ProductReviewResponseModel> HienThiDSReview(GetPageProductReviewRequestModels input)
        {
            var query = repoProductReview.GetQueryable(x => x.IsDeleted == false);
            query = ApplySearchAndFilter(query, input);
            query.OrderByDescending(x => x.created_at);
            var data = query.Select(x => new ProductReviewResponseModel(x));
            var result = Utilities.ApplyPaging(data, input.PageSize, input.PageNo);
            return result;
        }

        public async Task<ProductReviewResponseModel> SuaReview(int id, UpdateProductReviewModels request)
        {
            var review =await repoProductReview.GetAsync(x => x.Id == id && !x.IsDeleted);
            review.updated_at = DateTime.Now;
            review.content_rated = request.content_rated ?? review.content_rated;
            review.point_evaluation = request.point_evaluation ==0 ? review.point_evaluation:request.point_evaluation ;
            review.content_seen = request.content_seen ??review.content_seen;
            await repoProductReview.UpdateAsync(review);
            return new ProductReviewResponseModel(review);
        }

        public async Task<ProductReviewResponseModel> ThemReview(CreateProductReviewModels request)
        {
            int userID = int.Parse(contextAccessor.HttpContext.User.FindFirst("Id").Value);
            var tk = await repoTK.GetAsync(x => x.Id == userID && !x.IsDeleted) ?? throw new Exception("Tài khoản không tồn tại");
            var user = await repoUser.GetAsync(x => x.TaiKhoanId == tk.Id && !x.IsDeleted) ?? throw new Exception("Người dùng không tồn tại");
            var product =await repoProduct.GetAsync(x => x.Id == request.productID && !x.IsDeleted) ?? throw new Exception("Sản phẩm không tồn tại");
            
            Product_review review = new Product_review()
            {
                userID = user.Id,
                productID = product.Id,
                content_rated = request.content_rated,
                created_at = DateTime.Now,
                IsDeleted = false,
                point_evaluation = request.point_evaluation,
                content_seen = request.content_seen,
                status = 1,
            };
            await repoProductReview.CreateAsync(review);
            return new ProductReviewResponseModel(review);
        }

        public async Task XoaReview(int id)
        {
            var review =await repoProductReview.GetAsync(x => x.Id == id && !x.IsDeleted);
            review.IsDeleted = true;
            await repoProductReview.UpdateAsync(review);

        }
    }
}
