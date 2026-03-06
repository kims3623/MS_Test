using Sphere.Application.DTOs.Account;

namespace Sphere.Application.Interfaces.Repositories;

/// <summary>
/// 계정 요청 리포지토리 인터페이스
/// </summary>
public interface IAccountRepository
{
    /// <summary>
    /// 동일 이메일의 대기중 계정 요청 존재 여부 확인
    /// </summary>
    Task<bool> HasPendingRequestByEmailAsync(
        string email,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 협력사 계정 요청 등록
    /// </summary>
    Task InsertVendorAccountRequestAsync(
        string divSeq,
        string requestId,
        string vendorName,
        string? vendorId,
        string contactName,
        string contactEmail,
        string? contactPhone,
        string? description,
        DateTime requestedAt,
        CancellationToken cancellationToken = default);
}
