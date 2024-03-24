using CSharpFunctionalExtensions;

namespace ApiApplication.Core.Entities
{
    public class Showtime
    {
        public int Id { get; protected set; }
        public Movie Movie { get; protected set; }
        public DateTime SessionDate { get; protected set; }
        
        public int AuditoriumId { get; protected set; }
        public Auditorium Auditorium { get; protected set; }

        private List<Reservation> _reservations = new ();
        
        private IReadOnlyList<Reservation> Reservations => _reservations.ToList();
        
        private List<Ticket> _tickets = new ();
        
        public IReadOnlyList<Ticket> Tickets => _tickets.ToList();

        protected Showtime()
        {
            
        }

        private Showtime(Movie movie, DateTime sessionDate, Auditorium auditorium)
        {
            Movie = movie;
            SessionDate = sessionDate;
            Auditorium = auditorium;
        }

        public static Showtime Create(Movie movie, DateTime sessionDate, Auditorium auditorium) =>
            new(movie, sessionDate, auditorium);
        
        
        public Result<Reservation> ReserveSeats(ICollection<Seat> seats, DateTime reservationDate)
        {
            var tickets = _tickets.Where(x =>
            {
                var (haveSeats, _) = x.ContainsSeats(seats);
                return haveSeats;
            });

            if (tickets.Any())
            {
                return Result.Failure<Reservation>("seat sold");
            }
            
            var reservations = _reservations.Where(x => x.ContainsSeatsNumbers(seats).Any());
            
            if(reservations.Any())
                return Result.Failure<Reservation>("seat already reserved");
            
            var reservationResult = Reservation.Create(this, seats, reservationDate);

            if(reservationResult.IsSuccess)
                _reservations.Add(reservationResult.Value);
           
            return reservationResult;
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
