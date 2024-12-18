using LibraNet.Data;
using LibraNet.Models.Entities;
using LibraNet.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraNet.Repository;

public class UserRepository : BaseRepository, IUserRepository
{
    private readonly LibraNetContext _context;
    public UserRepository(LibraNetContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> GetUserByIdAsync(Guid id)
        => await _context.Users
            .Include(l => l.Loans)
                .ThenInclude(b => b.Book)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<User>> GetUsersAsync()
        => await _context.Users
            .ToListAsync();
}
