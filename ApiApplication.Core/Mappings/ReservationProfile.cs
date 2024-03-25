using ApiApplication.Core.Dtos;
using ApiApplication.Core.Entities;
using AutoMapper;

namespace ApiApplication.Core.Mappings;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<Reservation, ReservationDto>()
            .ForMember(x => x.ReservationId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.AuditoriumName, x => x.MapFrom(y => y.Showtime.Auditorium.Name))
            .ForMember(x => x.MovieTitle, x => x.MapFrom(y => y.Showtime.Movie.Title))
            .ForMember(x => x.Seats, x => x.MapFrom(y => y.Seats))
            .ForMember(x => x.IsExpired, x => x.MapFrom(y => y.IsExpired(DateTime.UtcNow)))
            .ForMember(x => x.IsConfirmed, x => x.MapFrom(y => y.IsConfirmed));
    }
}