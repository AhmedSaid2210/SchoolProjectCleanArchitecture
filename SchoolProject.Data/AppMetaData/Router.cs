using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "Api";
        public const string version = "V1";
        public const string Rule = root+"/"+version+"/";
        public const string SingleRoute = "/{id}";

        public static class StudentRouting
        {
            public const string Prefix  = Rule+"Student/";
            public const string List    = Prefix + "List";
            public const string GetById = Prefix + "GetById"+ SingleRoute;
            public const string AddStudent = Prefix + "AddStudent";
            public const string Update = Prefix + "Update";
            public const string Delete = Prefix + "Delete"+ SingleRoute;
            public const string Paginated = Prefix + "Paginated";

        }

    }
}
