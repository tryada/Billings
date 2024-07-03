using Billings.Models.Orders;
using Billings.Persistence.Configurations.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Billings.Persistence.Configurations;

internal class OrdersConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(ConfigurationConstants.TableNames.Orders);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.OrderId).IsRequired();
        builder.Property(x => x.ErpOrderId);
        builder.Property(x => x.InvoiceId);
        builder.Property(x => x.StoreId).IsRequired();
    }
}