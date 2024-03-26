using ApiApplication.Core.ValueObjects;

namespace ApiApplication.Core.Entities
{
    public class Auditorium
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        private readonly List<Showtime> _showtimes = new();
        public virtual IReadOnlyList<Showtime> Showtimes => _showtimes.ToList();
        
        private readonly List<Seat> _seats = new();
        public virtual IReadOnlyList<Seat> Seats => _seats.ToList(); 
        
        protected Auditorium()
        {
            
        }

        private Auditorium(string name)
        {
            Name = name;
        }

        public void SetSeats(IEnumerable<Seat> seats)
        {
            foreach (var seat in seats)
            {
                seat.SetAuditorium(this);
            }
            
            _seats.AddRange(seats); 
        }

        public (bool AllSeatsFound, ICollection<Seat> Seats) HasSeatsOn(ICollection<Position> positions)
        {
            var existingSeats = _seats.Where(x => positions.Contains(x.Position)).ToList();
        
            if (positions.Count == existingSeats.Count)
            {
                return (true, existingSeats);
            }
        
            return (false, null);
        }
        
        public static Auditorium Create(string name) => new(name);
        
        public bool IsFreeForShowtime(DateTime sessionDate, int movieLengthInMinutes)
        {
            var endDate = sessionDate.AddMinutes(movieLengthInMinutes);
            
            bool isFree = !Showtimes.Any(showtime =>
                (sessionDate >= showtime.SessionAtUtc && sessionDate < showtime.SessionAtUtc.AddMinutes(movieLengthInMinutes)) ||
                (endDate > showtime.SessionAtUtc && endDate <= showtime.SessionAtUtc.AddMinutes(movieLengthInMinutes)) ||
                (sessionDate <= showtime.SessionAtUtc && endDate >= showtime.SessionAtUtc.AddMinutes(movieLengthInMinutes)));

            return isFree;
        }
        
        public Seat this[ushort rowNumber, ushort seatNumber]
        {
            get
            {
                return _seats.FirstOrDefault(seat => seat.Position.RowNumber == rowNumber && seat.Position.SeatNumber == seatNumber);
            }
        }
    }
}
