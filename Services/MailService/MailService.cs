using System.Net.Mail;
using MailServiceApi.Dtos;
using MailServiceApi.Models;
using MailServiceApi.Services.SmtpService;
using MailServiceApi.Services.MessageReportService;

namespace MailServiceApi.Services.MailService;

/// <summary>
/// Реализация интерфейса сервиса отправки писем.
/// Добавлены зависимости на интерфейсы ISmtpService, IMessageReportService
/// ISmtpService - для получения клиента SMTP и сообщения
/// IMessageReportService - для сохранения отчета по результатам отправки сообщения
/// </summary>
public class MailService : IMailService
{
    ISmtpService smtpService;
    IMessageReportService reportService;
    public MailService(ISmtpService smtpService, IMessageReportService reportService)
    {
        this.smtpService = smtpService;
        this.reportService = reportService;
    }
    /// <summary>
    /// Реаолизация метода интерфейса IMailService
    /// Обработаны ошибки на каждом этапе отправки сообщения
    /// </summary>
    /// <param name="mail"></param>
    public MessageReport Send(MailDto mail)
    {
        MailMessage message;
        SmtpClient client;

        var report = new MessageReport();
        report.Subject = mail.Subject;
        report.MessageBody = new MessageBody
        (
            report.Id,
            mail.Body
        );
        report.MessageRecipients = mail.Recipients.Select(recipient =>
            new MessageRecipient(report.Id, recipient)).ToList();

        try
        {
            message = smtpService.GetMessage(mail);
            client = smtpService.GetSmtpClient();
        }
        catch (Exception e)
        {
            report.FailedMessage = e.ToString();
            report.Result = "Failed";
            report.SendDate = DateTime.Now;
            reportService.SaveReport(report);
            return report;
        }

        try
        {
            client.Send(message);
            report.Result = "Ok";
        }
        catch (Exception e)
        {
            report.FailedMessage = e.ToString();
            report.Result = "Failed";
        }
        finally
        {
            report.MessageFrom = message.From.Address;
            message.Dispose();
            client.Dispose();

            report.SendDate = DateTime.Now;
            reportService.SaveReport(report);
        }

        return report;
    }
}