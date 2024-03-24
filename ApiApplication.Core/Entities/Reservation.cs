using CSharpFunctionalExtensions;

namespace ApiApplication.Core.Entities;

public class Reservation
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAtUtc { get; protected set; }
    public Showtime Showtime { get; protected set; }
    public bool IsExpired(DateTime currentDate) => CreatedAtUtc.AddMinutes(10) < currentDate;
    public IReadOnlyList<Seat> Seats => _seats.ToList();
    
    public bool Confirmed { get; protected set; }

    private List<Seat> _seats = new ();

    protected Reservation()
    {
        
    }
    
    private Reservation(Showtime showtime, ICollection<Seat> seatsToReserve, DateTime createdAtUtc)
    {
        Showtime = showtime;
        _seats.AddRange(seatsToReserve);
        CreatedAtUtc = createdAtUtc;
    }

    public void SetConfirmed()
    {
        Confirmed = true;
    }
    
    public static Result<Reservation> Create(Showtime showtime, ICollection<Seat> seatsToReserve, DateTime createdAtUtc)
    {
        if (!AreSeatsContiguous(seatsToReserve))
        {
            return Result.Failure<Reservation>("Seats are not contiguous");
        }
        
        return new Reservation(showtime, seatsToReserve, createdAtUtc);
    }
    
    private static bool AreSeatsContiguous(ICollection<Seat> seats)
    {
        if (seats.Count <= 1)
            return true; 
        
        var sortedSeats = seats.OrderBy(x=>x.Position.RowNumber).ThenBy(x=>x.Position.SeatNumber);
        
        var expectedSeat = seats.First().Position.SeatNumber;
        var currentRow =seats.First().Position.RowNumber;

        foreach (var seat in sortedSeats)
        {
            if (seat.Position.RowNumber != currentRow || seat.Position.SeatNumber != expectedSeat)
            {
                return false;
            }
            expectedSeat++;
        }
        
        return true;
    }

    public IEnumerable<Seat> ContainsSeatsNumbers(IEnumerable<Seat> seats)
    {
        return _seats.Join(seats,x=>x.Position, y=>y.Position, (seat, _) => seat).ToList();
    }
}