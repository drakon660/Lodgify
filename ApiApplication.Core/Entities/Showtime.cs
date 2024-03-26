using ApiApplication.Core.ValueObjects;
using Ardalis.Result;

namespace ApiApplication.Core.Entities
{
    public class Showtime
    {
        public int Id { get; protected set; }
        public Movie Movie { get; protected set; }
        public DateTime SessionAtUtc { get; protected set; }
        public Auditorium Auditorium { get; protected set; }

        private readonly List<Reservation> _reservations = new ();
        public virtual IReadOnlyList<Reservation> Reservations => _reservations.ToList();
        
        private List<Ticket> _tickets = new ();
        
        public IReadOnlyList<Ticket> Tickets => _tickets.ToList();

        protected Showtime()
        {
            
        }

        private Showtime(Movie movie, DateTime sessionAtUtc, Auditorium auditorium)
        {
            Movie = movie;
            SessionAtUtc = sessionAtUtc;
            Auditorium = auditorium;
        }

        public IEnumerable<Seat> FreeSeats()
        {
            var reserveSeats = _reservations.SelectMany(x => x.Seats).ToList();
            var boughtSeats = _tickets.SelectMany(x => x.Seats).ToList();

            var union = reserveSeats.Union(boughtSeats);
            
            var freeSeats = Auditorium.Seats.ExceptBy(union.Select(x=>x.Position), x=>x.Position);
            return freeSeats;
        }
        
        public static Result<Showtime> Create(Movie movie, DateTime sessionDate, Auditorium auditorium)
        {
            bool isFree = auditorium.IsFreeForShowtime(sessionDate, movie.LengthInMinutes);

            if (isFree)
            {
                return new Showtime(movie, sessionDate, auditorium);    
            }

            return Result.Invalid(new ValidationError("showtime is overlapping"));
        }
        
        public Result<Reservation> ReserveSeats(ICollection<Position> positionsToSeat, DateTime reservationDate)
        {
            var seatsResults = Auditorium.HasSeatsOn(positionsToSeat);
        
            if (!seatsResults.AllSeatsFound)
                return Result.Invalid(new ValidationError("seats outside auditorium"));
        
            var tickets = _tickets.Where(x =>
            {
                var (haveSeats, _) = x.ContainsSeats(seatsResults.Seats);
                return haveSeats;
            });
        
            if (tickets.Any())
            {
                return Result.Invalid(new ValidationError("seat sold"));
            }
            
            var reservations = _reservations.Where(x => x.ContainsSeatsNumbers(seatsResults.Seats).Any()).ToList();
            
            if(reservations.Count > 0)
                return Result.Invalid(new ValidationError($"seat {string.Join(' ', reservations.SelectMany(x=>x.Seats).Select(x=>x.Position))} already reserved"));
            
            var reservationResult = Reservation.Create(this, seatsResults.Seats, reservationDate);
        
            if(reservationResult.IsSuccess)
                _reservations.Add(reservationResult.Value);
           
            return  reservationResult;
        }


        public Result<Ticket> BuySeats(Reservation reservation, DateTime buyingDate)
        {
            var ticketResult = Ticket.Create(reservation, buyingDate);
            
            if(ticketResult.IsSuccess)
                _tickets.Add(ticketResult.Value);

            return ticketResult;
        }
    }
}
