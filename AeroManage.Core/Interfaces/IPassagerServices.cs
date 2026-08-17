using AeroManage.Core.Entities;
using AeroManage.Core.DTOs;

namespace AeroManage.Core.Interfaces;
public interface IPassagerService
{
    Task<IEnumerable<PassagerDto>> GetPassagerAsync();
    Task<PassagerDto?>GetByIdAsync(int idPassager);
    Task<PassagerDto> CreateAsync(CreatePassagerDto dto);
    Task <bool>UpdateAsync(int passager,CreatePassagerDto dto);
    Task <bool>DeleteAsync(int idPassager);
}