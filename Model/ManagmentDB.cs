using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerManagmentsys.Model
{
    internal class ManagmentDB : DbContext
    {
        public ManagmentDB(){ }
        public ManagmentDB(DbContextOptions<ManagmentDB> options) : base(options) { }

        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-2008HFE\\SQLEXPRESS;Initial Catalog=TasksDB;Integrated Security=True;Trust Server Certificate=True");
            }
        }
        
    }
    
}
