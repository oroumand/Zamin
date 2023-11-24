using Microsoft.EntityFrameworkCore;

namespace Zamin.Utilities.OpenTelemetryRegistration.Sample.Models
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

    }
}
