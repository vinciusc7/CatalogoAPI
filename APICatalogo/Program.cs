using APICatalogo.Context;
using APICatalogo.Filter;
using APICatalogo.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
                    .AddJsonOptions(options => 
                        options.JsonSerializerOptions
                            .ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ApiLoggingFilter>();

string mysqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
                                    options.UseMySql(mysqlConnection, 
                                    ServerVersion.AutoDetect(mysqlConnection)));

builder.Services.AddTransient<IMeuServico, MeuServico>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//middleware para redirecionar para https
app.UseHttpsRedirection();

//middleware que habilita a autorização
app.UseAuthorization();

//adiciona o middleware que executa o endpoint
app.MapControllers();

app.Run();
