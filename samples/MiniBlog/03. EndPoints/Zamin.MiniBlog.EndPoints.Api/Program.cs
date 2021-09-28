using Zamin.EndPoints.Web;

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
