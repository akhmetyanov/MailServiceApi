using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailServiceApi.Models;

/// <summary>
/// Сущность базы данных для хранения данных по телу письма
/// </summary>
public class MessageBody
{
    public MessageBody(Guid messageReportId, string body)
    {
        this.MessageReportId = messageReportId;
        this.Body = body;
    }
    /// <summary>
    /// Id записи в таблице MessageReport
    /// </summary>
    public Guid MessageReportId { get; set; }
    /// <summary>
    /// Само тело письма
    /// </summary>
    public string Body { get; set; }
    /// <summary>
    /// Навигационное свойство для связи с объектом MessageReport
    /// </summary>
    public virtual MessageReport MessageReport {get; set;}
}

/// <summary>
/// Класс для установки конфигурации
/// </summary>
public class MessageBodyConfigure : IEntityTypeConfiguration<MessageBody>
{
    /// <summary>
    /// Установка конфигурации - поле MessageReportId используется как PK
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<MessageBody> builder)
    {
        builder.HasKey(p => p.MessageReportId);

    }
}