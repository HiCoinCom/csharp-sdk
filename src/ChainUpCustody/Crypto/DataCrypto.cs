using System;

namespace ChainUpCustody.Crypto
{
  /// <summary>
  /// Default implementation of data encryption and decryption
  /// This class uses RSA encryption for secure data transmission.
  /// Private key is used for encryption, public key for decryption.
  /// Default: uses RsaUtil (standard .NET Cryptography API)
  /// Legacy: can switch to RsaHelper for backward compatibility
  /// </summary>
  public class DataCrypto : IDataCrypto
  {
    /// <summary>
    /// Private key used to encrypt data before sending
    /// </summary>
    private readonly string _privateKey;

    /// <summary>
    /// Public key used to decrypt data received from server
    /// </summary>
    private readonly string _publicKey;

    /// <summary>
    /// Optional private key specifically used for transaction signing.
    /// If not set, privateKey will be used for signing.
    /// </summary>
    private string? _signPrivateKey;

    /// <summary>
    /// Whether to use legacy RsaHelper instead of RsaUtil
    /// </summary>
    private readonly bool _useLegacy;

    /// <summary>
    /// Default constructor
    /// </summary>
    public DataCrypto()
    {
      _privateKey = string.Empty;
      _publicKey = string.Empty;
      _useLegacy = false;
    }

    /// <summary>
    /// Constructor with key initialization
    /// </summary>
    /// <param name="privateKey">RSA private key for encryption</param>
    /// <param name="publicKey">RSA public key for decryption</param>
    /// <param name="useLegacy">Whether to use legacy RsaHelper (default: false, use RsaUtil)</param>
    public DataCrypto(string privateKey, string publicKey, bool useLegacy = false)
    {
      _privateKey = privateKey ?? string.Empty;
      _publicKey = publicKey ?? string.Empty;
      _useLegacy = useLegacy;
    }

    /// <summary>
    /// Constructor with key initialization including sign private key
    /// </summary>
    /// <param name="privateKey">RSA private key for encryption</param>
    /// <param name="publicKey">RSA public key for decryption</param>
    /// <param name="signPrivateKey">RSA private key for transaction signing (optional)</param>
    /// <param name="useLegacy">Whether to use legacy RsaHelper (default: false, use RsaUtil)</param>
    public DataCrypto(string privateKey, string publicKey, string? signPrivateKey, bool useLegacy = false)
        : this(privateKey, publicKey, useLegacy)
    {
      _signPrivateKey = signPrivateKey;
    }

    /// <summary>
    /// Decrypt ciphertext using public key
    /// </summary>
    /// <param name="cipher">Encrypted data to decrypt</param>
    /// <returns>Decrypted plaintext</returns>
    public string Decode(string cipher)
    {
      if (_useLegacy)
      {
        return RsaHelper.DecryptByPublicKey(cipher, _publicKey);
      }
      return RsaUtil.DecryptByPublicKey(cipher, _publicKey);
    }

    /// <summary>
    /// Encrypt plaintext using private key
    /// </summary>
    /// <param name="raw">Plaintext data to encrypt</param>
    /// <returns>Encrypted ciphertext</returns>
    public string Encode(string raw)
    {
      if (_useLegacy)
      {
        return RsaHelper.EncryptByPrivateKey(raw, _privateKey);
      }
      return RsaUtil.EncryptByPrivateKey(raw, _privateKey);
    }

    /// <summary>
    /// Sign data using private key (RSA SHA256withRSA)
    /// Uses signPrivateKey if set, otherwise uses privateKey
    /// </summary>
    /// <param name="data">The data to sign</param>
    /// <returns>The signature (Base64 encoded), or empty string if signing fails</returns>
    public string Sign(string data)
    {
      if (string.IsNullOrEmpty(data))
      {
        return string.Empty;
      }

      var keyToUse = !string.IsNullOrEmpty(_signPrivateKey) ? _signPrivateKey : _privateKey;
      if (string.IsNullOrEmpty(keyToUse))
      {
        return string.Empty;
      }

      if (_useLegacy)
      {
        return RsaHelper.GetSign(data, keyToUse);
      }
      return RsaUtil.GetSign(data, keyToUse);
    }

    /// <summary>
    /// Check if sign private key is explicitly configured
    /// </summary>
    /// <returns>True if signPrivateKey is explicitly set</returns>
    public bool HasSignKey()
    {
      return !string.IsNullOrEmpty(_signPrivateKey);
    }

    /// <summary>
    /// Get sign private key
    /// </summary>
    /// <returns>signPrivateKey if set, otherwise privateKey</returns>
    public string GetSignPrivateKey()
    {
      return !string.IsNullOrEmpty(_signPrivateKey) ? _signPrivateKey : _privateKey;
    }

    /// <summary>
    /// Set sign private key for transaction signing
    /// </summary>
    /// <param name="signPrivateKey">RSA private key for signing</param>
    public void SetSignPrivateKey(string signPrivateKey)
    {
      _signPrivateKey = signPrivateKey;
    }
  }
}
