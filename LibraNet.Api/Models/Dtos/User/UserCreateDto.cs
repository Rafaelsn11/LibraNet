namespace LibraNet.Models.Dtos.User;

public record UserCreateDto
(
    string Name,
    string Email,
    string Password,
    DateOnly BirthDate
);
