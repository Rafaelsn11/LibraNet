using LibraNet.Exceptions.ExceptionsBase;
using LibraNet.Models.Dtos.Book;
using LibraNet.Models.Dtos.Edition;
using LibraNet.Models.Dtos.Media;
using LibraNet.Models.Entities;
using LibraNet.Repository.Interfaces;
using LibraNet.Services.Interfaces;
using LibraNet.Services.LoggedUser;

namespace LibraNet.Services;

public class EditionService : IEditionService
{
    private readonly IEditionRepository _repository;
    private readonly ILoggedUser _loggedUser;
    public EditionService(IEditionRepository repository, ILoggedUser loggedUser)
    {
        _repository = repository;
        _loggedUser = loggedUser;
    }
    public async Task<IEnumerable<EditionListDto>> GetEditionsAsync()
    {
        var editions = await _repository.GetEditionsAsync();

        var editionsList = editions.Select(x => new EditionListDto(x.Id, x.Year, x.Status, (DateTime)x.LastLoanDate));

        return editionsList;
    }

    public async Task<EditionDetailDto> GetEditionByIdAsync(int id)
    {
        var edition = await _repository.GetEditionByIdAsync(id);
        if (edition == null)
            throw new NotFoundException("Edition not found");

        var editionDetail = new EditionDetailDto(
            edition.Id,
            edition.Year,
            edition.Status,
            (DateTime)edition.LastLoanDate,
            new BookDto(edition.Book.Id, edition.Book.Title, edition.Book.Subject),
            new MediaDto(edition.Media.Id, edition.Media.Description)
        );

        return editionDetail;
    }

    public async Task<EditionDto> EditionCreateAsync(EditionCreateDto edition)
    {
        await ValidateEdition(edition);

        var entity = new Edition
        {
            Year = edition.Year,
            Status = 'D',
            LastLoanDate = DateTime.MinValue,
            BookId = edition.BookId,
            MediaId = edition.MediaId
        };

        _repository.Add(entity);

        await _repository.SaveChangesAsync();

        return new EditionDto(entity.Id, entity.Year, entity.Status);
    }

    private async Task ValidateEdition(EditionCreateDto edition)
    {
        var errors = new List<string>();

        var properties = typeof(EditionCreateDto).GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(edition);

            if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
            {
                errors.Add($"{property.Name} is invalid");
            }
            else if (value is int number && number <= 0)
            {
                errors.Add($"{property.Name} is invalid (number must be greater than 0)");
            }
        }

        if (errors.Count > 0)
            throw new ErrorOrValidationException(errors);

        var editionsDb = await _repository.GetEditionsAsync();

        if (editionsDb.Any(e => e.Year == edition.Year && 
            e.BookId == edition.BookId && e.MediaId == edition.MediaId))
                throw new ResourceConflictException("Edition already registered");
    }

    public async Task EditionLoanAsync(int id, DateTime date)
    {
        var user = await _loggedUser.User();
        var userId = user.Id;

        ValidateUserLoanLimit(user, date);

        var edition = await _repository.GetEditionByIdAsync(id);

        if (edition == null)
            throw new NotFoundException("Edition not found");
        
        if(edition.Status == 'L')
            throw new ResourceConflictException("Edition is already loaned");

        edition.Status = 'L';
        edition.LastLoanDate = date;
        edition.UserId = userId;

        _repository.Update(edition);
        await _repository.SaveChangesAsync();
    }

    private void ValidateUserLoanLimit(User user, DateTime loanDate)
    {
        int maxLoans = loanDate.Month == user.BirthDate.Month ? 6 : 3;
        
        if (user.Loans.Count() >= maxLoans)
            throw new ResourceConflictException("Limit on loans obtained");
    }

    public async Task EditionReturnAsync(int id)
    {
        var user = await _loggedUser.User();
        var userId = user.Id;

        var edition = await _repository.GetEditionByIdAsync(id);

        if (edition == null)
            throw new NotFoundException("Edition not found");
        
        if(edition.Status == 'D')
            throw new ResourceConflictException("Edition has already been returned");
        
        edition.Status = 'D';
        edition.UserId = null;

        _repository.Update(edition);
        await _repository.SaveChangesAsync();
    }
}
