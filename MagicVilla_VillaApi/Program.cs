using Microsoft.OpenApi.Models;
using Serilog;
using Microsoft.EntityFrameworkCore;
using MagicVilla_VillaApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//TO CREARE A LOG FILE....

//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
//    .WriteTo.File("log/villaLogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();

builder.Host.UseSerilog();  

builder.Services.AddControllers(option =>
{
    //option.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();  // Enables API explorer for Swagger
builder.Services.AddSwaggerGen();  // Enables OpenAPI (Swagger UI)
//builder.Services.AddSingleton<ILogging, Logging>();  

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Generates the OpenAPI JSON
    app.UseSwaggerUI();  // Serves the Swagger UI
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
