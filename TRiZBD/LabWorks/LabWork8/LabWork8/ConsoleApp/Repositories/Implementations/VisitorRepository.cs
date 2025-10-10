using ConsoleApp.DataBaseContexts;
using ConsoleApp.Models;
using Dapper;
using System.Data;

namespace ConsoleApp.Repositories.Implementations;

public class VisitorRepository(DatabaseContext dbContext) : IRepository<Visitor>
{
    private readonly DatabaseContext _dbContext = dbContext;

    public Task<int> AddAsync(Visitor entity)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        return await dbConnection.ExecuteScalarAsync<int>("INSERT INTO Visitor (Phone, Name, Birthday, Email) OUTPUT INSERTED.GenreId VALUES (@Title)", entity);
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Visitor>> GetAllAsync()
    {
        using IDbConnection connection = _dbContext.CreateConnection();
        return await connection.QueryAsync<Visitor>("SELECT * FROM Visitor");
    }

    public async Task<Visitor?> GetByIdAsync(int id)
    {
        using IDbConnection connection = _dbContext.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Visitor>("SELECT * FROM Visitor WHERE VisitorId = @id", new { id });
    }

    public Task UpdateAsync(Visitor entity)
    {
        throw new NotImplementedException();
    }
}
