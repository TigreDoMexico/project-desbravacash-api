using System.Security.Cryptography;

namespace TigreDoMexico.DesbravaCash.Api.Infrastructure.Security;

public static class Hashing
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 600_000;
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;
    private const char Separator = ':';

    /// <summary>
    /// Gera o hash de uma senha usando PBKDF2-SHA256.
    /// </summary>
    public static string HashSenha(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            Iterations,
            Algorithm,
            HashSize
        );

        return string.Join(
            Separator,
            Iterations,
            Convert.ToBase64String(salt),
            Convert.ToBase64String(hash)
        );
    }

    /// <summary>
    /// Verifica se a senha corresponde ao hash armazenado.
    /// </summary>
    public static bool SenhaValida(string senha, string hashSalvo)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(senha);
        ArgumentException.ThrowIfNullOrWhiteSpace(hashSalvo);

        var parts = hashSalvo.Split(Separator);
        if (parts.Length != 3) return false;

        if (!int.TryParse(parts[0], out int storedIterations)) return false;

        byte[] salt;
        byte[] storedHashBytes;

        try
        {
            salt = Convert.FromBase64String(parts[1]);
            storedHashBytes = Convert.FromBase64String(parts[2]);
        }
        catch (FormatException)
        {
            return false;
        }

        var computedHash = Rfc2898DeriveBytes.Pbkdf2(
            senha,
            salt,
            storedIterations,
            Algorithm,
            storedHashBytes.Length
        );

        return CryptographicOperations.FixedTimeEquals(computedHash, storedHashBytes);
    }
}