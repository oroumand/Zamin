using Serilog;
namespace Zamin.Utilities.SerilogRegistration.Extensions;

public class SerilogExtensions
{
    public static void RunWithSerilogExceptionHandling(Action action, string startUpMessage = "Starting up", string exceptionMessage = "Unhandled exception", string shutdownMessage = "Shutdown complete")
    {
        Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
        Log.Information(startUpMessage);
        try
        {
            action();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, exceptionMessage);
        }
        finally
        {
            Log.Information(shutdownMessage);
            Log.CloseAndFlush();
        }
    }
}
