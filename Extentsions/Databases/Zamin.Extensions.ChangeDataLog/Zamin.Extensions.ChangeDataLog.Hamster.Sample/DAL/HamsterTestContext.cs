using Microsoft.EntityFrameworkCore;

namespace Zamin.Extensions.ChangeDataLog.Hamster.Sample.DAL
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
    }



    public class HamsterTestContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public HamsterTestContext(DbContextOptions<HamsterTestContext> options) : base(options)
        {
        }
    }
}
