using Sphere.Application.DTOs.Lookup;
using Sphere.Application.DTOs.Master;

namespace Sphere.Application.Interfaces.Repositories;

/// <summary>
/// 코드 마스터 Repository 인터페이스
/// SPC_CODE_MST 테이블에 대한 CRUD 및 Lookup 조회 기능 제공
/// </summary>
public interface ICodeMasterRepository
{
    #region CRUD Operations (기존 메서드)

    /// <summary>
    /// Gets code master list. (USP_SPC_CODE_MST_SELECT)
    /// </summary>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="filter">Optional filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of code masters</returns>
    Task<IEnumerable<CodeMasterDto>> GetCodeMasterListAsync(
        string divSeq,
        CodeMasterFilterDto? filter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single code master by key. (USP_SPC_CODE_MST_SELECT)
    /// </summary>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="codeClassId">Code class ID</param>
    /// <param name="codeId">Code ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Code master or null</returns>
    Task<CodeMasterDto?> GetCodeMasterByIdAsync(
        string divSeq,
        string codeClassId,
        string codeId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new code master. (USP_SPC_CODE_MST_INSERT)
    /// </summary>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="dto">Create data</param>
    /// <param name="userId">User ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Operation result</returns>
    Task<CodeMasterResultDto> CreateCodeMasterAsync(
        string divSeq,
        CreateCodeMasterDto dto,
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing code master. (USP_SPC_CODE_MST_UPDATE)
    /// </summary>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="codeClassId">Code class ID</param>
    /// <param name="codeId">Code ID</param>
    /// <param name="dto">Update data</param>
    /// <param name="userId">User ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Operation result</returns>
    Task<CodeMasterResultDto> UpdateCodeMasterAsync(
        string divSeq,
        string codeClassId,
        string codeId,
        UpdateCodeMasterDto dto,
        string userId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a code master. (USP_SPC_CODE_MST_DELETE)
    /// </summary>
    /// <param name="divSeq">Division sequence</param>
    /// <param name="codeClassId">Code class ID</param>
    /// <param name="codeId">Code ID</param>
    /// <param name="userId">User ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Operation result</returns>
    Task<CodeMasterResultDto> DeleteCodeMasterAsync(
        string divSeq,
        string codeClassId,
        string codeId,
        string userId,
        CancellationToken cancellationToken = default);

    #endregion

    #region 공통 조회 메서드 (2개)

    /// <summary>
    /// 코드 분류별 Lookup 조회
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="codeClassId">코드 분류 ID (ACT_PROD, STEP, ITEM 등)</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부 (기본: true)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>Lookup 데이터 목록</returns>
    Task<IEnumerable<CodeMasterLookupDto>> GetLookupByClassIdAsync(
        string divSeq,
        string codeClassId,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 특정 코드 조회
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="codeClassId">코드 분류 ID</param>
    /// <param name="codeId">코드 ID</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>Lookup 데이터 또는 null</returns>
    Task<CodeMasterLookupDto?> GetByIdAsync(
        string divSeq,
        string codeClassId,
        string codeId,
        string? language = null,
        CancellationToken cancellationToken = default);

    #endregion

    #region 타입별 조회 메서드 (18개)

    /// <summary>
    /// 제품 타입 목록 조회 (ACT_PROD)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>제품 타입 목록</returns>
    Task<IEnumerable<ActProdLookupDto>> GetActProdsAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 공정 단계 목록 조회 (STEP)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>공정 단계 목록</returns>
    Task<IEnumerable<StepLookupDto>> GetStepsAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 측정 항목 목록 조회 (ITEM)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>측정 항목 목록</returns>
    Task<IEnumerable<ItemLookupDto>> GetItemsAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 측정 방법 목록 조회 (MEASM)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>측정 방법 목록</returns>
    Task<IEnumerable<MeasmLookupDto>> GetMeasmsAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 설비 목록 조회 (EQP)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>설비 목록</returns>
    Task<IEnumerable<EqpLookupDto>> GetEqpsAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 라인 목록 조회 (LINE)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>라인 목록</returns>
    Task<IEnumerable<LineLookupDto>> GetLinesAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 근무조 목록 조회 (SHIFT)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>근무조 목록</returns>
    Task<IEnumerable<ShiftLookupDto>> GetShiftsAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 스테이지 목록 조회 (STAGE)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>스테이지 목록</returns>
    Task<IEnumerable<StageLookupDto>> GetStagesAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 주기 목록 조회 (FREQUENCY)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>주기 목록</returns>
    Task<IEnumerable<FrequencyLookupDto>> GetFrequenciesAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 고객사 목록 조회 (VENDOR)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>고객사 목록</returns>
    Task<IEnumerable<CustLookupDto>> GetCustsAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 최종 사용자 목록 조회 (END_USER)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>최종 사용자 목록</returns>
    Task<IEnumerable<EndUserLookupDto>> GetEndUsersAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 런룰 마스터 목록 조회 (RUNRULE)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>런룰 마스터 목록</returns>
    Task<IEnumerable<RunRuleLookupDto>> GetRunRulesAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 메뉴 목록 조회 (MENU)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>메뉴 목록</returns>
    Task<IEnumerable<MenuLookupDto>> GetMenusAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 차트 유형 목록 조회 (PIC_TYPE)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>차트 유형 목록</returns>
    Task<IEnumerable<PicTypeLookupDto>> GetPicTypesAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 통계 유형 목록 조회 (STAT_TYPE)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>통계 유형 목록</returns>
    Task<IEnumerable<StatTypeLookupDto>> GetStatTypesAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 관리선 유형 목록 조회 (CNTLN_TYPE)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>관리선 유형 목록</returns>
    Task<IEnumerable<CntlnTypeLookupDto>> GetCntlnTypesAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 스펙 유형 목록 조회 (SPEC_TYPE)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>스펙 유형 목록</returns>
    Task<IEnumerable<SpecTypeLookupDto>> GetSpecTypesAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 데이터 유형 목록 조회 (DATA_TYPE)
    /// </summary>
    /// <param name="divSeq">사업부 코드</param>
    /// <param name="language">언어 코드 (ko-KR, en-US, zh-CN, vi-VN)</param>
    /// <param name="activeOnly">활성 데이터만 조회 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>데이터 유형 목록</returns>
    Task<IEnumerable<DataTypeLookupDto>> GetDataTypesAsync(
        string divSeq,
        string? language = null,
        bool activeOnly = true,
        CancellationToken cancellationToken = default);

    #endregion
}
