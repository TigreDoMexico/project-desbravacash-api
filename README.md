# DesbravaCash API

API do projeto DesbravaCash

## Variáveis de Ambiente

Este projeto depende das seguintes informações configuradas:

| Nome Configuração | Contexto                       | Comando                                             |
|-------------------|--------------------------------|-----------------------------------------------------|
| `Jwt:Chave`       | chave secreta de 32 caracteres | `dotnet user-secrets set "Jwt:Chave" "SUA_CHAVE"`   |
| `Jwt:Issuer`      | issuer da chave jwt            | `dotnet user-secrets set "Jwt:Chave" "ISSUER"`      |
| `Jwt:Audience`    | audience da chave jwt          | `dotnet user-secrets set "Jwt:Audience" "AUDIENCE"` |