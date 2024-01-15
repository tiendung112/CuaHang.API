using CuaHang.Business.Implements;
using CuaHang.Business.IServices;
using CuaHang.Models.Context;
using CuaHang.Models.Entities;
using CuaHang.Repositories.Repo.Imp;
using CuaHang.Repositories.Repo.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CuaHang.Business
{
    public class Bootstrap
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAppDBContext, AppDBContext>();

            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<ICartServices, CartServices>();
            services.AddScoped<IProductTypeServices, ProductTypeServices>();
            services.AddScoped<IProductReviewServices, ProductReviewServices>();
            services.AddScoped<IHttpContextAccessor, IHttpContextAccessor>();

            RegisterRepositoryDependencies(services);
        }
        private static void RegisterRepositoryDependencies(IServiceCollection services)
        {
            services.AddScoped<IRepoBase<TaiKhoan>>
                (service => new RepoBase<TaiKhoan>(service.GetService<IAppDBContext>()));
            services.AddScoped<IRepoBase<Cart_item>>
                (services => new RepoBase<Cart_item>(services.GetService<IAppDBContext>()));
            services.AddScoped<IRepoBase<Carts>>
                (services => new RepoBase<Carts>(services.GetService<IAppDBContext>()));
            services.AddScoped<IRepoBase<Role>>
                (services => new RepoBase<Role>(services.GetService<IAppDBContext>()));
            services.AddScoped<IRepoBase<EmailValidate>>(services => new RepoBase<EmailValidate>(services.GetService<IAppDBContext>()));
            services.AddScoped<IRepoBase<Order>>
                (services => new RepoBase<Order>(services.GetService<IAppDBContext>()));
            services.AddScoped<IRepoBase<Order_detail>>
                (services => new RepoBase<Order_detail>(services.GetService<IAppDBContext>()));
            services.AddScoped<IRepoBase<Order_status>>
                (services => new RepoBase<Order_status>(services.GetService<IAppDBContext>()));
            services.AddScoped<IRepoBase<Payment>>
                (services => new RepoBase<Payment>(services.GetService<IAppDBContext>()));
            services.AddScoped<IRepoBase<Product>>
                (services => new RepoBase<Product>(services.GetService<IAppDBContext>()));
            services.AddScoped<IRepoBase<Product_review>>
                (services => new RepoBase<Product_review>(services.GetService<IAppDBContext>()));
            services.AddScoped<IRepoBase<Product_type>>
                (services => new RepoBase<Product_type>
                (services.GetService<IAppDBContext>()));
            services.AddScoped<IRepoBase<RefreshToken>>
                (services => new RepoBase<RefreshToken>(services.GetService<IAppDBContext>()));
            services.AddScoped<IRepoBase<NguoiDung>>
                (services => new RepoBase<NguoiDung>(services.GetService<IAppDBContext>()));
            
        }
    }
}
