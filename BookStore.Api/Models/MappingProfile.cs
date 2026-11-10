using AutoMapper;
using BookStore.Api.Models.Dtos;

namespace BookStore.Api.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
        }
        
    }
}
