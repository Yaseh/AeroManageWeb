namespace AeroManage.Core.DTOs;
public class PersonnelDto 
{
    public int IdPersonnel {get; set ;} 
    public string Nom { get; set;} = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string Role { get; set ;} = string.Empty;
}

public class CreatePersonnelDto
{
    public string Nom { get; set;} = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public string Role { get; set ;} = string.Empty;
}