using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BorrowBuddy.Data.Configuration {
  public class Participant : IEntityTypeConfiguration<Domain.Participant> {
    public void Configure(EntityTypeBuilder<Domain.Participant> builder) {
      builder.Property(c => c.FirstName).IsRequired();
    }
  }
}
