using Microsoft.EntityFrameworkCore;
using MailServiceApi.Models;

namespace MailServiceApi.Models;

public class MaileServiceDbContext : DbContext
{
    public MaileServiceDbContext(DbContextOptions options)
        : base(options)
    {

    }

    /// <summary>
    /// Выполнение конфигурации сущностей базы данных
    /// </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder){
        new MessageReportConfigure().Configure(builder.Entity<MessageReport>());
        new MessageRecipientConfigure().Configure(builder.Entity<MessageRecipient>());
        new MessageBodyConfigure().Configure(builder.Entity<MessageBody>());
    }

    /// <summary>
    /// Таблица списока отчетов в БД
    /// </summary>
    public DbSet<MessageReport> MessageReports { get; set; }
    /// <summary>
    /// Таблица списка тем в БД
    /// </summary>
    public DbSet<MessageRecipient> MessageRecipients { get; set; }
    /// <summary>
    /// Таблица списка тел писем в БД
    /// </summary>
    public DbSet<MessageBody> MessageBodies { get; set; }
}