using Microsoft.EntityFrameworkCore;
using MZWalksApi_6.Data;
using MZWalksApi_6.Models.Domain;

namespace MZWalksApi_6.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly MZWalksDbContext mZWalksDbContext;

        public RegionRepository(MZWalksDbContext mZWalksDbContext)
        {
            this.mZWalksDbContext = mZWalksDbContext;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await mZWalksDbContext.Regions.ToListAsync();
        }
    }
}