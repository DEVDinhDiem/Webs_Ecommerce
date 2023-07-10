using Ecommerce.AdminApp.ApiIntegration;
using Ecommerce.AdminApp.Services;
using Ecommerce.ViewModels.System.Users;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient();

//builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddControllersWithViews()
		 .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());

builder.Services.AddTransient<Ecommerce.AdminApp.Services.IUserApiClient, UserApiClient>();
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
#if DEBUG
if (environment == Environments.Development)
{
	builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
}
#endif
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
