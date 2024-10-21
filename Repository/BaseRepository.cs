using LibraNet.Data;
using LibraNet.Repository.Interfaces;

namespace LibraNet.Repository;

public class BaseRepository : IBaseRepository
{
    private readonly LibraNetContext _context;
    public BaseRepository(LibraNetContext context)
    {
        _context = context;
    }
    public void Add<T>(T entity) where T : class
        => _context.Add(entity);

    public void Delete<T>(T entity) where T : class
        => _context.Remove(entity);

    public async Task<bool> SaveChangesAsync()
        => await _context.SaveChangesAsync() > 1;

    public void Update<T>(T entity) where T : class
        => _context.Update(entity);
}
