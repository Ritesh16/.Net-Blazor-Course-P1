using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Tangy_Common;
using Tangy_Data;
using Tangy_Data.Entities;
using TangyWeb_Server.Services.Interfaces;

namespace TangyWeb_Server.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        public DbInitializer(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext context)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
                if (!_roleManager.RoleExistsAsync(Tangy_Common.Constants.Role_Admin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(Tangy_Common.Constants.Role_Admin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(Tangy_Common.Constants.Role_Customer)).GetAwaiter().GetResult();
                }
                else
                {
                    return;
                }

                ApplicationUser user = new()
                {
                    UserName = "user@tangy.com",
                    Email = "user@tangy.com",
                    EmailConfirmed = true
                };

                _userManager.CreateAsync(user, "Welcome@1234").GetAwaiter().GetResult();

                _userManager.AddToRoleAsync(user, Tangy_Common.Constants.Role_Admin).GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {

            }
        }
    }
}
