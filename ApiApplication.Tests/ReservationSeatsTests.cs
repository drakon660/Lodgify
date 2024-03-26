using ApiApplication.Core.Common;
using ApiApplication.Core.Entities;
using ApiApplication.Core.ValueObjects;
using FluentAssertions;
using Microsoft.Extensions.Time.Testing;

namespace ApiApplication.Tests;

public class ReservationSeatsTests
{
    [Fact]
    public void Check_If_Reservation_Contains_Only_Contiguous_Seats()
    {
        var (showtime, _) = Create();
        
        var seats = new List<Position>()
        {
            Position.Create(4, 5),
            Position.Create(4, 8),
            Position.Create(5, 5),
        };

        var reservationResult = showtime.ReserveSeats(seats, DateTime.Now);

        reservationResult.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Check_If_User_Cannot_Reserve_Same_Seats_Twice()
    {
        var dt = new DateTime(2020, 01, 02);
        
        var fakeTimeProvider = new FakeTimeProvider
        {
            AutoAdvanceAmount = TimeSpan.FromMinutes(5)
        };
        
        fakeTimeProvider.SetUtcNow(dt);
        
        var (showtime, _) = Create();
        
        var seats = new List<Position>()
        {
            Position.Create(4, 5),
            Position.Create(4, 6),
        };
        
        var reservationResult = showtime.ReserveSeats(seats, fakeTimeProvider.GetUtcNow().DateTime);
        reservationResult.IsSuccess.Should().BeTrue();
        
        var secondSeats = new List<Position>()
        {
            Position.Create(4, 5)
        };
        
        var reservationResultOneMoreTime = showtime.ReserveSeats(secondSeats, fakeTimeProvider.GetUtcNow().DateTime);
        reservationResultOneMoreTime.IsSuccess.Should().BeFalse();
    }
   
    [Fact]
    public void Check_If_User_Can_Buy_Ticket_With_Not_Expired_Reservation()
    {
        var dt = new DateTime(2020, 01, 02);
        
        var fakeTimeProvider = new FakeTimeProvider
        {
            AutoAdvanceAmount = TimeSpan.FromMinutes(10)
        };
        
        fakeTimeProvider.SetUtcNow(dt);

        var (showtime, auditorium) = Create();
        
        var seats = new List<Position>()
        {
            Position.Create(4, 5),
            Position.Create(4, 6),
        };

        var reservationResult = showtime.ReserveSeats(seats, fakeTimeProvider.GetUtcNow().DateTime);
        var buyingTime = fakeTimeProvider.GetUtcNow().DateTime;
        var buyingSeatsResult = showtime.BuySeats(reservationResult.Value, buyingTime);

        reservationResult.IsSuccess.Should().BeTrue();
        reservationResult.Value.Showtime.Auditorium.Name.Should().Be("Barcelona");
        reservationResult.Value.Seats.Should().BeEquivalentTo([auditorium[4, 5], auditorium[4, 6]]);
        buyingSeatsResult.IsSuccess.Should().BeTrue();
        buyingSeatsResult.Value.Showtime.Should().BeEquivalentTo(showtime);
        buyingSeatsResult.Value.CreatedAtUtc.Should().Be(buyingTime);
        buyingSeatsResult.Value.Seats.Should().BeEquivalentTo([auditorium[4,5],auditorium[4,6]]);
        buyingSeatsResult.Value.Showtime.Auditorium.Name.Should().Be("Barcelona");
    }


    [Fact]
    public void Check_If_User_Cannot_Reserve_Sold_Seats()
    {
        var (showtime, auditorium) = Create();
        
        var seats = new List<Position>()
        {
            Position.Create(4, 5),
        };

        var reservationResult = showtime.ReserveSeats(seats, DateTime.UtcNow);
        var ticketResult = Ticket.Create(reservationResult.Value, DateTime.UtcNow);
        var reservationResultSecondTime = showtime.ReserveSeats(seats, DateTime.UtcNow);
        
        reservationResult.IsSuccess.Should().BeTrue();
        ticketResult.IsSuccess.Should().BeTrue();
        reservationResultSecondTime.IsSuccess.Should().BeFalse();
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