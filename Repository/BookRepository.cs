using LibraNet.Data;
using LibraNet.Models.Dtos.Book;
using LibraNet.Models.Dtos.Edition;
using LibraNet.Models.Entities;
using LibraNet.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraNet.Repository;

public class BookRepository : BaseRepository, IBookRepository
{
    private readonly LibraNetContext _context;
    public BookRepository(LibraNetContext context) : base(context)
    {
        _context = context;
    }

    public async Task<BookDetailDto> GetBookByIdAsync(int id)
        => await _context.Books
            .Where(x => x.Id == id)
            .Select(x => new BookDetailDto(
                x.Title,
                x.Subject,
                x.Editions.Select(e => new EditionDto(e.Id, e.Year, e.Status)).ToList()
            ))
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<BookListDto>> GetBooksAsync()
        => await _context.Books
            .Select(x => new BookListDto(x.Id, x.Title))
            .ToListAsync();
}
