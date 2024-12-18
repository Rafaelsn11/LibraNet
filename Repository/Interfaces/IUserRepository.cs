using LibraNet.Models.Entities;

namespace LibraNet.Repository.Interfaces;

public interface IUserRepository : IBaseRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(Guid id);
}
