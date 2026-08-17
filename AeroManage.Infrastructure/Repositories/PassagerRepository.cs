using Dapper;
using AeroManage.Core.Entities;
using AeroManage.Core.Interfaces;
using AeroManage.Infrastructure.Data;

namespace AeroManage.Infrastructure.Repositories;

public class PassagerRepository : IPassagerRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    public PassagerRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Passager>> GetPassagerAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Passager>("SELECT * FROM Passager");
    }

    public async Task<Passager?> GetByIdAsync(int idPassager)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT * FROM Passager WHERE IdPassager = @IdPassager";
        return await connection.QueryFirstOrDefaultAsync<Passager>(sql, new { IdPassager = idPassager });
    }

    public async Task<Passager> AddAsync(Passager passager)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "INSERT INTO Passager (Nom, Prenom, Nationalite) VALUES (@Nom, @Prenom, @Nationalite); SELECT last_insert_rowid();";
        var nouvelId = await connection.ExecuteScalarAsync<int>(sql, passager);
        passager.IdPassager = nouvelId;
        return passager;
    }

    public async Task<bool> UpdateAsync(Passager passager)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "UPDATE Passager SET Nom = @Nom, Prenom = @Prenom, Nationalite = @Nationalite WHERE IdPassager = @IdPassager;";
        var resultat = await connection.ExecuteAsync(sql, passager);
        return resultat > 0;
    }

    public async Task<bool> DeleteAsync(int idPassager)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "DELETE FROM Passager WHERE IdPassager = @IdPassager;";
        var resultat = await connection.ExecuteAsync(sql, new { IdPassager = idPassager });
        return resultat > 0;
    }
}
