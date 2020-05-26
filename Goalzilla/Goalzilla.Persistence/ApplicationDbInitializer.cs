using System;
using Microsoft.EntityFrameworkCore;

namespace Goalzilla.Goalzilla.Persistence
{
    /// <summary>
    /// Makes some operations before the app starts.
    /// </summary>
    public class ApplicationDbInitializer
    {
        /// <summary>
        /// Apply all new migrations.
        /// </summary>
        public static void ApplyMigrations(GoalzillaDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            dbContext.Database.Migrate();
        }
    }
}