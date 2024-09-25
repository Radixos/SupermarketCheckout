using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SupermarketCheckout.Repositories.Ef.Entities
{
    public sealed class BasketItemEntityConfig : IEntityTypeConfiguration<BasketItemEntity>
    {
        public void Configure(EntityTypeBuilder<BasketItemEntity> builder)
        {
            builder.HasKey(e => e.Sku);

            builder.Property(e => e.Sku)
                .IsRequired()
                .HasMaxLength(1);

            builder.Property(e => e.Price)
                .IsRequired();

            builder.Property(e => e.OfferId)
                .IsRequired(false);

            builder.HasOne(e => e.Offer)
                .WithMany(oe => oe.BasketItems)
                .HasForeignKey(e => e.OfferId);
        }
    }
}
