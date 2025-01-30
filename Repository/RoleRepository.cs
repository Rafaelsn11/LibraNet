using LibraNet.Data;
using LibraNet.Models.Entities;
using LibraNet.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraNet.Repository;

public class RoleRepository : BaseRepository, IRoleRepository
{
    private readonly LibraNetContext _context;
    public RoleRepository(LibraNetContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Role> GetRoleByNameAsync(string roleName)
        => await _context.Roles
            .Where(r => r.RoleName == roleName)
            .FirstOrDefaultAsync();
}
