using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
// builder.Services.AddDbContext<DataContext>(opt =>
// {
//     opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection");
// })

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    // app.UseDeveloperExceptionPage();
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
