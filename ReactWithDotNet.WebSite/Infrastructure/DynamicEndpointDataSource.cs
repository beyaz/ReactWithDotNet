using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.Primitives;

namespace ReactWithDotNet.WebSite;

sealed class DynamicEndpointDataSource : EndpointDataSource
{
    public static DynamicEndpointDataSource Instance;

    private readonly object _lock = new();

    private CancellationTokenSource _cancellationTokenSource;

    private IChangeToken _changeToken;

    private IReadOnlyList<Endpoint> _endpoints;

    public DynamicEndpointDataSource()
    {
        Instance = this;
        SetEndpoints(Array.Empty<Endpoint>());
    }

    public override IReadOnlyList<Endpoint> Endpoints => _endpoints;

    public static Endpoint CreateEndpoint(string httpMethod, string pattern, RequestDelegate requestDelegate)
    {
        var endpointBuilder = new RouteEndpointBuilder(requestDelegate, RoutePatternFactory.Parse(pattern), 0)
        {
            Metadata =
            {
                new HttpMethodMetadata(new[] { httpMethod })
            }
        };

        return endpointBuilder.Build();
    }

    public static void UseEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.DataSources.Add(new DynamicEndpointDataSource());
    }

    public override IChangeToken GetChangeToken()
    {
        return _changeToken;
    }

    public void SetEndpoints(IReadOnlyList<Endpoint> endpoints)
    {
        lock (_lock)
        {
            var oldCancellationTokenSource = _cancellationTokenSource;

            _endpoints = endpoints;

            _cancellationTokenSource = new CancellationTokenSource();
            _changeToken             = new CancellationChangeToken(_cancellationTokenSource.Token);

            oldCancellationTokenSource?.Cancel();
        }
    }
}