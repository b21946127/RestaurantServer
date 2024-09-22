using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext to use PostgreSQL
builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register repositories and managers
builder.Services.AddScoped<IMenuDal, EfMenuDal>();
builder.Services.AddScoped<IMenuItemDal, EfMenuItemDal>();
builder.Services.AddScoped<IMenuCategoryDal, EfMenuCategoryDal>();
builder.Services.AddScoped<IMenuItemSetDal, EfMenuItemSetDal>();
builder.Services.AddScoped<IMenuItemOptionDal, EfMenuItemOptionDal>();
builder.Services.AddScoped<IIntegrationDal, EfIntegrationDal>();
builder.Services.AddScoped<IMenuItemMenuItemSetDal, EfMenuItemMenuItemSetDal>();
builder.Services.AddScoped<IMenuService, MenuManager>();
builder.Services.AddScoped<IOrderDal, EfOrderDal>();
builder.Services.AddScoped<IOrderItemDal, EfOrderItemDal>();
builder.Services.AddScoped<IOrderService, OrderManager>();
builder.Services.AddScoped<ICustomerDal, EfCustomerDal>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Global exception handling middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        // Log the exception
        context.Response.StatusCode = 500; // Internal Server Error
        await context.Response.WriteAsync("An error occurred: " + ex.Message);
    }
});

app.UseAuthorization();

app.MapControllers();

app.Run();
