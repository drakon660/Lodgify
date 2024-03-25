using ApiApplication.Core.Dtos;
using ApiApplication.Core.Entities;
using AutoMapper;

namespace ApiApplication.Core.Mappings;

public class TicketProfile : Profile
{
    public TicketProfile()
    {
        CreateMap<Ticket, TicketDto>().ForMember(x => x.TicketId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.MovieTitle, x => x.MapFrom(y => y.Showtime.Movie.Title))
            .ForMember(x => x.AuditoriumName, x => x.MapFrom(y => y.Showtime.Auditorium.Name))
            .ForMember(x => x.Seats, x => x.MapFrom(y => y.Seats));
    }
}