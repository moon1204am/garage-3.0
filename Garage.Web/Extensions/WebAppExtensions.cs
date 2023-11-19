using Garage.Data.Data;
using Garage.Data;
namespace Garage2._0.Extensions
{

    public static class WebAppExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var db = serviceProvider.GetRequiredService<Garage2_0Context>();

                //await db.Database.EnsureDeletedAsync();
                //await db.Database.MigrateAsync();

                try
                {
                    await SeedData.InitAsync(db);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }

            }
        }
    }

}
