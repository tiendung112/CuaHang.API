using CuaHang.Models.Pagination;
using CuaHang.Models.Requests;
using CuaHang.Models.Requests.Products;
using CuaHang.Models.RequestsModels.Products;
using CuaHang.Models.Responses;
using CuaHang.Models.ResponsesModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHang.Business.IServices
{
    public interface IProductServices
    {
        IQueryable<ProductResponseModel> HienThiDanhSachSanPham(GetPageProductRequestModel input);
        Task<ProductResponseModel> ThemSanPham(CreateProductModel request);
        Task<ProductResponseModel> SuaSanPham(int id, UpdateProductModel request);
        Task XoaSanPham(int id);
        IQueryable<ProductResponseModel> HienThiCacSanPhamNoiBat(GetPageProductRequestModel input);
        Task<ProductResponseModel> HienThiSanPham(int id);
    }
}
