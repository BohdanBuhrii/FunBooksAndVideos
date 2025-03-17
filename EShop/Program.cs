using EShop.Database;
using EShop.Interfaces;
using EShop.Repositories;
using EShop.Services;
using EShop.Strategies;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database configurations
builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
  new SqlConnectionFactory(builder.Configuration["EShopConnectionString"]!));

builder.Services.AddScoped<ICustomerMembershipsRepository, CustomerMembershipsRepository>();
builder.Services.AddScoped<IMembershipsRepository, MembershipsRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IOrderValidator, OrderValidator>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();

builder.Services.AddScoped<IPurchaseOrderProcessor, PurchaseOrderProcessor>();

builder.Services.AddScoped<IPurchaseOrderStrategy, CreateOrderStrategy>(); 
builder.Services.AddScoped<IPurchaseOrderStrategy, MembershipStrategy>();
builder.Services.AddScoped<IPurchaseOrderStrategy, ShippingStrategy>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
