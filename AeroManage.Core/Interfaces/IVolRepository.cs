using AeroManage.Core.Entities;
using AeroManage.Core.DTOs;

namespace AeroManage.Core.Interfaces;
public interface IVolRepository
{
    Task<IEnumerable<Vol>> GetVolAsync();
    Task<Vol?>GetByIdAsync(int idVol);
    Task<Vol> AddAsync(Vol vol);
    Task <bool>UpdateAsync(Vol vol);
    Task <bool>DeleteAsync(int idVol);
}