using AeroManage.Core.DTOs;
using AeroManage.Core.Entities;
using AeroManage.Core.Interfaces;

namespace AeroManage.Core.Services;

public class PassagerService : IPassagerService
{
    private readonly IPassagerRepository _passagerRepository;
    public PassagerService (IPassagerRepository passagerRepository)
    {
        _passagerRepository = passagerRepository;
    }
    public async Task<IEnumerable<PassagerDto>>GetPassagerAsync()
    {
        var passager = await _passagerRepository.GetPassagerAsync();
        return passager.Select(P => new PassagerDto
        {
            IdPassager = P.IdPassager,
            Nom = P.Nom,
            Prenom = P.Prenom,
            Nationalite = P.Nationalite
        }).ToList();
    }
    public async Task<PassagerDto> CreateAsync(CreatePassagerDto dto)
{
    var nouveauPassager = new Passager
    {
        Nom = dto.Nom,
        Prenom = dto.Prenom,
        Nationalite = dto.Nationalite
    };
    var passagerCree = await _passagerRepository.AddAsync(nouveauPassager);
    return new PassagerDto
    {
        IdPassager = passagerCree.IdPassager,
        Nom = passagerCree.Nom,
        Prenom = passagerCree.Prenom,
        Nationalite = passagerCree.Nationalite
    };
}
    public async Task<PassagerDto?>GetByIdAsync(int idPassager)
    {
        var passagerid = await _passagerRepository.GetByIdAsync(idPassager);
        if (passagerid is null)
        {
            return null;
        }
        return new PassagerDto
        {
            IdPassager =passagerid.IdPassager,
            Nom =passagerid.Nom,
            Prenom= passagerid.Prenom,
            Nationalite = passagerid.Nationalite
        };
    }
    public async Task<bool>DeleteAsync(int idPassager)
    {
        var passagersupp = await _passagerRepository.DeleteAsync(idPassager);
        return passagersupp;
    }
    public async Task<bool>UpdateAsync(int idPassager,CreatePassagerDto dto)
    {
        var passagermodifier = new Passager
        {
            IdPassager=idPassager,
            Nom=dto.Nom,
            Prenom=dto.Prenom,
            Nationalite=dto.Nationalite
        };
        var resultat = await _passagerRepository.UpdateAsync(passagermodifier);
        return resultat;
    }
}
