using Microsoft.EntityFrameworkCore;
using Serilog;
using UmDoisTresVendas.Application.Interfaces;
using UmDoisTresVendas.Application.Mappings;
using UmDoisTresVendas.Application.Services;
using UmDoisTresVendas.Application.Validations;
using UmDoisTresVendas.Infrastructure.Data;
using UmDoisTresVendas.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Setting up log
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() 
    .WriteTo.Console()
    .CreateLogger();

// Use Serilog for logging
builder.Host.UseSerilog(Log.Logger);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the DbContext from Infrastructure
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<SaleDtoValidator>();

// Adding AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
