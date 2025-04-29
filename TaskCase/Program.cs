using HospitalManagement.Persistence;
using Microsoft.EntityFrameworkCore;
using TaskCase.Application;
using TaskCase.Domain.Entities;
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


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.Migrate();

    if (!db.Products.Any())
    {
        db.Products.Add(new Product
        {
            Name = "Kalem 1",
            Price = 100,
            Stock = 100,
            CreatedDate = DateTime.UtcNow,
            Guid = Guid.NewGuid(),
            UpdatedDate = DateTime.UtcNow,
        });
        db.SaveChanges();
    }
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/taskCase_V1/swagger.json", "TaskCase V1");
    });
}
app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
