using ApiApplication.Core.Common;
using ApiApplication.Core.Entities;
using ApiApplication.Core.Repositories;
using FluentAssertions;
using Moq;

namespace ApiApplication.Tests;

public class ShowtimeTests
{
    [Fact]
    public void Check_If_Multiple_Showtimes_Cannot_Be_Set_in_Same_Suditorium_With_time_Collision()
    {
        var auditorium = Auditorium.Create("Barcelona");
        auditorium.SetSeats(Utils.Generate(15, 7));
        
        Movie movie = Movie.Create("Johny Mnemonic", "tt0113481", "Keanu Reeves, Dolph Lundgren, Dina Meyer",
            new DateTime(1995, 06, 06),96);
        
        var movie2 = Movie.Create("Road House", "tt0098206", "Patrick Swayze, Kelly Lynch, Sam Elliott",
            new DateTime(1989, 01, 01),114);

        //var showTimeRepositoryMock = new Mock<IShowtimeRepository>();
        
        var showTime1Result = Showtime.Create(movie, new DateTime(2020, 01, 01, 12,45,00), auditorium);

        var showTime2Result = Showtime.Create(movie2, new DateTime(2020, 01, 01, 13,45,00), auditorium);
        
        showTime1Result.IsSuccess.Should().BeTrue();

        showTime2Result.IsSuccess.Should().BeTrue();
    }
}