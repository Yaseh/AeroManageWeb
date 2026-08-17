using AeroManage.Core.Entities;
using AeroManage.Core.DTOs;

namespace AeroManage.Core.Interfaces;
public interface IVolService
{
    Task<IEnumerable<VolDto>> GetVolAsync();
    Task<VolDto?>GetByIdAsync(int idVol);
    Task<VolDto> CreateAsync(CreateVolDto dto);
    Task <bool>UpdateAsync(int idVol,CreateVolDto dto);
    Task <bool>DeleteAsync(int idVol);
}