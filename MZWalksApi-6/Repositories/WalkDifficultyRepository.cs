using Microsoft.EntityFrameworkCore;
using MZWalksApi_6.Data;
using MZWalksApi_6.Models.Domain;

namespace MZWalksApi_6.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly MZWalksDbContext mZWalksDbContext;

        public WalkDifficultyRepository(MZWalksDbContext mZWalksDbContext)
        {
            this.mZWalksDbContext = mZWalksDbContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await mZWalksDbContext.WalkDifficulty.AddAsync(walkDifficulty);
            await mZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;

        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var existingWalkDifficulty = await mZWalksDbContext.WalkDifficulty.FindAsync(id);
            if (existingWalkDifficulty != null)
            {
                mZWalksDbContext.WalkDifficulty.Remove(existingWalkDifficulty);
                await mZWalksDbContext.SaveChangesAsync();
                return existingWalkDifficulty;
            }

            return null;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await mZWalksDbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await mZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await mZWalksDbContext.WalkDifficulty.FindAsync(id);
            if (existingWalkDifficulty == null)
            {
                return null;
            }

            existingWalkDifficulty.Code = walkDifficulty.Code;

            await mZWalksDbContext.SaveChangesAsync();

            return existingWalkDifficulty;
        }



    }
}
