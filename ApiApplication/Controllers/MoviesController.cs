using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiApplication.Core.Dtos;
using ApiApplication.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers;

[ApiController]
[Route("movies")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }
    
    [HttpGet("")]
    public async Task<IEnumerable<MovieDto>> GetMovies(CancellationToken cancellationToken)
    {
        return await _movieService.GetAll(cancellationToken);
    }

    [HttpGet("{title}")]
    public async Task<MovieDto> GetMovie(string tile, CancellationToken cancellationToken)
    {
        return await _movieService.GetMovieByTitle(tile,cancellationToken);
    }
}