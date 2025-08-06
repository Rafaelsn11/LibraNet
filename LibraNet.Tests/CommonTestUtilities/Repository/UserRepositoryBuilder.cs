using LibraNet.Models.Entities;
using LibraNet.Repository.Interfaces;
using Moq;

namespace LibraNet.Tests.CommonTestUtilities.Repository;
public class UserRepositoryBuilder
{
    private readonly Mock<IUserRepository> _repository;

    public UserRepositoryBuilder() => _repository = new Mock<IUserRepository>();
    
    public void GetByEmailAndPassword(string email, string password, User? result = null)
    {
        _repository.Setup(repository => repository.GetByEmailAndPassword(email, password)).ReturnsAsync(result);
    }

    public void GetUserByEmail(string email, User? result = null)
    {
        _repository.Setup(r => r.GetUserByEmail(email)).ReturnsAsync(result);
    }

    public IUserRepository Build() => _repository.Object;
}

