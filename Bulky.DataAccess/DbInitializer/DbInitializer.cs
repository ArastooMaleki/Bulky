using Bulky.DataAccess.Data;
using Bulky.Models.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public DbInitializer(
            UserManager<IdentityUser> _userManager,
            RoleManager<IdentityRole> _roleManager,
            ApplicationDbContext _dbContext)
        {
            this._userManager = _userManager;
            this._roleManager = _roleManager;
            this._dbContext = _dbContext;
        }
        public void Initialize()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }
            }catch (Exception ex) { }

            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                }

                ApplicationUser MainAdmin = new ApplicationUser() { 
                    UserName = "mainadmin@gmail.com",
                    Email = "mainadmin@gmail.com",
                    Name = "Arma",
                    PhoneNumber="1234567890",
                    StreetAddress = "dfg",
                    State = "agdh",
                    PostalCode = "134514",
                    City = "adh"
                };
                _userManager.CreateAsync(MainAdmin, "Admin1234/").GetAwaiter().GetResult();
                var admin = _dbContext.applicationUsers.FirstOrDefault(u=>u.Email== "mainadmin@gmail.com");
                _userManager.AddToRoleAsync(admin, SD.Role_Admin).GetAwaiter().GetResult();
            }

            return;
        }
    }
}
