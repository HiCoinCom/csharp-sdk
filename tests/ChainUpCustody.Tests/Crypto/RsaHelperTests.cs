using System;
using System.Collections.Generic;
using ChainUpCustody.Crypto;
using Xunit;

namespace ChainUpCustody.Tests.Crypto
{
  /// <summary>
  /// Unit tests for RSA encryption/decryption
  /// </summary>
  public class RsaHelperTests
  {
    [Fact]
    public void GenerateKeys_ShouldGenerateValidKeyPair()
    {
      // Act
      var (privateKey, publicKey) = RsaHelper.GenerateKeys();

      // Assert
      Assert.NotNull(privateKey);
      Assert.NotNull(publicKey);
      // Keys are returned as Base64 encoded PKCS8 (private) and X509 (public) formats
      Assert.NotEmpty(privateKey);
      Assert.NotEmpty(publicKey);
      // Verify Base64 format - should be able to decode
      var privateKeyBytes = Convert.FromBase64String(privateKey);
      var publicKeyBytes = Convert.FromBase64String(publicKey);
      Assert.True(privateKeyBytes.Length > 0);
      Assert.True(publicKeyBytes.Length > 0);
    }

    [Fact]
    public void EncryptDecrypt_ShouldRoundTrip()
    {
      // Arrange
      var (privateKey, publicKey) = RsaHelper.GenerateKeys();
      var originalText = "Hello, ChainUp Custody!";

      // Act - Encrypt with private key, decrypt with public key
      var encrypted = RsaHelper.EncryptByPrivateKey(originalText, privateKey);
      var decrypted = RsaHelper.DecryptByPublicKey(encrypted, publicKey);

      // Assert
      Assert.NotNull(encrypted);
      Assert.NotEqual(originalText, encrypted);
      Assert.Equal(originalText, decrypted);
    }

    [Fact]
    public void EncryptDecrypt_LongText_ShouldRoundTrip()
    {
      // Arrange
      var (privateKey, publicKey) = RsaHelper.GenerateKeys();
      var originalText = new string('A', 1000); // Long text that requires segment encryption

      // Act
      var encrypted = RsaHelper.EncryptByPrivateKey(originalText, privateKey);
      var decrypted = RsaHelper.DecryptByPublicKey(encrypted, publicKey);

      // Assert
      Assert.NotNull(encrypted);
      Assert.Equal(originalText, decrypted);
    }

    [Fact]
    public void SignAndVerify_ShouldWork()
    {
      // Arrange
      var (privateKey, publicKey) = RsaHelper.GenerateKeys();
      var content = "Data to be signed";

      // Act
      var signature = RsaHelper.GetSign(content, privateKey);
      var isValid = RsaHelper.VerifySign(content, signature, publicKey);

      // Assert
      Assert.NotNull(signature);
      Assert.True(isValid);
    }

    [Fact]
    public void VerifySign_WrongContent_ShouldReturnFalse()
    {
      // Arrange
      var (privateKey, publicKey) = RsaHelper.GenerateKeys();
      var content = "Data to be signed";
      var wrongContent = "Wrong data";

      // Act
      var signature = RsaHelper.GetSign(content, privateKey);
      var isValid = RsaHelper.VerifySign(wrongContent, signature, publicKey);

      // Assert
      Assert.False(isValid);
    }

    [Fact]
    public void HttpParamsBuild_ShouldBuildCorrectString()
    {
      // Arrange
      var parameters = new Dictionary<string, object>
      {
        { "app_id", "testAppId" },
        { "data", "encryptedData" },
        { "time", 1234567890L }
      };

      // Act
      var result = RsaHelper.HttpParamsBuild(parameters);

      // Assert
      Assert.Contains("app_id=testAppId", result);
      Assert.Contains("data=encryptedData", result);
      Assert.Contains("time=1234567890", result);
      Assert.Contains("&", result);
    }
  }
}
