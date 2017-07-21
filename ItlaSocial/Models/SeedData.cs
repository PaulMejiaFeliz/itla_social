using ItlaSocial.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace ItlaSocial.Models
{
    class SeedData
    {
        public static void Seed(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (!context.Users.Any())
            {
                userManager.CreateAsync(
                    new ApplicationUser()
                    {
                        UserName = "PaulMejia",
                        Name = "Paul",
                        LastName = "Mejia",
                        Email = "admin@admin.admin"
                    },
                "Paul.123"
                );
            }
        }
    }
}