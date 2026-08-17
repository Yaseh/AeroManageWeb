using Microsoft.AspNetCore.Mvc;
using AeroManage.Core.Interfaces;
using AeroManage.Core.DTOs;

namespace AeroManage.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;
    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservationAsync()
    {
        var reservations = await _reservationService.GetReservationAsync();
        return Ok(reservations);
    }

    [HttpGet("{id}", Name = "GetReservationById")]
    public async Task<ActionResult<ReservationDto>> GetByIdAsync(int id)
    {
        var reservation = await _reservationService.GetByIdAsync(id);
        if (reservation is null) return NotFound();
        return Ok(reservation);
    }

    [HttpPost]
    public async Task<ActionResult<ReservationDto>> CreateAsync(CreateReservationDto dto)
    {
        var reservation = await _reservationService.CreateAsync(dto);
        return CreatedAtRoute("GetReservationById", new { id = reservation.IdReservation }, reservation);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateAsync(int id, CreateReservationDto dto)
    {
        var resultat = await _reservationService.UpdateAsync(id, dto);
        if (resultat is false) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(int id)
    {
        var resultat = await _reservationService.DeleteAsync(id);
        if (resultat is false) return NotFound();
        return NoContent();
    }
}
