using Assignment1_Middleware.Middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Enforce HTTPS
app.UseHttpsRedirection();

// Custom Logging Middleware
app.UseMiddleware<ReqLoggingMiddleware>();

//Security Header(CSP)
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy",
        "default-src 'self'; script-src 'self'; style-src 'self';");

    await next();
});

// Error Handling
app.UseExceptionHandler("/error");

// Serve Static Files
app.UseStaticFiles();

app.MapGet("/error", () => "Something went wrong!");

app.Run();

