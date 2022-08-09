using app23Receiver.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app23Receiver.DataContext
{
    public class UserDbContext : DbContext
    {
        public DbSet<UserClass> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyUsers2Db;Integrated Security=True;");
        }

    }
}
