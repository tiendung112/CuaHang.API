using CuaHang.Business.IServices;
using CuaHang.Common;
using CuaHang.Common.CustomException;
using CuaHang.Models.Entities;
using CuaHang.Models.RequestModels;
using CuaHang.Models.Requests.Auth;
using CuaHang.Models.RequestsModels.Auth;
using CuaHang.Models.ResponsesModels;
using CuaHang.Models.ResponsesModels.Account;
using CuaHang.Repositories.Repo.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
namespace CuaHang.Business.Implements
{
    public class AuthServices : BaseServices, IAuthServices
    {
        private readonly IRepoBase<TaiKhoan> repoTK;
        private readonly IRepoBase<NguoiDung> repoUser;
        private readonly IRepoBase<RefreshToken> repoToken;
        private readonly IRepoBase<Models.Entities.Role> repoDece;
        private readonly IConfiguration _configuration;
        private readonly IRepoBase<EmailValidate> repoValidate;
        private readonly IHttpContextAccessor contextAccessor;
        public AuthServices(IRepoBase<TaiKhoan> _repoTK, IRepoBase<NguoiDung> _repoUser
            , IRepoBase<RefreshToken> _repoToken, IRepoBase<Models.Entities.Role> _repoDece,
            IConfiguration configuration, IRepoBase<EmailValidate> _repoValidate,
            IHttpContextAccessor _httpContextAccessor)
        {
            repoTK = _repoTK;
            repoUser = _repoUser;
            repoToken = _repoToken;
            _configuration = configuration;
            repoDece = _repoDece;
            repoValidate = _repoValidate;
            contextAccessor = _httpContextAccessor;
        }
        public async Task<LoginResponseModel> Login(SignInData signInData)
        {
            try
            {
                signInData.Email.EmailValid();
                //signInData.MatKhau.PasswordValid();
                NguoiDung user = await repoUser.GetAsync(record => record.email.Contains(signInData.Email) && record.IsDeleted==false);
                TaiKhoan taiKhoan = await repoTK.GetAsync(record => record.Id == user.TaiKhoanId && record.IsDeleted == false);
                if (!taiKhoan.isActive)
                    throw new Exception(CommonContaint.ExceptionMessage.EMAIL_NOT_ACTIVATED);

                bool isValidPassword = signInData.MatKhau.IsPasswordValid(taiKhoan.password);
                if (!isValidPassword) throw new InvalidException(nameof(TaiKhoan.password));

                var token = GenerateToken(taiKhoan);

                return new LoginResponseModel
                {
                    Token = await token,
                    UserData = new AccountResponseModels(taiKhoan)
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string> GenerateToken(TaiKhoan taiKhoan)
        {
            try
            {
                var JWTtokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_configuration.GetSection(CommonContaint.ToKen.AUTH_SECRET).Value!);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(CommonContaint.Id, taiKhoan.Id.ToString()),
                        new Claim(CommonContaint.Username, taiKhoan.user_name),
                        new Claim(CommonContaint.Role.RoleId, taiKhoan.roleId.ToString()),
                        new Claim(ClaimTypes.Email, taiKhoan.nguoiDung.email),
                    }),
                    Expires = DateTime.Now.AddDays(100),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = JWTtokenHandler.CreateToken(tokenDescriptor);
                var resultToken = JWTtokenHandler.WriteToken(token);
                RefreshToken refreshToken = new RefreshToken
                {
                    accountID = taiKhoan.Id,
                    ExpiredTime = DateTime.Now.AddDays(100),
                    Token = resultToken,
                };
                await repoToken.CreateAsync(refreshToken);
                return resultToken;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task Register(RegisterData registerData)
        {
            try
            {
                await UserNameExist(registerData.username);
                registerData.email.EmailValid();
                await EmailExist(registerData.email); // Kiểm tra email đã tồn tại chưa.
                registerData.password.PasswordValid();
                registerData.phone.PhoneNumberValid();

                registerData.password = registerData.password.HashPassword();
                var taiKhoan = registerData.Mapping();
                var tk = await repoTK.CreateAsync(taiKhoan);
                var user = registerData.MappingUser(tk.Id);
                await repoUser.CreateAsync(user);
                await SendCodeActive(tk.Id, registerData.email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task XacThucTaiKhoan(ValidateRegisterModel request)
        {
            var validate = await repoValidate.GetAsync(x => x.MaXacNhan == request.MaXacNhan) ?? throw new NotFoundException(nameof(EmailValidate));
            if (validate.DaXacNhan) throw new Exception(CommonContaint.ExceptionMessage.VERIFIED);
            if (validate.ThoiGianHetHan < DateTime.Now) throw new Exception(CommonContaint.ExceptionMessage.EXPIRED);
            validate.DaXacNhan = true;
            await repoValidate.UpdateAsync(validate);
        }
        public async Task FogotPassWord(ForgotPasswordModel request)
        {
            request.Email.EmailValid();
            var user = await repoUser.GetAsync(record => record.email == request.Email && record.IsDeleted==false) ?? throw new NotFoundException(nameof(NguoiDung));
            var taiKhoan = await repoTK.GetAsync(record => record.Id == user.TaiKhoanId && record.IsDeleted == false);
            if (!taiKhoan.isActive) throw new Exception(CommonContaint.ExceptionMessage.EMAIL_NOT_ACTIVATED);
            await SendCodeActive(taiKhoan.Id, request.Email);
        }
        public async Task CreateNewPassword(CreateNewPasswordModel request)
        {
            var validate = await repoValidate.GetAsync(record => record.MaXacNhan == request.MaXacNhan ) ?? throw new NotFoundException(nameof(EmailValidate));
            if (validate.ThoiGianHetHan < DateTime.Now) throw new Exception(CommonContaint.ExceptionMessage.EXPIRED);
            var taiKhoan = await repoTK.GetAsync(record => record.Id == validate.TaiKhoanID && record.IsDeleted==false);
            request.MatKhau.PasswordValid();
            taiKhoan.password = request.MatKhau.HashPassword();
            taiKhoan.updated_at = DateTime.Now;
            await repoTK.UpdateAsync(taiKhoan);
        }
        public async Task ChangePassword(ChangePasswordModel request)
        {
            try
            {
                var id = contextAccessor.HttpContext.User.FindFirst(CommonContaint.Id).Value;
                var taiKhoan = await repoTK.GetAsync(record => record.Id.ToString() == id && record.isActive && record.IsDeleted==false ) ?? throw new NotFoundException(nameof(TaiKhoan));
                request.NewPassword.PasswordValid();
                taiKhoan.password = request.NewPassword.HashPassword();
                taiKhoan.updated_at = DateTime.Now;
                await repoTK.UpdateAsync(taiKhoan);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #region private 

        // Kiểm tra xem email đã tồn tại trong cơ sở dữ liệu hay chưa.
        private async Task EmailExist(string email)
        {
            var user = await repoUser.GetAsync(record => record.email.ToLower().Contains(email.ToLower()) && record.IsDeleted==false);
            if (user != null)
                throw new Exception(CommonContaint.ExceptionMessage.EMAIL_EXIST); // Nếu tồn tại, ném ra ngoại lệ.
        }
        private async Task UserNameExist(string userName)
        {
            var taikhoan = await repoTK.GetAsync(record => record.user_name.ToLower().Equals(userName.ToLower()) && record.IsDeleted==false);   
            if (taikhoan != null)
                throw new Exception(CommonContaint.ExceptionMessage.USer_EXIST); // Nếu tồn tại, ném ra ngoại lệ.
        }
        private async Task SendCodeActive(int taiKhoanId, string email)
        {
            var code = GenerateCodeActive();
           // await repoTK.DeleteAsync(x => x.Id == taiKhoanId);
            await repoValidate.DeleteAsync(x => x.TaiKhoanID == taiKhoanId);
            var confirm = new EmailValidate
            {
                DaXacNhan = false,
                MaXacNhan = GenerateCodeActive(),
                TaiKhoanID = taiKhoanId,
                ThoiGianHetHan = DateTime.Now.AddMinutes(15),
            };
            await repoValidate.CreateAsync(confirm);
            SendEmail(new Models.Handler.Email.EmailTo
            {
                Mail = email,
                Content = CommonContaint.MailMessage.newCodeAccount,
                Subject = string.Format(CommonContaint.MailMessage.inforCodeAccount, code.ToString()),
            });
        }

        #endregion
    }
}
