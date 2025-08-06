using LibraNet.Models.Dtos.Edition;

namespace LibraNet.Models.Dtos.Book;

public record BookDetailDto(string Title, string Subject, List<EditionDto> Editions);

