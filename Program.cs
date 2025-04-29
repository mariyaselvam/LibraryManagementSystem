using LibraryManagementSystem.Data;
using LibraryManagementSystem.Repositories.Interfaces;
using LibraryManagementSystem.Repositories.Implementations;
using LibraryManagementSystem.Services.Interfaces;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // For Swagger configuration

var builder = WebApplication.CreateBuilder(args);

// Add DbContext - using SQL Server
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Repository Layer
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Add Service Layer
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

// Add controllers
builder.Services.AddControllers();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // You might configure this further later

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add custom exception handling middleware if you made it
// app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();