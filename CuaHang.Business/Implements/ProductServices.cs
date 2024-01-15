using Azure;
using CuaHang.Business.IServices;
using CuaHang.Common;
using CuaHang.Models.Entities;
using CuaHang.Models.Handler.Image;
using CuaHang.Models.Requests;
using CuaHang.Models.Requests.Products;
using CuaHang.Models.RequestsModels.Products;
using CuaHang.Models.ResponsesModels.Product;
using CuaHang.Repositories.Repo.IRepo;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace CuaHang.Business.Implements
{
    public class ProductServices : BaseServices, IProductServices
    {
        private readonly IRepoBase<Product> repoProduct;
        private readonly IRepoBase<Product_type> repoProductType;
        public ProductServices(IRepoBase<Product> _repoProduct, IRepoBase<Product_type> _repoProductType)
        {
            repoProduct = _repoProduct;
            repoProductType = _repoProductType;

        }

        public IQueryable<ProductResponseModel> HienThiCacSanPhamNoiBat(GetPageProductRequestModel input)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductResponseModel> HienThiDanhSachSanPham(GetPageProductRequestModel input)
        {
            var query = repoProduct.GetQueryable(x=>x.IsDeleted==false);
            query = ApplySearchAndFilter(query, input);
            var data = query.Select(x => new ProductResponseModel(x));
            var result = Utilities.ApplyPaging(data, input.PageSize, input.PageNo);
            return result;
        }

        public async Task<ProductResponseModel> HienThiSanPham(int id)
        {
            var product =await repoProduct.GetAsync(x => x.Id == id && x.IsDeleted == false);
            product.number_of_views++;
            return new ProductResponseModel(product);
        }

        public async Task<ProductResponseModel> SuaSanPham(int id, UpdateProductModel request)
        {
            var product =await repoProduct.GetAsync(x => x.Id == id && x.IsDeleted==false);
            if(request.product_typeID !=0 && repoProductType.GetAsync(x=>x.Id==request.product_typeID)==null)
            {
                throw new Exception("Loại sản phẩm không tồn tại");
            }
            product.product_typeID = request.product_typeID.Value;
            product.name_product = request.name_product ?? product.name_product;
            product.price = request.price==null?product.price:request.price;
            product.title = request.title ?? product.title;
            product.discount = request.discount==0?product.discount:request.discount.Value;
            product.avartar_image_product =request.avartar_image_product==null?product.avartar_image_product:await FileToURL(request.avartar_image_product, product.Id);
            await repoProduct.UpdateAsync(product);
            return new ProductResponseModel(product);
        }

        public async Task<ProductResponseModel> ThemSanPham(CreateProductModel request)
        {
                var producttype = await repoProductType.GetAsync(x => x.Id == request.product_typeID && x.IsDeleted==false) ?? throw new Exception("Loại sản phẩm không tồn tại");
                Product product = new Product()
                {
                    name_product = request.name_product,
                    price = request.price,
                    product_typeID = producttype.Id,
                    created_at = DateTime.Now,
                    status = 1,
                    IsDeleted=false,
                };
                var sp = await repoProduct.CreateAsync(product);
                sp.avartar_image_product = await FileToURL(request.avartar_image_product, sp.Id);
                await repoProduct.UpdateAsync(sp);
                return new ProductResponseModel(sp);
        }

        IQueryable<Product> ApplySearchAndFilter(IQueryable<Product> query, GetPageProductRequestModel input)
        {
            //Search by Product Name Or ManuFacture Name Or Category Name
            if (!string.IsNullOrEmpty(input.Keyword)||!string.IsNullOrEmpty(input.title))
            {
                query =repoProduct.GetQueryable().Where(record =>
                    record.name_product.ToLower().Contains(input.Keyword.ToLower())
                    ||record.title.ToLower().Contains(input.title.ToLower()));
            }

            // Filter
            if (input.product_typeID.HasValue)
            {
                query = query.Where(record => record.product_typeID==input.product_typeID.Value);
            }

            if (input.TuGia.HasValue)
            {
                query = query.Where(record => record.price> input.TuGia);
            }

            if (input.DenGia.HasValue)
            {
                query = query.Where(record => record.price<input.DenGia);
            }

            return query;
        }
 
        public async Task XoaSanPham(int id)
        {
            var product =await repoProduct.GetAsync(x => x.Id == id&& x.IsDeleted==false);
            product.IsDeleted = true;
            await repoProduct.UpdateAsync(product);
        }
    }
}
