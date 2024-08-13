using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)
    {
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>()
                .HasData(
                    new Coupon { Id = 1, ProductName = "Iphone X", Description = "Iphone Discount", Amount = 199 },
                    new Coupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 159 }
                );
        }
    }
}
