using Microsoft.EntityFrameworkCore;
using MZWalksApi_6.Data;
using MZWalksApi_6.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MZWalksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MZWalks"));
});

builder.Services.AddScoped<IRegionRepository, RegionRepository>();

builder.Services.AddScoped<IWalkRepository, WalkRepository>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

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