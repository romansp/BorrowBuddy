using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BorrowBuddy.Data.Configuration {
  public class Flow : IEntityTypeConfiguration<Domain.Flow> {
    public void Configure(EntityTypeBuilder<Domain.Flow> builder) {
      builder.OwnsOne(p => p.Amount);
    }
  }
}
