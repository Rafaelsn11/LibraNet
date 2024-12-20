namespace LibraNet.Models.Dtos.User;

public record UserCreateDto
(
    string Name,
    string Email,
    DateTime BirthDate
);
