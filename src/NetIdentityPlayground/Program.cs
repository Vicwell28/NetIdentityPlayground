using Microsoft.EntityFrameworkCore;
using NetIdentityPlayground.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TransactionalDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("TransactionalDb"))
);

builder.Services.AddDbContext<ReadOnlyDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ReadingDb"))
);


// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
