using LibraNet.Models.Dtos.Media;

namespace LibraNet.Services.Interfaces;

public interface IMediaService
{
    Task<IEnumerable<MediaListDto>> GetMediaAsync();
    Task<MediaDetailDto> GetMediaByIdAsync(int id);
    Task<MediaDto> MediaCreateAsync(MediaCreateDto media);
    Task<MediaUpdateViewDto> MediaUpdateAsync(int id, MediaUpdateDto media);
    Task MediaDeleteAsync(int id);
}
