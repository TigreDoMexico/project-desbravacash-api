using TigreDoMexico.DesbravaCash.Api.Modules;

namespace TigreDoMexico.DesbravaCash.Api.Setup;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder ConfigurarApplication(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }

    public static WebApplication ConfigurarCors(this WebApplication app)
    {
        app.UseCors("AllowFrontend");
        return app;
    }

    public static WebApplication MapearEndpoints(this WebApplication app)
    {
        app.MapHealthChecks("api/health");
        app.RegisterEndpoints();

        return app;
    }
}