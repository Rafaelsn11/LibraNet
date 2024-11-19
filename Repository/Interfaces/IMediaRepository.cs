using LibraNet.Models.Dtos.Media;

namespace LibraNet.Repository.Interfaces;

public interface IMediaRepository : IBaseRepository
{
    Task<IEnumerable<MediaListDto>> GetMediaAsync();
    Task<MediaDetailDto> GetMediaByIdAsync(int id);
}
