using System.ComponentModel.DataAnnotations.Schema;

namespace MZWalksApi_6.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string EmailAdress { get; set; }

        public string Password { get; set; }

   //     public List<string> Roles { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        public List<string> Roles { get; set; }

        //Navigation Properties

        public List<User_Role> UserRoles { get; set; }
    }
}
