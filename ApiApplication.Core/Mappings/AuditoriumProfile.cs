using ApiApplication.Core.Dtos;
using ApiApplication.Core.Entities;
using AutoMapper;

namespace ApiApplication.Core.Mappings;

public class AuditoriumProfile : Profile
{
    public AuditoriumProfile()
    {
        CreateMap<Auditorium, AuditoriumDto>()
            .ForMember(x=>x.AuditoriumId, x=>x.MapFrom(y=>y.Id));
    }
}