using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SchoolProject.Data.Helper
{
    public static class ClaimsStore
    {
        public static List<Claim> claims = new()
        {
            new Claim("Create Student","false"),
            new Claim("Update Student","false"),
            new Claim("Delete Student","false"),
        };
    }
}
