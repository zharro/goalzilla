using Goalzilla.Goalzilla.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goalzilla.Goalzilla.Persistence.Mappings
{
    public class EventMapping : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.EventId);
            builder.Property(e => e.EventId)
                .ValueGeneratedNever();

            builder.HasOne(e => e.Creator)
                .WithMany()
                .HasForeignKey("CreatorId")
                .IsRequired();
            
            builder.HasMany(e => e.Rsvps)
                .WithOne()
                .HasForeignKey(nameof(Event.EventId))
                .IsRequired();

            builder.Property(e => e.Title)
                .HasMaxLength(128)
                .IsRequired();
        }
    }
}