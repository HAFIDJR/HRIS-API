public class CustomAuthMiddleware
{
    private readonly RequestDelegate _next;

    public CustomAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"message\":\"Akses ditolak. Hanya pengguna dengan peran HRD yang diizinkan.\"}");
        }

        if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"message\":\"Anda belum login atau token tidak valid.\"}");
        }
    }

}