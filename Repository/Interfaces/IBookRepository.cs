using LibraNet.Models.Dtos.Book;
using LibraNet.Models.Entities;

namespace LibraNet.Repository.Interfaces;

public interface IBookRepository : IBaseRepository
{
    Task<IEnumerable<BookListDto>> GetBooksAsync();
    Task<Book> GetBookByIdAsync(int id);
}
