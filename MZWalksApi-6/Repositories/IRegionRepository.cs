using MZWalksApi_6.Models.Domain;

namespace MZWalksApi_6.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();

        Task<Region> GetAsync(Guid id);

        Task<Region> AddAsync(Region region);

        Task<Region> DeleteAsync(Guid Id);

        Task<Region> UpdateAsync(Guid id, Region region);
    }
}