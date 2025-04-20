using Microsoft.AspNetCore.Identity;
using WebBookStore.Models;

namespace WebBookStore.Data
{
    public class seed
    {
        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "Employee", "Customer", "Company" };
            foreach (var role in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Tạo tài khoản admin nếu chưa có
            string adminEmail = "admin@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var user = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    Name = "Admin toàn hệ thống",
                    Address = "123 Admin Street",
                    Gender = 1,
                    Birthday = new DateOnly(1990, 12, 19)
                };

                var result = await userManager.CreateAsync(user, "Admin@123"); // password mạnh
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

    }
}
