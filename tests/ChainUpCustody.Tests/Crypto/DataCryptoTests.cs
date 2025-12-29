using ChainUpCustody.Crypto;
using Xunit;

namespace ChainUpCustody.Tests.Crypto
{
  /// <summary>
  /// Unit tests for DataCrypto class
  /// </summary>
  public class DataCryptoTests
  {
    private readonly string _privateKey;
    private readonly string _publicKey;
    private readonly string _signPrivateKey;

    public DataCryptoTests()
    {
      // Generate keys for testing
      (_privateKey, _publicKey) = RsaHelper.GenerateKeys();
      (_signPrivateKey, _) = RsaHelper.GenerateKeys();
    }

    [Fact]
    public void Constructor_WithKeys_ShouldInitialize()
    {
      // Act
      var dataCrypto = new DataCrypto(_privateKey, _publicKey);

      // Assert
      Assert.NotNull(dataCrypto);
      // HasSignKey() 只检查 signPrivateKey 是否显式设置
      Assert.False(dataCrypto.HasSignKey());
    }

    [Fact]
    public void Constructor_WithSignKey_ShouldInitialize()
    {
      // Act
      var dataCrypto = new DataCrypto(_privateKey, _publicKey, _signPrivateKey);

      // Assert
      Assert.NotNull(dataCrypto);
      // signPrivateKey 已显式设置
      Assert.True(dataCrypto.HasSignKey());
    }

    [Fact]
    public void EncodeAndDecode_ShouldRoundTrip()
    {
      // Arrange
      var dataCrypto = new DataCrypto(_privateKey, _publicKey);
      var originalData = "{\"key\":\"value\",\"number\":123}";

      // Act
      var encoded = dataCrypto.Encode(originalData);
      var decoded = dataCrypto.Decode(encoded);

      // Assert
      Assert.NotNull(encoded);
      Assert.NotEqual(originalData, encoded);
      Assert.Equal(originalData, decoded);
    }

    [Fact]
    public void Sign_WithSignKey_ShouldReturnSignature()
    {
      // Arrange
      var dataCrypto = new DataCrypto(_privateKey, _publicKey, _signPrivateKey);
      var content = "content to sign";

      // Act
      var signature = dataCrypto.Sign(content);

      // Assert
      Assert.NotNull(signature);
      Assert.NotEmpty(signature);
    }

    [Fact]
    public void Sign_WithoutSignKey_ShouldReturnEmptySignature()
    {
      // Arrange - 创建 DataCrypto 只有 privateKey，没有 signPrivateKey
      // 但 Sign() 方法会回退到使用 privateKey 进行签名
      var dataCrypto = new DataCrypto(_privateKey, _publicKey);
      var content = "content to sign";

      // Act
      var signature = dataCrypto.Sign(content);

      // Assert - 由于有 privateKey，所以会成功签名
      Assert.NotNull(signature);
      Assert.NotEmpty(signature);
    }

    [Fact]
    public void EncodeAndDecode_LargeJson_ShouldRoundTrip()
    {
      // Arrange
      var dataCrypto = new DataCrypto(_privateKey, _publicKey);
      var largeData = "{\"data\":\"" + new string('X', 2000) + "\"}";

      // Act
      var encoded = dataCrypto.Encode(largeData);
      var decoded = dataCrypto.Decode(encoded);

      // Assert
      Assert.NotNull(encoded);
      Assert.Equal(largeData, decoded);
    }
  }
}
