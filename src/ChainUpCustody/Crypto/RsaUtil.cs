using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

namespace ChainUpCustody.Crypto;

/// <summary>
/// RSA encryption and decryption utility class.
/// Uses BouncyCastle library for compatibility with Java SDK's RSAHelper implementation.
/// </summary>
public static class RsaUtil
{
  #region Constants

  /// <summary>
  /// RSA cipher algorithm specification.
  /// </summary>
  private const string CipherAlgorithm = "RSA/ECB/PKCS1Padding";

  /// <summary>
  /// Hash algorithm for signing.
  /// </summary>
  private const string HashAlgorithm = "SHA256";

  /// <summary>
  /// Default RSA key size in bits.
  /// </summary>
  private const int DefaultKeySize = 2048;

  /// <summary>
  /// Maximum encrypt block size (key_size - 11 bytes for PKCS1 padding).
  /// For 2048-bit key: 256 - 11 - 11 = 234 bytes.
  /// </summary>
  private const int MaxEncryptBlock = 234;

  /// <summary>
  /// Maximum decrypt block size (equals key size in bytes).
  /// For 2048-bit key: 2048 / 8 = 256 bytes.
  /// </summary>
  private const int MaxDecryptBlock = 256;

  /// <summary>
  /// PEM header/footer markers to be removed when cleaning keys.
  /// </summary>
  private static readonly string[] PemMarkers =
  [
      "-----BEGIN PUBLIC KEY-----",
        "-----END PUBLIC KEY-----",
        "-----BEGIN PRIVATE KEY-----",
        "-----END PRIVATE KEY-----",
        "-----BEGIN RSA PRIVATE KEY-----",
        "-----END RSA PRIVATE KEY-----",
        "-----BEGIN RSA PUBLIC KEY-----",
        "-----END RSA PUBLIC KEY-----"
  ];

  #endregion

  #region Key Generation

  /// <summary>
  /// Generates a new RSA key pair.
  /// </summary>
  /// <returns>A dictionary containing "privateKey" and "publicKey" as Base64-encoded strings.</returns>
  public static Dictionary<string, string> GenerateRsaPriAndPub()
  {
    using var rsa = RSA.Create(DefaultKeySize);

    return new Dictionary<string, string>
    {
      ["privateKey"] = Convert.ToBase64String(rsa.ExportPkcs8PrivateKey()),
      ["publicKey"] = Convert.ToBase64String(rsa.ExportSubjectPublicKeyInfo())
    };
  }

  /// <summary>
  /// Generates a new RSA key pair.
  /// </summary>
  /// <returns>A tuple containing (publicKey, privateKey) as Base64-encoded strings.</returns>
  public static (string PublicKey, string PrivateKey) GenerateKeys()
  {
    using var rsa = RSA.Create(DefaultKeySize);

    var privateKey = Convert.ToBase64String(rsa.ExportPkcs8PrivateKey());
    var publicKey = Convert.ToBase64String(rsa.ExportSubjectPublicKeyInfo());

    return (publicKey, privateKey);
  }

  #endregion

  #region Encryption

  /// <summary>
  /// Encrypts data using a private key with segmented encryption.
  /// Uses BouncyCastle RSA/ECB/PKCS1Padding format.
  /// </summary>
  /// <param name="privateKey">Base64-encoded PKCS8 private key.</param>
  /// <param name="plainText">The plain text to encrypt.</param>
  /// <returns>URL-safe Base64-encoded ciphertext, or empty string on failure.</returns>
  public static string EncryptByPrivateKey(string plainText, string privateKey)
  {
    if (string.IsNullOrEmpty(plainText) || string.IsNullOrEmpty(privateKey))
    {
      return string.Empty;
    }

    try
    {
      var keyBytes = Convert.FromBase64String(CleanKey(privateKey));

      using var rsa = RSA.Create();
      rsa.ImportPkcs8PrivateKey(keyBytes, out _);

      var keyPair = DotNetUtilities.GetKeyPair(rsa);
      var cipher = CipherUtilities.GetCipher(CipherAlgorithm);
      cipher.Init(forEncryption: true, keyPair.Private);

      var dataToEncrypt = Encoding.UTF8.GetBytes(plainText);
      var encryptedData = ProcessInBlocks(cipher, dataToEncrypt, MaxEncryptBlock);

      return ToUrlSafeBase64(encryptedData);
    }
    catch (Exception ex)
    {
      LogError("PrivateKeyEncrypt", ex);
      return string.Empty;
    }
  }

  #endregion

  #region Decryption

  /// <summary>
  /// Decrypts data using a public key with segmented decryption.
  /// Uses BouncyCastle RSA/ECB/PKCS1Padding format.
  /// </summary>
  /// <param name="publicKey">Base64-encoded public key.</param>
  /// <param name="cipherText">URL-safe Base64-encoded ciphertext.</param>
  /// <returns>Decrypted plain text, or empty string on failure.</returns>
  public static string DecryptByPublicKey(string cipherText, string publicKey)
  {
    if (string.IsNullOrEmpty(cipherText) || string.IsNullOrEmpty(publicKey))
    {
      return string.Empty;
    }

    try
    {
      using var rsa = RSA.Create();
      rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(CleanKey(publicKey)), out _);

      var rsaParameters = rsa.ExportParameters(includePrivateParameters: false);
      var publicKeyParameter = DotNetUtilities.GetRsaPublicKey(rsaParameters);

      var cipher = CipherUtilities.GetCipher(CipherAlgorithm);
      cipher.Init(forEncryption: false, publicKeyParameter);

      var dataToDecrypt = FromUrlSafeBase64(cipherText);
      var decryptedData = ProcessInBlocks(cipher, dataToDecrypt, MaxDecryptBlock);

      return Encoding.UTF8.GetString(decryptedData);
    }
    catch (Exception ex)
    {
      LogError("PublicKeyDecrypt", ex);
      return string.Empty;
    }
  }

  #endregion

  #region Signing

  /// <summary>
  /// Signs data using a private key with SHA256withRSA algorithm.
  /// </summary>
  /// <param name="data">The data to sign.</param>
  /// <param name="privateKey">Base64-encoded PKCS8 private key.</param>
  /// <returns>Base64-encoded signature, or empty string on failure.</returns>
  public static string GetSign(string data, string privateKey)
  {
    if (string.IsNullOrEmpty(data) || string.IsNullOrEmpty(privateKey))
    {
      return string.Empty;
    }

    try
    {
      var dataBytes = Encoding.UTF8.GetBytes(data);

      using var sha256 = SHA256.Create();
      var hash = sha256.ComputeHash(dataBytes);

      using var rsa = RSA.Create();
      rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(CleanKey(privateKey)), out _);

      var formatter = new RSAPKCS1SignatureFormatter(rsa);
      formatter.SetHashAlgorithm(HashAlgorithm);

      var signature = formatter.CreateSignature(hash);
      return Convert.ToBase64String(signature);
    }
    catch (Exception ex)
    {
      LogError("Sign", ex);
      return string.Empty;
    }
  }

  /// <summary>
  /// Verifies a signature using a public key.
  /// </summary>
  /// <param name="data">The original data that was signed.</param>
  /// <param name="signature">Base64-encoded signature to verify.</param>
  /// <param name="publicKey">Base64-encoded public key.</param>
  /// <returns>True if the signature is valid; otherwise, false.</returns>
  public static bool VerifySign(string data, string signature, string publicKey)
  {
    if (string.IsNullOrEmpty(data) || string.IsNullOrEmpty(signature) || string.IsNullOrEmpty(publicKey))
    {
      return false;
    }

    try
    {
      var dataBytes = Encoding.UTF8.GetBytes(data);

      using var sha256 = SHA256.Create();
      var hash = sha256.ComputeHash(dataBytes);

      using var rsa = RSA.Create();
      rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(CleanKey(publicKey)), out _);

      var deformatter = new RSAPKCS1SignatureDeformatter(rsa);
      deformatter.SetHashAlgorithm(HashAlgorithm);

      var signatureBytes = Convert.FromBase64String(signature);
      return deformatter.VerifySignature(hash, signatureBytes);
    }
    catch (Exception ex)
    {
      LogError("VerifySign", ex);
      return false;
    }
  }

  #endregion

  #region Utility Methods

  /// <summary>
  /// Builds an HTTP parameter string from a dictionary, sorted by key in ASCII order.
  /// Excludes "sign" and "key" parameters, as well as null/empty values.
  /// </summary>
  /// <param name="parameters">The parameters dictionary.</param>
  /// <returns>A formatted parameter string (e.g., "a=1&amp;b=2").</returns>
  public static string HttpParamsBuild(IDictionary<string, object?> parameters)
  {
    var sortedParams = new SortedDictionary<string, object?>(parameters, StringComparer.Ordinal);
    var builder = new StringBuilder();

    foreach (var (key, value) in sortedParams)
    {
      if (value is null || string.IsNullOrEmpty(value.ToString()))
      {
        continue;
      }

      if (key is "sign" or "key")
      {
        continue;
      }

      builder.Append($"{key}={value}&");
    }

    // Remove trailing '&'
    if (builder.Length > 0)
    {
      builder.Length--;
    }

    return builder.ToString();
  }

  #endregion

  #region Private Helper Methods

  /// <summary>
  /// Cleans a key string by removing PEM headers/footers and whitespace.
  /// </summary>
  /// <param name="key">The key string to clean.</param>
  /// <returns>A cleaned Base64 key string.</returns>
  private static string CleanKey(string key)
  {
    if (string.IsNullOrEmpty(key))
    {
      return key;
    }

    var result = key;

    foreach (var marker in PemMarkers)
    {
      result = result.Replace(marker, string.Empty);
    }

    return result
        .Replace("\r", string.Empty)
        .Replace("\n", string.Empty)
        .Replace(" ", string.Empty)
        .Trim();
  }

  /// <summary>
  /// Processes data in blocks using the specified cipher.
  /// </summary>
  /// <param name="cipher">The cipher to use for processing.</param>
  /// <param name="data">The data to process.</param>
  /// <param name="blockSize">The maximum block size.</param>
  /// <returns>The processed data.</returns>
  private static byte[] ProcessInBlocks(IBufferedCipher cipher, byte[] data, int blockSize)
  {
    using var outputStream = new MemoryStream();

    var inputLength = data.Length;
    var offset = 0;
    var blockIndex = 0;

    while (offset < inputLength)
    {
      var currentBlockSize = Math.Min(blockSize, inputLength - offset);
      var processedBlock = cipher.DoFinal(data, offset, currentBlockSize);
      outputStream.Write(processedBlock, 0, processedBlock.Length);

      blockIndex++;
      offset = blockIndex * blockSize;
    }

    return outputStream.ToArray();
  }

  /// <summary>
  /// Converts a byte array to a URL-safe Base64 string.
  /// </summary>
  /// <param name="data">The byte array to convert.</param>
  /// <returns>A URL-safe Base64 string.</returns>
  private static string ToUrlSafeBase64(byte[] data)
  {
    return Convert.ToBase64String(data)
        .Replace("+", "-")
        .Replace("/", "_")
        .TrimEnd('=');
  }

  /// <summary>
  /// Converts a URL-safe Base64 string back to a byte array.
  /// </summary>
  /// <param name="urlSafeBase64">The URL-safe Base64 string.</param>
  /// <returns>The decoded byte array.</returns>
  private static byte[] FromUrlSafeBase64(string urlSafeBase64)
  {
    var base64 = urlSafeBase64
        .Replace("\r", string.Empty)
        .Replace("\n", string.Empty)
        .Replace("-", "+")
        .Replace("_", "/");

    // Add padding if necessary
    var paddingNeeded = (4 - (base64.Length % 4)) % 4;
    if (paddingNeeded > 0)
    {
      base64 = base64.PadRight(base64.Length + paddingNeeded, '=');
    }

    return Convert.FromBase64String(base64);
  }

  /// <summary>
  /// Logs an error message to the console.
  /// </summary>
  /// <param name="methodName">The name of the method where the error occurred.</param>
  /// <param name="exception">The exception that was caught.</param>
  private static void LogError(string methodName, Exception exception)
  {
    Console.Error.WriteLine($"[RsaUtil.{methodName}] Error: {exception.Message}");
  }

  #endregion
}
