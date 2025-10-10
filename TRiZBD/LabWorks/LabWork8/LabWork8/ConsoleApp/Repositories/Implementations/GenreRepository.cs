using ConsoleApp.DataBaseContexts;
using ConsoleApp.Models;
using Dapper;
using System.Data;

namespace ConsoleApp.Repositories.Implementations;

public class GenreRepository(DatabaseContext dbContext) : IRepository<Genre>
{
    private readonly DatabaseContext _dbContext = dbContext;

    public async Task<int> AddAsync(Genre entity)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        return await dbConnection.ExecuteScalarAsync<int>("INSERT INTO Genre (Title) OUTPUT INSERTED.GenreId VALUES (@Title)", entity);
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Genre>> GetAllAsync()
    {
        using IDbConnection connection = _dbContext.CreateConnection();
        return await connection.QueryAsync<Genre>("SELECT * FROM Genre");
    }

    public async Task<Genre?> GetByIdAsync(int id)
    {
        using IDbConnection connection = _dbContext.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Genre>("SELECT * FROM Genre WHERE GenreId = @id", new { id });
    }

    public Task UpdateAsync(Genre entity)
    {
        throw new NotImplementedException();
    }
}
