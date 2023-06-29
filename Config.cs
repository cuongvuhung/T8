using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T8
{
    internal class Config
    {
        public static string dataSource = @"Data Source=10.36.0.36;";
        public static string catalog = "Initial Catalog=cuongvh;";
        public static string user = "User ID=cuongvt;";
        public static string password = "Password=Cupi@731";
        public string conStr = dataSource + catalog + user + password;
        public Config() 
        {
            
        }
    }
}
