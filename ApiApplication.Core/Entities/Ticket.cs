﻿using Ardalis.Result;

namespace ApiApplication.Core.Entities
{
    public class Ticket
    {
        public Guid Id { get; protected set; }
        public IReadOnlyList<Seat> Seats { get; protected set; }
        public DateTime CreatedAtUtc { get; protected set; }
        public Showtime Showtime { get; protected set; }

        protected Ticket()
        {
            
        }
        
        private Ticket(Showtime showtime, IReadOnlyList<Seat> seats, DateTime createdAtUtc)
        {
            Showtime = showtime;
            Seats = seats;
            CreatedAtUtc = createdAtUtc;
        }
        
        public static Result<Ticket> Create(Reservation reservation, DateTime currentDate)
        {
            if (reservation.IsExpired(currentDate))
                return Result.Invalid(new ValidationError("reservation expired"));

            if(reservation.IsConfirmed)
                return Result.Invalid(new ValidationError("ticket sold for that reservation"));
            
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
