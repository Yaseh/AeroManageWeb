using Microsoft.AspNetCore.Mvc;
using AeroManage.Core.Interfaces;
using AeroManage.Core.DTOs;

namespace AeroManage.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PassagerController : ControllerBase
{
    private readonly IPassagerService _passagerService;
    public PassagerController(IPassagerService passagerService)
    {
        _passagerService = passagerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PassagerDto>>> GetPassagerAsync()
    {
        var passagers = await _passagerService.GetPassagerAsync();
        return Ok(passagers);
    }

    [HttpGet("{id}", Name = "GetPassagerById")]
    public async Task<ActionResult<PassagerDto>> GetByIdAsync(int id)
    {
        var passager = await _passagerService.GetByIdAsync(id);
        if (passager is null) return NotFound();
        return Ok(passager);
    }

    [HttpPost]
    public async Task<ActionResult<PassagerDto>> CreateAsync(CreatePassagerDto dto)
    {
        var passager = await _passagerService.CreateAsync(dto);
        return CreatedAtRoute("GetPassagerById", new { id = passager.IdPassager }, passager);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateAsync(int id, CreatePassagerDto dto)
    {
        var resultat = await _passagerService.UpdateAsync(id, dto);
        if (resultat is false) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(int id)
    {
        var resultat = await _passagerService.DeleteAsync(id);
        if (resultat is false) return NotFound();
        return NoContent();
    }
}
