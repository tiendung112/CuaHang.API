using CuaHang.Business.IServices;
using CuaHang.Common;
using CuaHang.Models.Requests.Product_Review;
using CuaHang.Models.RequestsModels.Product_Review;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CuaHang.API.Controllers
{
    [Route(CommonContaint.Router.Route)]
    [ApiController]
    public class ProductReviewController : ControllerBase
    {
        private readonly IProductReviewServices _productReviewService;
        public ProductReviewController(IProductReviewServices productReviewService)
        {
            _productReviewService = productReviewService;
        }
        [HttpPost]
        public async Task<IActionResult> ThemReview(CreateProductReviewModels request)
        {
            var result =await _productReviewService.ThemReview(request);
            return Ok(result);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> SuaReview(UpdateProductReviewModels request, int id)
        {
            var result = await _productReviewService.SuaReview(id, request);
            return Ok(result);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> XoaReview(int id)
        {
            await _productReviewService.XoaReview(id);
            return Ok();
        }
        [HttpGet]
        public IActionResult HienThiDanhSachReview(GetPageProductReviewRequestModels input)
        {
            var result = _productReviewService.HienThiDSReview(input);
            return Ok(result);

        }
        [HttpGet]
        public async Task<IActionResult> HienThiReview(GetPageProductReviewRequestModels input)
        {
            var result =await _productReviewService.HienThiDSReviewID(input);
            return Ok(result);
        }

    }
}
