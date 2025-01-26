using LibraNet.Models.Entities;

namespace LibraNet.Repository.Interfaces;

public interface IUserRepository : IBaseRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User> GetUserByIdAsync(int id);
    Task<User?> GetUserByEmail(string email);
    Task<bool> ExistsActiveUserWithEmail(string email);
    Task<bool> ExistsActiveUserWithIdentifier(Guid userIdentifier);
    Task<User?> GetByEmailAndPassword(string email, string password);
    Task<User> GetActiveUserByIdentifierAsync(Guid userIdentifier);
}
