using ApiApplication.Core.Common;
using ApiApplication.Core.Entities;
using ApiApplication.Core.ValueObjects;
using FluentAssertions;
using Microsoft.Extensions.Time.Testing;

namespace ApiApplication.Tests;

public class TicketTests
{
    [Fact]
    public void Check_If_Ticket_Contains_Seats()
    {
        var (showtime, auditorium) = Create();
        
        var seats = new List<Position>()
        {
            Position.Create(4, 5),
            Position.Create(4, 6),
            Position.Create(4, 7),
        };
        
        var reservationResult = showtime.ReserveSeats(seats, DateTime.Now);
       
        var ticketResult = Ticket.Create(reservationResult.Value, DateTime.UtcNow);
        
        var (containsSeats, commonSeats) = ticketResult.Value.ContainsSeats([auditorium[5, 5], auditorium[4, 6], auditorium[4, 7]]);
        
        containsSeats.Should().BeTrue();
        commonSeats.Select(x=>x.Position).Should().BeEquivalentTo([Position.Create(4,6), Position.Create(4, 7)]);
    }
    
    [Fact]
    public void Check_If_User_Cannot_Buy_Tickets_Outside_Auditorium()
    {
        var (showtime, _) = Create();

        var seats = new List<Position>()
        {
            Position.Create(4, 7),
            Position.Create(4, 8),
        };
        
        var reservationResult = showtime.ReserveSeats(seats, DateTime.Now);

        reservationResult.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public void Check_If_User_Cannot_Buy_Same_Seats_Twice()
    {
        var dt = new DateTime(2020, 01, 02);
        
        var fakeTimeProvider = new FakeTimeProvider
        {
            AutoAdvanceAmount = TimeSpan.FromMinutes(10)
        };
        
        fakeTimeProvider.SetUtcNow(dt);

        var (showtime, _) = Create();
        
        var seats = new List<Position>()
        {
            Position.Create(4, 5),
            Position.Create(4, 6),
        };
        
        var reservationResult = showtime.ReserveSeats(seats, fakeTimeProvider.GetUtcNow().DateTime);
        var buyingTime = fakeTimeProvider.GetUtcNow().DateTime;
        var buyingSeatsResult = showtime.BuySeats(reservationResult.Value, buyingTime);
        var buyingSeatsSecondTimeResult = showtime.BuySeats(reservationResult.Value, buyingTime);

        reservationResult.IsSuccess.Should().BeTrue();
        buyingSeatsResult.IsSuccess.Should().BeTrue();
        buyingSeatsSecondTimeResult.IsSuccess.Should().BeFalse();
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