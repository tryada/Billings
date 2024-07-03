using Billings.Models.Offers;
using Billings.Persistence.Configurations.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Billings.Persistence.Configurations;

internal class OffersConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable(ConfigurationConstants.TableNames.Offers);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.OfferId).IsRequired();
        builder.Property(x => x.Name).IsRequired();
    }
}

internal class OfferFixedCostsConfiguration : IEntityTypeConfiguration<OfferFixedCost>
{
    public void Configure(EntityTypeBuilder<OfferFixedCost> builder)
    {
        builder.ToView(ConfigurationConstants.TableNames.OfferFixedCosts);
        builder.HasNoKey();
        builder.Property(x => x.Value)
            .HasColumnType(ConfigurationConstants.Types.Decimal);
    }
}