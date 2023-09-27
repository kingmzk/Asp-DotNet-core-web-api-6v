using MZWalksApi_6.Models.Domain;

namespace MZWalksApi_6.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();

        Task<Walk> GetAsync(Guid id);

        Task<Walk> AddAsync(Walk walk);

        Task<Walk> UpdateAsync(Guid id, Walk walk);

        Task<Walk> DeleteAsync(Guid id);
    }
}