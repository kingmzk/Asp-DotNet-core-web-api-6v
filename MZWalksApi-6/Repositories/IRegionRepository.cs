using MZWalksApi_6.Models.Domain;

namespace MZWalksApi_6.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}