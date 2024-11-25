using LibraNet.Data;
using LibraNet.Models.Entities;
using LibraNet.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraNet.Repository;

public class MediaRepository : BaseRepository, IMediaRepository
{
    private readonly LibraNetContext _context;
    public MediaRepository(LibraNetContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Media> GetMediaByIdAsync(int id)
        => await _context.MediaFormats
            .Include(e => e.Editions)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<Media>> GetMediaAsync()
        => await _context.MediaFormats
            .ToListAsync();
}
