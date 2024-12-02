using LibraNet.Models.Entities;

namespace LibraNet.Repository.Interfaces;

public interface IEditionRepository : IBaseRepository
{
    Task<IEnumerable<Edition>> GetEditionsAsync();
    Task<Edition> GetEditionByIdAsync(int id);
}
