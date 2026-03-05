using System.Data;
using Dapper;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// Base class for Dapper repositories that execute stored procedures.
/// </summary>
public abstract class DapperRepositoryBase
{
    protected readonly IDbConnection _connection;

    protected DapperRepositoryBase(IDbConnection connection)
    {
        _connection = connection;
    }

    /// <summary>
    /// Executes a stored procedure and returns multiple results.
    /// </summary>
    /// <typeparam name="T">Result type</typeparam>
    /// <param name="storedProcedure">Stored procedure name</param>
    /// <param name="parameters">Optional parameters</param>
    /// <param name="commandTimeout">Command timeout in seconds</param>
    /// <returns>Collection of results</returns>
    protected async Task<IEnumerable<T>> QueryAsync<T>(
        string storedProcedure,
        object? parameters = null,
        int? commandTimeout = null)
    {
        return await _connection.QueryAsync<T>(
            storedProcedure,
            ConvertToSpParameters(parameters),
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Executes a stored procedure and returns the first result or default.
    /// </summary>
    /// <typeparam name="T">Result type</typeparam>
    /// <param name="storedProcedure">Stored procedure name</param>
    /// <param name="parameters">Optional parameters</param>
    /// <param name="commandTimeout">Command timeout in seconds</param>
    /// <returns>First result or default</returns>
    protected async Task<T?> QueryFirstOrDefaultAsync<T>(
        string storedProcedure,
        object? parameters = null,
        int? commandTimeout = null)
    {
        return await _connection.QueryFirstOrDefaultAsync<T>(
            storedProcedure,
            ConvertToSpParameters(parameters),
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Executes a stored procedure and returns the single result.
    /// </summary>
    /// <typeparam name="T">Result type</typeparam>
    /// <param name="storedProcedure">Stored procedure name</param>
    /// <param name="parameters">Optional parameters</param>
    /// <param name="commandTimeout">Command timeout in seconds</param>
    /// <returns>Single result</returns>
    protected async Task<T> QuerySingleAsync<T>(
        string storedProcedure,
        object? parameters = null,
        int? commandTimeout = null)
    {
        return await _connection.QuerySingleAsync<T>(
            storedProcedure,
            ConvertToSpParameters(parameters),
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Executes a stored procedure without returning results.
    /// </summary>
    /// <param name="storedProcedure">Stored procedure name</param>
    /// <param name="parameters">Optional parameters</param>
    /// <param name="commandTimeout">Command timeout in seconds</param>
    /// <returns>Number of affected rows</returns>
    protected async Task<int> ExecuteAsync(
        string storedProcedure,
        object? parameters = null,
        int? commandTimeout = null)
    {
        return await _connection.ExecuteAsync(
            storedProcedure,
            ConvertToSpParameters(parameters),
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Executes a stored procedure and returns a scalar value.
    /// </summary>
    /// <typeparam name="T">Result type</typeparam>
    /// <param name="storedProcedure">Stored procedure name</param>
    /// <param name="parameters">Optional parameters</param>
    /// <param name="commandTimeout">Command timeout in seconds</param>
    /// <returns>Scalar result</returns>
    protected async Task<T?> ExecuteScalarAsync<T>(
        string storedProcedure,
        object? parameters = null,
        int? commandTimeout = null)
    {
        return await _connection.ExecuteScalarAsync<T>(
            storedProcedure,
            ConvertToSpParameters(parameters),
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Executes a stored procedure with multiple result sets.
    /// </summary>
    /// <param name="storedProcedure">Stored procedure name</param>
    /// <param name="parameters">Optional parameters</param>
    /// <param name="commandTimeout">Command timeout in seconds</param>
    /// <returns>Grid reader for multiple result sets</returns>
    protected async Task<SqlMapper.GridReader> QueryMultipleAsync(
        string storedProcedure,
        object? parameters = null,
        int? commandTimeout = null)
    {
        return await _connection.QueryMultipleAsync(
            storedProcedure,
            ConvertToSpParameters(parameters),
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Executes raw SQL (non-SP) and returns affected row count.
    /// Use when no stored procedure exists for the operation.
    /// </summary>
    protected async Task<int> ExecuteRawSqlAsync(
        string sql,
        object? parameters = null,
        int? commandTimeout = null)
    {
        return await _connection.ExecuteAsync(
            sql,
            parameters,
            commandType: CommandType.Text,
            commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Executes raw SQL (non-SP) and returns the first result or default.
    /// Use when no stored procedure exists for the operation.
    /// </summary>
    protected async Task<T?> QueryFirstOrDefaultRawSqlAsync<T>(
        string sql,
        object? parameters = null,
        int? commandTimeout = null)
    {
        return await _connection.QueryFirstOrDefaultAsync<T>(
            sql,
            parameters,
            commandType: CommandType.Text,
            commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Executes a stored procedure with pre-built DynamicParameters (supports TVP).
    /// Use when parameters include DataTable for Table-Valued Parameters.
    /// </summary>
    protected async Task<T?> QueryFirstOrDefaultWithTvpAsync<T>(
        string storedProcedure,
        DynamicParameters parameters,
        int? commandTimeout = null)
    {
        return await _connection.QueryFirstOrDefaultAsync<T>(
            storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure,
            commandTimeout: commandTimeout);
    }

    /// <summary>
    /// Creates DynamicParameters from an anonymous object with null handling.
    /// </summary>
    /// <param name="parameters">Anonymous object with parameters</param>
    /// <returns>DynamicParameters instance</returns>
    protected static DynamicParameters CreateParameters(object? parameters)
    {
        var dynamicParams = new DynamicParameters();
        if (parameters == null) return dynamicParams;

        var properties = parameters.GetType().GetProperties();
        foreach (var prop in properties)
        {
            var value = prop.GetValue(parameters);
            dynamicParams.Add(prop.Name, value);
        }

        return dynamicParams;
    }

    /// <summary>
    /// Converts anonymous object parameters to DynamicParameters with @P_ prefix for stored procedures.
    /// SQL Server stored procedures expect parameters with @P_ prefix (e.g., @P_div_seq, @P_code_class_id).
    /// </summary>
    /// <param name="parameters">Anonymous object with parameters</param>
    /// <returns>DynamicParameters with @P_ prefixed parameter names</returns>
    protected static DynamicParameters ConvertToSpParameters(object? parameters)
    {
        var dynamicParams = new DynamicParameters();
        if (parameters == null) return dynamicParams;

        var properties = parameters.GetType().GetProperties();
        foreach (var prop in properties)
        {
            var value = prop.GetValue(parameters);
            // SP uses pattern: WHERE (@P_param = '' OR column = @P_param)
            // NULL breaks this pattern (NULL = '' is NULL/falsy), so convert null strings to ""
            if (value is null && prop.PropertyType == typeof(string))
                value = string.Empty;
            // Also handle nullable string (string?)
            if (value is null && Nullable.GetUnderlyingType(prop.PropertyType) == typeof(string))
                value = string.Empty;
            // Add @P_ prefix to match SQL Server stored procedure parameter naming convention
            dynamicParams.Add($"P_{prop.Name}", value);
        }

        return dynamicParams;
    }
}
