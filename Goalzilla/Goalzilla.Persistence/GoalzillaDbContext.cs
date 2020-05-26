using System.Reflection;
using Goalzilla.Goalzilla.Domain;
using Microsoft.EntityFrameworkCore;

namespace Goalzilla.Goalzilla.Persistence
{
    public class GoalzillaDbContext : DbContext
    {
        protected GoalzillaDbContext()
        { }

        public GoalzillaDbContext(DbContextOptions options) : base(options)
        { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Rsvp> Rsvps { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}