using CuaHang.Models.Entities;
using Microsoft.AspNetCore.Http;
namespace CuaHang.Models.RequestsModels.Auth
{
    public class RegisterData
    {
        public string username { get; set; }
        public string name { get; set; }
        //public IFormFile? avatar { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public Entities.TaiKhoan Mapping()
        {
            return new Entities.TaiKhoan
            {
                user_name = username,
                password = password,
                created_at = DateTime.Now,
                roleId = 2,
                isActive = false,
            };
        }
        public NguoiDung MappingUser(int taiKhoanId)
        {
            return new NguoiDung
            {
                TaiKhoanId = taiKhoanId,
                address = address,
                email = email,
                phone = phone,
                user_name = username
            };
        }
    }

}
