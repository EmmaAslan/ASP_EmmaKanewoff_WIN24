using Business.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetAllProjectsAsync();
}

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        var list = await _projectRepository.GetAllAsync();



        var projects = list.Select(x => new Project
        {
            Id = x.Id,
            ProjectName = x.ProjectName,
            ClientName = x.ClientName,
            Description = x.Description,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            Budget = x.Budget,
            Status = x.Status.StatusName
        });

        return projects;
    }
}
