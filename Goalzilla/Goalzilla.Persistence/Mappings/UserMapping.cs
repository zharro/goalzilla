using Goalzilla.Goalzilla.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goalzilla.Goalzilla.Persistence.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserId)
                .ValueGeneratedNever();

            builder.Property(u => u.FirstName)
                .HasMaxLength(128)
                .IsRequired();
            builder.Property(u => u.LastName)
                .HasMaxLength(128)
                .IsRequired();

            builder.OwnsOne(u => u.Email,o =>
            {
                o.Property(c => c.Value)
                    .HasColumnName(nameof(User.Email))
                    .HasMaxLength(128)
                    .IsRequired();
                o.HasIndex(c => c.Value)
                    .IsUnique();
            });
        }
    }
}