namespace AeroManage.Core.DTOs;
public class AvionDto 
{
    public int IdAvion {get; set; } 
    public string Model { get; set; } = string.Empty;
    public int Capacite { get; set; }
    public string Statut { get; set; } = string.Empty;
}


public class CreateAvionDto
{
    public string Model { get; set; } = string.Empty;
    public int Capacite { get; set; }
    public string Statut { get; set; } = string.Empty;
}