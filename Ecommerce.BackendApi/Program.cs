using Microsoft.EntityFrameworkCore;
using Ecommerce.Data.EF;
using Ecommerce.Utilities.Constants;
using Ecommerce.Application.Catalog.Products;
using Microsoft.OpenApi.Models;
using Ecommerce.Application.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EcommerceDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString(SystemConstants.MainConnectionString));
});

//Declare DI
builder.Services.AddTransient<IPublicProductService, PublicProductService>();
builder.Services.AddTransient<IManageProductService, ManageProductService>();
builder.Services.AddTransient<IStorageService, FileStorageService>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger Ecommerce Solution", Version = "v1" });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger EcommerceWeb V1");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
