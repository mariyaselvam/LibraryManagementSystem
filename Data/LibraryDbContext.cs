using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Data
{
    public class LibraryDbContext : IdentityDbContext<ApplicationUser>
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public static async Task SeedAsync(LibraryDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Check if the database is already seeded
            if (!context.Users.Any())
            {
                // Create Admin role if not exist
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                // Create Staff role if not exist
                if (!await roleManager.RoleExistsAsync("Staff"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Staff"));
                }

                // Create a default admin user
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@library.com",
                    Email = "admin@library.com",
                    FullName = "Admin User"
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@1234"); // Default password
                if (result.Succeeded)
                {
                    // Assign the Admin role to the new user
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
          }

        }
}
