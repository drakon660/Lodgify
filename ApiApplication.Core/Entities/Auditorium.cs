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

        private Auditorium(IEnumerable<Seat> seats)
        {
            foreach (var seat in seats)
            {
                seat.SetAuditorium(this);
            }
            
            Seats = seats;
        }
        
        public static Auditorium Create(IEnumerable<Seat> seats) => new(seats);
    }
}
