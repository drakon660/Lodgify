using System.Runtime.CompilerServices;
using ApiApplication.Core.Common;
using ApiApplication.Core.Entities;
using ApiApplication.Core.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Time.Testing;
using Moq;

namespace ApiApplication.Tests;

public class ShowtimeTests
{
    //[Fact]
    // public void Check_If_Showtime_Was_Created_Successfully()
    // {
    //     //var movieRepositoryMock = new Mock<IMovieRepository>();
    //     var timeProvider = new FakeTimeProvider();
    //     var start = timeProvider.Start;
    //     
    //     Movie movie = Movie.Create("Matrix", "tt0133093","Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss", new DateTime(01,01,1999));
    //     Auditorium auditorium = Auditorium.Create("Barcelona",Utils.Generate(5,18));
    //     //movieRepositoryMock.Setup(x=>x.GetMovieByImdbId())
    //
    //     var showtime = Showtime.Create(movie, new DateTime(2020, 12, 30, 12, 00, 00), auditorium);
    // }

    [Fact]
    public void Check_If_Multiple_Showtimes_Cannot_be_set_in_same_auditorium_with_time_collision()
    {
        var auditorium = Auditorium.Create("Barcelona");
        auditorium.SetSeats(Utils.Generate(15, 7));
        
        Movie movie = Movie.Create("Johny Mnemonic", "tt0113481", "Keanu Reeves, Dolph Lundgren, Dina Meyer",
            new DateTime(1995, 06, 06),96);
        
        var movie2 = Movie.Create("Road House", "tt0098206", "Patrick Swayze, Kelly Lynch, Sam Elliott",
            new DateTime(1989, 01, 01),114);

        var showTime1Result = Showtime.Create(movie, new DateTime(2020, 01, 01, 12,45,00), auditorium);

        var showTime2Result = Showtime.Create(movie2, new DateTime(2020, 01, 01, 13,45,00), auditorium);
        
        showTime1Result.IsSuccess.Should().BeTrue();

        showTime2Result.IsFailure.Should().BeTrue();
    }
}