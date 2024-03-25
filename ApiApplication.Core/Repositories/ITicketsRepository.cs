using ApiApplication.Core.Entities;

namespace ApiApplication.Core.Repositories
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAll(CancellationToken cancellationToken); 
        // Task<Ticket> ConfirmPaymentAsync(Ticket ticket, CancellationToken cancel);
        // Task<Ticket> CreateAsync(Showtime showtime, IEnumerable<Seat> selectedSeats, CancellationToken cancel);
        // Task<Ticket> GetAsync(Guid id, CancellationToken cancel);
        // Task<IEnumerable<Ticket>> GetEnrichedAsync(int showtimeId, CancellationToken cancel);
        Task Save(Ticket ticket, CancellationToken cancellationToken);
    }
}