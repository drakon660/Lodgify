using ApiApplication.Core.Dtos;
using ApiApplication.Core.Entities;
using AutoMapper;

namespace ApiApplication.Core.Mappings;

public class SeatsProfile : Profile
{
    public SeatsProfile()
    {
        CreateMap<Seat, SeatDto>()
            .ForMember(x=>x.RowNumber, x=>x.MapFrom(y=>y.Position.RowNumber))
            .ForMember(x=>x.SeatNumber, x=>x.MapFrom(y=>y.Position.SeatNumber));
    }
}