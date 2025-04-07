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
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/EventApp"));
            configurationManager.AddJsonFile("appsettings.json");
            return configurationManager.GetConnectionString("DatabasePostgres");
            
            }
         }
    }
}