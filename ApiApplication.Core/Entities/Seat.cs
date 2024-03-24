using ApiApplication.Core.ValueObjects;

namespace ApiApplication.Core.Entities
{
    public class Seat
    {
        public Position Position { get; set; }
        public int AuditoriumId { get; set; }
        public Auditorium Auditorium { get; set; }

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
        
        public ReservationTimeWindow ReservationTimeWindow { get; protected set; }

        public bool ReserveSeat(DateTime reservationDate, int movieLengthInMinute)
        {
            if (WithinReservationWindow(reservationDate))
            {
                ReservationTimeWindow =
                    new ReservationTimeWindow(reservationDate, reservationDate.AddMinutes(movieLengthInMinute));
            }

            return false;
        }

        private bool WithinReservationWindow(DateTime currentTime)
        {
            if (ReservationTimeWindow != null)
            {
                return ReservationTimeWindow.StartTime <= currentTime && currentTime <= ReservationTimeWindow.EndTime;
            }

            return false;
        }
    }
}



public class ReservationTimeWindow
{
    public DateTime StartTime { get; protected set; }
    public DateTime EndTime { get; protected set; }

    public ReservationTimeWindow(DateTime startTime, DateTime endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }
}