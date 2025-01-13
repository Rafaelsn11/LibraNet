using LibraNet.Exceptions.ExceptionsBase;
using LibraNet.Models.Dtos.Edition;
using LibraNet.Models.Dtos.Token;
using LibraNet.Models.Dtos.User;
using LibraNet.Models.Entities;
using LibraNet.Repository.Interfaces;
using LibraNet.Security.Tokens.Interfaces;
using LibraNet.Services.Cryptography;
using LibraNet.Services.Interfaces;

namespace LibraNet.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _acessTokenGenerator;
    
    public UserService(
        IUserRepository repository, 
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator acessTokenGenerator)
    {
        _repository = repository;
        _passwordEncripter = passwordEncripter;
        _acessTokenGenerator = acessTokenGenerator;
    }
    public async Task<UserDetailDto> GetUserByIdAsync(Guid id)
    {
        var user = await _repository.GetUserByIdAsync(id);

        if (user == null)
            throw new NotFoundException("User not found");

        var userDetail = new UserDetailDto(
            user.Name, user.BirthDate, user.IsActive,
            user.Loans.Select(x => new EditionDto(x.Id, x.Year, x.Status)).ToList()
        );

        return userDetail;
    }

    public async Task<IEnumerable<UserListDto>> GetUsersAsync()
    {
        var user = await _repository.GetUsersAsync();

        var userLists = user.Select(x => new UserListDto(x.Name, x.IsActive));

        return userLists;
    }

    public async Task<UserDto> UserCreateAsync(UserCreateDto user)
    {
        var errors = ValidateUserCreate(user);

        if (errors.Count > 0)
            throw new ErrorOrValidationException(errors);

        var saltUser = _passwordEncripter.GenerateSalt();
        var encriptedPassword = _passwordEncripter.Encrypt(user.Password, saltUser!);

        var entity = new User
        {
            Name = user.Name,
            Email = user.Email,
            Password = encriptedPassword,
            Salt = saltUser,
            BirthDate = user.BirthDate,
            IsActive = true
        };

        _repository.Add(entity);

        await _repository.SaveChangesAsync();

        var tokens = new TokenDto(_acessTokenGenerator.Generate(entity.UserIdentifier));
        return new UserDto
        (
            entity.Name,
            tokens
        );
    }

    private List<string> ValidateUserCreate(UserCreateDto user)
    {
        var errorMessages = new List<string>();

        var properties = typeof(UserCreateDto).GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(user);

            if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
            {
                errorMessages.Add($"{property.Name} is invalid");
            }
        }

        return errorMessages;
    }
}
