using Microsoft.EntityFrameworkCore;
using SupermarketCheckout.Application.Services;
using SupermarketCheckout.Model.Repositories;
using SupermarketCheckout.Repositories.Ef;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SupermarketContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SupermarketConnection")));

builder.Services
    .AddScoped<ICheckoutService, CheckoutService>()
    .AddScoped<IItemCatalogRepository, ItemCatalogRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();