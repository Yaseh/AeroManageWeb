using Microsoft.AspNetCore.Mvc;
using AeroManage.Core.Interfaces;
using AeroManage.Core.DTOs;

namespace AeroManage.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AvionController : ControllerBase
{
    private readonly IAvionService _avionService;
    public AvionController (IAvionService avionService)
    {
        _avionService = avionService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AvionDto>>>GetAvionAsync()
    {
        var avion = await _avionService.GetAvionAsync();
        return Ok (avion);
    }
   [HttpGet("{id}", Name = "GetAvionById")]
    public async Task<ActionResult<AvionDto>>GetByIdAsync(int id)
    {
        var avion = await _avionService.GetByIdAsync(id);
        if (avion is null) return NotFound();
        return Ok (avion);
    }
    [HttpPost]
    public async Task<ActionResult<AvionDto>>CreateAsync(CreateAvionDto dto)
    {
        var avion = await _avionService.CreateAsync(dto);
        return CreatedAtRoute(("GetAvionById"),new{id = avion.IdAvion},avion);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>>UpdateAsync(int id,CreateAvionDto dto)
    {
        var avion = await _avionService.UpdateAsync(id,dto);
        if (avion is false) return NotFound() ;
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>>DeleteAsync(int id)
    {
        var avion = await _avionService.DeleteAsync(id);
        if ( avion is false) return NotFound();
        return NoContent();
    }
}
