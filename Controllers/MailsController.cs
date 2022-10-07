using Microsoft.AspNetCore.Mvc;
using MailServiceApi.Dtos;
using MailServiceApi.Services.MailService;
using MailServiceApi.Services.MessageReportService;

namespace MailServiceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MailsController : ControllerBase
{
    IMailService mailService;
    IMessageReportService reportService;
    public MailsController(IMailService mailService, IMessageReportService reportService)
    {
        this.mailService = mailService;
        this.reportService = reportService;
    }
    /// <summary>
    /// Метод для получения списка всех сообщений из БД
    /// </summary>
    /// <returns>Список отчетов отправки сообщений</returns>
    [HttpGet]
    public IActionResult Get()
    {
        this.reportService.GetReports();
        return Ok(this.reportService.GetReports());
    }

    /// <summary>
    /// Метод получения данных для составления mail
    /// На вход получаем: тему, списко адресов и тело сообщения
    /// </summary>
    /// <param name="mail"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Send(MailDto mail)
    {
        var report = this.mailService.Send(mail);
        return Ok(report);
    }
}
