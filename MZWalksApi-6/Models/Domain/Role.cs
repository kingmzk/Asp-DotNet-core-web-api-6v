namespace MZWalksApi_6.Models.Domain
{
    public class Role
    {
        public Guid Id { get; set; }

        public string Name { get; set; }



        //Navigation Properties
        public List<User_Role> UserRoles { get; set; }
    }
}
