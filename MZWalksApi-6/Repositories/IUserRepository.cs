using MZWalksApi_6.Models.Domain;

namespace MZWalksApi_6.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string userName, string password);
    }
}
