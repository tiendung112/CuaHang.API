using CuaHang.Business.IServices;
using CuaHang.Common;
using CuaHang.Models.Entities;
using CuaHang.Models.Requests.Cart;
using CuaHang.Models.Requests.Cart_Item;
using CuaHang.Models.ResponsesModels.Cart;
using CuaHang.Repositories.Repo.IRepo;
using Microsoft.AspNetCore.Http;
namespace CuaHang.Business.Implements
{
    public class CartServices : BaseServices, ICartServices
    {
        private readonly IRepoBase<Carts> repoCart;
        private readonly IRepoBase<Cart_item> repoCart_Item;
        private readonly IRepoBase<TaiKhoan> repoTK;
        private readonly IRepoBase<NguoiDung> repoUser;
        private readonly IRepoBase<Product> repoProduct;
        private readonly IHttpContextAccessor contextAccessor;

        public CartServices(IRepoBase<Carts> _repoCart, IRepoBase<Cart_item> _repoCart_Item,
            IRepoBase<TaiKhoan> _repoTK, IRepoBase<NguoiDung> _repoUser,
            IRepoBase<Product> _repoProduct, IHttpContextAccessor _contextAccessor)
        {
            repoCart = _repoCart;
            repoCart_Item = _repoCart_Item;
            repoTK = _repoTK;
            repoUser = _repoUser;
            repoProduct = _repoProduct;
            contextAccessor = _contextAccessor;
        }

        public IQueryable<CartResponseModel> HienThiCart(int pageSize, int pageNo)
        {
            var cart = repoCart.GetQueryable(record => record.IsDeleted == false).Select(record => new CartResponseModel(record));
            var result = Utilities.ApplyPaging(cart, pageSize, pageNo);
            return result;
        }

        public async Task<CartResponseModel> HienThiCartTheoID(int accid)
        {
            var cart = await repoCart.GetAsync(record => record.Id == accid && record.IsDeleted == false)
                ?? throw new Exception("Không tìm thấy giỏ hàng");
            return new CartResponseModel(cart);
        }

        public Task<CartResponseModel> SuaCart(int cartid, CreateCartModel request)
        {
            throw new NotImplementedException();
        }

        public async Task<CartResponseModel> ThemCart(CreateCartModel request)
        {
            var userid = int.Parse(contextAccessor.HttpContext.User.FindFirst("Id").Value);
            var TaiKhoan = await repoTK.GetAsync(record => record.Id == userid && record.IsDeleted == false)
                ?? throw new Exception("Không tìm thấy người dùng");
            var NguoiDung = await repoUser.GetAsync(record => record.TaiKhoanId == TaiKhoan.Id && record.IsDeleted == false)
                ?? throw new Exception("Không tìm thấy người dùng");
            Carts newcart = new Carts()
            {
                created_at = DateTime.Now,
            };
            var cart = await repoCart.CreateAsync(newcart);
            cart.cart_Items = await ThemCartItem(cart.Id, request.cart_Items);
            await repoCart_Item.CreateAsync(cart.cart_Items);
            await repoCart.UpdateAsync(cart);
            return new CartResponseModel(cart);
        }

        private async Task<List<Cart_item>> ThemCartItem(int cartid, List<CreateCartItemModel> cart_Item)
        {
            List<Cart_item> CartITem = new List<Cart_item>();
            foreach (var item in cart_Item)
            {
                var sp = await repoProduct.GetAsync(record => record.Id == item.productId && record.IsDeleted == false)
                    ?? throw new Exception("Sản phẩm không tồn tại");
                Cart_item detail = new()
                {
                    created_at = DateTime.Now,
                    productId = item.productId,
                    cartID = cartid,
                    quantity = item.quantity,
                };
                CartITem.Add(detail);
            }
            return CartITem;
        }
        public async Task XoaCart(int id)
        {
            var userid = int.Parse(contextAccessor.HttpContext.User.FindFirst("Id").Value);
            var TaiKhoan = repoTK.GetAsync(record => record.Id == userid && record.IsDeleted == false)
                ?? throw new Exception("Không tìm thấy người dùng");
            var NguoiDung = repoUser.GetAsync(record => record.TaiKhoanId == TaiKhoan.Id && record.IsDeleted == false)
                ?? throw new Exception("Không tìm thấy người dùng");
            var cart = await repoCart.GetAsync(x => x.Id == id)
                ?? throw new Exception("Không tìm thấy giỏ hàng");
            var cart_item = await repoCart_Item.GetAllAsync(record => record.cartID == cart.Id && record.IsDeleted == false);
            foreach (var item in cart_item)
            {
                item.IsDeleted = true;
                await repoCart_Item.UpdateAsync(item);
            }
            cart.IsDeleted = true;
            await repoCart.UpdateAsync(cart);
        }
    }
}
