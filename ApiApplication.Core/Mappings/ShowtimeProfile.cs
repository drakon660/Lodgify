using ApiApplication.Core.Dtos;
using ApiApplication.Core.Entities;
using AutoMapper;

namespace ApiApplication.Core.Mappings;

public class ShowtimeProfile : Profile
{
    public ShowtimeProfile()
    {
        CreateMap<Showtime, ShowtimeDto>()
            .ForMember(x => x.ShowtimeId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Title, x => x.MapFrom(y => y.Movie.Title))
            .ForMember(x => x.AuditoriumName, x => x.MapFrom(y => y.Auditorium.Name))
            .ForMember(x => x.SessionDate, x => x.MapFrom(y => y.SessionDate))
            .ForMember(x => x.FreeSeats, x => x.MapFrom(y => y.FreeSeats()));

        CreateMap<Showtime, CreatedShowtimeDto>()
            .ForMember(x => x.ShowtimeId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Title, x => x.MapFrom(y => y.Movie.Title))
            .ForMember(x => x.AuditoriumName, x => x.MapFrom(y => y.Auditorium.Name))
            .ForMember(x => x.SessionDate, x => x.MapFrom(y => y.SessionDate));
    }
}