using ClubMembershipApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubMembershipApplication.Data
{
    internal class ClassMembershipDBContext : DbContext
    {
        // setting up the database connection string to use a local SQLite database file named "ClassMembership.db" located in the application's base directory.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={AppDomain.CurrentDomain.BaseDirectory}ClassMembership.db");
            base.OnConfiguring(optionsBuilder);
        }

        // defining a DbSet property named Users, which represents the collection of User entities in the database.
        // This allows you to perform CRUD (Create, Read, Update, Delete) operations on the User table in the database using Entity Framework Core.
        public DbSet<User> Users { get; set; }
    }
}
