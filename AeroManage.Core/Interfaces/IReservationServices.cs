using AeroManage.Core.Entities;
using AeroManage.Core.DTOs;

namespace AeroManage.Core.Interfaces;
public interface IReservationService
{
    Task<IEnumerable<ReservationDto>> GetReservationAsync();
    Task<ReservationDto?>GetByIdAsync(int idReservation);
    Task<ReservationDto> CreateAsync(CreateReservationDto dto);
    Task <bool>UpdateAsync(int idResrvation,CreateReservationDto dto );
    Task <bool>DeleteAsync(int idReservation);
}