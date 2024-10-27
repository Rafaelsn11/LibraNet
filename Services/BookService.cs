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

    public Task<IEnumerable<BookListDto>> GetBooksAsync()
        => _repository.GetBooksAsync();


}
