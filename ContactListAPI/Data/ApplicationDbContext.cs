using ContactListAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ContactGroups> ContactGroups { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
