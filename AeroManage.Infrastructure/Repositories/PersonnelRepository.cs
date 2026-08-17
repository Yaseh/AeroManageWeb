using Dapper;
using AeroManage.Core.Entities;
using AeroManage.Core.Interfaces;
using AeroManage.Infrastructure.Data;

namespace AeroManage.Infrastructure.Repositories;

public class PersonnelRepository : IPersonnelRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    public PersonnelRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Personnel>> GetPersonnelAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Personnel>("SELECT * FROM Personnel");
    }

    public async Task<Personnel?> GetByIdAsync(int idPersonnel)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT * FROM Personnel WHERE IdPersonnel = @IdPersonnel";
        return await connection.QueryFirstOrDefaultAsync<Personnel>(sql, new { IdPersonnel = idPersonnel });
    }

    public async Task<Personnel> AddAsync(Personnel personnel)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "INSERT INTO Personnel (Nom, Prenom, Role) VALUES (@Nom, @Prenom, @Role); SELECT last_insert_rowid();";
        var nouvelId = await connection.ExecuteScalarAsync<int>(sql, personnel);
        personnel.IdPersonnel = nouvelId;
        return personnel;
    }

    public async Task<bool> UpdateAsync(Personnel personnel)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "UPDATE Personnel SET Nom = @Nom, Prenom = @Prenom, Role = @Role WHERE IdPersonnel = @IdPersonnel;";
        var resultat = await connection.ExecuteAsync(sql, personnel);
        return resultat > 0;
    }

    public async Task<bool> DeleteAsync(int idPersonnel)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "DELETE FROM Personnel WHERE IdPersonnel = @IdPersonnel;";
        var resultat = await connection.ExecuteAsync(sql, new { IdPersonnel = idPersonnel });
        return resultat > 0;
    }
}
