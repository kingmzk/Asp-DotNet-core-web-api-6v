using MZWalksApi_6.Models.Domain;

namespace MZWalksApi_6.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
