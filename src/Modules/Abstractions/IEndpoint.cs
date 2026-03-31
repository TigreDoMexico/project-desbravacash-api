namespace TigreDoMexico.DesbravaCash.Api.Modules.Abstractions;

public interface IEndpoint
{
    static abstract void MapEndpoint(IEndpointRouteBuilder endpoints);
}