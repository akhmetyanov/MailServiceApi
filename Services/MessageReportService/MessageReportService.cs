using MailServiceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MailServiceApi.Services.MessageReportService;

public class MessageReportService : IMessageReportService
{
    IDbContextFactory<MaileServiceDbContext> contextFactory;
    public MessageReportService(IDbContextFactory<MaileServiceDbContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public List<MessageReport> GetReports()
    {
        var context = contextFactory.CreateDbContext();
        var reports = context
            .MessageReports
            .Include(r => r.MessageBody)
            .Include(r => r.MessageRecipients)
            .ToList();

        return reports;
    }
    public void SaveReport(MessageReport report)
    {
        var context = contextFactory.CreateDbContext();
        context.MessageReports.Add(report);
        context.SaveChanges();
    }
}