
using EventApp.Application.ViewModels.Companies;
using EventApp.Infrastructure;
using EventApp.Infrastructure.Filters;
using EventApp.Infrastructure.Services.Storage.Local;
using EventApp.Persistence;
using FluentValidation;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
<<<<<<< Updated upstream
builder.Services.AddInfrastructureService();

builder.Services.AddStorage<LocalStorage>();
// builder.Services.AddStorage();

builder.Services.AddCors(opt => opt.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));


builder.Services.AddControllers(op => op.Filters.Add<ValidationFilter>());





=======
builder.Services.AddCors(opt => opt.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000")));
builder.Services.AddControllers();
>>>>>>> Stashed changes
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(opt =>
    {
        opt
            .WithTitle("API Reference")
            .WithTheme( ScalarTheme.DeepSpace)
            
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
    
    
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.Run();
