namespace MultiTenan.Api.Middleware;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value?.Split('/');
        if (path is { Length: > 1 })
        {
            var tenantName = path[1];
            context.Items["TenantName"] = tenantName; 
        }

        await _next(context);
    }
}