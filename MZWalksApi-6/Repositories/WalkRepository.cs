using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MZWalksApi_6.Data;
using MZWalksApi_6.Models.Domain;

namespace MZWalksApi_6.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly MZWalksDbContext mZWalksDbContext;

        public WalkRepository(MZWalksDbContext mZWalksDbContext)
        {
            this.mZWalksDbContext = mZWalksDbContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            //Assign new ID
            walk.Id = new Guid();
            await mZWalksDbContext.Walks.AddAsync(walk);
            await mZWalksDbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk = await mZWalksDbContext.Walks.FindAsync(id);

            if (existingWalk == null)
            {
                return null;
            }
            mZWalksDbContext.Walks.Remove(existingWalk);
            await mZWalksDbContext.SaveChangesAsync();

            return existingWalk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await mZWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            return await mZWalksDbContext.Walks
                  .Include(x => x.Region)
                 .Include(x => x.WalkDifficulty)
                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await mZWalksDbContext.Walks.FindAsync(id);

            if (existingWalk != null)
            {
                existingWalk.Length = walk.Length;
                existingWalk.Name = walk.Name;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                existingWalk.RegionId = walk.RegionId;
                await mZWalksDbContext.SaveChangesAsync();
                return walk;
            }

            return null;
        }
    }
}