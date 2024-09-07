using Microsoft.EntityFrameworkCore;
using PlatformService.Heplers;

namespace PlatformService.Data
{
    public class PrebDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                Utils.Write("--> Attempting to apply migration ...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Utils.Write($"--> Could not run migrations: {ex.Message}");
                }
            }

            if (!context.Platforms.Any())
            {
                Utils.Write("--> Seeding Data(s) ...");

                context.Platforms.AddRange(
                    new Models.Platform { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Models.Platform { Name = "SQL Server", Publisher = "Microsoft", Cost = "Free" },
                    new Models.Platform { Name = "Kuberneted", Publisher = "Cloud", Cost = "Free" }
                    );

                context.SaveChanges();
            }
            else
            {
                Utils.Write("--> We aleady have data");
            }
        }
    }
}
