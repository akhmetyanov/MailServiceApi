using MailServiceApi.Dtos;
using System.Net.Mail;

namespace MailServiceApi.Services.SmtpService;

/// <summary>
/// Сервис для работы Smtp протколом
/// </summary>
public interface ISmtpService {
    /// <summary>
    /// На основе MailDto создается объект MailMessage
    /// </summary>
    /// <param name="mail"></param>
    /// <returns></returns>
    public MailMessage GetMessage(MailDto mail);
    
    /// <summary>
    /// Получение SmtpClient
    /// </summary>
    /// <returns></returns>
    public SmtpClient GetSmtpClient();
}