using CuaHang.Business;
namespace CuaHang.API
{
    public class Bootstrap
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            Business.Bootstrap.Register(services, configuration);
            RegisterBusinessDependencies(services);
        }

        private static void RegisterBusinessDependencies(IServiceCollection services)
        {

        }
    }
}
