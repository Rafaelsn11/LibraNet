using LibraNet.Exceptions.ExceptionsBase;
using LibraNet.Models.Dtos.Book;
using LibraNet.Models.Dtos.Edition;
using LibraNet.Models.Dtos.Media;
using LibraNet.Models.Entities;
using LibraNet.Repository.Interfaces;
using LibraNet.Services.Interfaces;

namespace LibraNet.Services;

public class EditionService : IEditionService
{
    private readonly IEditionRepository _repository;
    public EditionService(IEditionRepository repository)
    {
        _repository = repository;
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
        var errors = ValidateEdition(edition);

        if (errors.Count > 0)
            throw new ErrorOrValidationException(errors);

        var entity = new Edition
        {
            Year = edition.Year,
            Status = edition.Status,
            LastLoanDate = null,
            BookId = edition.BookId,
            MediaId = edition.MediaId,
            UserId = edition.UserId
        };

        _repository.Add(entity);

        await _repository.SaveChangesAsync();

        return new EditionDto(entity.Id, entity.Year, entity.Status);
    }

    private List<string> ValidateEdition(EditionCreateDto edition)
    {
        var errorMessages = new List<string>();

        var properties = typeof(EditionCreateDto).GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(edition);

            if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
            {
                errorMessages.Add($"{property.Name} is invalid");
            }
            else if (value is int number && number <= 0)
            {
                errorMessages.Add($"{property.Name} is invalid (number must be greater than 0)");
            }
        }

        return errorMessages;
    }
}
