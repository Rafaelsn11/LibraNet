using LibraNet.Models.Entities;

namespace LibraNet.Repository.Interfaces;

public interface IAdminRepository
{
    Task<IEnumerable<User>> GetUsersNotActive();
    Task DeleteAllUserNotActive(IEnumerable<User> users);
}
