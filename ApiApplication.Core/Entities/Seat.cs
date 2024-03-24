using ApiApplication.Core.ValueObjects;

namespace ApiApplication.Core.Entities
{
    public class Seat
    {
        public Guid Id { get; protected set; }
        public Position Position { get; protected set; }

        public virtual Auditorium Auditorium { get; protected set; }
        
        protected Seat()
        {
            
        }
        
        private Seat(Position position)
        {
            Position = position;
        }
        
        public static Seat Create(Position position) => new(position);
        
    }
}