using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;

namespace ApiApplication.Core.Entities
{
    public class Ticket
    {
        public Guid Id { get; protected set; }
        public int ShowtimeId { get; protected set; }
        public IReadOnlyList<Seat> Seats { get; protected set; }
        
        public DateTime CreatedTime { get; protected set; }
        public bool Paid { get; set; }
        public Showtime Showtime { get; protected set; }

        protected Ticket()
        {
            
        }
        
        private Ticket(Showtime showtime, IReadOnlyList<Seat> seats, DateTime createdTime)
        {
            Showtime = showtime;
            Seats = seats;
            CreatedTime = createdTime;
        }
        
        public static Result<Ticket> Create(Reservation reservation, DateTime currentDate)
        {
            if (reservation.IsExpired(currentDate))
                return Result.Failure<Ticket>("Reservation expired");

            if(reservation.Confirmed)
                return Result.Failure<Ticket>("Ticket sold for that reservation");
            
            reservation.SetConfirmed();
            
            return Result.Success(new Ticket(reservation.Showtime, reservation.Seats, currentDate));
        }

        public (bool Contains, IEnumerable<Seat> CommonSeats) ContainsSeats(IEnumerable<Seat> seats)
        {
            var commonSeats = Seats.Join(seats,x=>x.Position, y=>y.Position, (seat, _) => seat).ToList();

            if (commonSeats.Count > 0)
                return (true, commonSeats);

            return (false, Enumerable.Empty<Seat>());
        }
    }
}
