using Microsoft.EntityFrameworkCore;
using WalksInfoViberBot.Data;
using WalksInfoViberBot.Repositories;
using WalksInfoViberBot.Repositories.Interfaces;
using WalksInfoViberBot.Services;
using WalksInfoViberBot.Services.Interfaces;

var configuration = GetConfiguration();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IWalkRepository, WalkRepository>();

builder.Services.AddTransient<IWalkService, WalkService>();

builder.Services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(configuration["ConnectionString"]));

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "CorsPolicy",
        builder => builder
            .SetIsOriginAllowed((host) => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapControllers();

app.Run();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}