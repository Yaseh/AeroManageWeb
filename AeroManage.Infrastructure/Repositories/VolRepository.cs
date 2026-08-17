using Dapper;
using AeroManage.Core.Entities;
using AeroManage.Core.Interfaces;
using AeroManage.Infrastructure.Data;

namespace AeroManage.Infrastructure.Repositories;

public class VolRepository : IVolRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    public VolRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Vol>> GetVolAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Vol>("SELECT * FROM Vol");
    }

    public async Task<Vol?> GetByIdAsync(int idVol)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT * FROM Vol WHERE IdVol = @IdVol";
        return await connection.QueryFirstOrDefaultAsync<Vol>(sql, new { IdVol = idVol });
    }

    public async Task<Vol> AddAsync(Vol vol)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "INSERT INTO Vol (NumeroVol, DateDepart, DateArrivee, Statut, AeroportDepart, AeroportArrivee, IdAvion, IdCommandant) " +
                  "VALUES (@NumeroVol, @DateDepart, @DateArrivee, @Statut, @AeroportDepart, @AeroportArrivee, @IdAvion, @IdCommandant); " +
                  "SELECT last_insert_rowid();";
        var nouvelId = await connection.ExecuteScalarAsync<int>(sql, vol);
        vol.IdVol = nouvelId;
        return vol;
    }

    public async Task<bool> UpdateAsync(Vol vol)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "UPDATE Vol SET NumeroVol = @NumeroVol, DateDepart = @DateDepart, DateArrivee = @DateArrivee, " +
                  "Statut = @Statut, AeroportDepart = @AeroportDepart, AeroportArrivee = @AeroportArrivee, " +
                  "IdAvion = @IdAvion, IdCommandant = @IdCommandant WHERE IdVol = @IdVol;";
        var resultat = await connection.ExecuteAsync(sql, vol);
        return resultat > 0;
    }

    public async Task<bool> DeleteAsync(int idVol)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "DELETE FROM Vol WHERE IdVol = @IdVol;";
        var resultat = await connection.ExecuteAsync(sql, new { IdVol = idVol });
        return resultat > 0;
    }
}
