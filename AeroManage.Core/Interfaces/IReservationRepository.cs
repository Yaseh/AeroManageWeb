using AeroManage.Core.Entities;
using AeroManage.Core.DTOs;

namespace AeroManage.Core.Interfaces;
public interface IReservationRepository
{
    Task<IEnumerable<Reservation>> GetReservationAsync();
    Task<Reservation?>GetByIdAsync(int idReservation);
    Task<Reservation> AddAsync(Reservation reservation);
    Task <bool>UpdateAsync(Reservation reservation);
    Task <bool>DeleteAsync(int idReservation);
}