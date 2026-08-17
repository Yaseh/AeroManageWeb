using AeroManage.Core.Entities;


namespace AeroManage.Core.Interfaces;
public interface IAeroportRepository
{
    Task<IEnumerable<Aeroport>> GetAeroportAsync();
    Task<Aeroport?>GetByIdAsync(string idIata);
    Task<Aeroport> AddAsync(Aeroport aeroport);
    Task <bool>UpdateAsync(Aeroport aeroport);
    Task <bool>DeleteAsync(string idIata);
}