﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await mZWalksDbContext.AddAsync(region);
            await mZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid Id)
        {
            var region = await mZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
            if (region == null)
            {
                return null;
            }
            //Delete here
            mZWalksDbContext.Regions.Remove(region);
            await mZWalksDbContext.SaveChangesAsync();

            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await mZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await mZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await mZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await mZWalksDbContext.SaveChangesAsync();

            return existingRegion;
        }

        public async Task<Region> UpdatesAsync(Guid id, Region region)
        {
            // Find the region by its ID in the database
            var existingRegion = await mZWalksDbContext.Regions.FindAsync(id);

            if (existingRegion == null)
            {
                return null; // Region not found
            }

            // Apply the changes from the provided 'region' object to the 'existingRegion'
            existingRegion.Code = region.Code;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Name = region.Name;
            existingRegion.Population = region.Population;

            try
            {
                // Save the changes to the database asynchronously
                await mZWalksDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return existingRegion; // Return the updated region
        }
    }
}