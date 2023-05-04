using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using Presentation;
using Presentation.Filters;
using Repositories;
using Repositories.Concrete;
using Repositories.Contract;
using Services;
using Services.Contract;
using System.Reflection;
using WebAPI.Extensitions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
    {
        options.RespectBrowserAcceptHeader = true;
        options.ReturnHttpNotAcceptable = true;
        options.Filters.AddService(typeof(LogFilter));
    })
    .AddXmlDataContractSerializerFormatters()
    .AddNewtonsoftJson()
    .AddApplicationPart(typeof(PresentationReferece).Assembly);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Logging.ClearProviders();
builder.Host.UseNLog();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.SqlServerConfigure(builder.Configuration);
builder.Services.RepositoryServicesConfigure();
builder.Services.BusinessServicesConfigure();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.ConfigureLogFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

var loggerService = app.Services.GetRequiredService<ILoggerService>();

app.ConfigureExceptionHandler(loggerService); 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
