namespace Sphere.Application.DTOs.Lookup;

/// <summary>
/// 제품 타입 Lookup (ACT_PROD)
/// SPC_CODE_MST의 code_class_id = 'ACT_PROD' 데이터
/// </summary>
public class ActProdLookupDto : CodeMasterLookupDto
{
    /// <summary>
    /// 제품 타입 코드 (code_opt)
    /// </summary>
    public string ProductType => CodeOpt;
}

/// <summary>
/// 공정 단계 Lookup (STEP)
/// SPC_CODE_MST의 code_class_id = 'STEP' 데이터
/// </summary>
public class StepLookupDto : CodeMasterLookupDto
{
    /// <summary>
    /// 단계 순서 (code_opt)
    /// </summary>
    public string StepOrder => CodeOpt;
}

/// <summary>
/// 측정 항목 Lookup (ITEM)
/// SPC_CODE_MST의 code_class_id = 'ITEM' 데이터
/// </summary>
public class ItemLookupDto : CodeMasterLookupDto
{
    /// <summary>
    /// 측정 단위 (code_opt)
    /// </summary>
    public string Unit => CodeOpt;
}

/// <summary>
/// 측정 방법 Lookup (MEASM)
/// SPC_CODE_MST의 code_class_id = 'MEASM' 데이터
/// </summary>
public class MeasmLookupDto : CodeMasterLookupDto
{
    /// <summary>
    /// 측정 방법 코드 (code_opt)
    /// </summary>
    public string MethodCode => CodeOpt;
}

/// <summary>
/// 설비 Lookup (EQP)
/// SPC_CODE_MST의 code_class_id = 'EQP' 데이터
/// </summary>
public class EqpLookupDto : CodeMasterLookupDto
{
    /// <summary>
    /// 설비 유형 (code_opt)
    /// </summary>
    public string EquipmentType => CodeOpt;
}

/// <summary>
/// 라인 Lookup (LINE)
/// SPC_CODE_MST의 code_class_id = 'LINE' 데이터
/// </summary>
public class LineLookupDto : CodeMasterLookupDto
{
    /// <summary>
    /// 라인 그룹 (code_opt)
    /// </summary>
    public string LineGroup => CodeOpt;
}

/// <summary>
/// 근무조 Lookup (SHIFT)
/// SPC_CODE_MST의 code_class_id = 'SHIFT' 데이터
/// </summary>
public class ShiftLookupDto : CodeMasterLookupDto
{
    /// <summary>
    /// 근무 시간대 (code_opt)
    /// </summary>
    public string TimeSlot => CodeOpt;
}

/// <summary>
/// 스테이지 Lookup (STAGE)
/// SPC_CODE_MST의 code_class_id = 'STAGE' 데이터
/// </summary>
public class StageLookupDto : CodeMasterLookupDto
{
    /// <summary>
    /// 스테이지 레벨 (code_opt)
    /// </summary>
    public string Level => CodeOpt;
}

/// <summary>
/// 주기 Lookup (FREQUENCY)
/// SPC_CODE_MST의 code_class_id = 'FREQUENCY' 데이터
/// </summary>
public class FrequencyLookupDto : CodeMasterLookupDto
{
    /// <summary>
    /// 주기 단위 (code_opt)
    /// </summary>
    public string PeriodUnit => CodeOpt;
}

/// <summary>
/// 고객사 Lookup (VENDOR)
/// SPC_CODE_MST의 code_class_id = 'VENDOR' 데이터
/// </summary>
public class CustLookupDto : CodeMasterLookupDto
{
    /// <summary>
    /// 고객사 유형 (code_opt)
    /// </summary>
    public string CustomerType => CodeOpt;
}

/// <summary>
/// 최종 사용자 Lookup (END_USER)
/// SPC_CODE_MST의 code_class_id = 'END_USER' 데이터
/// </summary>
public class EndUserLookupDto : CodeMasterLookupDto
{
    /// <summary>
    /// 사용자 분류 (code_opt)
    /// </summary>
    public string UserCategory => CodeOpt;
}

/// <summary>
/// 런룰 마스터 Lookup (RUNRULE)
/// SPC_CODE_MST의 code_class_id = 'RUNRULE' 데이터
/// </summary>
public class RunRuleLookupDto : CodeMasterLookupDto
{
    /// <summary>
    /// 런룰 알고리즘 ID (code_opt)
    /// </summary>
    public string AlgorithmId => CodeOpt;
}

/// <summary>
/// 메뉴 Lookup (MENU)
/// SPC_CODE_MST의 code_class_id = 'MENU' 데이터
/// </summary>
public class MenuLookupDto : CodeMasterLookupDto
{
    /// <summary>
    /// 메뉴 URL/권한 코드 (code_opt)
    /// </summary>
    public string MenuUrl => CodeOpt;
}

/// <summary>
/// 차트 유형 Lookup (PIC_TYPE)
/// SPC_CODE_MST의 code_class_id = 'PIC_TYPE' 데이터
/// </summary>
public class PicTypeLookupDto : CodeMasterLookupDto
{
    /// <summary>
    /// 차트 유형 코드 (code_opt)
    /// </summary>
    public string ChartTypeCode => CodeOpt;
}

/// <summary>
/// 통계 유형 Lookup (STAT_TYPE)
/// SPC_CODE_MST의 code_class_id = 'STAT_TYPE' 데이터
/// </summary>
public class StatTypeLookupDto : CodeMasterLookupDto
{
    /// <summary>
    /// 통계 계산 방식 (code_opt)
    /// </summary>
    public string CalculationMethod => CodeOpt;
}

/// <summary>
/// 관리선 유형 Lookup (CNTLN_TYPE)
/// SPC_CODE_MST의 code_class_id = 'CNTLN_TYPE' 데이터
/// </summary>
public class CntlnTypeLookupDto : CodeMasterLookupDto
{
    /// <summary>
    /// 관리선 계산 방식 (code_opt)
    /// </summary>
    public string ControlLineMethod => CodeOpt;
}

/// <summary>
/// 스펙 유형 Lookup (SPEC_TYPE)
/// SPC_CODE_MST의 code_class_id = 'SPEC_TYPE' 데이터
/// </summary>
public class SpecTypeLookupDto : CodeMasterLookupDto
{
    /// <summary>
    /// 스펙 적용 방식 (code_opt)
    /// </summary>
    public string SpecificationMethod => CodeOpt;
}

/// <summary>
/// 데이터 유형 Lookup (DATA_TYPE)
/// SPC_CODE_MST의 code_class_id = 'DATA_TYPE' 데이터
/// </summary>
public class DataTypeLookupDto : CodeMasterLookupDto
{
    /// <summary>
    /// 데이터 형식 코드 (code_opt)
    /// </summary>
    public string DataFormatCode => CodeOpt;
}
