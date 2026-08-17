using AeroManage.Core.Entities;
using AeroManage.Core.DTOs;

namespace AeroManage.Core.Interfaces;

public interface IAeroportService
{
    Task<IEnumerable<AeroportDto>> GetAeroportsAsync();
    Task<AeroportDto?>GetByIdAsync(string idIata);
    Task<AeroportDto> CreateAsync(CreateAeroportDto dto);
    Task <bool>UpdateAsync(string idIata, CreateAeroportDto dto);
    Task <bool>DeleteAsync(string idIata);
}