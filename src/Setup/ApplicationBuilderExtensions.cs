using TigreDoMexico.DesbravaCash.Api.Infrastructure.Data;
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

    public static async Task AdicionarSeedDados(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DesbravaCashDbContext>();
        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        await DesbravaCashSeeder.SeedAsync(context, config);
    }
}