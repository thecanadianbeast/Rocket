using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rocket.Models;

namespace Rocket {
    public class Program {
        public static void Main (string[] args) {
            CreateWebHostBuilder (args).Build ().Run ();
            // var dbColumn = new David_appContext();
            // var columns = dbColumn.Columns.ToList();
            // foreach (var c in columns) {
            //     System.Console.WriteLine($"Status:{c.Status} Type:{c.BuildingType}");
            // }
        }

        public static IWebHostBuilder CreateWebHostBuilder (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .UseStartup<Startup> ();
    }
}