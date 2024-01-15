using System.ComponentModel.DataAnnotations.Schema;

namespace CuaHang.Models.Entities
{
    [Table(name: "payment")]
    public class Payment : BaseEntity
    {

        public string? payment_method { get; set; }
        public int? status { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; }
        public IEnumerable<Order>? orders { get; set; }
    }
}
