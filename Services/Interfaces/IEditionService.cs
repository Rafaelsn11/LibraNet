using LibraNet.Models.Dtos.Edition;

namespace LibraNet.Services.Interfaces;

public interface IEditionService
{
    Task<IEnumerable<EditionListDto>> GetEditionsAsync();
    Task<EditionDetailDto> GetEditionByIdAsync(int id);
    Task<EditionDto> EditionCreateAsync(EditionCreateDto edition);
}
