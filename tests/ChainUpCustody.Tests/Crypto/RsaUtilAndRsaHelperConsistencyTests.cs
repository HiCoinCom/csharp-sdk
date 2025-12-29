using ChainUpCustody.Crypto;
using Xunit;

namespace ChainUpCustody.Tests.Crypto;

/// <summary>
/// Tests to verify that RsaUtil and RsaHelper produce consistent results
/// for encryption, decryption, and signing operations.
/// </summary>
public class RsaUtilAndRsaHelperConsistencyTests
{
    #region Test Data

    /// <summary>
    /// Short test data for basic operations.
    /// </summary>
    private const string ShortTestData = "Hello, ChainUp!";

    /// <summary>
    /// JSON test data simulating API request.
    /// </summary>
    private const string JsonTestData = "{\"time\":1234567890,\"charset\":\"utf-8\",\"symbol\":\"ETH\",\"amount\":\"1.5\"}";

    /// <summary>
    /// Long test data requiring segmented encryption.
    /// </summary>
    private static readonly string LongTestData = new('A', 500);

    #endregion

    #region Key Generation Tests

    [Fact]
    public void GenerateKeys_BothImplementations_ShouldProduceValidKeys()
    {
        // Act
        var (rsaUtilPublicKey, rsaUtilPrivateKey) = RsaUtil.GenerateKeys();
        var (rsaHelperPrivateKey, rsaHelperPublicKey) = RsaHelper.GenerateKeys();

        // Assert - Both should produce non-empty keys
        Assert.NotEmpty(rsaUtilPublicKey);
        Assert.NotEmpty(rsaUtilPrivateKey);
        Assert.NotEmpty(rsaHelperPublicKey);
        Assert.NotEmpty(rsaHelperPrivateKey);

        // Keys should be valid Base64
        Assert.True(IsValidBase64(rsaUtilPublicKey), "RsaUtil public key should be valid Base64");
        Assert.True(IsValidBase64(rsaUtilPrivateKey), "RsaUtil private key should be valid Base64");
        Assert.True(IsValidBase64(rsaHelperPublicKey), "RsaHelper public key should be valid Base64");
        Assert.True(IsValidBase64(rsaHelperPrivateKey), "RsaHelper private key should be valid Base64");
    }

    #endregion

    #region Signing Consistency Tests

    [Fact]
    public void Sign_SameData_BothImplementationsShouldProduceSameSignature()
    {
        // Arrange
        var (_, privateKey) = RsaUtil.GenerateKeys();
        var testData = "amount=1.5&symbol=ETH&time=1234567890";

        // Act
        var rsaUtilSignature = RsaUtil.GetSign(testData, privateKey);
        var rsaHelperSignature = RsaHelper.GetSign(testData, privateKey);

        // Assert - Both should produce the same signature
        Assert.NotEmpty(rsaUtilSignature);
        Assert.NotEmpty(rsaHelperSignature);
        Assert.Equal(rsaUtilSignature, rsaHelperSignature);
    }

    [Fact]
    public void Sign_WithMd5Hash_BothImplementationsShouldProduceSameSignature()
    {
        // Arrange
        var (_, privateKey) = RsaUtil.GenerateKeys();
        // Simulate MD5 hash result (32 character hex string)
        var md5Hash = "d41d8cd98f00b204e9800998ecf8427e";

        // Act
        var rsaUtilSignature = RsaUtil.GetSign(md5Hash, privateKey);
        var rsaHelperSignature = RsaHelper.GetSign(md5Hash, privateKey);

        // Assert
        Assert.Equal(rsaUtilSignature, rsaHelperSignature);
    }

    [Fact]
    public void Sign_MultipleDataSamples_AllShouldMatch()
    {
        // Arrange
        var (_, privateKey) = RsaUtil.GenerateKeys();
        var testCases = new[]
        {
            "simple text",
            "amount=0.001&request_id=12345&symbol=BTC",
            JsonTestData,
            "a=1&b=2&c=3&d=4&e=5",
            new string('X', 100)
        };

        foreach (var testData in testCases)
        {
            // Act
            var rsaUtilSignature = RsaUtil.GetSign(testData, privateKey);
            var rsaHelperSignature = RsaHelper.GetSign(testData, privateKey);

            // Assert
            Assert.Equal(rsaUtilSignature, rsaHelperSignature);
        }
    }

    #endregion

    #region Signature Verification Cross-Tests

    [Fact]
    public void SignatureByRsaUtil_ShouldBeVerifiableByRsaHelper()
    {
        // Arrange
        var (publicKey, privateKey) = RsaUtil.GenerateKeys();
        var testData = ShortTestData;

        // Act - Sign with RsaUtil
        var signature = RsaUtil.GetSign(testData, privateKey);

        // Assert - Verify with RsaHelper
        var isValid = RsaHelper.VerifySign(testData, signature, publicKey);
        Assert.True(isValid, "Signature created by RsaUtil should be verifiable by RsaHelper");
    }

    [Fact]
    public void SignatureByRsaHelper_ShouldBeVerifiableByRsaUtil()
    {
        // Arrange
        var (publicKey, privateKey) = RsaUtil.GenerateKeys();
        var testData = ShortTestData;

        // Act - Sign with RsaHelper
        var signature = RsaHelper.GetSign(testData, privateKey);

        // Assert - Verify with RsaUtil
        var isValid = RsaUtil.VerifySign(testData, signature, publicKey);
        Assert.True(isValid, "Signature created by RsaHelper should be verifiable by RsaUtil");
    }

    #endregion

    #region Encryption/Decryption Consistency Tests

    [Fact]
    public void EncryptByRsaUtil_DecryptByRsaHelper_ShouldReturnOriginalData()
    {
        // Arrange
        var (publicKey, privateKey) = RsaUtil.GenerateKeys();
        var testData = ShortTestData;

        // Act - Encrypt with RsaUtil, decrypt with RsaHelper
        var encrypted = RsaUtil.EncryptByPrivateKey(testData, privateKey);
        var decrypted = RsaHelper.DecryptByPublicKey(encrypted, publicKey);

        // Assert
        Assert.Equal(testData, decrypted);
    }

    [Fact]
    public void EncryptByRsaHelper_DecryptByRsaUtil_ShouldReturnOriginalData()
    {
        // Arrange
        var (publicKey, privateKey) = RsaUtil.GenerateKeys();
        var testData = ShortTestData;

        // Act - Encrypt with RsaHelper, decrypt with RsaUtil
        var encrypted = RsaHelper.EncryptByPrivateKey(testData, privateKey);
        var decrypted = RsaUtil.DecryptByPublicKey(encrypted, publicKey);

        // Assert
        Assert.Equal(testData, decrypted);
    }

    [Fact]
    public void EncryptDecrypt_JsonData_CrossCompatibility()
    {
        // Arrange
        var (publicKey, privateKey) = RsaUtil.GenerateKeys();

        // Test RsaUtil encrypt -> RsaHelper decrypt
        var encryptedByUtil = RsaUtil.EncryptByPrivateKey(JsonTestData, privateKey);
        var decryptedByHelper = RsaHelper.DecryptByPublicKey(encryptedByUtil, publicKey);
        Assert.Equal(JsonTestData, decryptedByHelper);

        // Test RsaHelper encrypt -> RsaUtil decrypt
        var encryptedByHelper = RsaHelper.EncryptByPrivateKey(JsonTestData, privateKey);
        var decryptedByUtil = RsaUtil.DecryptByPublicKey(encryptedByHelper, publicKey);
        Assert.Equal(JsonTestData, decryptedByUtil);
    }

    [Fact]
    public void EncryptDecrypt_LongData_CrossCompatibility()
    {
        // Arrange
        var (publicKey, privateKey) = RsaUtil.GenerateKeys();

        // Test RsaUtil encrypt -> RsaHelper decrypt
        var encryptedByUtil = RsaUtil.EncryptByPrivateKey(LongTestData, privateKey);
        var decryptedByHelper = RsaHelper.DecryptByPublicKey(encryptedByUtil, publicKey);
        Assert.Equal(LongTestData, decryptedByHelper);

        // Test RsaHelper encrypt -> RsaUtil decrypt
        var encryptedByHelper = RsaHelper.EncryptByPrivateKey(LongTestData, privateKey);
        var decryptedByUtil = RsaUtil.DecryptByPublicKey(encryptedByHelper, publicKey);
        Assert.Equal(LongTestData, decryptedByUtil);
    }

    #endregion

    #region URL-Safe Base64 Consistency Tests

    [Fact]
    public void Encrypt_OutputShouldBeUrlSafeBase64()
    {
        // Arrange
        var (_, privateKey) = RsaUtil.GenerateKeys();
        var testData = JsonTestData;

        // Act
        var rsaUtilEncrypted = RsaUtil.EncryptByPrivateKey(testData, privateKey);
        var rsaHelperEncrypted = RsaHelper.EncryptByPrivateKey(testData, privateKey);

        // Assert - Both should be URL-safe (no + or /)
        Assert.DoesNotContain("+", rsaUtilEncrypted);
        Assert.DoesNotContain("/", rsaUtilEncrypted);
        Assert.DoesNotContain("+", rsaHelperEncrypted);
        Assert.DoesNotContain("/", rsaHelperEncrypted);

        // Should contain URL-safe characters (- and _)
        // Note: Not all encrypted data will have these, but they shouldn't have + or /
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void Sign_EmptyData_BehaviorDifference()
    {
        // Arrange
        var (_, privateKey) = RsaUtil.GenerateKeys();

        // Act
        var rsaUtilResult = RsaUtil.GetSign(string.Empty, privateKey);
        var rsaHelperResult = RsaHelper.GetSign(string.Empty, privateKey);

        // Assert - RsaUtil returns empty for empty input (safer behavior)
        // RsaHelper signs empty string (different behavior, but valid for non-empty private key)
        Assert.Empty(rsaUtilResult);
        // Note: RsaHelper will actually sign an empty string, which is a behavioral difference
        // This is acceptable as long as the caller validates input before signing
    }

    [Fact]
    public void Encrypt_EmptyData_BothShouldReturnEmpty()
    {
        // Arrange
        var (_, privateKey) = RsaUtil.GenerateKeys();

        // Act
        var rsaUtilResult = RsaUtil.EncryptByPrivateKey(string.Empty, privateKey);
        var rsaHelperResult = RsaHelper.EncryptByPrivateKey(string.Empty, privateKey);

        // Assert
        Assert.Empty(rsaUtilResult);
        Assert.Empty(rsaHelperResult);
    }

    [Fact]
    public void Decrypt_EmptyData_BothShouldReturnEmpty()
    {
        // Arrange
        var (publicKey, _) = RsaUtil.GenerateKeys();

        // Act
        var rsaUtilResult = RsaUtil.DecryptByPublicKey(string.Empty, publicKey);
        var rsaHelperResult = RsaHelper.DecryptByPublicKey(string.Empty, publicKey);

        // Assert
        Assert.Empty(rsaUtilResult);
        Assert.Empty(rsaHelperResult);
    }

    #endregion

    #region Helper Methods

    private static bool IsValidBase64(string input)
    {
        try
        {
            Convert.FromBase64String(input);
            return true;
        }
        catch
        {
            return false;
        }
    }

    #endregion
}
