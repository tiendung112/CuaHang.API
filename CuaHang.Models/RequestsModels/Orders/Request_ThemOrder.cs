
namespace CuaHang.Models.Requests.Orders
{
    public class Request_ThemOrder
    {
        public int paymentID { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
        //public List<Request_ThemOrderDetail>? details { get; set; } 
    }
}
