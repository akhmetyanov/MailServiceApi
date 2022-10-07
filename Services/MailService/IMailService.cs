using MailServiceApi.Dtos;
using MailServiceApi.Models;

namespace MailServiceApi.Services.MailService;

/// <summary>
/// Интерфейс для сервиса писем
/// Реализует только один метод - Send.
/// Метод отправяет сообщения по протоколу SMTP
/// </summary>
public interface IMailService
{
    public MessageReport Send(MailDto mail);
}