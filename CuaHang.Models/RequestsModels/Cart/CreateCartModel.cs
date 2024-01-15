using CuaHang.Models.Requests.Cart_Item;

namespace CuaHang.Models.Requests.Cart
{
    public class CreateCartModel
    {
        public List<CreateCartItemModel> cart_Items { get; set; }
    }
}
