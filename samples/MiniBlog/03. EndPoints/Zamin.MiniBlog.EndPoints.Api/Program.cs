using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zamin.EndPoints.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Zamin.MiniBlog.EndPoints.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new ZaminProgram().Main(args, typeof(Startup), "appsettings.json", "appsettings.zamin.json", "appsettings.serilog.json");
        }


    }
}
