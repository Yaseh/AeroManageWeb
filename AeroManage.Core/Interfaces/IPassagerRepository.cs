using AeroManage.Core.Entities;
using AeroManage.Core.DTOs;

namespace AeroManage.Core.Interfaces;
public interface IPassagerRepository
{
    Task<IEnumerable<Passager>> GetPassagerAsync();
    Task<Passager?>GetByIdAsync(int idPassager);
    Task<Passager> AddAsync(Passager passager);
    Task <bool>UpdateAsync(Passager passager);
    Task <bool>DeleteAsync(int idPassager);
}