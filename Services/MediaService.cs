using LibraNet.Exceptions.ExceptionsBase;
using LibraNet.Models.Dtos.Media;
using LibraNet.Repository.Interfaces;
using LibraNet.Services.Interfaces;

namespace LibraNet.Services;

public class MediaService : IMediaService
{
    private readonly IMediaRepository _repository;
    public MediaService(IMediaRepository repository)
    {
        _repository = repository;
    }

    public async Task<MediaDetailDto> GetMediaByIdAsync(int id)
    {
        var media = await _repository.GetMediaByIdAsync(id);

        if (media == null)
            throw new NotFoundException("Media not found exception");

        return media;
    }

    public async Task<IEnumerable<MediaListDto>> GetMediaAsync()
        => await _repository.GetMediaAsync();
}
