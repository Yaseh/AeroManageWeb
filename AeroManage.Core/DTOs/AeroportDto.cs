namespace AeroManage.Core.DTOs;
public class AeroportDto
{
    public string IdIata {get; set ;} =string.Empty;
    public string Nom { get; set ;} = string.Empty;
    public string Ville { get; set ; } = string.Empty;
    public string Pays { get; set ;} = string.Empty;
}

public class CreateAeroportDto
{
    public string IdIata {get; set ;} =string.Empty;
    public string Nom { get; set ;} = string.Empty;
    public string Ville { get; set ; } = string.Empty;
    public string Pays { get; set ;} = string.Empty;
}