using API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace API.Data
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "28d65a5b-a7db-4850-b380-83591f7d7531";
            var writerRoleId = "9740f16c-24a1-4224-a7be-1bb00b7c6892";
        
            // Create Reader and writer roles
            var roles = new List<IdentityRole>{
                new IdentityRole(){
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole(){
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        
            // create admin user
            var adminUserId = Guid.NewGuid().ToString(); // Generate new ID
            var hasher = new PasswordHasher<IdentityUser>();
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@librarytwo.com",
                Email = "admin@librarytwo.com",
                NormalizedEmail = "ADMIN@LIBRARYTWO.COM",
                NormalizedUserName = "ADMIN@LIBRARYTWO.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = false
            };
            admin.PasswordHash = hasher.HashPassword(admin, "Admin123@");
            builder.Entity<IdentityUser>().HasData(admin);
        
            var adminRoles = new List<IdentityUserRole<string>>(){
                new() {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new() {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };
        
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}