using LibraNet.Models.Dtos.Book;
using LibraNet.Models.Entities;

namespace LibraNet.Services.Interfaces;

public interface IBookService
{
    Task<Book> GetBookByIdAsync(int id);
    Task<IEnumerable<BookListDto>> GetBooksAsync();
}
