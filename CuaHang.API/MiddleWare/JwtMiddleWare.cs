using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CuaHang.Common;
using static CuaHang.Common.CommonContaint;
namespace CuaHang.API.MiddleWare
{
    public class JwtMiddleWare :IMiddleware
    {
        private readonly IConfiguration configuration;

        public JwtMiddleWare(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string Token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrEmpty(Token))
            {
                Token = Convert.ToString(context.Request.Query["access_token"]);
            }
            if (Token != null)
            {
                AttachUserToContext(context, Token);
            }
            return next(context);
        }   

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.UTF8.GetBytes(configuration.GetSection(CommonContaint.ToKen.AUTH_SECRET).Value!);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
                var permissions = jwtToken.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
                context.Items[CommonContaint.ContextItem.PERMISSIONS] = permissions;
            }
            catch
            { 
            }
        }

    }
}
