using AeroManage.Core.Entities;


namespace AeroManage.Core.Interfaces;
public interface IAvionRepository
{
    Task<IEnumerable<Avion>> GetAvionAsync();
    Task<Avion?>GetByIdAsync(int idAvion);
    Task<Avion> AddAsync(Avion avion);
    Task <bool>UpdateAsync(Avion avion);
    Task <bool>DeleteAsync(int idAvion);
}