using Billings.Models.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Billings.Persistence.Configurations.Tokens;

internal class AllegroTokensConfiguration : IEntityTypeConfiguration<AllegroToken>
{
    public void Configure(EntityTypeBuilder<AllegroToken> builder)
    {
        builder.ToTable("AllegroTokens");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.AccessToken)
            .IsRequired();
        builder.Property(x => x.RefreshToken)
            .IsRequired();
        builder.Property(x => x.ExpiresAt)
            .IsRequired();
        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}