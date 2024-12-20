using Blazored.Toast;
using BlazorServerApp.Infrastructure.Repositories;
using BlazorServerApp.Application.UseCases;
using BlazorServerApp.Application.Interfaces;
using BlazorServerApp.Managers;
using Microsoft.AspNetCore.Components.Authorization;
using Orders;
using Users;
using Items;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddBlazoredToast();
builder.Services.AddServerSideBlazor();

builder.Services.AddAuthorizationCore();

// Register the gRPC client for AuthService
builder.Services.AddGrpcClient<AuthService.AuthServiceClient>(options =>
{
    options.Address = new Uri("http://localhost:8090"); // Replace with your Spring Boot gRPC server URL
});

builder.Services.AddGrpcClient<ItemService.ItemServiceClient>(options =>
{
    options.Address = new Uri("http://localhost:8090"); // Replace with your Spring Boot gRPC server URL
});

builder.Services.AddGrpcClient<OrderService.OrderServiceClient>(options =>
{
    options.Address = new Uri("http://localhost:8090"); // Replace with your Spring Boot gRPC server URL
});

builder.Services.AddGrpcClient<UserService.UserServiceClient>(options =>
{
    options.Address = new Uri("http://localhost:8090"); // Ensure this is the correct URL
});

// Register Repositories, UseCases, and Managers using Dependency Injection
builder.Services.AddScoped<IAuthRepository, AuthRepository>(); // Register IAuthRepository
builder.Services.AddScoped<AuthUseCases>(); // Register Auth Use Case
builder.Services.AddScoped<LoginManager>(); // Register LoginManager

builder.Services.AddScoped<IItemRepository, ItemRepository>(); // Register IItemRepository to use ItemRepository
builder.Services.AddScoped<IOrderRepository, OrderRepository>(); // Register IOrderRepository to use OrderRepository

builder.Services.AddScoped<ItemUseCases>(); // Register ItemUseCases
builder.Services.AddScoped<OrderUseCases>(); // Register OrderUseCases

builder.Services.AddScoped<InventoryManager>(); // Register InventoryManager
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// Register OrderHistoryManager
builder.Services.AddScoped<OrderHistoryManager>(); // Register OrderHistoryManager

builder.Services.AddScoped<IUserRepository, UserRepository>(); // Register IUserRepository with UserRepository
builder.Services.AddScoped<UserUseCases>(); // Register UserUseCases
builder.Services.AddScoped<UserManager>(); // Register UserManager

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
