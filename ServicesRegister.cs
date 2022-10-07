using MailServiceApi.Services.MailService;
using MailServiceApi.Services.SmtpService;
using MailServiceApi.Services.MessageReportService;

namespace MailServiceApi;

/// <summary>
/// Класс для регистрации сервисов
/// </summary>
public static class ServicesRegister
{
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<IMailService, MailService>();
        services.AddSingleton<ISmtpService, SmtpService>();
        services.AddSingleton<IMessageReportService, MessageReportService>();
    }
}