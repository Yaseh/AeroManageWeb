using Microsoft.AspNetCore.Mvc;
using AeroManage.Core.Interfaces;
using AeroManage.Core.DTOs;

namespace AeroManage.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VolController : ControllerBase
{
    private readonly IVolService _volService;
    public VolController(IVolService volService)
    {
        _volService = volService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VolDto>>> GetVolAsync()
    {
        var vols = await _volService.GetVolAsync();
        return Ok(vols);
    }

    [HttpGet("{id}", Name = "GetVolById")]
    public async Task<ActionResult<VolDto>> GetByIdAsync(int id)
    {
        var vol = await _volService.GetByIdAsync(id);
        if (vol is null) return NotFound();
        return Ok(vol);
    }

    [HttpPost]
    public async Task<ActionResult<VolDto>> CreateAsync(CreateVolDto dto)
    {
        var vol = await _volService.CreateAsync(dto);
        return CreatedAtRoute("GetVolById", new { id = vol.IdVol }, vol);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateAsync(int id, CreateVolDto dto)
    {
        var resultat = await _volService.UpdateAsync(id, dto);
        if (resultat is false) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(int id)
    {
        var resultat = await _volService.DeleteAsync(id);
        if (resultat is false) return NotFound();
        return NoContent();
    }
}
