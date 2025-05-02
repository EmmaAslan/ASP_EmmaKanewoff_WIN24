using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public interface IStatusService
{
    Task<IEnumerable<string>> GetAllStatusNamesAsync();
    Task<int?> GetStatusIdByNameAsync(string name);
}

public class StatusService(DataContext context) : IStatusService
{
    private readonly DataContext _context = context;

    public async Task<int?> GetStatusIdByNameAsync(string name)
    {
        var status = await _context.Statuses.FirstOrDefaultAsync(s => s.StatusName == name);
        return status?.Id;
    }

    public async Task<IEnumerable<string>> GetAllStatusNamesAsync()
    {
        return await _context.Statuses.Select(s => s.StatusName).ToListAsync();
    }
}
