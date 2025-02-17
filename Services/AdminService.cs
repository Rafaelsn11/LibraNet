using LibraNet.Exceptions.ExceptionsBase;
using LibraNet.Repository.Interfaces;
using LibraNet.Services.Interfaces;

namespace LibraNet.Services;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _repository;
    public AdminService(IAdminRepository repository)
    {
        _repository = repository;
    }
    public async Task DeleteAllUserNotActive()
    {
        var userNotActive = await _repository.GetUsersNotActive();

        if (userNotActive.Count() == 0)
            throw new NotFoundException("There aren't inactive users");
        
        await _repository.DeleteAllUserNotActive(userNotActive);
    }
}