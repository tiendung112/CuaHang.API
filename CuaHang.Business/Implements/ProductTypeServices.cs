using CuaHang.Business.IServices;
using CuaHang.Common;
using CuaHang.Models.Entities;
using CuaHang.Models.Requests.Product_type;
using CuaHang.Models.RequestsModels.Product_type;
using CuaHang.Models.RequestsModels.Products;
using CuaHang.Models.ResponsesModels.ProductType;
using CuaHang.Repositories.Repo.IRepo;

namespace CuaHang.Business.Implements
{
    public class ProductTypeServices : BaseServices, IProductTypeServices
    {
        private readonly IRepoBase<Product> repoProduct;
        private readonly IRepoBase<Product_type> repoProductType;
        public ProductTypeServices(IRepoBase<Product> _repoProduct, IRepoBase<Product_type> _repoProductType)
        {
            repoProduct = _repoProduct;
            repoProductType = _repoProductType;

        }
        public async Task DeleteProudct_type(int id)
        {
            var productType =await repoProductType.GetAsync(record => record.Id == id && record.IsDeleted == false) 
                ?? throw new Exception("Không tìm thấy loại sản phẩm");
            var product = await repoProduct.GetAllAsync(record => record.product_typeID == productType.Id && record.IsDeleted == false);
            if(product.Any())
            {
                foreach (var item in product)
                {
                    item.IsDeleted = true;
                    await repoProduct.UpdateAsync(item);
                }
            }
            productType.IsDeleted = true;
            await repoProductType.UpdateAsync(productType);
        }

        public IQueryable<ProductTypeResponseModel> GetPageProductType(GetPageProductTypeRequestModel input)
        {
            var query  = repoProductType.GetQueryable(record => record.IsDeleted == false).Select(record => new ProductTypeResponseModel(record));
            query.ApplyPaging(input.PageSize, input.PageNo);
            var data = query.OrderByDescending(record => record.product_type_id);
            return query;
        }
        IQueryable<Product_type> ApplySearchAndFilter(IQueryable<Product_type> query, GetPageProductRequestModel input)
        {
            //Search by Product Name Or ManuFacture Name Or Category Name
            if (!string.IsNullOrEmpty(input.Keyword) || !string.IsNullOrEmpty(input.title))
            {
                query = repoProductType.GetQueryable().Where(record =>
                    record.name_product_type.ToLower().Contains(input.Keyword.ToLower()));
            }

            // Filter
                
            return query;
        }


        public async Task<ProductTypeResponseModel> GetProduct_typeById(int id)
        {
            var data = await repoProductType.GetByIDAsync(id);
            var result =  new ProductTypeResponseModel(data);
            return result;
        }

        public Task<ProductTypeResponseModel> SuaProudct_type(CreateProductTypeModel request)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductTypeResponseModel> ThemProudct_type(CreateProductTypeModel request)
        {
            try
            {
                Product_type type = new Product_type()
                {
                    name_product_type = request.name_product_type,
                    created_at = DateTime.Now,
                    IsDeleted = false,
                };
                await repoProductType.CreateAsync(type);
                var result = new ProductTypeResponseModel(type);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
