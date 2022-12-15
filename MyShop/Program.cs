using MyShop.Interfaces;
using MyShop.Models;
using MyShop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// IoC
builder.Services.AddScoped(typeof(IRepository<CatalogItem>), typeof(LocalCatalogItemRepository));
builder.Services.AddScoped<ICatalogItemViewModelService, CatalogItemViewModelService>();

var app = builder.Build();

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

app.Run();
