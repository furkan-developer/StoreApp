using AutoMapper;
using Entities;
using Entities.Dtos.Book;

namespace WebAPI.Utilities.AutoMapper
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookDtoForUpdate,Book>().ReverseMap();
        }
    }
}
