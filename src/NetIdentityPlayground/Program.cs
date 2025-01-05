using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetIdentityPlayground.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Configuración del DbContext para Escritura (TransactionalDbContext)
builder.Services.AddDbContext<TransactionalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TransactionalDb"))
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
);

// Configuración del DbContext para Lectura (ReadOnlyDbContext)
builder.Services.AddDbContext<ReadOnlyDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ReadingDb"))
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<TransactionalDbContext>()
.AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

app.Run();
