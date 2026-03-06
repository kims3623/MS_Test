using System.Data;
using Microsoft.Data.SqlClient;
using Sphere.Application.Common.Interfaces;

namespace Sphere.Infrastructure.Persistence;

/// <summary>
/// SQL Server 커넥션 팩토리 구현체
/// </summary>
public class SqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
