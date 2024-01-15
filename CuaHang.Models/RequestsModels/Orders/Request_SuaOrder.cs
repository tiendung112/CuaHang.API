using CuaHang.Models.Requests.OrderDetails;

namespace CuaHang.Models.Requests.Orders
{
    public class Request_SuaOrder
    {
        public int? paymentID { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
        public int? order_statusID { get; set; }
        public List<Request_ThemOrderDetail>? details { get; set; }
    }
}
