using asp_project.Services;
using asp_project.Models;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

var supportedCultures = new[] 
{ 
    CultureInfo.InvariantCulture,
    new CultureInfo("en-US")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(CultureInfo.InvariantCulture);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddControllersWithViews(options =>
{
    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
        _ => "Значение не может быть пустым");
});

builder.Services.AddScoped<ICalculationService, CalculationService>();
builder.Services.AddScoped<IDrawingService, DrawingService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
});

var app = builder.Build();

app.UseRequestLocalization();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Calculation/Error");
}

app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.MapGet("/", context => 
{
    context.Response.Redirect("/calculation");
    return Task.CompletedTask;
});

app.MapControllerRoute(
    name: "calculation",
    pattern: "calculation",
    defaults: new { controller = "Calculation", action = "Index" });

app.MapControllerRoute(
    name: "calculate",
    pattern: "calculate",
    defaults: new { controller = "Calculation", action = "Calculate" });

app.MapControllerRoute(
    name: "result",
    pattern: "result",
    defaults: new { controller = "Calculation", action = "Result" });

app.MapControllerRoute(
    name: "error",
    pattern: "error",
    defaults: new { controller = "Calculation", action = "Error" });

app.Run();

