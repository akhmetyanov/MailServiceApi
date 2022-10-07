namespace MailServiceApi.Dtos;

/// <summary>
/// Класс для получения данных с клиента
/// </summary>
public class MailDto {
    public MailDto() {
        Subject = string.Empty;
        Body = string.Empty;
        Recipients = new List<string>();
    }
    /// <summary>
    /// Тема письма
    /// </summary>
    public string Subject { get; set; }
    /// <summary>
    /// Тело письма
    /// </summary>
    public string Body { get; set; }
    /// <summary>
    /// Список адресов
    /// </summary>
    public List<string> Recipients { get; set; }
    
}