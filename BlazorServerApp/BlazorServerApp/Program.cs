using Blazored.Toast;
using BlazorServerApp.Application.Interfaces;
using BlazorServerApp.Application.UseCases;
using BlazorServerApp.Infrastructure.Repositories;
using BlazorServerApp.Managers;
using Items;
using Microsoft.AspNetCore.Components.Authorization;
using Orders;
using Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddBlazoredToast();
builder.Services.AddServerSideBlazor();

builder.Services.AddAuthorizationCore();

// Register the gRPC clients
builder.Services.AddGrpcClient<AuthService.AuthServiceClient>(options =>
{
    options.Address = new Uri("http://localhost:8090");
});

builder.Services.AddGrpcClient<ItemService.ItemServiceClient>(options =>
{
    options.Address = new Uri("http://localhost:8090");
});

builder.Services.AddGrpcClient<OrderService.OrderServiceClient>(options =>
{
    options.Address = new Uri("http://localhost:8090");
});

builder.Services.AddGrpcClient<UserService.UserServiceClient>(options =>
{
    options.Address = new Uri("http://localhost:8090");
});

// Register the CustomAuthenticationStateProvider
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// Register Repositories, UseCases, and Managers
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<AuthUseCases>();
builder.Services.AddScoped<LoginManager>();

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<ItemUseCases>();
builder.Services.AddScoped<OrderUseCases>();

builder.Services.AddScoped<InventoryManager>();
builder.Services.AddScoped<OrderHistoryManager>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserUseCases>();
builder.Services.AddScoped<UserManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
