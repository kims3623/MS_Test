using System.Security.Cryptography;
using Sphere.Application.Common.Interfaces;

namespace Sphere.Infrastructure.Identity;

/// <summary>
/// Password hashing service using PBKDF2.
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 100000;
    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;
    private const char Delimiter = ':';

    public string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            Iterations,
            HashAlgorithm,
            KeySize);

        return string.Join(
            Delimiter,
            Convert.ToBase64String(salt),
            Convert.ToBase64String(hash),
            Iterations,
            HashAlgorithm.Name);
    }

    public bool Verify(string password, string passwordHash)
    {
        if (string.IsNullOrEmpty(passwordHash))
            return false;

        var parts = passwordHash.Split(Delimiter);
        if (parts.Length != 4)
        {
            // Legacy hash format - simple comparison (for migration)
            return passwordHash == password;
        }

        var salt = Convert.FromBase64String(parts[0]);
        var hash = Convert.FromBase64String(parts[1]);
        var iterations = int.Parse(parts[2]);
        var algorithm = new HashAlgorithmName(parts[3]);

        var computedHash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            iterations,
            algorithm,
            hash.Length);

        return CryptographicOperations.FixedTimeEquals(hash, computedHash);
    }
}
