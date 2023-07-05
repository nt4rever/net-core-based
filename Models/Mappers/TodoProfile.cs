using AutoMapper;
using net_core_based.Entities;

namespace net_core_based.Models.Mappers
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<TodoCreateDto, Todo>()
                .ForMember(
                    destination => destination.Id,
                    option => option.MapFrom(src => Guid.NewGuid())
                )
                .ForMember(
                    destination => destination.IsCompleted,
                    option => option.MapFrom(src => false)
                )
                .ForMember(
                    destination => destination.CreatedAt,
                    option => option.MapFrom(src => DateTime.UtcNow)
                )
                .ForMember(
                    destination => destination.UpdatedAt,
                    option => option.MapFrom(src => DateTime.UtcNow)
                );
        }
    }
}
