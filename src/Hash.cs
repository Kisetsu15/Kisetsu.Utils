using System.Text;
using System.Security.Cryptography;

namespace Kisetsu.Utils {
    /// <summary>
    /// Provides utility methods for Hashing.
    /// </summary>
    public class Hash {
        /// <summary>
        /// Computes the SHA-1 hash of a stream and returns it as a hexadecimal string.
        /// </summary>
        /// <param name="data">The stream to compute the hash for.</param>
        /// <returns>The SHA-1 hash as a hexadecimal string.</returns>
        public static string Sha1(string data) {
            using Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(data));
            using SHA1 sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(stream);
            return Convert.ToHexString(hashBytes).ToLower();
        }

        /// <summary>
        /// Computes the SHA-256 hash of a stream and returns it as a hexadecimal string.
        /// </summary>
        /// <param name="data">The stream to compute the hash for.</param>
        /// <returns>The SHA-256 hash as a hexadecimal string.</returns>
        public static string Sha256(string data) {
            using Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(data));
            using SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(stream);
            return Convert.ToHexString(hashBytes).ToLower();
        }

        /// <summary>
        /// Computes the SHA-512 hash of a stream and returns it as a hexadecimal string.
        /// </summary>
        /// <param name="data">The stream to compute the hash for.</param>
        /// <returns>The SHA-512 hash as a hexadecimal string.</returns>
        public static string Sha512(string data) {
            using Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(data));
            using SHA512 sha512 = SHA512.Create();
            byte[] hashBytes = sha512.ComputeHash(stream);
            return Convert.ToHexString(hashBytes).ToLower();
        }
    }
}
