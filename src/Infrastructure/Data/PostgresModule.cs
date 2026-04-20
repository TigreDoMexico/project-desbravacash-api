using Microsoft.EntityFrameworkCore;
using TigreDoMexico.DesbravaCash.Api.Domain.Transacoes.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Persistence;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Persistence;
using TigreDoMexico.DesbravaCash.Api.Infrastructure.Data.Repositories;
using TigreDoMexico.DesbravaCash.Api.Modules.Abstractions;

namespace TigreDoMexico.DesbravaCash.Api.Infrastructure.Data;

public class PostgresModule : IModule
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DesbravaCashDbContext>(opt =>
            opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

        ConfigurarDependencias(builder.Services);

        if (builder.Environment.EnvironmentName != "Test")
        {
            ExecutarMigrations(builder.Services);
        }
    }

    private static void ConfigurarDependencias(IServiceCollection services)
    {
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IUnidadeRepository, UnidadeRepository>();
        services.AddScoped<ITransacaoRepository, TransacaoRepository>();
    }

    private static IServiceCollection ExecutarMigrations(IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DesbravaCashDbContext>();
        dbContext.Database.Migrate();

        return services;
    }
}