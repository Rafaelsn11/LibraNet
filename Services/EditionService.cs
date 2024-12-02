using LibraNet.Exceptions.ExceptionsBase;
using LibraNet.Models.Dtos.Book;
using LibraNet.Models.Dtos.Edition;
using LibraNet.Models.Dtos.Media;
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
}
