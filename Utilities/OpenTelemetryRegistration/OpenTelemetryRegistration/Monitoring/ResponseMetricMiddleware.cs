using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zamin.Utilities.OpenTelemetryRegistration.Monitoring;
public class ResponseMetricMiddleware(RequestDelegate request)
{
    private readonly RequestDelegate _request = request ?? throw new ArgumentNullException(nameof(request));

    public async Task Invoke(HttpContext httpContext, MetricReporter reporter)
    {
        var path = httpContext.Request.Path.Value;

        if (path == "/metrics")
        {
            await _request.Invoke(httpContext);
            return;
        }
        var sw = Stopwatch.StartNew();

        try
        {
            await _request.Invoke(httpContext);
        }
        finally
        {
            sw.Stop();
            reporter.RegisterRequest(path);
            reporter.RegisterResponseTime(httpContext.Response.StatusCode,
                httpContext.Request.Method, path, sw.Elapsed);
        }
    }

}