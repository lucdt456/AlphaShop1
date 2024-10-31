using AlphaShop1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlphaShop1.Repository
{
	public class DataContext : IdentityDbContext<AppUserModel>
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}
		public DbSet<BrandModel> Brands { get; set; }
		public DbSet<ProductModel> Products { get; set; }
		public DbSet<CategoryModel> Categories { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderModel> Orders { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Khóa chính => 2 khóa ngoại
			modelBuilder.Entity<OrderDetail>()
				.HasKey(od => new { od.OrderCode, od.ProductId });

			// Thiết lập khóa ngoại cho OrderCode tham chiếu đến OrderModel
			modelBuilder.Entity<OrderDetail>()
				.HasOne(od => od.Order)
				.WithMany() // Nếu có ICollection<OrderDetail> trong OrderModel, thay bằng WithMany("OrderDetails")
				.HasForeignKey(od => od.OrderCode)
				.HasPrincipalKey(o => o.OrderCode); // Đảm bảo OrderCode là duy nhất

			// Thiết lập khóa ngoại cho ProductId tham chiếu đến ProductModel
			modelBuilder.Entity<OrderDetail>()
				.HasOne(od => od.Product)
				.WithMany() // Nếu có ICollection<OrderDetail> trong ProductModel, thay bằng WithMany("OrderDetails")
				.HasForeignKey(od => od.ProductId);
		}
	}
}
