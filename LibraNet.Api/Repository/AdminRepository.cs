using LibraNet.Data;
using LibraNet.Models.Entities;
using LibraNet.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraNet.Repository;

public class AdminRepository : BaseRepository, IAdminRepository
{
    private readonly LibraNetContext _context;
    public AdminRepository(LibraNetContext context) : base(context)
    {
        _context = context;
    }

    public async Task DeleteAllUserNotActive(IEnumerable<User> users)
    {
        _context.Users.RemoveRange(users);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetUsersNotActive()
        => await _context.Users
            .Where(u => u.IsActive == false)
            .ToListAsync();
}
