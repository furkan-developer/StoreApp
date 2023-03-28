using AutoMapper;
using Entities;
using Entities.Dtos.UpdateDtos;

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
