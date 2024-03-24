
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using System.Linq.Expressions;
using ApiApplication.Core.Entities;
using ApiApplication.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication.Database.Repositories
{
    public class ShowtimesRepository : IShowtimesRepository
    {
        private readonly CinemaContext _context;

        public ShowtimesRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<Showtime> GetWithMoviesByIdAsync(int id, CancellationToken cancel)
        {
            return await _context.Showtimes
                .Include(x => x.Movie)
                .FirstOrDefaultAsync(x => x.Id == id, cancel);
        }

        public async Task<Showtime> GetWithTicketsByIdAsync(int id, CancellationToken cancel)
        {
            // return await _context.Showtimes
            //     .Include(x => x.Tickets)
            //     .FirstOrDefaultAsync(x => x.Id == id, cancel);

            throw new NotImplementedException();
        }

        public Task<Showtime> CreateShowtime(Showtime showtime, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }

        public Task<Showtime> GetByMovieTitleAsync(string movieTitle)
        {
            throw new NotImplementedException();
        }

        public Task<Showtime> UpdateShowTime(Showtime showtime)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Showtime>> GetAllAsync(Expression<Func<Showtime, bool>> filter, CancellationToken cancel)
        {
            if (filter == null)
            {
                return await _context.Showtimes
                .Include(x => x.Movie)
                .ToListAsync(cancel);
            }
            return await _context.Showtimes
                .Include(x => x.Movie)
                .Where(filter)
                .ToListAsync(cancel);
        }

        // public async Task<Showtime> CreateShowtime(Showtime showtime, CancellationToken cancel)
        // {
        //     var showtime = await _context.Showtimes.AddAsync(showtime, cancel);
        //     await _context.SaveChangesAsync(cancel);
        //     return showtime;
        // }
    }
}
