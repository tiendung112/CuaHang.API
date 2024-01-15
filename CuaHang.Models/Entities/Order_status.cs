using System.ComponentModel.DataAnnotations.Schema;

namespace CuaHang.Models.Entities
{
    [Table(name: "order_status")]
    public class Order_status : BaseEntity
    {
        public string? status_name { get; set; }
        public IEnumerable<Order>? orders { get; set; }

    }
}
