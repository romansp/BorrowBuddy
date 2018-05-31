using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BorrowBuddy.Data.Configuration {
  public class Currency : IEntityTypeConfiguration<Domain.Currency> {
    public void Configure(EntityTypeBuilder<Domain.Currency> builder) {
      builder.HasKey(p => p.Code);
      builder.Property(p => p.Code).HasMaxLength(MaxLength.CurrencyCode);
      builder.Property(p => p.Symbol).HasMaxLength(10);
      builder.Property(p => p.Scale).HasDefaultValue(100);
    }
  }
}
