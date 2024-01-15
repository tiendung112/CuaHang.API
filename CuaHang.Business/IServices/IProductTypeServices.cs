using CuaHang.Models.Pagination;
using CuaHang.Models.Requests.Product_type;
using CuaHang.Models.RequestsModels.Product_Review;
using CuaHang.Models.RequestsModels.Product_type;
using CuaHang.Models.Responses;
using CuaHang.Models.ResponsesModels.ProductType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHang.Business.IServices
{
    public interface IProductTypeServices
    {
        Task<ProductTypeResponseModel> ThemProudct_type(CreateProductTypeModel request);
        Task<ProductTypeResponseModel> SuaProudct_type(int id ,CreateProductTypeModel request);
        Task DeleteProudct_type(int id);
        Task<ProductTypeResponseModel> GetProduct_typeById(int id);
        IQueryable<ProductTypeResponseModel> GetPageProductType(GetPageProductTypeRequestModel input);

    }
}
