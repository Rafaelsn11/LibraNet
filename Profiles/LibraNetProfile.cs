using AutoMapper;
using LibraNet.Models.Dtos.Book;
using LibraNet.Models.Entities;

namespace LibraNet.Profiles;

public class LibraNetProfile : Profile
{
    public LibraNetProfile()
    {
        CreateMap<BookUpdateDto, Book>();
    }
}
