using LibraNet.Data;
using LibraNet.Models.Dtos.Book;
using LibraNet.Models.Dtos.Edition;
using LibraNet.Models.Dtos.Media;
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

    public async Task<MediaDetailDto> GetMediaByIdAsync(int id)
        => await _context.MediaFormats
            .Where(x => x.Id == id)
            .Select(x => new MediaDetailDto(
                x.Description,
                x.Editions.Select(e => new EditionDto(e.Id, e.Year, e.Status)).ToList()
            ))
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<MediaListDto>> GetMediaAsync()
        => await _context.MediaFormats
            .Select(x => new MediaListDto(x.Id, x.Description))
            .ToListAsync();
}
