namespace ReactWithDotNet.WebSite.Pages;

using h1 = BlogH1;
using p = BlogP;

sealed class PageReactContexts : PureComponent
{
    protected override Element render()
    {
        return new BlogPageLayout
        {
            new h1 { "React context" },
            SpaceY(8),
            new FlexColumn
            {
                new p
                {
                    "React Context is a powerful feature that allows you to manage and share state or data across a component tree without having to pass props down manually at every level.",
                    br,
                    br,
                    "ReactWithDotNet has only one context. Every component can access this context",
                    br,
                    "Let's show you on an example how to manage db connection."
                },

                SpaceY(16),
                
                new CodeViewer(CodeViewer.LangCSharp, Height(70), MD(Width(550)))
                {
                    """
                    /// <summary>
                    ///      Defines a key for access db connection instance
                    /// </summary>
                    static ReactContextKey<IDbConnection> DbConnection = new(nameof(DbConnection));
                    """
                },
                SpaceY(16),
                new p
                {
                    "When request started"
                },
                
                new CodeViewer(CodeViewer.LangCSharp, Height(240), MD(Width(550)))
                {
                    """
                    /// <summary>
                    ///      Every component request enter this method.
                    /// </summary>
                    static Task HandleReactWithDotNetRequest(HttpContext httpContext)
                    {
                        httpContext.Response.ContentType =  "application/json; charset=utf-8";
                    
                        return ProcessReactWithDotNetComponentRequest(new()
                        {
                            HttpContext           = httpContext,
                            OnReactContextCreated = OnReactContextCreated,
                            OnReactContextDisposed = OnReactContextDisposed
                        });
                    }
                    """
                },
                
                SpaceY(16),
                
                new p
                {
                    "initialize db connection"
                },
                
                new CodeViewer(CodeViewer.LangCSharp, Height(140), MD(Width(550)))
                {
                    """
                    static Task OnReactContextCreated(ReactContext context)
                    {
                        var httpContext = context.HttpContext;
                        
                        context.Set(DbConnection, CreateDbConnection(httpContext));
                        
                        return Task.CompletedTask;
                    }
                    """
                },
                
                SpaceY(16),
                new p
                {
                    "Before send response to client"
                },
                
                new CodeViewer(CodeViewer.LangCSharp, Height(290), MD(Width(550)))
                {
                    """
                    static Task OnReactContextDisposed(ReactContext context, Exception exception)
                    {
                        var dbConnection = DbConnection[context];
                    
                        if (exception == null)
                        {
                            // commit
                        }
                        else
                        {
                            // rollback
                        }
                        
                        dbConnection?.Dispose();
                        
                        return Task.CompletedTask;
                    }
                    """
                },
                
                
                SpaceY(32),
                new p
                {
                    "You can use this connection object in component"
                },
                
                new CodeViewer(CodeViewer.LangCSharp, Height(150), MD(Width(550)))
                {
                    """
                    protected override Task componentDidMount()
                    {
                        var dbConnection = DbConnection[Context];
                        
                        dbConnection.....
                        
                        return Task.CompletedTask;
                    }
                    """
                }
                
            },
            SpaceY(32),
            new p
            {
                "You can manage session, theme or other futures that you want as just like in this sample"
            },
            
            SpaceY(50)
        };
    }
}

