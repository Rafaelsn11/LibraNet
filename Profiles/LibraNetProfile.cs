using AutoMapper;
using LibraNet.Models.Dtos.Book;
using LibraNet.Models.Entities;

namespace LibraNet.Profiles;

public class LibraNetProfile : Profile
{
    public LibraNetProfile()
    {
        CreateMap<BookUpdateDto, Book>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
            srcMember != null &&
            (srcMember is not string || !string.IsNullOrEmpty((string)srcMember)) &&
            (srcMember is not int || (int)srcMember != 0)));
    }
}
