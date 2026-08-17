namespace AeroManage.Core.Entities;
public class Reservation 
{
    public int IdReservation {get; set;} 
    public string Numerosiege { get; set;} = string.Empty;
    public int IdVol { get; set; } 
    public int IdPassager { get; set ;} 
}