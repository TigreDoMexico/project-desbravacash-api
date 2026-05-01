using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;
using TigreDoMexico.DesbravaCash.Api.Infrastructure.Security;

namespace TigreDoMexico.DesbravaCash.Api.Infrastructure.Data;

public static class DesbravaCashSeeder
{
    public static async Task SeedAsync(DesbravaCashDbContext context, IConfiguration config)
    {
        if (!context.Unidades.Any())
        {
            var nomesUnidades = config.GetSection("Seed:Unidades").Get<string[]>() ?? [];
            var adminUnidadeId = Guid.Empty;

            foreach (var nomeUnidade in nomesUnidades)
            {
                var unidade = new Unidade
                {
                    Id = Guid.NewGuid(),
                    Nome = nomeUnidade,
                };

                context.Unidades.Add(unidade);
                adminUnidadeId = unidade.Id;
            }

            var senhaAdmin = config["Seed:Admin:Senha"];
            var telefoneAdmin = config["Seed:Admin:Telefone"];

            var senhaTesoureiro = config["Seed:Tesoureiro:Senha"];
            var telefoneTesoureiro = config["Seed:Tesoureiro:Telefone"];

            var usuarioAdmin = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = "Admin",
                Telefone = telefoneAdmin ?? "123456789",
                Senha = Hashing.HashSenha(senhaAdmin ?? "senha"),
                UnidadeId = adminUnidadeId,
                Cargo = "Admin",
                Role = UsuarioRole.Admin,
            };

            context.Usuarios.Add(usuarioAdmin);

            var usuarioTesoureiro = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = "Tesoureiro",
                Telefone = telefoneTesoureiro ?? "123456789",
                Senha = Hashing.HashSenha(senhaTesoureiro ?? "senha"),
                UnidadeId = adminUnidadeId,
                Cargo = "Tesoureiro",
                Role = UsuarioRole.Tesoureiro,
            };

            context.Usuarios.Add(usuarioTesoureiro);

            await context.SaveChangesAsync();
        }
    }
}