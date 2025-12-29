using ChainUpCustody.Client;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using Xunit;

namespace ChainUpCustody.Tests.Client
{
  /// <summary>
  /// Unit tests for WaasClient
  /// </summary>
  public class WaasClientTests
  {
    private readonly string _privateKey;
    private readonly string _publicKey;

    public WaasClientTests()
    {
      (_privateKey, _publicKey) = RsaHelper.GenerateKeys();
    }

    [Fact]
    public void Builder_ShouldCreateClient()
    {
      // Act
      var client = WaasClient.Builder()
          .SetAppId("test_app_id")
          .SetUserPrivateKey(_privateKey)
          .SetWaasPublicKey(_publicKey)
          .EnableLog(true)
          .Build();

      // Assert
      Assert.NotNull(client);
      Assert.NotNull(client.UserApi);
      Assert.NotNull(client.AccountApi);
      Assert.NotNull(client.CoinApi);
      Assert.NotNull(client.BillingApi);
      Assert.NotNull(client.TransferApi);
      Assert.NotNull(client.AsyncNotifyApi);
    }

    [Fact]
    public void Constructor_WithConfig_ShouldCreateClient()
    {
      // Arrange
      var config = new WaasConfig
      {
        AppId = "test_app_id",
        UserPrivateKey = _privateKey,
        WaasPublicKey = _publicKey,
        EnableLog = false
      };

      // Act
      var client = new WaasClient(config);

      // Assert
      Assert.NotNull(client);
      Assert.NotNull(client.UserApi);
      Assert.NotNull(client.AccountApi);
      Assert.NotNull(client.CoinApi);
      Assert.NotNull(client.BillingApi);
      Assert.NotNull(client.TransferApi);
      Assert.NotNull(client.AsyncNotifyApi);
    }

    [Fact]
    public void Factory_CreateWaasClient_ShouldWork()
    {
      // Act
      var client = WaasClientFactory.CreateWaasClient(
          appId: "test_app_id",
          userPrivateKey: _privateKey,
          waasPublicKey: _publicKey,
          enableLog: true);

      // Assert
      Assert.NotNull(client);
      Assert.NotNull(client.UserApi);
      Assert.NotNull(client.TransferApi);
    }
  }
}
