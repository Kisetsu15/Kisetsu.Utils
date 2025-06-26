using System.Security.Cryptography;
using System.Text;

namespace Kisetsu.Utils {
    /// <summary>
    /// Provides AES encryption and decryption utilities using a passphrase and salt.
    /// </summary>
    public class AES {

        /// <summary>
        /// Encrypts the specified plain text using AES with the given passphrase and salt.
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        /// <param name="passphrase">The passphrase for key derivation.</param>
        /// <param name="salt">The salt as a string.</param>
        /// <returns>The encrypted text, Base64-encoded, containing salt, IV, and ciphertext.</returns>
        public static string Encrypt(string plainText, string passphrase, string salt) {
            var saltBytes = Encoding.UTF8.GetBytes(salt);
            using var aes = Aes.Create();
            using var kdf = new Rfc2898DeriveBytes(passphrase, saltBytes, 100_000, HashAlgorithmName.SHA256);
            aes.Key = kdf.GetBytes(32);
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            byte[] output = new byte[saltBytes.Length + aes.IV.Length + encryptedBytes.Length];
            Buffer.BlockCopy(saltBytes, 0, output, 0, saltBytes.Length);
            Buffer.BlockCopy(aes.IV, 0, output, saltBytes.Length, aes.IV.Length);
            Buffer.BlockCopy(encryptedBytes, 0, output, saltBytes.Length + aes.IV.Length, encryptedBytes.Length);

            return Convert.ToBase64String(output);
        }

        /// <summary>
        /// Decrypts the specified encrypted text using AES with the given passphrase and salt length.
        /// </summary>
        /// <param name="encryptedText">The Base64-encoded encrypted text.</param>
        /// <param name="passphrase">The passphrase for key derivation.</param>
        /// <param name="saltLength">The length of the salt in bytes (default is 16).</param>
        /// <returns>The decrypted plain text.</returns>
        public static string Decrypt(string encryptedText, string passphrase, int saltLength = 16) {
            byte[] combined = Convert.FromBase64String(encryptedText);
            byte[] saltBytes = new byte[saltLength];
            byte[] iv = new byte[16];
            Buffer.BlockCopy(combined, 0, saltBytes, 0, saltLength);
            Buffer.BlockCopy(combined, saltLength, iv, 0, 16);

            using var aes = Aes.Create();
            using var kdf = new Rfc2898DeriveBytes(passphrase, saltBytes, 100_000, HashAlgorithmName.SHA256);
            aes.Key = kdf.GetBytes(32);
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor();
            byte[] decryptedBytes = decryptor.TransformFinalBlock(combined, saltLength + 16, combined.Length - saltLength - 16);
            return Encoding.UTF8.GetString(decryptedBytes);
        }

        /// <summary>
        /// Generates a cryptographically secure random salt.
        /// </summary>
        /// <param name="length">The length of the salt in bytes (default is 16).</param>
        /// <returns>The generated salt as a Base64-encoded string.</returns>
        public static string GenerateSalt(int length = 16) {
            var salt = new byte[length];
            RandomNumberGenerator.Fill(salt);
            return Convert.ToBase64String(salt);
        }
    }
}
