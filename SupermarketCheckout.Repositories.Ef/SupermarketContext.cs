using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Model;
using SupermarketCheckout.Repositories.Ef.Entities;

namespace SupermarketCheckout.Repositories.Ef
{
    public class SupermarketContext : DbContext
    {
        public SupermarketContext(DbContextOptions<SupermarketContext> options) : base(options)
        {

        }

        public virtual DbSet<ProductEntity> Product { get; set; }
        public virtual DbSet<OfferEntity> Offer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityConfig());
            modelBuilder.ApplyConfiguration(new OfferEntityConfig());
        }
    }
}
