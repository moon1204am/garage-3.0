using Microsoft.EntityFrameworkCore;
using Garage.Data.Data;
using Garage2._0.Extensions;
using Garage2._0.AutoMapperConfig;
using Garage2._0.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(GarageMappings));

builder.Services.AddDbContext<Garage2_0Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Garage2_0Context") ?? throw new InvalidOperationException("Connection string 'Garage2_0Context' not found.")));


builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IVehicleTypeSelectListService, VehicleTypeSelectListService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    await app.SeedDataAsync();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Vehicles}/{action=Index}/{id?}");

app.Run();
