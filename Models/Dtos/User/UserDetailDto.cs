using LibraNet.Models.Dtos.Edition;

namespace LibraNet.Models.Dtos.User;

public record UserDetailDto(string Name, DateTime BirthDate, bool IsActive, List<EditionDto> Loans);
