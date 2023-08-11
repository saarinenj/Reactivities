using Application.Activities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(opt =>
{
   opt.AddPolicy("CorsPolicy", policy =>
   {
        // allow any method or any header from the react client-app running in localhost port 3000
        policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
   });
});

builder.Services.AddMediatR(typeof(List.Handler));


// builder.Services.AddDbContext<DataContext>(opt =>
// {
//     opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection");
// })

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// scope in which service can be used
using var scope = app.Services.CreateScope();

// services to be used within scope
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    // same as "dotnet ef database migrate" command on command line
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
} 
catch(Exception ex)
{
    // logging against class Program
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}
app.Run();
