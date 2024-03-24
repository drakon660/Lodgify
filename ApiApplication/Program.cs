using ApiApplication;
using ApiApplication.Core;
using ApiApplication.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Marker));
builder.Services.AddDbContext<CinemaContext>(options =>
    {
        options.UseInMemoryDatabase("CinemaDb")
            .EnableSensitiveDataLogging()
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
    });
builder.Services.AddCinema();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

SampleData.Initialize(app);

app.Run();