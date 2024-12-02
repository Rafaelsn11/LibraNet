using LibraNet.Data;
using LibraNet.Models.Entities;
using LibraNet.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraNet.Repository;

public class EditionRepository : BaseRepository, IEditionRepository
{
    private readonly LibraNetContext _context;
    public EditionRepository(LibraNetContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Edition> GetEditionByIdAsync(int id)
        => await _context.Editions
            .Include(b => b.Book)
            .Include(m => m.Media)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<Edition>> GetEditionsAsync()
        => await _context.Editions
            .Include(b => b.Book)
            .Include(m => m.Media)
            .ToListAsync();
}
