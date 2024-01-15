using System.ComponentModel.DataAnnotations.Schema;

namespace CuaHang.Models.Entities
{
    [Table(name: "product_review")]
    public class Product_review : BaseEntity
    {
        public int productID { get; set; }
        public Product? product { get; set; }
        public int userID { get; set; }
        public NguoiDung? nguoiDung { get; set; }
        public string? content_rated { get; set; }
        public int point_evaluation { get; set; } = 0;
        public string? content_seen { get; set; }
        public int? status { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; }

    }
}
