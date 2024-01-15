using System.ComponentModel.DataAnnotations.Schema;

namespace CuaHang.Models.Entities
{
   // [Table(name: "account")]
    public class TaiKhoan : BaseEntity
    {
        public string user_name { get; set; }
        public string? avatar { get; set; }
        public string password { get; set; }
        public bool isActive { get; set; } = false;
        public int roleId { get; set; }
        public Role? Role { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; }
        public NguoiDung nguoiDung { get; set; }
        public IEnumerable<RefreshToken>? refreshTokens { get; set; }
        public IEnumerable<EmailValidate>? emailValidates { get; set; }
    }
}