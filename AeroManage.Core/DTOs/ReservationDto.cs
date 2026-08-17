namespace AeroManage.Core.DTOs;
public class ReservationDto
{
    public int IdReservation {get; set;}
    public string Numerosiege { get; set;} = string.Empty;
    public int IdVol { get; set; }
    public int IdPassager { get; set ;}
}

public class CreateReservationDto
{
    public string Numerosiege { get; set; } = string.Empty;
    public int IdVol { get; set; }
    public int IdPassager { get; set; }
}