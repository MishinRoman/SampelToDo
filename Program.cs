using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Data.SqlTypes;

using MongoDB.Driver;

using SampelToDo.Data;
using SampelToDo.Services;
using SampelToDo.Services.Interfaces;

using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PostgresDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PslqConnection"));
    options.UseSnakeCaseNamingConvention();

});

builder.Services.AddTransient<ITodoService, TodoPostgresDbSevice>();
builder.Services.AddScoped<DbContext,PostgresDbContext>(); //или builder.Services.AddTransient(typeof(DbContext),typeof(PostgresDbContext));
builder.Services.AddScoped(typeof(MongoUrl), (_) => new MongoUrl(builder.Configuration.GetConnectionString("mongo")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
