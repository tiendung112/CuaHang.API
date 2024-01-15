using CuaHang.Models.RequestModels;
using CuaHang.Models.Requests.Auth;
using CuaHang.Models.RequestsModels.Auth;
using CuaHang.Models.ResponsesModels;
using Microsoft.AspNetCore.Http;

namespace CuaHang.Business.IServices
{
    public interface IAuthServices
    {
        Task<LoginResponseModel> Login(SignInData signInData);
        Task Register(RegisterData registerData);
        Task XacThucTaiKhoan(ValidateRegisterModel request);
        Task FogotPassWord(ForgotPasswordModel request);
        Task CreateNewPassword(CreateNewPasswordModel request);
        Task ChangePassword(ChangePasswordModel request);
    }
}
