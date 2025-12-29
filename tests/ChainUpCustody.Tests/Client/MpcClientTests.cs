using ChainUpCustody.Client;
using ChainUpCustody.Config;
using ChainUpCustody.Crypto;
using Xunit;

namespace ChainUpCustody.Tests.Client
{
  /// <summary>
  /// Unit tests for MpcClient
  /// </summary>
  public class MpcClientTests
  {
    private readonly string _privateKey;
    private readonly string _publicKey;
    private readonly string _signPrivateKey;

    public MpcClientTests()
    {
      (_privateKey, _publicKey) = RsaHelper.GenerateKeys();
      (_signPrivateKey, _) = RsaHelper.GenerateKeys();
    }

    [Fact]
    public void Builder_ShouldCreateClient()
    {
      // Act
      var client = MpcClient.Builder()
          .SetAppId("test_app_id")
          .SetUserPrivateKey(_privateKey)
          .SetWaasPublicKey(_publicKey)
          .SetSignPrivateKey(_signPrivateKey)
          .EnableLog(true)
          .Build();

      // Assert
      Assert.NotNull(client);
      Assert.NotNull(client.WorkSpaceApi);
      Assert.NotNull(client.WalletApi);
      Assert.NotNull(client.DepositApi);
      Assert.NotNull(client.WithdrawApi);
      Assert.NotNull(client.Web3Api);
      Assert.NotNull(client.AutoSweepApi);
      Assert.NotNull(client.NotifyApi);
      Assert.NotNull(client.TronBuyResourceApi);
    }

    [Fact]
    public void Constructor_WithConfig_ShouldCreateClient()
    {
      // Arrange
      var config = new MpcConfig
      {
        AppId = "test_app_id",
        UserPrivateKey = _privateKey,
        WaasPublicKey = _publicKey,
        SignPrivateKey = _signPrivateKey,
        EnableLog = false
      };

      // Act
      var client = new MpcClient(config);

      // Assert
      Assert.NotNull(client);
      Assert.NotNull(client.WorkSpaceApi);
      Assert.NotNull(client.WalletApi);
      Assert.NotNull(client.DepositApi);
      Assert.NotNull(client.WithdrawApi);
      Assert.NotNull(client.Web3Api);
      Assert.NotNull(client.AutoSweepApi);
      Assert.NotNull(client.NotifyApi);
      Assert.NotNull(client.TronBuyResourceApi);
    }

    [Fact]
    public void Factory_CreateMpcClient_ShouldWork()
    {
      // Act
      var client = WaasClientFactory.CreateMpcClient(
          appId: "test_app_id",
          userPrivateKey: _privateKey,
          waasPublicKey: _publicKey,
          signPrivateKey: _signPrivateKey,
          enableLog: true);

      // Assert
      Assert.NotNull(client);
      Assert.NotNull(client.WorkSpaceApi);
    }

    [Fact]
    public void Builder_WithoutSignKey_ShouldCreateClient()
    {
      // Act
      var client = MpcClient.Builder()
          .SetAppId("test_app_id")
          .SetUserPrivateKey(_privateKey)
          .SetWaasPublicKey(_publicKey)
          .Build();

      // Assert
      Assert.NotNull(client);
      Assert.NotNull(client.WorkSpaceApi);
    }
  }
}
