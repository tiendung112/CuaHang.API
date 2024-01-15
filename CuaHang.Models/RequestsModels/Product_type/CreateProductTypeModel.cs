using Microsoft.AspNetCore.Http;

namespace CuaHang.Models.Requests.Product_type
{
    public class CreateProductTypeModel
    {
        public string? name_product_type { get; set; }
        public IFormFile? image_type_product { get; set; }
    }
}
