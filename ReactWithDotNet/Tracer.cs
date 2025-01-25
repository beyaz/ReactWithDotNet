using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace ReactWithDotNet;

sealed class Tracer
{
    readonly LinkedList<(string message, long duration)> _items = [];

    readonly Stopwatch stopwatch = new();

    public Tracer()
    {
        stopwatch.Start();
    }

    public long ElapsedMilliseconds => stopwatch.ElapsedMilliseconds;

    public void Trace(string message, long duration)
    {
        _items.AddLast((message, duration));
    }

    public void WriteToResponseHeader(HttpContext httpContext)
    {
        var sb = new StringBuilder();

        foreach (var (message, duration) in _items)
        {
            sb.Append("_;desc=\"");
            sb.Append(message);
            sb.Append("\";dur=");
            sb.Append(duration);
            sb.Append(", ");
        }

        sb.Append("total");
        sb.Append(";dur=");
        sb.Append(ElapsedMilliseconds);

        httpContext.Response.Headers["Server-Timing"] = sb.ToString();
    }
}