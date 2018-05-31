using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BorrowBuddy.Data.Configuration {
  public class Participant : IEntityTypeConfiguration<Domain.Participant> {
    public void Configure(EntityTypeBuilder<Domain.Participant> builder) {
      builder.Property(c => c.FirstName).HasMaxLength(MaxLength.Name).IsRequired();
      builder.Property(c => c.LastName).HasMaxLength(MaxLength.Name);
      builder.Property(c => c.MiddleName).HasMaxLength(MaxLength.Name);
      builder.HasMany(p => p.Borrowed).WithOne(c => c.Lendee).IsRequired();
      builder.HasMany(p => p.Lended).WithOne(c => c.Lender).IsRequired().OnDelete(DeleteBehavior.Restrict);
    }
  }
}
