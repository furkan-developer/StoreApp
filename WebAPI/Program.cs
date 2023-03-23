using Microsoft.EntityFrameworkCore;
using Presentation;
using Repositories;
using Repositories.Concrete;
using Repositories.Contract;
using Services;
using Services.Contract;
using System.Reflection;
using WebAPI.Extensitions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson().AddApplicationPart(typeof(PresentationReferece).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.SqlServerConfigure(builder.Configuration);
builder.Services.RepositoryServicesConfigure();
builder.Services.BusinessServicesConfigure();

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
