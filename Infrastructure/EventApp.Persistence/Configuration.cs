using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EventApp.Persistence
{
    static class Configuration
    {
        public static string  ConnectionString
         {
            
            get
            {
                ConfigurationManager configurationManager = new ();
            configurationManager.SetBasePath(Directory.GetCurrentDirectory());
            configurationManager.AddJsonFile("appsettings.json");
            var connectionString = configurationManager.GetConnectionString("DatabasePostgres");
            return connectionString;
            }
         }
    }
}