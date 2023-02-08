using Ebook_Model.Constant;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook_Model.Seeder
{
    public  class DbSeeder
    {
        public static async Task SeederDefault(IServiceProvider service)
        {
           var userMgr =  service.GetService<UserManager<IdentityUser>>();
            var roleMgr = service.GetService<RoleManager<IdentityRole>>();
            await  roleMgr.CreateAsync(new IdentityRole(UserRoles.Admin.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(UserRoles.User.ToString()));

            // create admin user

            var admin = new IdentityUser()
            {
                UserName ="admin@gmail.com",
                Email ="admin@gmail.com",
                EmailConfirmed = true,
            };
             var userdb =  userMgr.FindByEmailAsync(admin.Email);
            if (userdb !=null)
            {
               await userMgr.CreateAsync(admin, "Qwerty12&");
               await userMgr.AddToRoleAsync(admin,UserRoles.Admin.ToString());
            }
        }
    }
}
