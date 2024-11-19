using LibraNet.Models.Dtos.Media;

namespace LibraNet.Services.Interfaces;

public interface IMediaService
{
    Task<IEnumerable<MediaListDto>> GetMediaAsync();
    Task<MediaDetailDto> GetMediaByIdAsync(int id);
}
