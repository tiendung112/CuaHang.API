using Microsoft.EntityFrameworkCore;
using CuaHang.Models.Entities;

namespace CuaHang.Models.Context
{
    public class AppDBContext : DbContext, IAppDBContext
    {
        public AppDBContext(DbContextOptions option) : base(option)
        {

        }
        public virtual DbSet<TaiKhoan> TaiKhoan { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Product_type> Products_type { get; set; }
        public virtual DbSet<Product_review> Products_review { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Order_detail> Order_Details { get; set; }
        public virtual DbSet<Order_status> Order_Statuses { get; set; }
        public virtual DbSet<Carts> Carts { get; set; }
        public  virtual DbSet<Cart_item> Cart_Items { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<EmailValidate> EmailValidates { get; set; }    
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        
        public async Task<int> CommitChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<TEntity> SetEntity<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

     /*   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(SourceData.MyConnect());
        }*/
    }
}
