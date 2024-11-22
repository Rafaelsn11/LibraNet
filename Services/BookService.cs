using AutoMapper;
using LibraNet.Exceptions.ExceptionsBase;
using LibraNet.Models.Dtos.Book;
using LibraNet.Models.Entities;
using LibraNet.Repository.Interfaces;
using LibraNet.Services.Interfaces;

namespace LibraNet.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;
    public BookService(IBookRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BookDetailDto> GetBookByIdAsync(int id)
    {
        var book = await _repository.GetBookByIdAsync(id);
        if (book == null)
            throw new NotFoundException("Book not found");

        return book;
    }

    public async Task<IEnumerable<BookListDto>> GetBooksAsync()
        => await _repository.GetBooksAsync();

    public async Task<BookDto> BookCreateAsync(BookCreateDto book)
    {
        var errors = ValidateBookCreate(book);

        if (errors.Count > 0)
            throw new ErrorOrValidationException(errors);

        var entity = new Book
        {
            Title = book.Title,
            Subject = book.Subject
        };

        _repository.Add(entity);
        await _repository.SaveChangesAsync();

        return new BookDto(entity.Id, entity.Title, entity.Subject);
    }
    private List<string> ValidateBookCreate(BookCreateDto book)
    {
        var messageErrors = new List<string>();

        if (string.IsNullOrWhiteSpace(book.Title))
            messageErrors.Add("Title invalid");
        if (string.IsNullOrWhiteSpace(book.Subject))
            messageErrors.Add("Subject invalid");

        return messageErrors;
    }

    public async Task<BookUpdateViewDto> BookUpdateAsync(int id, BookUpdateDto book)
    {
        var bookDB = await _repository.GetBookByIdAsync(id);
        if (bookDB == null)
            throw new NotFoundException("Book not found");

        var bookUpdate = _mapper.Map(book, bookDB);

        _repository.Update(bookUpdate);
        await _repository.SaveChangesAsync();

        return new BookUpdateViewDto(bookUpdate.Title, bookUpdate.Subject);
    }

    public async Task BookDeleteAsync(int id)
    {
        var bookDB = await _repository.GetBookByIdAsync(id);

        if (bookDB == null)
            throw new NotFoundException("Book not found");

        _repository.Delete(bookDB);
        await _repository.SaveChangesAsync();
    }
}
