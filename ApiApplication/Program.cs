using System;
using System.Linq;
using System.Net;
using ApiApplication;
using ApiApplication.Core;
using ApiApplication.Infrastructure;
using ApiApplication.Services;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddGrpc();
// builder.Services.AddHttpLogging(o =>
// {
//     o.LoggingFields = HttpLoggingFields.Duration | HttpLoggingFields.RequestPath | HttpLoggingFields.RequestQuery;
//     o.CombineLogs = true;
// });

builder.Services.AddControllers(mvcOptions => mvcOptions
    .AddResultConvention(resultStatusMap => resultStatusMap
        .AddDefaultMap()
            .For(ResultStatus.Ok, HttpStatusCode.OK, resultStatusOptions => resultStatusOptions
            .For("POST", HttpStatusCode.Created))
            .For(ResultStatus.Error, HttpStatusCode.InternalServerError)
            .For(ResultStatus.Invalid, HttpStatusCode.BadRequest, resultStatusOptions => resultStatusOptions
            .With((_, result) => string.Join("\r\n", result.ValidationErrors.Select(x=>x.ErrorMessage))))
    ));
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

builder.Services.AddStackExchangeRedisOutputCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("CinemaCache");
    options.InstanceName = "Cinema";
});

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder => 
        builder.Expire(TimeSpan.FromSeconds(20)));
});


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpLogging();
app.UseRequestLogging();

app.UseAuthentication();
app.UseAuthorization();
app.UseOutputCache();
app.MapGrpcService<ShowTimeGrpcService>();
app.MapControllers();

SampleData.Initialize(app);

app.Run();