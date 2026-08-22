using Microsoft.EntityFrameworkCore;
using TestTask.Calculators;
using TestTask.Csv;
using TestTask.Data;
using TestTask.Exceptions;
using TestTask.Repository;
using TestTask.Services;
using TestTask.Validators;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddScoped<IFileImportService, FileImportService>();
builder.Services.AddScoped<IResultCalculator, ResultCalculator>();
builder.Services.AddScoped<IFileImportRepository, FileImportRepository>();
builder.Services.AddScoped<ICsvFileReader, CsvFileReader>();
builder.Services.AddScoped<CsvValidate>();
builder.Services.AddScoped<IResultsService, ResultsService>();
builder.Services.AddScoped<IResultsRepository, ResultsRepository>();

builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();