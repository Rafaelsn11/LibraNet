using LibraNet.Models.Dtos.Book;
using LibraNet.Models.Entities;

namespace LibraNet.Services.Interfaces;

public interface IBookService
{
    Task<BookDetailDto> GetBookByIdAsync(int id);
    Task<IEnumerable<BookListDto>> GetBooksAsync();
    Task<BookDto> BookCreateAsync(BookCreateDto book);
    Task<BookUpdateViewDto> BookUpdateAsync(int id, BookUpdateDto book);
    Task BookDeleteAsync(int id);
}
