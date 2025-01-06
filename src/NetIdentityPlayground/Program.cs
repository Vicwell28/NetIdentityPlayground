using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NetIdentityPlayground.Domain.Interfaces;
using NetIdentityPlayground.Domain.Services;
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

builder.Services.AddScoped<IUserService, UserService>();

// Add services to the container.
builder.Services.AddControllers();

// *** Configuración de Swagger ***
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Identity Playground",
        Version = "v1",
        Description = "Identity Playground"
    });

    // Habilita las anotaciones
    c.EnableAnnotations();
});

// *** Registro de MediatR ***
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity Playground v1");
    c.RoutePrefix = string.Empty;
});


// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

app.Run();
