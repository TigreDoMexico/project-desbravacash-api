using TigreDoMexico.DesbravaCash.Api.Setup;

var builder = WebApplication.CreateBuilder(args);
builder.RegistrarAppServices()
    .RegistrarCors()
    .RegistrarAutenticacao();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app
    .ConfigurarCors()
    .MapearEndpoints()
    .UseHttpsRedirection()
    .ConfigurarApplication();

await app.AdicionarSeedDados();

app.Run();