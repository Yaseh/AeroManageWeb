using AeroManage.Core.Entities;
using AeroManage.Core.DTOs;

namespace AeroManage.Core.Interfaces;
public interface IPersonnelRepository
{
    Task<IEnumerable<Personnel>> GetPersonnelAsync();
    Task<Personnel?>GetByIdAsync(int idPersonnel);
    Task<Personnel> AddAsync(Personnel personnel);
    Task <bool>UpdateAsync(Personnel personnel);
    Task <bool>DeleteAsync(int idPersonnel);
}