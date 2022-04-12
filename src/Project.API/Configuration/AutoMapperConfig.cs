using AutoMapper;
using Project.API.Dto;
using Project.Business.Models;

namespace Project.API.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<BookDto, Book>().ReverseMap();

            CreateMap<Book, BookDto>().ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name));
        }
    }
}
