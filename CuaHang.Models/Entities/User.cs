using System.ComponentModel.DataAnnotations.Schema;

namespace CuaHang.Models.Entities
{
    [Table(name: "NguoiDung")]
    public class NguoiDung : BaseEntity
    {
        public string? user_name { get; set; }

        public string? phone { get; set; }
        public string? email { get; set; }
        public string? address { get; set; }
        public int? TaiKhoanId { get; set; }
        public TaiKhoan? TaiKhoan { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; }

        public IEnumerable<Carts>? carts { get; set; }
        public IEnumerable<Order>? orders { get; set; }
        public IEnumerable<Product_review>? product_review { get; set; }
    }
}
