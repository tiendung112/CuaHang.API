using CuaHang.Models.Entities;
using CuaHang.Models.ResponsesModels.User;

namespace CuaHang.Models.ResponsesModels.Account
{
    public class AccountResponseModels
    {
        public string? user_name { get; set; }
        public string? avatar { get; set; }
        public string? password { get; set; }
        public String? isActive { get; set; }
        public UserResponseModels? Users { get; set; }
        //public IEnumerable<UserResponseModels>? Users { get; set; }

        public AccountResponseModels(TaiKhoan account)
        {
            user_name = account.user_name;
            avatar = account.avatar;
            password = account.password;
            isActive = account.isActive == false ? "chưa xác thực" : "đã xác thực";
            Users =new UserResponseModels(account.nguoiDung);
        }

    }
}
