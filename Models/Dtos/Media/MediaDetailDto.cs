using LibraNet.Models.Dtos.Edition;

namespace LibraNet.Models.Dtos.Media;

public record MediaDetailDto(int Id, string Description, List<EditionDto> Editions);
