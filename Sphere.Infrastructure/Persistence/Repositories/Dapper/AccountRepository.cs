using System.Data;
using Dapper;
using Sphere.Application.Interfaces.Repositories;

namespace Sphere.Infrastructure.Persistence.Repositories.Dapper;

/// <summary>
/// 계정 요청 리포지토리 - Dapper raw SQL 구현체
/// 테이블: SPC_VENDOR_ACCOUNT_REQUEST
/// </summary>
public class AccountRepository : DapperRepositoryBase, IAccountRepository
{
    public AccountRepository(IDbConnection connection) : base(connection) { }

    public async Task<bool> HasPendingRequestByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT COUNT(1)
            FROM SPC_VENDOR_ACCOUNT_REQUEST
            WHERE CONTACT_EMAIL = @Email
              AND STATUS  = 'PENDING'
              AND USE_YN  = 'Y'";

        var count = await _connection.ExecuteScalarAsync<int>(
            new CommandDefinition(sql, new { Email = email }, cancellationToken: cancellationToken));
        return count > 0;
    }

    public async Task InsertVendorAccountRequestAsync(
        string divSeq,
        string requestId,
        string vendorName,
        string? vendorId,
        string contactName,
        string contactEmail,
        string? contactPhone,
        string? description,
        DateTime requestedAt,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
            INSERT INTO SPC_VENDOR_ACCOUNT_REQUEST
                (DIV_SEQ, REQUEST_ID, VENDOR_NAME, VENDOR_ID,
                 CONTACT_NAME, CONTACT_EMAIL, CONTACT_PHONE,
                 DESCRIPTION, STATUS, REQUESTED_AT,
                 USE_YN, CREATE_USER_ID, CREATE_DATE)
            VALUES
                (@DivSeq, @RequestId, @VendorName, @VendorId,
                 @ContactName, @ContactEmail, @ContactPhone,
                 @Description, 'PENDING', @RequestedAt,
                 'Y', 'SYSTEM', GETDATE())";

        await _connection.ExecuteAsync(
            new CommandDefinition(sql,
                new
                {
                    DivSeq       = divSeq,
                    RequestId    = requestId,
                    VendorName   = vendorName,
                    VendorId     = vendorId,
                    ContactName  = contactName,
                    ContactEmail = contactEmail,
                    ContactPhone = contactPhone,
                    Description  = description,
                    RequestedAt  = requestedAt
                },
                cancellationToken: cancellationToken));
    }
}
