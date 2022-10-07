using System.Net;
using System.Net.Mail;
using MailServiceApi.Dtos;

namespace MailServiceApi.Services.SmtpService;

/// <summary>
/// Реализация сервиса получения SMTP клиента
/// </summary>
public class SmtpService : ISmtpService
{
    IConfiguration conf;

    string senderAddress;
    string senderDisplayName;
    string userName;
    string password;
    string host;
    int port;
    bool enableSSL;
    bool useDefaultCredentials;
    bool isBodyHTML;

    public SmtpService(IConfiguration conf)
    {
        this.conf = conf;
        this.initConfFeilds();
    }

    void initConfFeilds()
    {
        var smtpSettings = conf.GetSection("SmtpSettings");
        senderAddress = smtpSettings.GetSection("SenderAddress").Value;
        senderDisplayName = smtpSettings.GetSection("SenderDisplayName").Value;
        userName = smtpSettings.GetSection("UserName").Value;
        password = smtpSettings.GetSection("Password").Value;
        host = smtpSettings.GetSection("Host").Value;
        int.TryParse(smtpSettings.GetSection("Port").Value, out port);
        enableSSL = Convert.ToBoolean(smtpSettings.GetSection("EnableSSL").Value);
        useDefaultCredentials = Convert.ToBoolean(smtpSettings.GetSection("UseDefaultCredentials").Value);
        isBodyHTML = Convert.ToBoolean(smtpSettings.GetSection("IsBodyHTML").Value);
    }
    /// <summary>
    /// На основе MailDto и данных полученных appsettings.json составляется MailMessage
    /// </summary>
    /// <param name="mailDto"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public MailMessage GetMessage(MailDto mailDto)
    {
        if (this.senderAddress == null || this.senderAddress == "")
        {
            throw new Exception("senderAddress is null or empty");
        }

        if (this.senderDisplayName == null || this.senderDisplayName == "")
        {
            throw new Exception("senderDisplayName is null or empty");
        }

        MailMessage mail = new MailMessage
        {
            Subject = mailDto.Subject,
            Body = mailDto.Body,
            From = new MailAddress(senderAddress, senderDisplayName),
            IsBodyHtml = isBodyHTML
        };

        foreach (var toEmail in mailDto.Recipients)
        {
            mail.To.Add(toEmail);
        }

        mail.BodyEncoding = System.Text.Encoding.Default;
        return mail;

    }

    /// <summary>
    /// На основе данных из appsettings.json создается SmtpClient
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public SmtpClient GetSmtpClient()
    {
        if (this.userName == null || this.userName == "")
        {
            throw new Exception("userName is null or empty");
        }

        if (this.password == null || this.password == "")
        {
            throw new Exception("password is null or empty");
        }

        if (this.host == null || this.host == "")
        {
            throw new Exception("host is null or empty");
        }

        if (this.port == 0)
        {
            throw new Exception("port is null or empty");
        }

        NetworkCredential networkCredential = new NetworkCredential(this.userName, this.password);
        SmtpClient smtpClient = new SmtpClient
        {
            Host = this.host,
            Port = this.port,
            EnableSsl = this.enableSSL,
            UseDefaultCredentials = this.useDefaultCredentials,
            Credentials = networkCredential
        };
        
        return smtpClient;
    }
}