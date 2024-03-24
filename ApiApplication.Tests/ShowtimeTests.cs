using System.Runtime.CompilerServices;
using ApiApplication.Core.Common;
using ApiApplication.Core.Entities;
using ApiApplication.Core.Repositories;
using Microsoft.Extensions.Time.Testing;
using Moq;

namespace ApiApplication.Tests;

public class ShowtimeTests
{
    [Fact]
    public void Check_If_Showtime_Was_Created_Successfully()
    {
        //var movieRepositoryMock = new Mock<IMovieRepository>();
        var timeProvider = new FakeTimeProvider();
        var start = timeProvider.Start;
        
        Movie movie = Movie.Create("Matrix", "tt0133093","Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss", new DateTime(01,01,1999));
        Auditorium auditorium = Auditorium.Create("Barcelona",Utils.Generate(5,18));
        //movieRepositoryMock.Setup(x=>x.GetMovieByImdbId())

        var showtime = Showtime.Create(movie, new DateTime(2020, 12, 30, 12, 00, 00), auditorium);
    }
}