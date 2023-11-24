using Microsoft.EntityFrameworkCore;

namespace Zamin.Utilities.OpenTelemetryRegistration.Sample.Models
{
    public static class SeedData
    {

        public static void EnsurePopulate(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<PersonContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.People.Any()) 
            {
                context.People.AddRange(
                [
                    new Person { FirstName = "Mo", LastName = "Abbasi" },
                    new Person { FirstName = "AmirHossein", LastName = "Tayyar" },
                ]);
                context.SaveChanges();
            }
        }
    }
}
