namespace AeroManage.Core.DTOs;
public class PassagerDto
{
    public int IdPassager {get; set;} 
    public string Nom { get; set;} = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string Nationalite { get; set ;} = string.Empty;
}

public class CreatePassagerDto
{
    public string Nom { get; set;} = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string Nationalite { get; set ;} = string.Empty;
}