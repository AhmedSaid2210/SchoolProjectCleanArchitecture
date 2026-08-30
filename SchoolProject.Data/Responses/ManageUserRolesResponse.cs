namespace SchoolProject.Data.Responses
{
    public class ManageUserRolesResponse
    {
        public int UserId { get; set; }
        public List<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
    }
    public class UserRoles
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool HasRole { get; set; }
    }
}
