using Dapper;
using AeroManage.Core.Entities;
using AeroManage.Core.Interfaces;
using AeroManage.Infrastructure.Data;

namespace AeroManage.Infrastructure.Repositories;

public class AeroportRepository : IAeroportRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    public AeroportRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Aeroport>> GetAeroportAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Aeroport>("SELECT * FROM Aeroport");
    }

    public async Task<Aeroport?> GetByIdAsync(string idIata)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT * FROM Aeroport WHERE IdIata = @IdIata";
        return await connection.QueryFirstOrDefaultAsync<Aeroport>(sql, new { IdIata = idIata });
    }

    public async Task<Aeroport> AddAsync(Aeroport aeroport)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "INSERT INTO Aeroport (IdIata, Nom, Ville, Pays) VALUES (@IdIata, @Nom, @Ville, @Pays);";
        await connection.ExecuteAsync(sql, aeroport);
        return aeroport;
    }

    public async Task<bool> UpdateAsync(Aeroport aeroport)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "UPDATE Aeroport SET Nom = @Nom, Ville = @Ville, Pays = @Pays WHERE IdIata = @IdIata;";
        var resultat = await connection.ExecuteAsync(sql, aeroport);
        return resultat > 0;
    }

    public async Task<bool> DeleteAsync(string idIata)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "DELETE FROM Aeroport WHERE IdIata = @IdIata;";
        var resultat = await connection.ExecuteAsync(sql, new { IdIata = idIata });
        return resultat > 0;
    }
}
