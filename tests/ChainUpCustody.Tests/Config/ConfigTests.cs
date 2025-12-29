using ChainUpCustody.Config;
using Xunit;

namespace ChainUpCustody.Tests.Config
{
  /// <summary>
  /// Unit tests for configuration classes
  /// </summary>
  public class ConfigTests
  {
    [Fact]
    public void WaasConfig_DefaultValues_ShouldBeCorrect()
    {
      // Act
      var config = new WaasConfig
      {
        AppId = "test",
        UserPrivateKey = "test",
        WaasPublicKey = "test"
      };

      // Assert
      Assert.Equal("https://openapi.chainup.com/api/v2", config.Domain);
      Assert.Equal("v2", config.Version);
      Assert.Equal("utf-8", config.Charset);
      Assert.False(config.EnableLog);
    }

    [Fact]
    public void WaasConfig_SetValues_ShouldWork()
    {
      // Act
      var config = new WaasConfig
      {
        AppId = "test_app_id",
        UserPrivateKey = "private_key",
        WaasPublicKey = "public_key",
        Domain = "https://custom.domain.com",
        EnableLog = true
      };

      // Assert
      Assert.Equal("test_app_id", config.AppId);
      Assert.Equal("private_key", config.UserPrivateKey);
      Assert.Equal("public_key", config.WaasPublicKey);
      Assert.Equal("https://custom.domain.com", config.Domain);
      Assert.True(config.EnableLog);
    }

    [Fact]
    public void MpcConfig_DefaultValues_ShouldBeCorrect()
    {
      // Act
      var config = new MpcConfig
      {
        AppId = "test",
        UserPrivateKey = "test",
        WaasPublicKey = "test"
      };

      // Assert
      Assert.Equal("https://openapi.chainup.com", config.Domain);
      Assert.Equal("v2", config.Version);
      Assert.Equal("utf-8", config.Charset);
      Assert.False(config.EnableLog);
    }

    [Fact]
    public void MpcConfig_SetValues_ShouldWork()
    {
      // Act
      var config = new MpcConfig
      {
        AppId = "test_app_id",
        UserPrivateKey = "private_key",
        WaasPublicKey = "public_key",
        SignPrivateKey = "sign_key",
        EnableLog = true
      };

      // Assert
      Assert.Equal("test_app_id", config.AppId);
      Assert.Equal("private_key", config.UserPrivateKey);
      Assert.Equal("public_key", config.WaasPublicKey);
      Assert.Equal("sign_key", config.SignPrivateKey);
      Assert.True(config.EnableLog);
    }

    [Fact]
    public void MpcConfig_InheritsFromWaasConfig()
    {
      // Arrange
      var config = new MpcConfig
      {
        AppId = "test",
        UserPrivateKey = "test",
        WaasPublicKey = "test"
      };

      // Assert - MpcConfig should be assignable to WaasConfig
      WaasConfig waasConfig = config;
      Assert.NotNull(waasConfig);
    }
  }
}
