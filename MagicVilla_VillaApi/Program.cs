using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();  // Enables API explorer for Swagger
builder.Services.AddSwaggerGen();  // Enables OpenAPI (Swagger UI)

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
