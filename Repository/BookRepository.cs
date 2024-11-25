using LibraNet.Data;
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
            .Include(e => e.Editions)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<Book>> GetBooksAsync()
        => await _context.Books
            .ToListAsync();
}
