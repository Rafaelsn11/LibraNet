using LibraNet.Models.Dtos.Book;
using LibraNet.Models.Entities;
using LibraNet.Repository.Interfaces;
using LibraNet.Services.Interfaces;

namespace LibraNet.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
        var book = await _repository.GetBookByIdAsync(id);
        if (book == null)
            throw new Exception("Book not found");

        return book;
    }

    public async Task<IEnumerable<BookListDto>> GetBooksAsync()
        => await _repository.GetBooksAsync();

    public async Task<BookDto> BookCreate(BookCreateDto book)
    {
        if (book == null || string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Subject))
            throw new ArgumentNullException("Invalid data - title and subject are required");

        var entity = new Book
        {
            Title = book.Title,
            Subject = book.Subject
        };

        _repository.Add(entity);
        await _repository.SaveChangesAsync();

        return new BookDto(entity.Id, entity.Title, entity.Subject);
    }
}
