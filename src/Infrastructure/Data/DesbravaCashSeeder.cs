using TigreDoMexico.DesbravaCash.Api.Domain.Desafios.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Unidades.Models;
using TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Models;
using TigreDoMexico.DesbravaCash.Api.Infrastructure.Security;

namespace TigreDoMexico.DesbravaCash.Api.Infrastructure.Data;

public static class DesbravaCashSeeder
{
    /// <summary>
    /// Método para popular o banco de dados com dados iniciais.
    /// </summary>
    /// <param name="context">Db Context da Aplicação.</param>
    /// <param name="config">Configurações do Appsettings.</param>
    public static async Task SeedAsync(DesbravaCashDbContext context, IConfiguration config)
    {
        if (!context.Unidades.Any())
        {
            var nomesUnidades = config.GetSection("Seed:Unidades").Get<string[]>() ?? [];
            foreach (var nomeUnidade in nomesUnidades)
            {
                var unidade = new Unidade
                {
                    Id = Guid.NewGuid(),
                    Nome = nomeUnidade,
                };

                context.Unidades.Add(unidade);
            }

            var adminUnidadeId = Guid.Parse("75b6e760-03e0-4b33-9dc7-7bdeee256d83");

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
            
            var unidadeId = Guid.Parse("75b6e760-03e0-4b33-9dc7-7bdeee256d83");

            var senhaConselheiro = config["Seed:Conselheiro:Senha"];
            var telefoneConselheiro = config["Seed:Conselheiro:Telefone"];

            var senhaDesbravador = config["Seed:Desbravador:Senha"];
            var telefoneDesbravador = config["Seed:Desbravador:Telefone"];
        
            var usuarioConselheiro = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = "Conselheiro",
                Telefone = telefoneConselheiro ?? "123456789",
                Senha = Hashing.HashSenha(senhaConselheiro ?? "senha"),
                UnidadeId = unidadeId,
                Cargo = "Conselheiro",
                Role = UsuarioRole.Conselheiro,
            };

            context.Usuarios.Add(usuarioConselheiro);

            var usuarioDesbravador = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = "Desbravador",
                Telefone = telefoneDesbravador ?? "123456789",
                Senha = Hashing.HashSenha(senhaDesbravador ?? "senha"),
                UnidadeId = unidadeId,
                Cargo = "Desbravador",
                Role = UsuarioRole.Tesoureiro,
            };

            context.Usuarios.Add(usuarioDesbravador);
            
            var desafio1 = new Desafio
            {
                Id = Guid.NewGuid(),
                Descricao = "Ordem Unida da Unidade",
                Pontuacao = 100,
                DataConclusao = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                PodeSolicitar = false
            };
        
            var desafio2 = new Desafio
            {
                Id = Guid.NewGuid(),
                Descricao = "Visitas",
                Pontuacao = 200,
                DataConclusao = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                PodeSolicitar = true
            };
        
            var desafio3 = new Desafio
            {
                Id = Guid.NewGuid(),
                Descricao = "Banderim",
                Pontuacao = 300,
                DataConclusao = new DateTime(2026, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                PodeSolicitar = true
            };
        
            var desafio4 = new Desafio
            {
                Id = Guid.NewGuid(),
                Descricao = "Portal",
                Pontuacao = 400,
                DataConclusao = new DateTime(2026, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                PodeSolicitar = false
            };
        
            context.Desafios.AddRange(desafio1, desafio2, desafio3, desafio4);
        }

        await context.SaveChangesAsync();
    }
}