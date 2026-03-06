using System.Data;

namespace Sphere.Application.Common.Interfaces;

/// <summary>
/// DB 커넥션 팩토리 인터페이스 - Dapper 전용
/// </summary>
public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
