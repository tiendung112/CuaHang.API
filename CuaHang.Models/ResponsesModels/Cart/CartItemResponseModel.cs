using CuaHang.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHang.Models.ResponsesModels.Cart
{
    public class CartItemResponseModel
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }  
        public int? quantity { get; set; }
        public CartItemResponseModel()
        {
            

        }
        public CartItemResponseModel(Cart_item cart_Item)
        {
            CartItemId = cart_Item.Id;
            ProductId = cart_Item.productId;
            quantity = cart_Item.quantity;
        }
    }
}
