namespace AeroManage.Core.Entities;
public class Vol 
{
    public int IdVol {get; set;} 
    public string NumeroVol { get; set;} = string.Empty;
    public string DateDepart { get; set; } = string.Empty;
    public string DateArrivee {get;set;}=string.Empty;
    public string Statut { get; set ;} = string.Empty;
    public string AeroportDepart { get; set;}= string.Empty;
    public string AeroportArrivee { get; set;}=string.Empty;
    public int IdAvion {get; set;}
    public int IdCommandant {get;set;}
}