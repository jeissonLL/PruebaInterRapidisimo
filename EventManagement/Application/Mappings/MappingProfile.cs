using Application.DTO;
using Domain.Entities;
using AutoMapper;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginDTO, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<EventDTO, Event>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}
