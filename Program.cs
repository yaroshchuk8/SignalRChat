using dotenv.net;
using SignalRChat.Components;
using SignalRChat.Hubs;
using SignalRChat.Services;

// load connection strings from .env file into environment variables
DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// get AzureSignalR connection string from environment variables
var connectionString = Environment.GetEnvironmentVariable("AzureSignalR");

// add SignalR Service with its Azure extension and provide connection string
builder.Services.AddSignalR().AddAzureSignalR(connectionString);

var languageKey = Environment.GetEnvironmentVariable("LanguageKey");
var languageEndpoint = Environment.GetEnvironmentVariable("LanguageEndpoint");

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
