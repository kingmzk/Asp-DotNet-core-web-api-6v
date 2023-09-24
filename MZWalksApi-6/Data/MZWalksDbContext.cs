using Microsoft.EntityFrameworkCore;
using MZWalksApi_6.Models.Domain;

namespace MZWalksApi_6.Data
{
    public class MZWalksDbContext : DbContext
    {
        public MZWalksDbContext(DbContextOptions<MZWalksDbContext> options) : base(options)
        {
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }
    }
}