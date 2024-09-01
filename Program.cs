using dotenv.net;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Components;
using SignalRChat.Data;
using SignalRChat.Hubs;
using SignalRChat.Services;

// load connection strings from .env file into environment variables
DotEnv.Load();

// extract connection strings, api key and endpoint
var dbConnectionString = Environment.GetEnvironmentVariable("AzureDb");
var signalRConnectionString = Environment.GetEnvironmentVariable("AzureSignalR");
var languageKey = Environment.GetEnvironmentVariable("LanguageKey");
var languageEndpoint = Environment.GetEnvironmentVariable("LanguageEndpoint");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// add database context to DI container
builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(dbConnectionString));

// add SignalR Service with its Azure extension
builder.Services.AddSignalR().AddAzureSignalR(signalRConnectionString);

// add LanguageService to DI container as singleton
builder.Services.AddSingleton<LanguageService>(new LanguageService(
    languageEndpoint,
    languageKey));

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
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapHub<ChatHub>("chathub");

app.Run();
