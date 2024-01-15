using CuaHang.Business.IServices;
using CuaHang.Common;
using CuaHang.Models.RequestModels;
using CuaHang.Models.Requests.Auth;
using CuaHang.Models.RequestsModels.Auth;
using CuaHang.Models.ResponsesModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CuaHang.API.Controllers
{
    [Route(CommonContaint.Router.Route)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices authServices;
        public AuthController(IAuthServices _authServices)
        {
            authServices= _authServices;
        }

        [HttpPost]
        public async Task<IActionResult> DangKyTaiKhoan([FromQuery] RegisterData registerData)
        {
            await authServices.Register(registerData);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> XacThucTaiKhoan([FromQuery] ValidateRegisterModel request)
        {
            await authServices.XacThucTaiKhoan(request);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> DangNhap([FromQuery] SignInData request)
        {
            var result =await authServices.Login(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> QuenMatKhau([FromQuery] ForgotPasswordModel request)
        {
            await authServices.FogotPassWord(request);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> TaoMatKhauMoi([FromQuery] CreateNewPasswordModel request)
        {
            await authServices.CreateNewPassword(request);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> DoiMatKhau([FromBody] ChangePasswordModel request)
        {
            await authServices.ChangePassword(request);
            return Ok();
        }
    }
}
