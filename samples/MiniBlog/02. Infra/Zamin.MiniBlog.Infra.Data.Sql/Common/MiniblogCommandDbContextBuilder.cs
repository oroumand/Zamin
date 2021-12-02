using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Zamin.MiniBlog.Infra.Data.Sql.Commands.Common
{
    public class MiniblogCommandDbContextBuilder : IDesignTimeDbContextFactory<MiniblogCommandDbContext>
    {
        public MiniblogCommandDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MiniblogCommandDbContext>();
            builder.UseSqlServer("Server =.; Database=MiniBlogDb ; Integrated Security = True; MultipleActiveResultSets = true");
            return new MiniblogCommandDbContext(builder.Options);
        }
    }
}
