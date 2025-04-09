using System.Diagnostics;
using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface IProjectRepository
{
    Task<bool> AddAsync(ProjectEntity entity);
    Task<bool> DeleteAsync(ProjectEntity ent);
    Task<IEnumerable<ProjectEntity>> GetAllAsync();
    Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> expression);
    Task<bool> UpdateAsync(ProjectEntity entity);
}

public class ProjectRepository(DataContext context) : IProjectRepository
{
    private readonly DataContext _context = context;

    public async Task<bool> AddAsync(ProjectEntity entity)
    {
        if (entity == null)
        {
            return false;
        }
        try
        {
            _context.Projects.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Projects
                .Include(x => x.Status)
                .ToListAsync();
            return entities;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return [];
        }
    }

    public async Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        try
        {
            var entity = await _context.Projects
                .Include(x => x.Status)
                .FirstOrDefaultAsync(expression);
            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<bool> UpdateAsync(ProjectEntity entity)
    {
        if (entity == null)
        {
            return false;
        }
        try
        {
            _context.Projects.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(ProjectEntity ent)
    {
        try
        {
            var entity = await _context.Projects.FindAsync(ent);
            if (entity == null)
            {
                return false;
            }

            _context.Projects.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

}
