using ApiApplication.Core.Entities;

namespace ApiApplication.Core.Repositories
{
    public interface ITicketsRepository
    {
        Task<Ticket> ConfirmPaymentAsync(Ticket ticket, CancellationToken cancel);
        Task<Ticket> CreateAsync(Showtime showtime, IEnumerable<Seat> selectedSeats, CancellationToken cancel);
        Task<Ticket> GetAsync(Guid id, CancellationToken cancel);
        Task<IEnumerable<Ticket>> GetEnrichedAsync(int showtimeId, CancellationToken cancel);
    }
}