namespace Sphere.Application.Common.Interfaces;

/// <summary>
/// Password hashing service interface.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes a password.
    /// </summary>
    /// <param name="password">Plain text password.</param>
    /// <returns>Hashed password.</returns>
    string Hash(string password);

    /// <summary>
    /// Verifies a password against a hash.
    /// </summary>
    /// <param name="password">Plain text password.</param>
    /// <param name="hash">Password hash.</param>
    /// <returns>True if password matches hash.</returns>
    bool Verify(string password, string hash);
}
