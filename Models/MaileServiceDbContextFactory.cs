using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MailServiceApi.Models;

public class MaileServiceDbContextFactory : IDesignTimeDbContextFactory<MaileServiceDbContext>
{
    /// <summary>
    /// Метод для создания контекста базы данных
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public MaileServiceDbContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<MaileServiceDbContext>();
        optionBuilder.UseNpgsql();
        return new MaileServiceDbContext(optionBuilder.Options);
    }
}