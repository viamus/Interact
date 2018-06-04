using Interact.Instance.Data.Postgresql.InteractDomain.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Interact.Instance.Data.Postgresql.InteractDomain.Security.User;

namespace Interact.Instance.Data.Postgresql.InteractDomain.Security
{
    public class IdentityInitializer
    {
        private readonly IdentityContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityInitializer(
            IdentityContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            _context.Database.Migrate();

            //if (_context.Database.EnsureCreated())
            //{
            if (!_roleManager.RoleExistsAsync(Roles.ROLE_API_ACCOUNT).Result)
            {
                var response = _roleManager.CreateAsync(
                    new IdentityRole(Roles.ROLE_API_ACCOUNT)).Result;

                if (!response.Succeeded)
                {
                    throw new Exception();
                }
            }

            CreateUser(
                new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin@interact.com.br",
                    EmailConfirmed = true
                }, "1Qazse$", Roles.ROLE_API_ACCOUNT);
            //}
        }

        private void CreateUser(ApplicationUser user, string password, string initialRole = null)
        {
            if (_userManager.FindByNameAsync(user.UserName).Result == null)
            {
                var response = _userManager.CreateAsync(user, password).Result;

                if (response.Succeeded && !String.IsNullOrWhiteSpace(initialRole))
                {
                    _userManager.AddToRoleAsync(user, initialRole).Wait();
                }
            }
        }
    }
}
