using ApiApplication.Core.Dtos;
using ApiApplication.Core.Entities;
using AutoMapper;

namespace ApiApplication.Core.Mappings;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieDto>()
            .ForMember(x=>x.MovieId, x=>x.MapFrom(y=>y.Id))
            .ForMember(x => x.Name, x => x.MapFrom(y => y.Title));
    }
}