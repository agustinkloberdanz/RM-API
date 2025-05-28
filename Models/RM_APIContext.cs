using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net;
using System.Security.Cryptography.Xml;

namespace RM_API.Models
{
    public class RM_APIContext : DbContext
    {
        public RM_APIContext(DbContextOptions<RM_APIContext> options) : base(options)
        {
        }
        public DbSet<Device> Devices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
