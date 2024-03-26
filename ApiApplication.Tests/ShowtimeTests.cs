using ApiApplication.Core.Common;
using ApiApplication.Core.Entities;
using ApiApplication.Core.Repositories;
using ApiApplication.Core.ValueObjects;
using FluentAssertions;
using Microsoft.Extensions.Time.Testing;
using Moq;

namespace ApiApplication.Tests;

public class ShowtimeTests
{
    [Fact]
    public void Check_If_Showtime_Contains_Reservation()
    {
        var (showtime, _) = Create();
        
        var seats = new List<Position>()
        {
            Position.Create(4, 5),
            Position.Create(4, 6),
        };

        _ = showtime.ReserveSeats(seats, DateTime.UtcNow);

        showtime.Reservations.Should().HaveCount(1);
        showtime.Reservations.First().Showtime.Should().BeEquivalentTo(showtime);
    }
    
    [Fact]
    public void Check_If_Showtime_Contains_Tickets()
    {
        var (showtime, _) = Create();
        
        var seats = new List<Position>()
        {
            Position.Create(4, 5),
            Position.Create(4, 6),
        };

        var reservationResult  = showtime.ReserveSeats(seats, DateTime.UtcNow);
        _ = showtime.BuySeats(reservationResult.Value, DateTime.UtcNow);

        showtime.Tickets.Should().HaveCount(1);
        showtime.Tickets.First().Showtime.Should().BeEquivalentTo(showtime);
    }
    
    [Fact]
    public void Check_If_Showtime_Have_Good_Number_Of_Seats()
    {  
        var (showtime, _) = Create();
        
        var seats = new List<Position>()
        {
            Position.Create(4, 5),
            Position.Create(4, 6),
        };

        var reservationResult  = showtime.ReserveSeats(seats, DateTime.UtcNow);
        _ = showtime.BuySeats(reservationResult.Value, DateTime.UtcNow);

        showtime.FreeSeats().Should().HaveCount(103);
    }

    private (Showtime showtime, Auditorium Auditorium) Create()
    {
        Movie movie = Movie.Create("Johny Mnemonic", "tt0113481", "Keanu Reeves, Dolph Lundgren, Dina Meyer",
            new DateTime(1995, 06, 06),96);
        var auditorium = Auditorium.Create("Barcelona");
        
        auditorium.SetSeats(Utils.Generate(15, 7));

        var showTime = Showtime.Create(movie, new DateTime(2020, 01, 01), auditorium);

        return (showTime.Value, auditorium);
    }
}