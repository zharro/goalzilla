using Goalzilla.Goalzilla.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goalzilla.Goalzilla.Persistence.Mappings
{
    public class RsvpMapping : IEntityTypeConfiguration<Rsvp>
    {
        public void Configure(EntityTypeBuilder<Rsvp> builder)
        {
            builder.HasKey(r => r.RsvpId);
            builder.Property(r => r.RsvpId)
                .ValueGeneratedNever();
            
            builder.HasOne(e => e.Attendee)
                .WithMany()
                .HasForeignKey("AttendeeId")
                .IsRequired();
        }
    }
}