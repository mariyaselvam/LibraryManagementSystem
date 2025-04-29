using LibraryManagementSystem.Data;
using LibraryManagementSystem.Services.Interfaces;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // For Swagger configuration

var builder = WebApplication.CreateBuilder(args);

// Add DbContext - using SQL Server
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register AutoMapper and its profiles
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());  // Register AutoMapper

// Add Repository Layer (only once)
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Add Service Layer
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Add controllers
builder.Services.AddControllers();

// Add Swagger/OpenAPI (configuration for API documentation)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library Management System API",
        Version = "v1",
        Description = "API for managing books and authors in a library system."
    });
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Management System API v1");
        c.RoutePrefix = string.Empty; // To serve Swagger UI at the root (optional)
    });
}

// Add custom exception handling middleware if you created one
// app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

// Map controllers to routes
app.MapControllers();

app.Run();