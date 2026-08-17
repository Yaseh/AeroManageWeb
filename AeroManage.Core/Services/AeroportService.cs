using AeroManage.Core.Entities;
using AeroManage.Core.DTOs;
using AeroManage.Core.Interfaces;

namespace AeroManage.Core.Services;

public class AeroportService : IAeroportService
{
    private readonly IAeroportRepository _aeroportRepository;
    public AeroportService(IAeroportRepository aeroportRepository)
    {
        _aeroportRepository = aeroportRepository;
    }

    public async Task<IEnumerable<AeroportDto>> GetAeroportsAsync()
    {
        var aeroports = await _aeroportRepository.GetAeroportAsync();
        return aeroports.Select(a => new AeroportDto
        {
            IdIata = a.IdIata,
            Nom = a.Nom,
            Ville = a.Ville,
            Pays = a.Pays
        }).ToList();
    }

    public async Task<AeroportDto?> GetByIdAsync(string idIata)
    {
        var aeroport = await _aeroportRepository.GetByIdAsync(idIata);
        if (aeroport is null) return null;
        return new AeroportDto
        {
            IdIata = aeroport.IdIata,
            Nom = aeroport.Nom,
            Ville = aeroport.Ville,
            Pays = aeroport.Pays
        };
    }

    public async Task<AeroportDto> CreateAsync(CreateAeroportDto dto)
    {
        var aeroport = new Aeroport
        {
            IdIata = dto.IdIata,
            Nom = dto.Nom,
            Ville = dto.Ville,
            Pays = dto.Pays
        };
        var aeroportCree = await _aeroportRepository.AddAsync(aeroport);
        return new AeroportDto
        {
            IdIata = aeroportCree.IdIata,
            Nom = aeroportCree.Nom,
            Ville = aeroportCree.Ville,
            Pays = aeroportCree.Pays
        };
    }

    public async Task<bool> UpdateAsync(string idIata, CreateAeroportDto dto)
    {
        var aeroport = new Aeroport
        {
            IdIata = idIata,
            Nom = dto.Nom,
            Ville = dto.Ville,
            Pays = dto.Pays
        };
        return await _aeroportRepository.UpdateAsync(aeroport);
    }

    public async Task<bool> DeleteAsync(string idIata)
    {
        return await _aeroportRepository.DeleteAsync(idIata);
    }
}
