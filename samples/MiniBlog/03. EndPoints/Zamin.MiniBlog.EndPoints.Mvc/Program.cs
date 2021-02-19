using Zamin.EndPoints.Web;

namespace Zamin.MiniBlog.EndPoints.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new ZaminProgram().Main(args, typeof(Startup), "appsettings.json", "appsettings.hamoon.json", "appsettings.serilog.json");
        }
    }
}
