using SHA1 = System.Security.Cryptography.SHA1;
using SHA256 = System.Security.Cryptography.SHA256;
using SHA512 = System.Security.Cryptography.SHA512;
using Stream = System.IO.Stream;
using System.Text;

namespace Kisetsu.Utils {
    /// <summary>
    /// Specifies the case for the hexadecimal string output.
    /// </summary>
    public enum Case {
        /// <summary>
        /// Lowercase hexadecimal string.
        /// </summary>
        Lower,
        /// <summary>
        /// Uppercase hexadecimal string.
        /// </summary>
        Upper
    }

    /// <summary>
    /// Supported hashing algorithms.
    /// </summary>
    public enum Algorithm {
        /// <summary>
        /// SHA-1 hashing algorithm.
        /// </summary>
        SHA1,
        /// <summary>
        /// SHA-256 hashing algorithm.
        /// </summary>
        SHA256,
        /// <summary>
        /// SHA-512 hashing algorithm.
        /// </summary>
        SHA512
    }

    /// <summary>
    /// Provides methods for computing cryptographic hashes using various algorithms.
    /// </summary>
    public class Hash {
        private static readonly Dictionary<Algorithm, Func<Stream, byte[]>> hashFunctions = new() {
        { Algorithm.SHA1, Sha1 },
        { Algorithm.SHA256, Sha256 },
        { Algorithm.SHA512, Sha512 }
    };

        /// <summary>
        /// Computes the hash of the specified string using the given algorithm and returns the result as a hexadecimal string.
        /// </summary>
        /// <param name="data">The input string to hash.</param>
        /// <param name="algorithm">The hashing algorithm to use.</param>
        /// <param name="case">The case for the hexadecimal output string (default is Lower).</param>
        /// <returns>The hexadecimal string representation of the hash.</returns>
        public static string Compute(string data, Algorithm algorithm, Case @case = Case.Lower) {
            byte[] hash = Compute(data, algorithm);
            return @case switch {
                Case.Lower => Convert.ToHexString(hash).ToLower(),
                Case.Upper => Convert.ToHexString(hash).ToUpper(),
                _ => Convert.ToHexString(hash)
            };
        }

        /// <summary>
        /// Computes the hash of the specified string using the given algorithm and returns the result as a byte array.
        /// </summary>
        /// <param name="data">The input string to hash.</param>
        /// <param name="algorithm">The hashing algorithm to use.</param>
        /// <returns>The hash as a byte array.</returns>
        public static byte[] Compute(string data, Algorithm algorithm) {
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(data));
            return Compute(stream, algorithm);
        }

        /// <summary>
        /// Computes the hash of the data from the provided stream using the given algorithm.
        /// </summary>
        /// <param name="stream">The input stream to hash.</param>
        /// <param name="algorithm">The hashing algorithm to use.</param>
        /// <returns>The hash as a byte array.</returns>
        /// <exception cref="ArgumentException">Thrown if the algorithm is not supported.</exception>
        public static byte[] Compute(Stream stream, Algorithm algorithm) {
            if (hashFunctions.TryGetValue(algorithm, out var hashFunction)) {
                return hashFunction(stream);
            }
            throw new ArgumentException($"Unsupported algorithm: {algorithm}");
        }

        /// <summary>
        /// Tries to compute the hash of the specified string using the given algorithm.
        /// </summary>
        /// <param name="data">The input string to hash.</param>
        /// <param name="algorithm">The hashing algorithm to use.</param>
        /// <param name="result">The resulting hash as a byte array, or an empty array if computation fails.</param>
        /// <returns>True if the hash was computed successfully; otherwise, false.</returns>
        public static bool TryCompute(string data, Algorithm algorithm, out byte[] result) {
            result = [];
            try {
                result = Compute(data, algorithm);
                return true;
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Tries to compute the hash of the data from the provided stream using the given algorithm.
        /// </summary>
        /// <param name="stream">The input stream to hash.</param>
        /// <param name="algorithm">The hashing algorithm to use.</param>
        /// <param name="result">The resulting hash as a byte array, or an empty array if computation fails.</param>
        /// <returns>True if the hash was computed successfully; otherwise, false.</returns>
        public static bool TryCompute(Stream stream, Algorithm algorithm, out byte[] result) {
            result = [];
            try {
                result = Compute(stream, algorithm);
                return true;
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Tries to compute the hash of the specified string using the given algorithm and returns the result as a hexadecimal string.
        /// </summary>
        /// <param name="data">The input string to hash.</param>
        /// <param name="algorithm">The hashing algorithm to use.</param>
        /// <param name="case">The case for the hexadecimal output string (default is Lower).</param>
        /// <param name="result">The resulting hash as a hexadecimal string, or an empty string if computation fails.</param>
        /// <returns>True if the hash was computed successfully; otherwise, false.</returns>
        public static bool TryCompute(string data, Algorithm algorithm, out string result, Case @case = Case.Lower) {
            result = string.Empty;
            try {
                result = Compute(data, algorithm, @case);
                return true;
            } catch {
                return false;
            }
        }

        /// <summary>
        /// Computes the SHA-1 hash of the data from the provided stream.
        /// </summary>
        /// <param name="stream">The input stream to hash.</param>
        /// <returns>The SHA-1 hash as a byte array.</returns>
        private static byte[] Sha1(Stream stream) {
            SHA1 sha1 = SHA1.Create();
            return sha1.ComputeHash(stream);
        }

        /// <summary>
        /// Computes the SHA-256 hash of the data from the provided stream.
        /// </summary>
        /// <param name="stream">The input stream to hash.</param>
        /// <returns>The SHA-256 hash as a byte array.</returns>
        private static byte[] Sha256(Stream stream) {
            SHA256 sha256 = SHA256.Create();
            return sha256.ComputeHash(stream);
        }

        /// <summary>
        /// Computes the SHA-512 hash of the data from the provided stream.
        /// </summary>
        /// <param name="stream">The input stream to hash.</param>
        /// <returns>The SHA-512 hash as a byte array.</returns>
        private static byte[] Sha512(Stream stream) {
            SHA512 sha512 = SHA512.Create();
            return sha512.ComputeHash(stream);
        }
    }
}
