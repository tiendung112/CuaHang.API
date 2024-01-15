using CuaHang.Models.Requests.Cart;
using CuaHang.Models.Responses;
using CuaHang.Models.ResponsesModels.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHang.Business.IServices
{
    public interface ICartServices
    {
        Task<CartResponseModel> ThemCart( CreateCartModel request);
        Task<CartResponseModel> SuaCart( int cartid, CreateCartModel request);
        Task XoaCart( int id);
        Task<CartResponseModel> HienThiCartTheoID(int accid);
        IQueryable<CartResponseModel> HienThiCart(int pageSize, int pageNo);    
    }
}
