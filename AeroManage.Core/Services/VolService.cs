using AeroManage.Core.Entities;
using AeroManage.Core.DTOs;
using AeroManage.Core.Interfaces;

namespace AeroManage.Core.Services;

public class VolService : IVolService
{
    private readonly IVolRepository _volRepository;
    public VolService(IVolRepository volRepository)
    {
        _volRepository = volRepository;
    }

    public async Task<IEnumerable<VolDto>> GetVolAsync()
    {
        var vols = await _volRepository.GetVolAsync();
        return vols.Select(v => new VolDto
        {
            IdVol = v.IdVol,
            NumeroVol = v.NumeroVol,
            DateDepart = v.DateDepart,
            DateArrivee = v.DateArrivee,
            Statut = v.Statut,
            AeroportDepart = v.AeroportDepart,
            AeroportArrivee = v.AeroportArrivee,
            IdAvion = v.IdAvion,
            IdCommandant = v.IdCommandant
        }).ToList();
    }

    public async Task<VolDto?> GetByIdAsync(int idVol)
    {
        var vol = await _volRepository.GetByIdAsync(idVol);
        if (vol is null) return null;
        return new VolDto
        {
            IdVol = vol.IdVol,
            NumeroVol = vol.NumeroVol,
            DateDepart = vol.DateDepart,
            DateArrivee = vol.DateArrivee,
            Statut = vol.Statut,
            AeroportDepart = vol.AeroportDepart,
            AeroportArrivee = vol.AeroportArrivee,
            IdAvion = vol.IdAvion,
            IdCommandant = vol.IdCommandant
        };
    }

    public async Task<VolDto> CreateAsync(CreateVolDto dto)
    {
        var vol = new Vol
        {
            NumeroVol = dto.NumeroVol,
            DateDepart = dto.DateDepart,
            DateArrivee = dto.DateArrivee,
            Statut = dto.Statut,
            AeroportDepart = dto.AeroportDepart,
            AeroportArrivee = dto.AeroportArrivee,
            IdAvion = dto.IdAvion,
            IdCommandant = dto.IdCommandant
        };
        var volCree = await _volRepository.AddAsync(vol);
        return new VolDto
        {
            IdVol = volCree.IdVol,
            NumeroVol = volCree.NumeroVol,
            DateDepart = volCree.DateDepart,
            DateArrivee = volCree.DateArrivee,
            Statut = volCree.Statut,
            AeroportDepart = volCree.AeroportDepart,
            AeroportArrivee = volCree.AeroportArrivee,
            IdAvion = volCree.IdAvion,
            IdCommandant = volCree.IdCommandant
        };
    }

    public async Task<bool> UpdateAsync(int idVol, CreateVolDto dto)
    {
        var vol = new Vol
        {
            IdVol = idVol,
            NumeroVol = dto.NumeroVol,
            DateDepart = dto.DateDepart,
            DateArrivee = dto.DateArrivee,
            Statut = dto.Statut,
            AeroportDepart = dto.AeroportDepart,
            AeroportArrivee = dto.AeroportArrivee,
            IdAvion = dto.IdAvion,
            IdCommandant = dto.IdCommandant
        };
        return await _volRepository.UpdateAsync(vol);
    }

    public async Task<bool> DeleteAsync(int idVol)
    {
        return await _volRepository.DeleteAsync(idVol);
    }
}
