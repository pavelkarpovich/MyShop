using Microsoft.EntityFrameworkCore;
using MyShop.ApplicationCore.Entities;
using MyShop.ApplicationCore.Interfaces;
using MyShop.Configuration;
using MyShop.Infrastructure.Data;
using MyShop.Interfaces;
using MyShop.Services;

var builder = WebApplication.CreateBuilder(args);

MyShop.Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

// Add services to the container.
builder.Services.AddControllersWithViews();

// IoC
builder.Services.AddCoreServices();
builder.Services.AddScoped(typeof(IRepository<CatalogItem>), typeof(EfRepository<CatalogItem>));
builder.Services.AddScoped<ICatalogItemViewModelService, CatalogItemViewModelService>();

var app = builder.Build();

app.Logger.LogInformation("App created...");
app.Logger.LogInformation("Database migration running...");
using(var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var catalogContext = scopedProvider.GetRequiredService<CatalogContext>();
        if (catalogContext.Database.IsSqlServer())
        {
            catalogContext.Database.Migrate();
        }
        await CatalogContextSeed.SeedAsync(catalogContext, app.Logger);
    }
    catch(Exception ex) 
    {
        app.Logger.LogError(ex, "An error occurred adding migration to Database");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalog}/{action=Index}/{id?}");

app.Logger.LogDebug("Starting the app...");

app.Run();
