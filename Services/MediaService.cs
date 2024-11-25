using AutoMapper;
using LibraNet.Exceptions.ExceptionsBase;
using LibraNet.Models.Dtos.Edition;
using LibraNet.Models.Dtos.Media;
using LibraNet.Models.Entities;
using LibraNet.Repository.Interfaces;
using LibraNet.Services.Interfaces;

namespace LibraNet.Services;

public class MediaService : IMediaService
{
    private readonly IMediaRepository _repository;
    private readonly IMapper _mapper;
    public MediaService(IMediaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<MediaDetailDto> GetMediaByIdAsync(int id)
    {
        var media = await _repository.GetMediaByIdAsync(id);

        if (media == null)
            throw new NotFoundException("Media not found");

        var mediaDetail = new MediaDetailDto
        (media.Description,
        media.Editions.Select(x => new EditionDto(x.Id, x.Year, x.Status)).ToList());

        return mediaDetail;
    }

    public async Task<IEnumerable<MediaListDto>> GetMediaAsync()
    {
        var media = await _repository.GetMediaAsync();

        var mediaLists = media.Select(x => new MediaListDto(x.Id, x.Description));

        return mediaLists;
    }

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

    public async Task<MediaUpdateViewDto> MediaUpdateAsync(int id, MediaUpdateDto media)
    {
        var mediaDB = await _repository.GetMediaByIdAsync(id);

        if (mediaDB == null)
            throw new NotFoundException("Media not found");

        var mediaUpdate = _mapper.Map(media, mediaDB);

        _repository.Update(mediaUpdate);
        await _repository.SaveChangesAsync();

        return new MediaUpdateViewDto(mediaUpdate.Description);
    }
}
