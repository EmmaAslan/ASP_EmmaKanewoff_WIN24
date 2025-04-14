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

    public static async Task SeedProjects(IServiceProvider services)
    {
        var db = services.GetRequiredService<DataContext>();

        if (!db.Projects.Any())
        {
            var projects = new List<ProjectEntity>
            {
                new() { 
                    ProjectName = "Website Redesign", 
                    ClientName = "GitLab Inc.", 
                    Description = "It is necessary to develop a website redesign in a corporate style.",
                    StartDate = new DateTime(2024, 12, 1),
                    EndDate = new DateTime(2025, 3, 31),
                    Budget = 68500.00m, 
                    StatusId = 1
                },
                new() {
                    ProjectName = "Landing Page",
                    ClientName = "Bitbucket, Inc.",
                    Description = "It is necessary to create a landing together with the development of design.",
                    StartDate = new DateTime(2025, 2, 15),
                    EndDate = null,
                    Budget = 68500.00m,
                    StatusId = 2 
                },

            };

            await db.Projects.AddRangeAsync(projects);
            await db.SaveChangesAsync();
        }


    }


}
