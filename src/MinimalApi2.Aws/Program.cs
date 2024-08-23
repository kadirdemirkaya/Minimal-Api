using Microsoft.AspNetCore.Antiforgery;
using MinimalApi2.Aws;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ApiServiceRegistration(builder.Configuration);


var app = builder.Build();

app.MapGet("antiforgery/token", (IAntiforgery forgeryService, HttpContext context) =>
{
    var tokens = forgeryService.GetAndStoreTokens(context);
    var xsrfToken = tokens.RequestToken!;
    return TypedResults.Content(xsrfToken, "text/plain");
});

app.ApiApplicationRegistration();

app.Run();
