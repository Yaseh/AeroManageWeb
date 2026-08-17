using AeroManage.Core.Entities;
using AeroManage.Core.DTOs;
using AeroManage.Core.Interfaces;

namespace AeroManage.Core.Services;

public class AvionService : IAvionService
{
    private readonly IAvionRepository _avionRepository ;
    public AvionService ( IAvionRepository avionRepository)
    {
        _avionRepository = avionRepository;
    }
    public async Task<IEnumerable<AvionDto>>GetAvionAsync()
    {
        var avion = await _avionRepository.GetAvionAsync();
        return avion.Select(A => new AvionDto
        {
            IdAvion = A.IdAvion,
            Model = A.Model,
            Capacite = A.Capacite,
            Statut = A.Statut
        }).ToList();
    } 
    public async Task<AvionDto?>GetByIdAsync(int idAvion)
    {
        var avionId = await _avionRepository.GetByIdAsync(idAvion);
        if (avionId is null)
        {
            return null;
        }
        return new AvionDto
        {
        IdAvion = avionId.IdAvion,
        Model = avionId.Model,
        Capacite = avionId.Capacite,
        Statut = avionId.Statut
    };
        
    }

    public async Task<AvionDto>CreateAsync(CreateAvionDto dto)
    {
        var avionNouveau = new Avion
        {
            Model = dto.Model,
            Capacite = dto.Capacite,
            Statut = dto.Statut

        };
        var avionCree = await _avionRepository.AddAsync(avionNouveau);
        return new AvionDto
        {
            IdAvion = avionCree.IdAvion,
            Model = avionCree.Model,
            Capacite = avionCree.Capacite,
            Statut = avionCree.Statut
        };
    }
    public async Task<bool>DeleteAsync(int idAvion)
    {
        var suppavion =await _avionRepository.DeleteAsync(idAvion);
        return suppavion;
    }
    public async Task<bool>UpdateAsync(int idAvion,CreateAvionDto dto)
    {
        var avionModifier =new Avion
        {
            IdAvion = idAvion,
            Model =dto.Model,
            Capacite = dto.Capacite,
            Statut = dto.Statut
        };
         var resultat = await _avionRepository.UpdateAsync(avionModifier);
         return resultat;
    }
}


