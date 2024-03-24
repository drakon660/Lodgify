using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using ApiApplication.Core.Entities;
using ApiApplication.Core.Repositories;

namespace ApiApplication.Database.Repositories
{
    public class TicketsRepository : ITicketsRepository
    {
        private readonly CinemaContext _context;

        public TicketsRepository(CinemaContext context)
        {
            _context = context;
        }

        public Task<Ticket> GetAsync(Guid id, CancellationToken cancel)
        {
            return _context.Tickets.FirstOrDefaultAsync(x => x.Id == id, cancel);
        }

        public async Task<IEnumerable<Ticket>> GetEnrichedAsync(int showtimeId, CancellationToken cancel)
        {
            return await _context.Tickets
                .Include(x => x.Showtime)
                .Include(x => x.Seats)
                .Where(x => x.ShowtimeId == showtimeId)
                .ToListAsync(cancel);
        }

        public async Task<Ticket> CreateAsync(Showtime showtime, IEnumerable<Seat> selectedSeats, CancellationToken cancel)
        {
            throw new NotImplementedException();
            // var ticket = _context.Tickets.Add(new Ticket
            // {
            //     Showtime = showtime,
            //     Seats = new List<Seat>(selectedSeats)
            // });
            //
            // await _context.SaveChangesAsync(cancel);
            //
            // return ticket.Entity;
        }

        public async Task<Ticket> ConfirmPaymentAsync(Ticket ticket, CancellationToken cancel)
        {
            ticket.Paid = true;
            _context.Update(ticket);
            await _context.SaveChangesAsync(cancel);
            return ticket;
        }
    }
}
