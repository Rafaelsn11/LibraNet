using LibraNet.Models.Dtos.Edition;

namespace LibraNet.Models.Dtos.Media;

public record MediaDetailDto(string Description, List<EditionDto> Editions);
