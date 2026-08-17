using AeroManage.Core.Entities;
using AeroManage.Core.DTOs;
using AeroManage.Core.Interfaces;

namespace AeroManage.Core.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    public ReservationService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<IEnumerable<ReservationDto>> GetReservationAsync()
    {
        var reservations = await _reservationRepository.GetReservationAsync();
        return reservations.Select(r => new ReservationDto
        {
            IdReservation = r.IdReservation,
            Numerosiege = r.Numerosiege,
            IdVol = r.IdVol,
            IdPassager = r.IdPassager
        }).ToList();
    }

    public async Task<ReservationDto?> GetByIdAsync(int idReservation)
    {
        var reservation = await _reservationRepository.GetByIdAsync(idReservation);
        if (reservation is null) return null;
        return new ReservationDto
        {
            IdReservation = reservation.IdReservation,
            Numerosiege = reservation.Numerosiege,
            IdVol = reservation.IdVol,
            IdPassager = reservation.IdPassager
        };
    }

    public async Task<ReservationDto> CreateAsync(CreateReservationDto dto)
    {
        var reservation = new Reservation
        {
            Numerosiege = dto.Numerosiege,
            IdVol = dto.IdVol,
            IdPassager = dto.IdPassager
        };
        var reservationCree = await _reservationRepository.AddAsync(reservation);
        return new ReservationDto
        {
            IdReservation = reservationCree.IdReservation,
            Numerosiege = reservationCree.Numerosiege,
            IdVol = reservationCree.IdVol,
            IdPassager = reservationCree.IdPassager
        };
    }

    public async Task<bool> UpdateAsync(int idReservation, CreateReservationDto dto)
    {
        var reservation = new Reservation
        {
            IdReservation = idReservation,
            Numerosiege = dto.Numerosiege,
            IdVol = dto.IdVol,
            IdPassager = dto.IdPassager
        };
        return await _reservationRepository.UpdateAsync(reservation);
    }

    public async Task<bool> DeleteAsync(int idReservation)
    {
        return await _reservationRepository.DeleteAsync(idReservation);
    }
}
