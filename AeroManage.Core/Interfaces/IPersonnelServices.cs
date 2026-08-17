using AeroManage.Core.Entities;
using AeroManage.Core.DTOs;

namespace AeroManage.Core.Interfaces;
public interface IPersonnelService
{
    Task<IEnumerable<PersonnelDto>> GetPersonnelAsync();
    Task<PersonnelDto?>GetByIdAsync(int idPersonnel);
    Task<PersonnelDto> CreateAsync(CreatePersonnelDto dto);
    Task <bool>UpdateAsync(int  idPersonnel, CreatePersonnelDto dto);
    Task <bool>DeleteAsync(int idPersonnel);
}