using Dapper;
using AeroManage.Core.Entities;
using AeroManage.Core.Interfaces;
using AeroManage.Infrastructure.Data;

namespace AeroManage.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    public ReservationRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Reservation>> GetReservationAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Reservation>("SELECT * FROM Reservation");
    }

    public async Task<Reservation?> GetByIdAsync(int idReservation)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT * FROM Reservation WHERE IdReservation = @IdReservation";
        return await connection.QueryFirstOrDefaultAsync<Reservation>(sql, new { IdReservation = idReservation });
    }

    public async Task<Reservation> AddAsync(Reservation reservation)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "INSERT INTO Reservation (Numerosiege, IdVol, IdPassager) VALUES (@Numerosiege, @IdVol, @IdPassager); SELECT last_insert_rowid();";
        var nouvelId = await connection.ExecuteScalarAsync<int>(sql, reservation);
        reservation.IdReservation = nouvelId;
        return reservation;
    }

    public async Task<bool> UpdateAsync(Reservation reservation)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "UPDATE Reservation SET Numerosiege = @Numerosiege, IdVol = @IdVol, IdPassager = @IdPassager WHERE IdReservation = @IdReservation;";
        var resultat = await connection.ExecuteAsync(sql, reservation);
        return resultat > 0;
    }

    public async Task<bool> DeleteAsync(int idReservation)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "DELETE FROM Reservation WHERE IdReservation = @IdReservation;";
        var resultat = await connection.ExecuteAsync(sql, new { IdReservation = idReservation });
        return resultat > 0;
    }
}
