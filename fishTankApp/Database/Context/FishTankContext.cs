using fishTankApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fishTankApp.Database.Context
{
    public class FishTankContext : DbContext
    {
        public DbSet<Fish> Fish { get; set; }
        public DbSet<Breed> Breed { get; set; }
        public DbSet<Decoration> Decoration { get; set; }
        public DbSet<Plant> Plant { get; set; }
        public DbSet<FishTank> FishTank { get; set; }


        public FishTankContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
