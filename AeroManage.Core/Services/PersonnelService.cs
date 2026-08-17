using AeroManage.Core.Entities;
using AeroManage.Core.DTOs;
using AeroManage.Core.Interfaces;

namespace AeroManage.Core.Services;

public class PersonnelService : IPersonnelService
{
    private readonly IPersonnelRepository _personnelRepository;
    public PersonnelService(IPersonnelRepository personnelRepository)
    {
        _personnelRepository = personnelRepository;
    }

    public async Task<IEnumerable<PersonnelDto>> GetPersonnelAsync()
    {
        var personnels = await _personnelRepository.GetPersonnelAsync();
        return personnels.Select(p => new PersonnelDto
        {
            IdPersonnel = p.IdPersonnel,
            Nom = p.Nom,
            Prenom = p.Prenom,
            Role = p.Role
        }).ToList();
    }

    public async Task<PersonnelDto?> GetByIdAsync(int idPersonnel)
    {
        var personnel = await _personnelRepository.GetByIdAsync(idPersonnel);
        if (personnel is null) return null;
        return new PersonnelDto
        {
            IdPersonnel = personnel.IdPersonnel,
            Nom = personnel.Nom,
            Prenom = personnel.Prenom,
            Role = personnel.Role
        };
    }

    public async Task<PersonnelDto> CreateAsync(CreatePersonnelDto dto)
    {
        var personnel = new Personnel
        {
            Nom = dto.Nom,
            Prenom = dto.Prenom,
            Role = dto.Role
        };
        var personnelCree = await _personnelRepository.AddAsync(personnel);
        return new PersonnelDto
        {
            IdPersonnel = personnelCree.IdPersonnel,
            Nom = personnelCree.Nom,
            Prenom = personnelCree.Prenom,
            Role = personnelCree.Role
        };
    }

    public async Task<bool> UpdateAsync(int idPersonnel, CreatePersonnelDto dto)
    {
        var personnel = new Personnel
        {
            IdPersonnel = idPersonnel,
            Nom = dto.Nom,
            Prenom = dto.Prenom,
            Role = dto.Role
        };
        return await _personnelRepository.UpdateAsync(personnel);
    }

    public async Task<bool> DeleteAsync(int idPersonnel)
    {
        return await _personnelRepository.DeleteAsync(idPersonnel);
    }
}
