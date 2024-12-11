using AsemrowendOnline.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AsemrowendOnline.Data
{
    public class RolesDbContext : IdentityDbContext<IdentityUser>
    {
        public RolesDbContext(DbContextOptions<RolesDbContext> options) : base(options) { }
    }
}
