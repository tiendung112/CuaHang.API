using CuaHang.Models.Entities;

namespace CuaHang.Models.ResponsesModels.User
{
    public class UserResponseModels
    {
        public string? user_name { get; set; }
        public int? accountID { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public string? address { get; set; }
        public UserResponseModels()
        {
            
        }

        public UserResponseModels(NguoiDung user)
        {
            user_name = user.user_name;
            accountID = user.TaiKhoanId;
            phone = user.phone;
            email = user.email;
            address = user.address;
            
        }
    }
}
