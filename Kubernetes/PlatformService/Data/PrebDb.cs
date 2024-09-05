using PlatformService.Heplers;

namespace PlatformService.Data
{
    public class PrebDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
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
