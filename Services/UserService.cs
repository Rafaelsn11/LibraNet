using LibraNet.Exceptions.ExceptionsBase;
using LibraNet.Models.Dtos.Edition;
using LibraNet.Models.Dtos.Token;
using LibraNet.Models.Dtos.User;
using LibraNet.Models.Entities;
using LibraNet.Repository.Interfaces;
using LibraNet.Security.Tokens.Interfaces;
using LibraNet.Services.Cryptography;
using LibraNet.Services.Interfaces;
using LibraNet.Services.LoggedUser;

namespace LibraNet.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _acessTokenGenerator;
    private readonly ILoggedUser _loggedUser;
    
    public UserService(
        IUserRepository repository, 
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator acessTokenGenerator,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _passwordEncripter = passwordEncripter;
        _acessTokenGenerator = acessTokenGenerator;
        _loggedUser = loggedUser;
    }

    public async Task<UserProfileDto> GetProfileAsync()
    {
        var user = await _loggedUser.User();

        return new UserProfileDto(
            user.Name,
            user.Email
        );
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
        await ValidateUserCreate(user);

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

    private async Task ValidateUserCreate(UserCreateDto user)
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

        var emailExists = await _repository.ExistsActiveUserWithEmail(user.Email);
        if (emailExists)
            errorMessages.Add("Email already Registred");

        if(user.Password.Length < 6)
        {
            errorMessages.Add("Password must be at least 6 characters");
        }
        
        if (errorMessages.Count > 0)
            throw new ErrorOrValidationException(errorMessages);
    }

    public async Task UserUpdateAsync(UserUpdateDto userUpdate)
    {
        var loggedUser = await _loggedUser.User();

        await ValidateUserUpdate(userUpdate, loggedUser.Email);

        var user = await _repository.GetUserByIdAsync(loggedUser.Id);

        user.Name = userUpdate.Name;
        user.Email = userUpdate.Email;

        _repository.Update(user);
        await _repository.SaveChangesAsync();
    }

    private async Task ValidateUserUpdate(UserUpdateDto user, string currentEmail)
    {
        var errorMessages = new List<string>();

        if (string.IsNullOrWhiteSpace(user.Name))
            errorMessages.Add("Name is invalid");
        
        if(string.IsNullOrWhiteSpace(user.Email))
            errorMessages.Add("Email is invalid");


        if (!currentEmail.Equals(user.Email))
        {
            var userExist = await _repository.ExistsActiveUserWithEmail(user.Email);
            if(userExist)
                errorMessages.Add("Email already Registred");
        }

        if (errorMessages.Count > 0)
            throw new ErrorOrValidationException(errorMessages);
    }

    public async Task UserChangePasswordAsync(UserChangePassword userChangePassword)
    {
        var loggedUser = await _loggedUser.User();

        ValidateChangePassword(userChangePassword, loggedUser);

        var user = await _repository.GetUserByIdAsync(loggedUser.Id);

        user.Password = _passwordEncripter.Encrypt(userChangePassword.NewPassword, user.Salt);

        _repository.Update(user);
        await _repository.SaveChangesAsync();
    }

    private void ValidateChangePassword(UserChangePassword userChangePassword, User user)
    {
        var errorMessages = new List<string>();

        if (string.IsNullOrWhiteSpace(userChangePassword.Password))
            errorMessages.Add("Name is invalid");
        
        if(string.IsNullOrWhiteSpace(userChangePassword.NewPassword))
            errorMessages.Add("Email is invalid");

        if(userChangePassword.NewPassword.Length < 6)
            errorMessages.Add("Password must be at least 6 characters");
        
        var currentPasswordEncripter = _passwordEncripter.Encrypt(userChangePassword.Password, user.Salt);

        if (!currentPasswordEncripter.Equals(user.Password))
            errorMessages.Add("Password different current password");
        
        if (errorMessages.Count > 0)
            throw new ErrorOrValidationException(errorMessages);
    }
}
