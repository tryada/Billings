using Billings.Models.Billings;
using Billings.Persistence.Configurations.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Billings.Persistence.Configurations;

internal class BillingsConfiguration : IEntityTypeConfiguration<Billing>
{
    public void Configure(EntityTypeBuilder<Billing> builder)
    {
        builder.ToTable(ConfigurationConstants.TableNames.Billings);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.BillingType)
            .WithMany()
            .HasForeignKey(x => x.BillingTypeId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.Property(x => x.BillingTypeId)
            .IsRequired();

        builder.Property(x => x.OccurredAt).IsRequired();
        
        builder.HasOne(x => x.Offer)
            .WithMany()
            .HasForeignKey(x => x.OfferId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.Property(x => x.OfferId);

        builder.HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.Property(x => x.OrderId);
        
        builder.Property(x => x.BillingTypeId);
        builder.Property(x => x.Value)
            .IsRequired()
            .HasColumnType(ConfigurationConstants.Types.Decimal);

        builder.Property(x => x.ValueCurrencyCode).IsRequired();
        builder.Property(x => x.Balance)
            .IsRequired()
            .HasColumnType(ConfigurationConstants.Types.Decimal);

        builder.Property(x => x.BalanceCurrencyCode).IsRequired();
    }
}

internal class BillingTypeConfiguration : IEntityTypeConfiguration<BillingType>
{
    public void Configure(EntityTypeBuilder<BillingType> builder)
    {
        builder.ToTable(ConfigurationConstants.TableNames.BillingTypes);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Description).IsRequired();
    }
}