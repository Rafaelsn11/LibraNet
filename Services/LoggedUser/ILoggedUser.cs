using LibraNet.Models.Entities;

namespace LibraNet.Services.LoggedUser;

public interface ILoggedUser
{
    Task<User> User();
}
