using Business.Models;
using Data.Entities;
using Data.Repositories;

namespace Business.Services;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetAllProjectsAsync();
    Task<Project?> GetProjectByIdAsync(string id);
    Task<bool> AddProjectAsync(AddProjectForm form);
    Task<bool> UpdateProjectAsync(string id, EditProjectForm form);
    Task<bool> DeleteProjectAsync(string id);
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
            Status = x.Status.StatusName,
            StatusId = x.StatusId
        });

        return projects;
    }

    public async Task<Project?> GetProjectByIdAsync(string id)
    {
        var entity = await _projectRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return null;

        return new Project
        {
            Id = entity.Id,
            ProjectName = entity.ProjectName,
            ClientName = entity.ClientName,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Budget = entity.Budget,
            Status = entity.Status.StatusName,
            StatusId = entity.Status.Id
        };
    }


    public async Task<bool> AddProjectAsync(AddProjectForm form)
    {
        var project = new ProjectEntity
        {
            Id = Guid.NewGuid().ToString(),
            ProjectName = form.ProjectName,
            ClientName = form.ClientName,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            Budget = form.Budget,
            StatusId = form.StatusId
        };

        return await _projectRepository.AddAsync(project);
    }


    public async Task<bool> UpdateProjectAsync(string id, EditProjectForm form)
    {
        var project = await _projectRepository.GetAsync(x => x.Id == id);

        if (project == null)
        {
            return false;
        }

        project.ProjectName = form.ProjectName;
        project.ClientName = form.ClientName;
        project.Description = form.Description;
        project.StartDate = form.StartDate;
        project.EndDate = form.EndDate;
        project.Budget = form.Budget;
        project.StatusId = form.StatusId;


        return await _projectRepository.UpdateAsync(project);
    }

    public async Task<bool> DeleteProjectAsync(string id)
    {
        var project = await _projectRepository.GetAsync(x => x.Id == id);
        if (project == null)
        {
            return false;
        }

        var result = await _projectRepository.DeleteAsync(project);
        return result;
    }


}
