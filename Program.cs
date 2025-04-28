using LibraryManagementSystem.Data;
using LibraryManagementSystem.Repositories.Interfaces;
using LibraryManagementSystem.Repositories.Implementations;
using LibraryManagementSystem.Services.Interfaces;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext - using InMemory for now (easy for practice)
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseInMemoryDatabase("LibraryDb")); // <-- no need for real SQL now

// Add Repository Layer
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Add Service Layer
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

// Add controllers
builder.Services.AddControllers();

// Add Swagger/OpenAPI (optional now, but useful later)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
