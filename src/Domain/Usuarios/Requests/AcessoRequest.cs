namespace TigreDoMexico.DesbravaCash.Api.Domain.Usuarios.Requests;

/// <summary>
/// Dados necessários para validar um acesso da request
/// </summary>
public class AcessoRequest
{
    /// <summary>
    /// Numero do telefone do usuário
    /// </summary>
    public string Telefone { get; set; } = string.Empty;
    
    /// <summary>
    /// Senha do usuário
    /// </summary>
    public string Senha { get; set; } = string.Empty;
}