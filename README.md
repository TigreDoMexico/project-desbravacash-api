# DesbravaCash API
Api do projeto DesbravaCash feito em .NET

## Objetivo
Este projeto faz parte da solução DesbravaCash, um sistema que gerencia a pontuação de Unidades dentro de um [Clube de Desbravadores](https://clubes.adventistas.org/br/about-pathfinder/). Cada Unidade adquire uma quantidade de pontos ao longo dos meses e eles podem trocar estes pontos por prêmios no fim do ano. Como este processo era feito de forma física, a ideia foi transformar este processo numa solução digital moderna que vários outros clubes podem aderir.

## Arquitetura
A arquitetura deste sistema foi pensado de forma simples para que o custo de mantê-lo seja o mais baixo possível.

Temos um [FrontEnd](https://github.com/TigreDoMexico/project-desbravacash-app) feito em NextJs e um BackEnd (este projeto) feito em C# que depende de um banco de dados.

## Executar Local

### Pré-requisitos

| Tecnologia                                                  | Versão | Uso                                                      |
| ----------------------------------------------------------- | ------ | -------------------------------------------------------- |
| [.NET SDK](https://dotnet.microsoft.com/download)           | 9.0   | Compilar e executar a API                                |
| [Docker](https://www.docker.com/products/docker-desktop)    | -      | Subir o banco de dados PostgreSQL                        |
| [dotnet-ef](https://learn.microsoft.com/ef/core/cli/dotnet) | -      | Executar migrations (`dotnet tool install -g dotnet-ef`) |

### Passos

1. Suba o banco de dados:
```bash
docker-compose up -d
```

2. Configure as variáveis de ambiente (ver seção abaixo)

3. Execute as migrations:
```bash
dotnet ef database update --project src/
```

4. Inicie a API:
```bash
dotnet run --project src/
```

> As migrations já são executadas assim que o projeto inicia o Startup.

## Variáveis de Ambiente

As configurações abaixo já possuem valores padrão no `appsettings.json` / `appsettings.Development.json` e funcionam para rodar localmente sem alteração. Para produção, sobrescreva via `dotnet user-secrets` ou variáveis de ambiente.

| Nome Configuração            | Descrição                                     | Padrão (local)                                                                     |
| ---------------------------- | --------------------------------------------- | ---------------------------------------------------------------------------------- |
| `ConnectionStrings:Postgres` | String de conexão com o PostgreSQL            | `Host=localhost;Port=5432;Database=desbravacash;Username=apisuer;Password=apipass` |
| `Cors:AllowedOrigins`        | Origins permitidas pelo CORS                  | `http://localhost:3000`                                                            |
| `Jwt:Chave`                  | Chave secreta JWT (mín. 32 caracteres)        | —                                                                                  |
| `Jwt:Issuer`                 | Issuer do token JWT                           | —                                                                                  |
| `Jwt:Audience`               | Audience do token JWT                         | —                                                                                  |
| `Seed:Unidades`              | Lista de nomes das unidades criadas no seed   | `["Coragem", "Honra", "Garra", "Força", "Diretoria"]`                              |
| `Seed:Admin:Telefone`        | Telefone do usuário Admin criado no seed      | `123456789`                                                                        |
| `Seed:Admin:Senha`           | Senha do usuário Admin criado no seed         | `senha123`                                                                         |
| `Seed:Tesoureiro:Telefone`   | Telefone do usuário Tesoureiro criado no seed | `987654321`                                                                        |
| `Seed:Tesoureiro:Senha`      | Senha do usuário Tesoureiro criado no seed    | `senha456`                                                                         |

As configurações de JWT não possuem valor padrão e devem ser definidas via `dotnet user-secrets`:

```bash
dotnet user-secrets set "Jwt:Chave" "SUA_CHAVE_DE_32_CARACTERES" --project src/
dotnet user-secrets set "Jwt:Issuer" "SEU_ISSUER" --project src/
dotnet user-secrets set "Jwt:Audience" "SUA_AUDIENCE" --project src/
```

## Open Source

Este projeto é open source e está disponível sob a licença MIT. Contribuições são bem-vindas!
