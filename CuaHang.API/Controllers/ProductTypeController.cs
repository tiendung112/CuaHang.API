using CuaHang.Business.IServices;
using CuaHang.Common;
using CuaHang.Models.Requests.Product_type;
using CuaHang.Models.RequestsModels.Product_type;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuaHang.API.Controllers
{
    [Route(CommonContaint.Router.Route)]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeServices productTypeServices;
        public ProductTypeController(IProductTypeServices _productTypeServices)
        {
            productTypeServices = _productTypeServices;
        }

        [HttpPost]
        public async Task<IActionResult>ThemLoaiSanPham(CreateProductTypeModel typeModel)
        {
            var result =await productTypeServices.ThemProudct_type(typeModel);
            return Ok(result);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> XoaLoaiSanPham(int id)
        {
           await productTypeServices.DeleteProudct_type(id);
            return Ok();
        }
        [HttpGet]
        public IActionResult HienThiLoaiSanPham([FromQuery] GetPageProductTypeRequestModel input)
        {
            var result = productTypeServices.GetPageProductType(input);
            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> HienThiLoaiSanPhamID(int id)
        {
            var result = await productTypeServices.GetProduct_typeById(id);
            return Ok(result);
        }
    }
}
