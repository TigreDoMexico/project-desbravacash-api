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

            var senha = config["Seed:Admin:Senha"];
            var telefone = config["Seed:Admin:Telefone"];

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = "Admin",
                Telefone = telefone ?? "123456789",
                Senha = Hashing.HashSenha(senha ?? "senha"),
                UnidadeId = adminUnidadeId,
                Cargo = "Admin",
            };

            context.Usuarios.Add(usuario);

            await context.SaveChangesAsync();
        }
    }
}