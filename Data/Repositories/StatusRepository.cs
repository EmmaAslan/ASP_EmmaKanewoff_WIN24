using System.Diagnostics;
using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface IStatusRepository
{
    Task<IEnumerable<StatusEntity>> GetAllAsync();
}

public class StatusRepository(DataContext context) : IStatusRepository
{
    private readonly DataContext _context = context;

    public async Task<IEnumerable<StatusEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Statuses.ToListAsync();
            return entities;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return [];
        }
    }
}
