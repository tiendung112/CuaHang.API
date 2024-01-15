using CuaHang.Models.ResponsesModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHang.Models.ResponsesModels.ProductType
{
    public class ProductTypeResponseModel
    {
        public int product_type_id { get; set; }
        public string? name_product_type { get; set; }
        public string? image_type_product { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public List<ProductResponseModel>? ProductDTOs { get; set; }
        public ProductTypeResponseModel()
        {
            

        }
        public ProductTypeResponseModel(Entities.Product_type type)
        {
            product_type_id = type.Id;
            name_product_type = type.name_product_type;
            image_type_product = type.image_type_product;
            created_at = type.created_at;
            type.updated_at = type.updated_at;
            ProductDTOs = type.products?.Select(x => new ProductResponseModel(x)).ToList();
        }
    }
}
