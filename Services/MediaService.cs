using LibraNet.Exceptions.ExceptionsBase;
using LibraNet.Models.Dtos.Media;
using LibraNet.Models.Entities;
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

    public async Task<MediaDto> MediaCreateAsync(MediaCreateDto media)
    {
        var errors = ValidateMediaCreate(media);

        if (errors.Count > 0)
            throw new ErrorOrValidationException(errors);

        var entity = new Media
        {
            Description = media.Description
        };

        _repository.Add(entity);
        await _repository.SaveChangesAsync();

        return new MediaDto(entity.Id, entity.Description);
    }

    private List<string> ValidateMediaCreate(MediaCreateDto media)
    {
        var errorMessages = new List<string>();

        if (string.IsNullOrWhiteSpace(media.Description))
            errorMessages.Add("Invalid description");

        return errorMessages;
    }
}
