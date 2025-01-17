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

    public Task<bool> ExistsActiveUserWithEmail(string email)
        => _context.Users.AnyAsync(x => x.Email.Equals(email) && x.IsActive);

    public async Task<bool> ExistsActiveUserWithIdentifier(Guid userIdentifier)
        => await _context.Users
            .AnyAsync(user => user.UserIdentifier.Equals(userIdentifier) && user.IsActive);

    public async Task<User?> GetByEmailAndPassword(string email, string password)
        => await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.IsActive && u.Email.Equals(email) && u.Password.Equals(password));

    public async Task<User?> GetUserByEmail(string email)
        => await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.IsActive && u.Email.Equals(email));

    public async Task<User?> GetUserByIdAsync(Guid id)
        => await _context.Users
            .Include(l => l.Loans)
                .ThenInclude(b => b.Book)
            .Where(x => x.UserIdentifier == id)
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<User>> GetUsersAsync()
        => await _context.Users
            .ToListAsync();
    
    public async Task<User> GetActiveUserByIdentifierAsync(Guid userIdentifier)
        => await _context.Users
            .AsNoTracking()
            .FirstAsync(user => user.IsActive && user.UserIdentifier == userIdentifier);
}
