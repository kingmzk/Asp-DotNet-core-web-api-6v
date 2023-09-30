using Microsoft.EntityFrameworkCore;
using MZWalksApi_6.Data;
using MZWalksApi_6.Models.Domain;

namespace MZWalksApi_6.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MZWalksDbContext mZWalksDbContext;

        public UserRepository(MZWalksDbContext mZWalksDbContext)
        {
            this.mZWalksDbContext = mZWalksDbContext;
        }

        public async Task<User> AuthenticateAsync(string userName, string password)
        {
            var user = await mZWalksDbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == userName.ToLower() && x.Password == password);

            if(user == null) { return null; }

            var userRoles = await mZWalksDbContext.User_Roles.Where(x => x.UserId == user.Id).ToListAsync();

            if(userRoles.Any()) 
            {
                user.Roles = new List<string>();
                foreach (var userRole in userRoles)
                {
                    var role = await mZWalksDbContext.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if(role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }

            user.Password = null;
            return user;
        }
    }
}
