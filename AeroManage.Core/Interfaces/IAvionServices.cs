using AeroManage.Core.Entities;
using AeroManage.Core.DTOs;

namespace AeroManage.Core.Interfaces;
public interface IAvionService
{
    Task<IEnumerable<AvionDto>> GetAvionAsync();
    Task<AvionDto?>GetByIdAsync(int idAvion);
    Task<AvionDto> CreateAsync(CreateAvionDto dto);
    Task <bool>UpdateAsync(int idAvion, CreateAvionDto dto);
    Task <bool>DeleteAsync(int idAvion);
}