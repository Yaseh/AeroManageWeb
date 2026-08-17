namespace AeroManage.Core.Entities;
public class Personnel 
{
    public int IdPersonnel {get; set ;} 
    public string Nom { get; set;} = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string Role { get; set ;} = string.Empty;
}