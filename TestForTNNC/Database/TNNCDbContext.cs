using System;
using Microsoft.EntityFrameworkCore;
using TestForTNNC.Database.Models;

namespace TestForTNNC.Database
{
    public class TNNCDbContext:DbContext
    {
        public TNNCDbContext(DbContextOptions<TNNCDbContext> options) : base(options)
        {
        }
        public DbSet<Position> position { get; set; }
        public DbSet<Division> division { get; set; }
        public DbSet<Workers> workers { get; set; }
        public DbSet<Levels> levels { get; set; }

    }
}
