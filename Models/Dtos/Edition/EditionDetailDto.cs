using LibraNet.Models.Dtos.Book;
using LibraNet.Models.Dtos.Media;

namespace LibraNet.Models.Dtos.Edition;

public record EditionDetailDto(int Id, int Year, char Status, DateTime LastLoanDate, BookDto Book, MediaDto Media);
