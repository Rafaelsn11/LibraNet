using LibraNet.Models.Entities;

namespace LibraNet.Repository.Interfaces;

public interface IMediaRepository : IBaseRepository
{
    Task<IEnumerable<Media>> GetMediaAsync();
    Task<Media> GetMediaByIdAsync(int id);
}
