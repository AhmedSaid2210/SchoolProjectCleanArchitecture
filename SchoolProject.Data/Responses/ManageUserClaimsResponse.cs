namespace SchoolProject.Data.Responses
{
    public class ManageUserClaimsResponse
    {
        public int UserId { get; set; }
        public List<UserClaims> UserClaims { get; set; } = new List<UserClaims>();
    }
    public class UserClaims
    {
       
        public string Type  { get; set; } = string.Empty;
        public bool Value { get; set; }

    }
}
