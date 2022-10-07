using MailServiceApi;
using MailServiceApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContextFactory<MaileServiceDbContext>(options =>
{
    var connectionString = builder.Configuration.GetSection("DbConnectionString").Value;
    if (connectionString == null || connectionString == "") throw new Exception("Db connection is empty");
    options.UseNpgsql(connectionString);
});

ServicesRegister.Register(builder.Services);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope
        .ServiceProvider
        .GetRequiredService<MaileServiceDbContext>()
        .Database
        .Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
