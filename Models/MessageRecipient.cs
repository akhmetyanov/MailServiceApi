using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailServiceApi.Models;

/// <summary>
/// Сущность базы данных для хранения адресата письма
/// </summary>
public class MessageRecipient
{
    public MessageRecipient(Guid messageReportId, string recipient)
    {
        this.MessageReportId = messageReportId;
        this.Recipient = recipient;
    }
    /// <summary>
    /// Id записи в таблице MessageReport
    /// </summary>
    public Guid MessageReportId { get; set; }
    /// <summary>
    /// Адресат
    /// </summary>
    public string Recipient { get; set; }
    /// <summary>
    /// Навигационное свойство для связи с объектом MessageReport
    /// </summary>
    public virtual MessageReport MessageReport { get; set; }
}

/// <summary>
/// Класс для установки конфигурации
/// </summary>
public class MessageRecipientConfigure : IEntityTypeConfiguration<MessageRecipient>
{
    /// <summary>
    /// Установка конфигурации - поля MessageReportId, Recipient используются как PK
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<MessageRecipient> builder)
    {
        builder.HasKey(p => new { p.MessageReportId, p.Recipient });
    }
}