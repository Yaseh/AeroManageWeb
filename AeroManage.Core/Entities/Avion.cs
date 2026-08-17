namespace AeroManage.Core.Entities;
public class Avion 
{
    public int IdAvion {get; set; } 
    public string Model { get; set; } = string.Empty;
    public int Capacite { get; set; }
    public string Statut { get; set; } = string.Empty;
}