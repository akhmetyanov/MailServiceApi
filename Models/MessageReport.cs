using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailServiceApi.Models;

/// <summary>
/// Сущность базы данных для хранения данных по письмам
/// </summary>
public class MessageReport
{
    public MessageReport( )
    {
        this.Id = new Guid();
    }
    /// <summary>
    /// Id письма
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Адрес отправителя
    /// </summary>
    public string MessageFrom { get; set; }
    /// <summary>
    /// Тема письма
    /// </summary>
    public string Subject { get; set; }
    /// <summary>
    /// Результат отправка. Успешно - "Ok", нет - "Failed"
    /// </summary>
    public string Result { get; set; }
    /// <summary>
    /// Если возника во время отправки письма, то текст ошибки будет записан сюда
    /// </summary>
    public string FailedMessage { get; set; }
    /// <summary>
    /// Дата отправки
    /// </summary>
    public DateTime SendDate { get; set; }
    /// <summary>
    /// Навигационное поле для свзяи с MessageBody
    /// </summary>
    public virtual MessageBody MessageBody { get; set; }
    /// <summary>
    /// Навигационное поле для свзяи с MessageRecipients
    /// </summary>
    public virtual ICollection<MessageRecipient> MessageRecipients { get; set; }
}

/// <summary>
/// Класс для установки конфигурации
/// </summary>
public class MessageReportConfigure : IEntityTypeConfiguration<MessageReport>
{   
    /// <summary>
    /// Установка конфигурации: 
    /// Поле Id как Pk
    /// И настройка связей с таблицами тела писем и списка адресатов
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<MessageReport> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.MessageBody)
            .WithOne(p => p.MessageReport)
            .HasForeignKey<MessageBody>(p => p.MessageReportId);

        builder.HasMany(p => p.MessageRecipients)
            .WithOne(p => p.MessageReport)
            .HasForeignKey(p => p.MessageReportId);
    }
}