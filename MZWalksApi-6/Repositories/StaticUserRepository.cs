using MZWalksApi_6.Models.Domain;

namespace MZWalksApi_6.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> Users = new List<User>()
        {
            new User()
            {
                FirstName="Read Only", LastName="User", EmailAdress="readonly@user.com", Id=Guid.NewGuid(), Username="readonly@user.com", Password="readonly@user0"
                , Roles = new List<string> { "reader" }
            },
            new User()
            {
                FirstName="Read Write", LastName="User", EmailAdress="readwrite@user.com", Id=Guid.NewGuid(), Username="readwrite@user.com", Password="readwrite@user"
                , Roles = new List<string> { "reader", "writer" }
            }

        };


        public async Task<User> AuthenticateAsync(string userName, string password)
        {
            var user =  Users.Find(x => x.Username.Equals(userName, StringComparison.InvariantCultureIgnoreCase)
            && x.Password.Equals(password, StringComparison.InvariantCultureIgnoreCase));

                return user;

        }
    }
}
