﻿using ApiApplication.Core.Dtos;
using ApiApplication.Core.Entities;
using ApiApplication.Core.Repositories;
using ApiApplication.Core.Services;
using ApiApplication.Core.ValueObjects;
using AutoMapper;
using CSharpFunctionalExtensions;

namespace ApiApplication.Infrastructure.Services;

public class ShowtimeService : IShowtimeService
{
    private readonly IMapper _mapper;
    private readonly IShowtimeRepository _showtimeRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly IAuditoriumsRepository _auditoriumsRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly ITicketRepository _ticketRepository;

    public ShowtimeService(IMapper mapper, IShowtimeRepository showtimeRepository, 
        IMovieRepository movieRepository,
        IAuditoriumsRepository auditoriumsRepository, 
        IReservationRepository reservationRepository, 
        ITicketRepository ticketRepository)
    {
        _mapper = mapper;
        _showtimeRepository = showtimeRepository;
        _movieRepository = movieRepository;
        _auditoriumsRepository = auditoriumsRepository;
        _reservationRepository = reservationRepository;
        _ticketRepository = ticketRepository;
    }

    public async Task<IEnumerable<ShowtimeDto>> GetAll(CancellationToken cancellationToken)
    {
        var showtimes = await _showtimeRepository.GetAllAsync(cancellationToken);

        return _mapper.Map<IEnumerable<Showtime>, IEnumerable<ShowtimeDto>>(showtimes);
    }

    public async Task<IEnumerable<ReservationDto>> GetAllReservations(CancellationToken cancellationToken)
    {
        var reservations = await _reservationRepository.GetByAll(cancellationToken);
        return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
    }


    public async Task<Result<ShowtimeDto>> CreateShowtime(CreateShowtimeDto createShowtimeDto, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetById(createShowtimeDto.MovieId, cancellationToken);
        var auditorium = await _auditoriumsRepository.GetById(createShowtimeDto.AuditoriumId, cancellationToken);

        var showtimeResult = Showtime.Create(movie, createShowtimeDto.SessionDate, auditorium);

        if (showtimeResult.IsSuccess)
        {
            await _showtimeRepository.CreateShowtime(showtimeResult.Value, cancellationToken);
            return _mapper.Map<ShowtimeDto>(showtimeResult.Value);
        }

        return Result.Failure<ShowtimeDto>(showtimeResult.Error);
    }

    public async Task<ReservationDto> ReserveShowtime(CreateReservationDto createReservationDto, CancellationToken cancellationToken)
    {
        var showTime = await _showtimeRepository.GetById(createReservationDto.ShowTimeId, cancellationToken);
        
        var seatsPositions = createReservationDto.Seats.Select(x => Position.Create(x.RowNumber, x.SeatNumber)).ToList();
        
        var reservationResult =
            showTime.ReserveSeats(seatsPositions,
                createReservationDto.SessionDate);
        
        if (reservationResult.IsSuccess)
        {
            await _showtimeRepository.UpdateShowtime(showTime, cancellationToken);
            return _mapper.Map<ReservationDto>(reservationResult.Value);
        }
        
        return null;
    }

    public async Task<IEnumerable<TicketDto>> GetAllTickets(CancellationToken cancellationToken)
    {
        var tickets = await _ticketRepository.GetAll(cancellationToken);

        return _mapper.Map<IEnumerable<TicketDto>>(tickets);
    }

    public async Task<TicketDto> ConfirmReservation(Guid reservationId, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.GetById(reservationId, cancellationToken);

        var ticketResult = Ticket.Create(reservation, DateTime.UtcNow);
        if (ticketResult.IsSuccess)
        {
            await _ticketRepository.Save(ticketResult.Value, cancellationToken);
            return _mapper.Map<TicketDto>(ticketResult.Value);
        }

        return null;
    }
}