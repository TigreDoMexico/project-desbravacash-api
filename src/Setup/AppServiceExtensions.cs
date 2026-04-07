using FluentValidation;
using TigreDoMexico.DesbravaCash.Api.Modules;

namespace TigreDoMexico.DesbravaCash.Api.Setup;

public static class AppServiceExtensions
{
    public static WebApplicationBuilder RegistrarAppServices(this WebApplicationBuilder builder)
    {
        builder.AddModules();

        builder.Services.AddOpenApi();
        
        builder.Services.AddValidatorsFromAssemblyContaining<Program>();
        
        return builder;
    }
    
    
}