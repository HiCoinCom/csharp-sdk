using System;

namespace ChainUpCustody.Crypto
{
  /// <summary>
  /// Interface for data encryption and decryption operations
  /// This interface defines the contract for encrypting and decrypting
  /// data when communicating with the ChainUp Custody API.
  /// </summary>
  public interface IDataCrypto
  {
    /// <summary>
    /// Decrypt ciphertext to plaintext
    /// </summary>
    /// <param name="cipher">The encrypted data to decrypt</param>
    /// <returns>The decrypted plaintext</returns>
    string Decode(string cipher);

    /// <summary>
    /// Encrypt plaintext to ciphertext
    /// </summary>
    /// <param name="raw">The plaintext data to encrypt</param>
    /// <returns>The encrypted ciphertext</returns>
    string Encode(string raw);

    /// <summary>
    /// Sign data using private key (RSA SHA256withRSA)
    /// Uses signPrivateKey if set, otherwise uses privateKey
    /// </summary>
    /// <param name="data">The data to sign</param>
    /// <returns>The signature (Base64 encoded)</returns>
    string Sign(string data);

    /// <summary>
    /// Check if sign private key is configured
    /// </summary>
    /// <returns>True if signPrivateKey or privateKey is available for signing</returns>
    bool HasSignKey();
  }
}
