using Xunit;
using ChainUpCustody.Crypto;

namespace ChainUpCustody.Tests.Crypto
{
  public class RsaUtilTests
  {
    // 测试密钥对（PKCS8 格式）
    private const string TestPrivateKey = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQDSxEURQHg5zk/6g7laQhXjAhUkPj59kwwtFm3Lv+aBplMYs+miFMK32xhL+XWXq0jTM3DX63YX+J9yQLuKo6fbzlQIil+4wXD8v1liVOM9RAA3dkeJIiOGZQZ2GKJyBsQIdJY3+n5A71MrCE5imVy/LA4iX3SKLtY7HkgruWWRHytQTK22jCxSa0hpltaqeONMUBfjRxFftXUrapVyFUFrtRiE9UEk1GPcq6l5KD81fNq64EKkNuBttXXBmxNGJK8X3L0KSKLuPfC+4VZerd/WC5lqTI2YmzfCGv02XM+v4A/Xy2HPKK+V2Fg4ULVmV6VxD7T1BqSoLd1kBF/tAgMBAAECggEAJE3PAp2F0W8zN/Dh3kYCF5XjSz9T/VLTxKHCzG+Hg6VXG0mBcPD+pBwsVHK1JW2GXVU1ZoQBJxvIxNYBKXqHYLvJTOpwEe1NDq8TiaVVZCg6WqSCnXlSRVBDSBuLf2D4QNiRlLbvCOJYKTjRYZV7+C/kCB1Hq1C7V/7D/h1JGqPuaL0X3JXDHI8kY9JH5gJ8K1bY6F8X7gqJ9QdTJvL1TKTM3L1TqJ6KWKXWXJ9JJTMQ8L0K8B2F6C9JGLJ5BL5J0K3L0L7JGLJ5BL5J0K3L0L7JGLJ5BL5J0K3L0L7JGLJ5BL5J0K3L0L7JGLJ5BL5J0K3L0L7JGLJ5BL5J0K3L0LwKBgQDzJGL";
    private const string TestPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA0sRFEUB4Oc5P+oO5WkIV4wIVJD4+fZMMLRZty7/mgaZTGLPpohTCt9sYS/l1l6tI0zNw1+t2F/ifckC7iqOn285UCIpfuMFw/L9ZYlTjPUQAN3ZHiSIjhmUGdhiicgbECHSWN/p+QO9TKwhOYplcvywOIl90ii7WOx4IK7llkR8rUEyttowsUmtIaZbWqnjjTFAX40cRX7V1K2qVchVBa7UYhPVBJNRj3KupeS";

    // 使用 Java SDK 格式的测试密钥
    private const string JavaTestPrivateKey = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQC3RQDl8bqyV97TFWJXifNxYfj3MKMlZnfVU8/UXD2raqYQ66PVCKr8HjJZlQ8EzxMSJQzp44yJ6j0l3RYRpXfhHKZpjjkVxNEnjDVX6sYL0TFGOQSjNLYt7XnOYq7JVqRZJFZJmNl9UXJ5NLfVN1OLz2Td7cJxvCkQxQd8sC8gO3QpVKnQO2bF3LRNT2zB5c5i+E9KcKQJQ/BYoRz9Kz2lJIqGvY4rMKfLt6CKKZ7Y1q7P3sL5eH5hJdE8A9e5dZfL8h2JGLJ5BL5J0K3L0L7JGLJ5BL5J0K3L0L7JGLJ5BL5J0K3L0L7JGLJ5BL5J0K3L0L7JGLJ5BL5J0K3L0LAgMBAAECggEAJE3PAp2F0W8zN/Dh3kYCF5XjSz9T/VLTxKHCzG+Hg6VXG0mBcPD+pBwsVHK1JW2GXVU1ZoQBJxvIxNYBKXqHYLvJTOpwEe1NDq8TiaVVZCg6WqSCnXlSRVBDSBuLf2D4QNiRlLbvCOJYKTjRYZV7+C/kCB1Hq1C7V/7D/h1JGqPuaL0X3JXDHI8kY9JH5gJ8K1bY6F8X7gqJ9QdTJvL1TKTM3L1TqJ6KWKXWXJ9JJTMQ8L0K8B2F6C9JGLJ5BL5J0K3L0L7JGLJ5BL5J0K3L0L7JGLJ5BL5J0K3L0L7JGLJ5BL5J0K3L0L7JGLJ5BL5J0K3L0L7JGLJ5BL5J0K3L0LwKBgQDzJGL";
    private const string JavaTestPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAt0UA5fG6slfe0xViV4nzcWH49zCjJWZ31VPP1Fw9q2qmEOuj1Qiq/B4yWZUPBM8TEiUM6eOMieo9Jd0WEaV34RymaY45FcTRJ4w1V+rGC9ExRjkEozS2Le15zmKuyVakWSRWSZjZfVFyeTS31TdTi89k3e3CcbwpEMUHfLAvIDt0KVSp0DtmxdS0TU9sweXOYvhPSnCkCUPwWKEc/Ss9pSSKhr2OKzCny7egiime2Nauz97C+Xh+YSXRPAPXuXWXy/IdiQKD";

    [Fact]
    public void GenerateKeys_ShouldReturnValidKeyPair()
    {
      // Act
      var (publicKey, privateKey) = RsaUtil.GenerateKeys();

      // Assert
      Assert.NotNull(publicKey);
      Assert.NotNull(privateKey);
      Assert.NotEmpty(publicKey);
      Assert.NotEmpty(privateKey);
    }

    [Fact]
    public void SignAndVerify_ShouldWorkCorrectly()
    {
      // Arrange
      var (publicKey, privateKey) = RsaUtil.GenerateKeys();
      var data = "test data to sign";

      // Act
      var signature = RsaUtil.GetSign(data, privateKey);
      var isValid = RsaUtil.VerifySign(data, signature, publicKey);

      // Assert
      Assert.NotEmpty(signature);
      Assert.True(isValid);
    }

    [Fact]
    public void VerifySign_WithWrongData_ShouldReturnFalse()
    {
      // Arrange
      var (publicKey, privateKey) = RsaUtil.GenerateKeys();
      var data = "original data";
      var wrongData = "tampered data";

      // Act
      var signature = RsaUtil.GetSign(data, privateKey);
      var isValid = RsaUtil.VerifySign(wrongData, signature, publicKey);

      // Assert
      Assert.False(isValid);
    }

    [Fact]
    public void EncryptAndDecrypt_ShouldWorkCorrectly()
    {
      // Arrange
      var (publicKey, privateKey) = RsaUtil.GenerateKeys();
      var plainText = "Hello, this is a test message for encryption!";

      // Act
      var encrypted = RsaUtil.EncryptByPrivateKey(plainText, privateKey);
      var decrypted = RsaUtil.DecryptByPublicKey(encrypted, publicKey);

      // Assert
      Assert.NotEmpty(encrypted);
      Assert.Equal(plainText, decrypted);
    }

    [Fact]
    public void EncryptAndDecrypt_WithLongData_ShouldWorkCorrectly()
    {
      // Arrange
      var (publicKey, privateKey) = RsaUtil.GenerateKeys();
      var plainText = new string('A', 1000); // 1000 字符，需要分段加密

      // Act
      var encrypted = RsaUtil.EncryptByPrivateKey(plainText, privateKey);
      var decrypted = RsaUtil.DecryptByPublicKey(encrypted, publicKey);

      // Assert
      Assert.NotEmpty(encrypted);
      Assert.Equal(plainText, decrypted);
    }

    [Fact]
    public void EncryptAndDecrypt_WithJson_ShouldWorkCorrectly()
    {
      // Arrange
      var (publicKey, privateKey) = RsaUtil.GenerateKeys();
      var jsonData = "{\"time\":1234567890,\"charset\":\"utf-8\",\"symbol\":\"ETH\",\"amount\":\"1.5\"}";

      // Act
      var encrypted = RsaUtil.EncryptByPrivateKey(jsonData, privateKey);
      var decrypted = RsaUtil.DecryptByPublicKey(encrypted, publicKey);

      // Assert
      Assert.NotEmpty(encrypted);
      Assert.Equal(jsonData, decrypted);
    }

    [Fact]
    public void HttpParamsBuild_ShouldSortParametersCorrectly()
    {
      // Arrange
      var parameters = new Dictionary<string, object?>
            {
                { "b", "value2" },
                { "a", "value1" },
                { "c", "value3" }
            };

      // Act
      var result = RsaUtil.HttpParamsBuild(parameters);

      // Assert
      Assert.Equal("a=value1&b=value2&c=value3", result);
    }

    [Fact]
    public void HttpParamsBuild_ShouldExcludeSignAndKey()
    {
      // Arrange
      var parameters = new Dictionary<string, object?>
            {
                { "data", "test" },
                { "sign", "should_be_excluded" },
                { "key", "should_be_excluded" }
            };

      // Act
      var result = RsaUtil.HttpParamsBuild(parameters);

      // Assert
      Assert.Equal("data=test", result);
    }

    [Fact]
    public void HttpParamsBuild_ShouldExcludeEmptyValues()
    {
      // Arrange
      var parameters = new Dictionary<string, object?>
            {
                { "a", "value1" },
                { "b", "" },
                { "c", null }
            };

      // Act
      var result = RsaUtil.HttpParamsBuild(parameters);

      // Assert
      Assert.Equal("a=value1", result);
    }

    [Fact]
    public void GetSign_WithEmptyData_ShouldReturnEmpty()
    {
      // Arrange
      var (_, privateKey) = RsaUtil.GenerateKeys();

      // Act
      var signature = RsaUtil.GetSign("", privateKey);

      // Assert
      Assert.Empty(signature);
    }

    [Fact]
    public void EncryptByPrivateKey_WithEmptyData_ShouldReturnEmpty()
    {
      // Arrange
      var (_, privateKey) = RsaUtil.GenerateKeys();

      // Act
      var encrypted = RsaUtil.EncryptByPrivateKey("", privateKey);

      // Assert
      Assert.Empty(encrypted);
    }

    [Fact]
    public void DecryptByPublicKey_WithEmptyData_ShouldReturnEmpty()
    {
      // Arrange
      var (publicKey, _) = RsaUtil.GenerateKeys();

      // Act
      var decrypted = RsaUtil.DecryptByPublicKey("", publicKey);

      // Assert
      Assert.Empty(decrypted);
    }
  }
}
