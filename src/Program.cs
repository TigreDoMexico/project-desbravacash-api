using TigreDoMexico.DesbravaCash.Api.Setup;

var builder = WebApplication.CreateBuilder(args);
builder.RegistrarAppServices()
    .RegistrarAutenticacao();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app
    .MapearEndpoints()
    .UseHttpsRedirection()
    .ConfigurarApplication();

app.Run();