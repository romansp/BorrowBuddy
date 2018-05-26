using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BorrowBuddy.Data.Configuration {
  public class Currency : IEntityTypeConfiguration<Domain.Currency> {
    public void Configure(EntityTypeBuilder<Domain.Currency> builder) {
      builder.HasKey(p => p.Code);
    }
  }
}
