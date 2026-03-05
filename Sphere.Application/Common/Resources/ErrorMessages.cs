namespace Sphere.Application.Common.Resources;

public static class ErrorMessages
{
    private static readonly Dictionary<string, Dictionary<string, string>> Messages = new()
    {
        ["validation.failed"] = new()
        {
            ["ko-KR"] = "유효성 검사에 실패했습니다.",
            ["en-US"] = "Validation failed.",
            ["vi-VN"] = "Xác thực thất bại.",
            ["zh-CN"] = "验证失败。"
        },
        ["error.general"] = new()
        {
            ["ko-KR"] = "요청 처리 중 오류가 발생했습니다.",
            ["en-US"] = "An error occurred while processing your request.",
            ["vi-VN"] = "Đã xảy ra lỗi khi xử lý yêu cầu của bạn.",
            ["zh-CN"] = "处理请求时发生错误。"
        },
        ["error.notFound"] = new()
        {
            ["ko-KR"] = "요청한 리소스를 찾을 수 없습니다.",
            ["en-US"] = "The requested resource was not found.",
            ["vi-VN"] = "Không tìm thấy tài nguyên được yêu cầu.",
            ["zh-CN"] = "未找到请求的资源。"
        },
        ["error.unauthorized"] = new()
        {
            ["ko-KR"] = "인증이 필요합니다.",
            ["en-US"] = "Authentication is required.",
            ["vi-VN"] = "Yêu cầu xác thực.",
            ["zh-CN"] = "需要身份验证。"
        },
        ["error.forbidden"] = new()
        {
            ["ko-KR"] = "이 작업에 대한 권한이 없습니다.",
            ["en-US"] = "You do not have permission for this action.",
            ["vi-VN"] = "Bạn không có quyền thực hiện hành động này.",
            ["zh-CN"] = "您没有执行此操作的权限。"
        }
    };

    public static string Get(string key, string locale)
    {
        if (Messages.TryGetValue(key, out var translations))
        {
            if (translations.TryGetValue(locale, out var msg))
                return msg;
            if (translations.TryGetValue("ko-KR", out var fallback))
                return fallback;
        }
        return key;
    }
}
