using LibraNet.Models.Entities;

namespace LibraNet.Repository.Interfaces;

public interface IRoleRepository : IBaseRepository
{
    Task<Role> GetRoleByNameAsync(string roleName);
}
