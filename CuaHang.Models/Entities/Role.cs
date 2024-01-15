using System.ComponentModel.DataAnnotations.Schema;

namespace CuaHang.Models.Entities
{
   // [Table(name: "decentralization")]
    public class Role : BaseEntity
    {
        public string? RoleName { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; }
        public IEnumerable<TaiKhoan>? TaiKhoan { get; set; }
    }
}
