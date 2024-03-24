using ApiApplication.Core.ValueObjects;

namespace ApiApplication.Core.Entities
{
    public class Seat
    {
        public Guid Id { get; protected set; }
        public Position Position { get; protected set; }
        public int AuditoriumId { get; protected set; }
        public Auditorium Auditorium { get; protected set; }

        protected Seat()
        {
            
        }
        
        private Seat(Position position, Auditorium auditorium)
        {
            Position = position;
            Auditorium = auditorium;
        }

        public void SetAuditorium(Auditorium auditorium)
        {
            Auditorium = auditorium;
        }
        
        public static Seat Create(Position position, Auditorium auditorium) => new(position, auditorium);
        
    }
}