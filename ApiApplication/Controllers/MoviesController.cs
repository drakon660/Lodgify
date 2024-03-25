using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiApplication.Core.Dtos;
using ApiApplication.Core.Services;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
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
    [TranslateResultToActionResult]
    public async Task<Result<IEnumerable<MovieDto>>> GetMovies(CancellationToken cancellationToken)
    {
        return await _movieService.GetAll(cancellationToken);
    }
}