using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SupermarketCheckout.Repositories.Ef.Entities
{
    public sealed class OfferEntityConfig : IEntityTypeConfiguration<OfferEntity>
    {
        public void Configure(EntityTypeBuilder<OfferEntity> builder)
        {
            builder.HasKey(e => e.OfferId);

            builder.Property(e => e.OfferId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.OfferType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.OfferQuantity)
                .IsRequired(false);

            builder.Property(e => e.OfferPrice)
                .IsRequired(false);
        }
    }
}
