namespace AeroManage.Core.Entities;
public class Aeroport 
{
    public string IdIata {get; set ;} =string.Empty;
    public string Nom { get; set ;} = string.Empty;
    public string Ville { get; set ; } = string.Empty;
    public string Pays { get; set ;} = string.Empty;
}