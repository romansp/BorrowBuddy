using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BorrowBuddy.Data.Configuration {
  public class Money : IEntityTypeConfiguration<Domain.Money> {
    public void Configure(EntityTypeBuilder<Domain.Money> builder) {
      builder.Property(p => p.Currency).IsRequired();
    }
  }
}
