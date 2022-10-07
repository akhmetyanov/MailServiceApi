using MailServiceApi.Models;

namespace MailServiceApi.Services.MessageReportService;

/// <summary>
/// Контракт для сервиса получения и сохранения отчетов по отправке сообщений
/// </summary>
public interface IMessageReportService
{
    /// <summary>
    /// Получение списка всех отчетов из БД
    /// </summary>
    /// <returns></returns>
    public List<MessageReport> GetReports();
    /// <summary>
    /// Сохранить отчет в БД
    /// </summary>
    /// <param name="report"></param>
    public void SaveReport(MessageReport report);
}