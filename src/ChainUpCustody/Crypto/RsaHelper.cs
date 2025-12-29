using System;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace ChainUpCustody.Crypto
{
  /// <summary>
  /// RSA Helper class for encryption, decryption, and signing operations
  /// </summary>
  public static class RsaHelper
  {
    /// <summary>
    /// RSA maximum encrypted plaintext size (key_size - 11 for PKCS#1 v1.5 padding)
    /// </summary>
    private const int MaxEncryptBlock = 245;

    /// <summary>
    /// RSA maximum decrypted ciphertext size (key_size)
    /// </summary>
    private const int MaxDecryptBlock = 256;

    /// <summary>
    /// Generate RSA key pair
    /// </summary>
    /// <returns>Tuple of (privateKey, publicKey) in Base64 format</returns>
    public static (string PrivateKey, string PublicKey) GenerateKeys()
    {
      using var rsa = RSA.Create(2048);
      var privateKey = Convert.ToBase64String(rsa.ExportPkcs8PrivateKey());
      var publicKey = Convert.ToBase64String(rsa.ExportSubjectPublicKeyInfo());
      return (privateKey, publicKey);
    }

    /// <summary>
    /// Create RSA instance from private key
    /// </summary>
    /// <param name="privateKey">Base64 encoded PKCS8 private key</param>
    /// <returns>RSA instance</returns>
    private static RSA CreateRsaFromPrivateKey(string privateKey)
    {
      var rsa = RSA.Create();
      var keyBytes = Convert.FromBase64String(privateKey);
      rsa.ImportPkcs8PrivateKey(keyBytes, out _);
      return rsa;
    }

    /// <summary>
    /// Create RSA instance from public key
    /// </summary>
    /// <param name="publicKey">Base64 encoded X509 public key</param>
    /// <returns>RSA instance</returns>
    private static RSA CreateRsaFromPublicKey(string publicKey)
    {
      var rsa = RSA.Create();
      var keyBytes = Convert.FromBase64String(publicKey);
      rsa.ImportSubjectPublicKeyInfo(keyBytes, out _);
      return rsa;
    }

    /// <summary>
    /// Sign data using private key with SHA256 + RSA
    /// Reference: https://github.com/HiCoinCom/rust-sdk/blob/main/src/crypto.rs#L354
    /// 
    /// Signing flow:
    /// 1. SHA256 hash the data (data should already be MD5 hex string from MpcSignUtil)
    /// 2. RSA sign with PKCS1v15
    /// 3. Base64 encode
    /// </summary>
    /// <param name="data">Data to sign (should already be MD5 hash hex string)</param>
    /// <param name="privateKey">Base64 encoded private key</param>
    /// <returns>Base64 encoded signature</returns>
    public static string GetSign(string data, string privateKey)
    {
      try
      {
        using var rsa = CreateRsaFromPrivateKey(privateKey);

        // Step 1: SHA256 hash the data (data is already MD5 hex string)
        using var sha256 = SHA256.Create();
        var sha256Bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));

        // Step 2: RSA sign with PKCS1v15 (SHA256)
        var signBytes = rsa.SignHash(sha256Bytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

        // Step 3: Base64 encode
        return Convert.ToBase64String(signBytes);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Sign error: {ex.Message}");
        return string.Empty;
      }
    }

    /// <summary>
    /// Verify signature using public key with SHA256 + RSA
    /// </summary>
    /// <param name="data">Data that was signed (should be MD5 hash hex string)</param>
    /// <param name="signature">Base64 encoded signature</param>
    /// <param name="publicKey">Base64 encoded public key</param>
    /// <returns>True if signature is valid</returns>
    public static bool VerifySign(string data, string signature, string publicKey)
    {
      try
      {
        using var rsa = CreateRsaFromPublicKey(publicKey);

        // SHA256 hash the data (data should already be MD5 hex string)
        using var sha256 = SHA256.Create();
        var dataBytes = Encoding.UTF8.GetBytes(data);
        var sha256Bytes = sha256.ComputeHash(dataBytes);
        var signBytes = Convert.FromBase64String(signature);
        return rsa.VerifyHash(sha256Bytes, signBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Verify sign error: {ex.Message}");
        return false;
      }
    }

    /// <summary>
    /// Encrypt data using private key (segment encryption)
    /// Uses raw RSA modular exponentiation: m^d mod n
    /// This is non-standard RSA for signature-like encryption
    /// </summary>
    /// <param name="data">Data to encrypt</param>
    /// <param name="privateKey">Base64 encoded private key</param>
    /// <returns>URL-safe Base64 encoded encrypted data</returns>
    public static string EncryptByPrivateKey(string data, string privateKey)
    {
      if (string.IsNullOrEmpty(data) || string.IsNullOrEmpty(privateKey))
      {
        return string.Empty;
      }

      try
      {
        using var rsa = CreateRsaFromPrivateKey(privateKey);
        var parameters = rsa.ExportParameters(true);
        var keySize = rsa.KeySize / 8; // Key size in bytes
        var maxBlock = keySize - 11;   // PKCS#1 v1.5 padding overhead

        var dataBytes = Encoding.UTF8.GetBytes(data);
        var inputLen = dataBytes.Length;

        using var output = new MemoryStream();
        var offset = 0;

        while (offset < inputLen)
        {
          var blockSize = Math.Min(inputLen - offset, maxBlock);
          var block = new byte[blockSize];
          Array.Copy(dataBytes, offset, block, 0, blockSize);

          // Apply PKCS#1 v1.5 type 1 padding: 0x00 0x01 [0xFF padding] 0x00 [data]
          var paddingLen = keySize - blockSize - 3;
          var paddedBlock = new byte[keySize];
          paddedBlock[0] = 0x00;
          paddedBlock[1] = 0x01;
          for (var j = 2; j < 2 + paddingLen; j++)
          {
            paddedBlock[j] = 0xFF;
          }
          paddedBlock[2 + paddingLen] = 0x00;
          Array.Copy(block, 0, paddedBlock, 3 + paddingLen, blockSize);

          // Raw RSA operation with private key: m^d mod n
          var m = new BigInteger(paddedBlock, true, true);
          var d = new BigInteger(parameters.D, true, true);
          var n = new BigInteger(parameters.Modulus, true, true);
          var c = BigInteger.ModPow(m, d, n);

          var encryptedBytes = c.ToByteArray(true, true);

          // Ensure the output is exactly keySize bytes
          if (encryptedBytes.Length < keySize)
          {
            var padded = new byte[keySize];
            Array.Copy(encryptedBytes, 0, padded, keySize - encryptedBytes.Length, encryptedBytes.Length);
            encryptedBytes = padded;
          }
          else if (encryptedBytes.Length > keySize)
          {
            encryptedBytes = encryptedBytes[^keySize..];
          }

          output.Write(encryptedBytes, 0, encryptedBytes.Length);
          offset += blockSize;
        }

        // Convert to URL-safe Base64 (no padding)
        var base64 = Convert.ToBase64String(output.ToArray());
        return base64.Replace('+', '-').Replace('/', '_').TrimEnd('=');
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Encrypt error: {ex.Message}");
        return string.Empty;
      }
    }

    /// <summary>
    /// Decrypt data using public key (segment decryption)
    /// Uses raw RSA modular exponentiation: c^e mod n
    /// This is non-standard RSA for signature-like decryption
    /// </summary>
    /// <param name="encryptedData">URL-safe Base64 encoded encrypted data</param>
    /// <param name="publicKey">Base64 encoded public key</param>
    /// <returns>Decrypted plaintext</returns>
    public static string DecryptByPublicKey(string encryptedData, string publicKey)
    {
      if (string.IsNullOrEmpty(encryptedData) || string.IsNullOrEmpty(publicKey))
      {
        return string.Empty;
      }

      try
      {
        // Clean up the data: remove whitespace
        encryptedData = encryptedData.Replace("\r", "").Replace("\n", "").Replace(" ", "");

        // Convert URL-safe Base64 to standard Base64
        encryptedData = encryptedData.Replace('-', '+').Replace('_', '/');

        // Add padding if needed
        var mod = encryptedData.Length % 4;
        if (mod > 0)
        {
          encryptedData += new string('=', 4 - mod);
        }

        var dataBytes = Convert.FromBase64String(encryptedData);
        var inputLen = dataBytes.Length;

        using var rsa = CreateRsaFromPublicKey(publicKey);
        var parameters = rsa.ExportParameters(false);
        var keySize = rsa.KeySize / 8; // Key size in bytes

        using var output = new MemoryStream();
        var offset = 0;

        while (offset < inputLen)
        {
          var blockSize = Math.Min(inputLen - offset, keySize);
          var block = new byte[blockSize];
          Array.Copy(dataBytes, offset, block, 0, blockSize);

          // Raw RSA operation with public key: c^e mod n
          var c = new BigInteger(block, true, true);
          var e = new BigInteger(parameters.Exponent, true, true);
          var n = new BigInteger(parameters.Modulus, true, true);
          var m = BigInteger.ModPow(c, e, n);

          var decryptedBytes = m.ToByteArray(true, true);

          // Pad to keySize for proper PKCS#1 parsing
          var paddedDecrypted = new byte[keySize];
          if (decryptedBytes.Length < keySize)
          {
            Array.Copy(decryptedBytes, 0, paddedDecrypted, keySize - decryptedBytes.Length, decryptedBytes.Length);
          }
          else
          {
            Array.Copy(decryptedBytes, decryptedBytes.Length - keySize, paddedDecrypted, 0, keySize);
          }

          // Remove PKCS#1 v1.5 padding: 0x00 0x01 [padding 0xFF...] 0x00 [data]
          // or for type 2: 0x00 0x02 [random padding] 0x00 [data]
          if (paddedDecrypted.Length >= 11 &&
              paddedDecrypted[0] == 0x00 &&
              (paddedDecrypted[1] == 0x01 || paddedDecrypted[1] == 0x02))
          {
            // Find the 0x00 separator after padding
            var separatorIdx = -1;
            for (var j = 2; j < paddedDecrypted.Length; j++)
            {
              if (paddedDecrypted[j] == 0x00)
              {
                separatorIdx = j;
                break;
              }
            }

            if (separatorIdx > 0 && separatorIdx < paddedDecrypted.Length - 1)
            {
              var dataLen = paddedDecrypted.Length - separatorIdx - 1;
              output.Write(paddedDecrypted, separatorIdx + 1, dataLen);
            }
          }
          else
          {
            // No valid padding found, try to skip leading zeros
            var startIdx = 0;
            while (startIdx < paddedDecrypted.Length && paddedDecrypted[startIdx] == 0)
            {
              startIdx++;
            }
            if (startIdx < paddedDecrypted.Length)
            {
              output.Write(paddedDecrypted, startIdx, paddedDecrypted.Length - startIdx);
            }
          }

          offset += blockSize;
        }

        return Encoding.UTF8.GetString(output.ToArray());
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Decrypt error: {ex.Message}");
        return string.Empty;
      }
    }

    /// <summary>
    /// Build HTTP parameters string from dictionary (sorted by key)
    /// </summary>
    /// <param name="parameters">Dictionary of parameters</param>
    /// <returns>URL encoded parameter string</returns>
    public static string HttpParamsBuild(IDictionary<string, object> parameters)
    {
      var sortedParams = new SortedDictionary<string, object>(parameters, StringComparer.Ordinal);
      var sb = new StringBuilder();

      foreach (var kvp in sortedParams)
      {
        if (kvp.Value != null && !string.IsNullOrEmpty(kvp.Value.ToString())
            && kvp.Key != "sign" && kvp.Key != "key")
        {
          sb.Append($"{kvp.Key}={kvp.Value}&");
        }
      }

      if (sb.Length > 0)
      {
        sb.Length--; // Remove last '&'
      }

      return sb.ToString();
    }
  }
}
