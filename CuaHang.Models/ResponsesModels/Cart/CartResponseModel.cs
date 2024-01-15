using CuaHang.Models.Entities;

namespace CuaHang.Models.ResponsesModels.Cart
{
    public class CartResponseModel
    {
        public int CartId { get; set; }
        public List<CartItemResponseModel>? CartItems { get; set; }

        public CartResponseModel()
        {

        }
        public CartResponseModel(Carts carts)
        {
            CartId = carts.Id;
            CartItems = carts.cart_Items?.Select(item => new CartItemResponseModel
            {
                CartItemId = item.Id,
                ProductId = item.productId,
                quantity= item.quantity
            }).ToList();
        }
    }

}
