using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using GEO.API;
using GEO.TESTE;
using api.Configuracao;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

string databaseUrl = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT_CONNECTSTRING");

if (databaseUrl == null)
{
    databaseUrl = builder.Configuration.GetConnectionString("DefaultConnection");
}


builder.Services.AddDbContext<Contexto>(options =>
    options.UseNpgsql(databaseUrl, npgsqlOptions =>
        npgsqlOptions.MigrationsAssembly(typeof(Program).Assembly.FullName)
    )
);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
