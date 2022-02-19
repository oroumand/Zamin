using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MiniBlog.Infra.Data.Sql.Commands.Common
{
    public class MiniblogCommandDbContextDesignTimeFactory : IDesignTimeDbContextFactory<MiniblogCommandDbContext>
    {
        public MiniblogCommandDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MiniblogCommandDbContext>();

            builder.UseSqlServer("Server =.; Database=MiniBlogDb ;User Id =sa;Password=1qaz!QAZ; MultipleActiveResultSets=true");

            return new MiniblogCommandDbContext(builder.Options);
        }
    }
}
