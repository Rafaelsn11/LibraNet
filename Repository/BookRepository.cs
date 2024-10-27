using LibraNet.Data;
using LibraNet.Models.Dtos.Book;
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

    public async Task<Book> GetBookByIdAsync(int id)
        => await _context.Books
            .Include(x => x.Editions)
            .ThenInclude(e => e.Media)
            .Where(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<IEnumerable<BookListDto>> GetBooksAsync()
        => await _context.Books
            .Select(x => new BookListDto(x.Id, x.Title))
            .ToListAsync();
}
