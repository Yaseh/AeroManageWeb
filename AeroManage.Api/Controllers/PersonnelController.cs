using Microsoft.AspNetCore.Mvc;
using AeroManage.Core.Interfaces;
using AeroManage.Core.DTOs;

namespace AeroManage.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonnelController : ControllerBase
{
    private readonly IPersonnelService _personnelService;
    public PersonnelController(IPersonnelService personnelService)
    {
        _personnelService = personnelService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonnelDto>>> GetPersonnelAsync()
    {
        var personnels = await _personnelService.GetPersonnelAsync();
        return Ok(personnels);
    }

    [HttpGet("{id}", Name = "GetPersonnelById")]
    public async Task<ActionResult<PersonnelDto>> GetByIdAsync(int id)
    {
        var personnel = await _personnelService.GetByIdAsync(id);
        if (personnel is null) return NotFound();
        return Ok(personnel);
    }

    [HttpPost]
    public async Task<ActionResult<PersonnelDto>> CreateAsync(CreatePersonnelDto dto)
    {
        var personnel = await _personnelService.CreateAsync(dto);
        return CreatedAtRoute("GetPersonnelById", new { id = personnel.IdPersonnel }, personnel);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateAsync(int id, CreatePersonnelDto dto)
    {
        var resultat = await _personnelService.UpdateAsync(id, dto);
        if (resultat is false) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(int id)
    {
        var resultat = await _personnelService.DeleteAsync(id);
        if (resultat is false) return NotFound();
        return NoContent();
    }
}
