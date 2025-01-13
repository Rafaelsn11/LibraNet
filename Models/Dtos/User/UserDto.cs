using LibraNet.Models.Dtos.Token;

namespace LibraNet.Models.Dtos.User;

public record UserDto
(
    string Name,
    TokenDto Tokens
);
