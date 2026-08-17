using Microsoft.AspNetCore.Mvc;
using AeroManage.Core.Interfaces;
using AeroManage.Core.DTOs;

namespace AeroManage.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AeroportController : ControllerBase
{
    private readonly IAeroportService _aeroportService;
    public AeroportController(IAeroportService aeroportService)
    {
        _aeroportService = aeroportService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AeroportDto>>> GetAeroportsAsync()
    {
        var aeroports = await _aeroportService.GetAeroportsAsync();
        return Ok(aeroports);
    }

    [HttpGet("{id}", Name = "GetAeroportById")]
    public async Task<ActionResult<AeroportDto>> GetByIdAsync(string id)
    {
        var aeroport = await _aeroportService.GetByIdAsync(id);
        if (aeroport is null) return NotFound();
        return Ok(aeroport);
    }

    [HttpPost]
    public async Task<ActionResult<AeroportDto>> CreateAsync(CreateAeroportDto dto)
    {
        var aeroport = await _aeroportService.CreateAsync(dto);
        return CreatedAtRoute("GetAeroportById", new { id = aeroport.IdIata }, aeroport);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateAsync(string id, CreateAeroportDto dto)
    {
        var resultat = await _aeroportService.UpdateAsync(id, dto);
        if (resultat is false) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(string id)
    {
        var resultat = await _aeroportService.DeleteAsync(id);
        if (resultat is false) return NotFound();
        return NoContent();
    }
}
