using System.Data;
using Dapper;
using AeroManage.Core.Entities;
using AeroManage.Core.Interfaces;
using AeroManage.Infrastructure.Data;
namespace AeroManage.Infrastructure.Repositories;
public class AvionRepository :IAvionRepository
{

    private readonly IDbConnectionFactory _connectionFactory;
    public AvionRepository( IDbConnectionFactory connectionFactory)
{
    _connectionFactory = connectionFactory ;
}

public async Task<IEnumerable<Avion>>GetAvionAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT * FROM Avion";
        return await connection.QueryAsync<Avion>(sql);
    }
    public async Task<Avion?>GetByIdAsync(int idAvion)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql="SELECT * FROM Avion WHERE IdAvion=@IdAvion";
        return await connection.QueryFirstOrDefaultAsync<Avion>(sql,new {IdAvion = idAvion});
    
    }
    
    public async Task<Avion>AddAsync(Avion avion)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql ="INSERT INTO Avion (Model,Capacite,Statut) VALUES (@Model,@Capacite,@Statut); Select last_insert_rowid();";
        var nouvelid= await connection.ExecuteScalarAsync<int>(sql,avion);
        avion.IdAvion = nouvelid;
        return avion;
    }
     public async Task<bool>UpdateAsync(Avion avion)
    {
        using var connection =_connectionFactory.CreateConnection();
        var sql ="UPDATE Avion SET Model=@Model ,Capacite=@Capacite,Statut=@Statut WHERE IdAvion =@IdAvion;";
        var resultat = await connection.ExecuteAsync(sql,avion);
        return resultat>0;
    }

 public async Task<bool> DeleteAsync(int idAvion)
{
    using var connection = _connectionFactory.CreateConnection();
    var sql = "DELETE FROM Avion WHERE IdAvion = @IdAvion;";
    var resultat = await connection.ExecuteAsync(sql, new { IdAvion = idAvion });
    return resultat > 0;
}

}