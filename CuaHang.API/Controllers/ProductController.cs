using CuaHang.Business.IServices;
using CuaHang.Common;
using CuaHang.Models.Requests.Products;
using CuaHang.Models.RequestsModels.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuaHang.API.Controllers
{
    [Route(CommonContaint.Router.Route)]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpPost]
        public async Task<IActionResult> ThemSanPham(CreateProductModel product)
        {
            var result =await _productServices.ThemSanPham(product);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> SuaSanPham(int id, UpdateProductModel product)
        {
            var result =await _productServices.SuaSanPham(id, product);
            return Ok(result);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> XoaSanPham(int id)
        {
            await _productServices.XoaSanPham(id);
            return Ok();
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> HienThiSanPham(int id)
        {
            var result =await _productServices.HienThiSanPham(id);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult HienThiSanPham(GetPageProductRequestModel input)
        {
            var result = _productServices.HienThiDanhSachSanPham(input);
            return Ok(result);
        }

    }
}
