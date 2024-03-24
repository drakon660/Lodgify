using ApiApplication.Core.ValueObjects;

namespace ApiApplication.Core.Entities
{
    public class Auditorium
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public List<Showtime> Showtimes { get; protected set; }
        public IEnumerable<Seat> Seats { get; protected set; }

        protected Auditorium()
        {
            
        }

        private Auditorium(string name, IEnumerable<Seat> seats)
        {
            Name = name;
            foreach (var seat in seats)
            {
                seat.SetAuditorium(this);
            }
            
            Seats = seats;
        }

        public (bool AllSeatsFound, ICollection<Seat> Seats) HasSeatsOn(ICollection<Position> positions)
        {
            var existingSeats = Seats.Where(x => positions.Contains(x.Position)).ToList();

            if (positions.Count == existingSeats.Count)
            {
                return (true, existingSeats);
            }

            return (false, null);
        }
        
        public static Auditorium Create(string name, IEnumerable<Seat> seats) => new(name, seats);
        
        public Seat this[ushort rowNumber, ushort seatNumber]
        {
            get
            {
                return Seats.FirstOrDefault(seat => seat.Position.RowNumber == rowNumber && seat.Position.SeatNumber == seatNumber);
            }
        }
    }
}
