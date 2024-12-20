using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization; // Added for AuthenticationStateProvider
using Blazored.LocalStorage; // Added for Blazored LocalStorage
using SEP3_T1_BlazorUI;
using SEP3_T1_BlazorUI.Application.Interfaces;
using SEP3_T1_BlazorUI.Application.UseCases;
using SEP3_T1_BlazorUI.Infrastructure.Repositories;
using SEP3_T1_BlazorUI.Infrastructure;
using SEP3_T1_BlazorUI.Presentation.Managers;
using Blazored.Toast;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7203/") // URL of Blazor Server app
});



// Add the authentication state provider as a scoped service
builder.Services.AddAuthorizationCore(); 

builder.Services.AddScoped<InventoryManager>();
builder.Services.AddScoped<LoginManager>();
builder.Services.AddScoped<OrderHistoryManager>();
builder.Services.AddScoped<UserManager>();

// Add repositories
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


// Add use cases
builder.Services.AddScoped<ItemUseCases>();
builder.Services.AddScoped<OrderUseCases>();
builder.Services.AddScoped<UserUseCases>();
builder.Services.AddScoped<AuthUseCases>();



await builder.Build().RunAsync();
