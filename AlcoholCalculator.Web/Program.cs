using AlcoholCalculator.Shared.Services;
using AlcoholCalculator.Web.Components;
using AlcoholCalculator.Web.Services;
using AlcoholCalculator.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IStorageService, WebStorageService>();
builder.Services.AddScoped<IDrinkService, DrinkService>();
builder.Services.AddScoped<IAlcoholCalculatorService, AlcoholCalculatorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(AlcoholCalculator.Shared._Imports).Assembly);

app.Run();