using Data.Contexts;
using Data.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Seeds;

public static class AppDbSeeder
{
    public static async Task SeedStatuses(IServiceProvider services)
    {
        var db = services.GetRequiredService<DataContext>();

        if (!db.Statuses.Any())
        {
            var statuses = new List<StatusEntity>
            {
                new() { StatusName = "Not Started" },
                new() { StatusName = "Started" },
                new() { StatusName = "Completed" }
            };

            await db.Statuses.AddRangeAsync(statuses);
            await db.SaveChangesAsync();
        }

       
    }


}
