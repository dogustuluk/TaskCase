using HospitalManagement.Persistence;
using Microsoft.EntityFrameworkCore;
using TaskCase.Application;
using TaskCase.Extensions.StartupExtensions;
using TaskCase.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseNpgsql(
        builder.Configuration
               .GetConnectionString("PostgreSQL")
    )
);
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();
builder.Services.HttpLoggingOptionsStartupExtension();
builder.Services.JsonOptionsStartupExtension();
builder.Services.AddEndpointsApiExplorer();
builder.Services.SwaggerOptionsExtension();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/taskCase_V1/swagger.json", "TaskCase V1");
    });

    //app.UseSwaggerUI();
}
app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
