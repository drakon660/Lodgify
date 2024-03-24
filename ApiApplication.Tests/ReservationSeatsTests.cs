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
        var movie = Movie.Create("Johny Mnemonic", "tt0113481", "Keanu Reeves, Dolph Lundgren, Dina Meyer",
            new DateTime(1995, 06, 06));
        var auditorium = Auditorium.Create("Barcelona",Utils.Generate(15, 7));
        var showTime = Showtime.Create(movie, new DateTime(2020, 01, 01), auditorium);

        var seats = new List<Seat>()
        {
            Seat.Create(Position.Create(4, 5), null),
            Seat.Create(Position.Create(4, 8), null),
            Seat.Create(Position.Create(5, 5), null)
        };

        var reservationResult = showTime.ReserveSeats(seats, DateTime.Now);

        reservationResult.IsFailure.Should().BeTrue();
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
        
        var movie = Movie.Create("Road House", "tt0098206", "Patrick Swayze, Kelly Lynch, Sam Elliott",
            new DateTime(1989, 01, 01));
        
        var auditorium = Auditorium.Create("Barcelona",Utils.Generate(15, 7));
        
        var showTime = Showtime.Create(movie, new DateTime(2020, 01, 03), auditorium);

        var seats = new List<Seat>()
        {
            Seat.Create(Position.Create(4, 5), null),
            Seat.Create(Position.Create(4, 6), null),
        };
        
        var reservationResult = showTime.ReserveSeats(seats, fakeTimeProvider.GetUtcNow().DateTime);
        reservationResult.IsSuccess.Should().BeTrue();
        
        var secondSeats = new List<Seat>()
        {
            Seat.Create(Position.Create(4, 5), null),
        };
        
        var reservationResultOneMoreTime = showTime.ReserveSeats(secondSeats, fakeTimeProvider.GetUtcNow().DateTime);
        reservationResultOneMoreTime.IsFailure.Should().BeTrue();
        reservationResultOneMoreTime.Error.Should().Be("seat already reserved");
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

        var movie = Movie.Create("Road House", "tt0098206", "Patrick Swayze, Kelly Lynch, Sam Elliott",
            new DateTime(1989, 01, 01));
        
        var auditorium = Auditorium.Create("Barcelona",Utils.Generate(15, 7));
        
        var showTime = Showtime.Create(movie, new DateTime(2020, 01, 03), auditorium);
        
        var seats = new List<Seat>()
        {
            Seat.Create(Position.Create(4, 5), null),
            Seat.Create(Position.Create(4, 6), null),
        };
        
        var reservationResult = showTime.ReserveSeats(seats, fakeTimeProvider.GetUtcNow().DateTime);
        reservationResult.IsSuccess.Should().BeTrue();

        var buyingTime = fakeTimeProvider.GetUtcNow().DateTime;
        
        var buyingSeatsResult = showTime.BuySeats(reservationResult.Value, buyingTime);
        
        var buyingSeatsSecondTimeResult = showTime.BuySeats(reservationResult.Value, buyingTime);

        buyingSeatsResult.IsSuccess.Should().BeTrue();
        buyingSeatsSecondTimeResult.IsFailure.Should().BeTrue();
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

        var movie = Movie.Create("Road House", "tt0098206", "Patrick Swayze, Kelly Lynch, Sam Elliott",
            new DateTime(1989, 01, 01));
        
        var auditorium = Auditorium.Create("Barcelona",Utils.Generate(15, 7));
        
        var showTime = Showtime.Create(movie, new DateTime(2020, 01, 03), auditorium);
        
        var seats = new List<Seat>()
        {
            Seat.Create(Position.Create(4, 5), null),
            Seat.Create(Position.Create(4, 6), null),
        };
        
        var reservationResult = showTime.ReserveSeats(seats, fakeTimeProvider.GetUtcNow().DateTime);
        reservationResult.IsSuccess.Should().BeTrue();

        var buyingTime = fakeTimeProvider.GetUtcNow().DateTime;
        
        var buyingSeatsResult = showTime.BuySeats(reservationResult.Value, buyingTime);

        buyingSeatsResult.IsSuccess.Should().BeTrue();
        buyingSeatsResult.Value.Showtime.Should().BeEquivalentTo(showTime);
        buyingSeatsResult.Value.CreatedTime.Should().Be(buyingTime);
        buyingSeatsResult.Value.Seats.Should().BeEquivalentTo(seats);
    }
}