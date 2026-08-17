namespace AeroManage.Core.Entities;
public class Passager 
{
    public int IdPassager {get; set;} 
    public string Nom { get; set;} = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string Nationalite { get; set ;} = string.Empty;
}