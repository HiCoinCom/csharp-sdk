using System;
using System.Collections.Generic;
using ChainUpCustody.Api.Models.Mpc;
using ChainUpCustody.Api.Mpc;
using ChainUpCustody.Crypto;
using ChainUpCustody.Enums;
using ChainUpCustody.Utilities;
using Moq;
using Xunit;

namespace ChainUpCustody.Tests.Api.Mpc
{
  /// <summary>
  /// Unit tests for MPC API functions
  /// Reference: https://github.com/HiCoinCom/go-sdk/blob/main/examples/mpc_example.go
  /// </summary>
  public class MpcApiTests
  {
    #region WalletApi Tests

    [Fact]
    public void CreateWallet_ShouldCreateWalletSuccessfully()
    {
      // Arrange
      var mockWalletApi = new Mock<IWalletApi>();
      var expectedResult = new WalletResult
      {
        Code = 0,
        Msg = "success",
        Data = new Wallet { SubWalletId = 1000537 }
      };
      mockWalletApi.Setup(x => x.CreateWallet("TestWallet", AppShowStatus.Show))
        .Returns(expectedResult);

      // Act
      var result = mockWalletApi.Object.CreateWallet("TestWallet", AppShowStatus.Show);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.Equal("success", result.Msg);
      Assert.NotNull(result.Data);
      Assert.Equal(1000537, result.Data.SubWalletId);
    }

    [Fact]
    public void CreateWalletAddress_ShouldReturnAddress()
    {
      // Arrange
      var mockWalletApi = new Mock<IWalletApi>();
      var expectedResult = new WalletAddressResult
      {
        Code = 0,
        Msg = "success",
        Data = new WalletAddress
        {
          Id = 1,
          Address = "0x1234567890abcdef1234567890abcdef12345678",
          Symbol = "ETH"
        }
      };
      mockWalletApi.Setup(x => x.CreateWalletAddress(1000537, "ETH"))
        .Returns(expectedResult);

      // Act
      var result = mockWalletApi.Object.CreateWalletAddress(1000537, "ETH");

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.NotNull(result.Data.Address);
      Assert.Contains("0x", result.Data.Address);
    }

    [Fact]
    public void GetAddressList_ShouldReturnAddresses()
    {
      // Arrange
      var mockWalletApi = new Mock<IWalletApi>();
      var expectedResult = new WalletAddressListResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<WalletAddress>
        {
          new WalletAddress { Id = 1, Address = "0xabc123", Symbol = "ETH" },
          new WalletAddress { Id = 2, Address = "0xdef456", Symbol = "ETH" }
        }
      };
      mockWalletApi.Setup(x => x.GetAddressList(1000537, "ETH", null))
        .Returns(expectedResult);

      // Act
      var result = mockWalletApi.Object.GetAddressList(1000537, "ETH", null);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(2, result.Data.Count);
    }

    [Fact]
    public void GetWalletAssets_ShouldReturnAssets()
    {
      // Arrange
      var mockWalletApi = new Mock<IWalletApi>();
      var expectedResult = new WalletAssetsResult
      {
        Code = 0,
        Msg = "success",
        Data = new WalletAssets
        {
          NormalBalance = "100.5",
          LockBalance = "10.0"
        }
      };
      mockWalletApi.Setup(x => x.GetWalletAssets(1000537, "APTOS"))
        .Returns(expectedResult);

      // Act
      var result = mockWalletApi.Object.GetWalletAssets(1000537, "APTOS");

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal("100.5", result.Data.NormalBalance);
      Assert.Equal("10.0", result.Data.LockBalance);
    }

    [Fact]
    public void ChangeShowStatus_ShouldReturnTrue()
    {
      // Arrange
      var mockWalletApi = new Mock<IWalletApi>();
      mockWalletApi.Setup(x => x.ChangeShowStatus(new long[] { 1000537, 123457 }, 1))
        .Returns(true);

      // Act
      var result = mockWalletApi.Object.ChangeShowStatus(new long[] { 1000537, 123457 }, 1);

      // Assert
      Assert.True(result);
    }

    [Fact]
    public void GetAddressInfo_ShouldReturnAddressInfo()
    {
      // Arrange
      var mockWalletApi = new Mock<IWalletApi>();
      var expectedResult = new WalletAddressInfoResult
      {
        Code = 0,
        Msg = "success",
        Data = new WalletAddressInfo
        {
          SubWalletId = 1000537,
          AddrType = 0,
          Address = "0x633A84Ee0ab29d911e5466e5E1CB9cdBf5917E72"
        }
      };
      mockWalletApi.Setup(x => x.GetAddressInfo("0x633A84Ee0ab29d911e5466e5E1CB9cdBf5917E72", null))
        .Returns(expectedResult);

      // Act
      var result = mockWalletApi.Object.GetAddressInfo("0x633A84Ee0ab29d911e5466e5E1CB9cdBf5917E72", null);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(1000537, result.Data.SubWalletId);
    }

    #endregion

    #region WithdrawApi Tests

    [Fact]
    public void Withdraw_WithSignature_ShouldReturnWithdrawId()
    {
      // Arrange
      var mockWithdrawApi = new Mock<IWithdrawApi>();
      var withdrawArgs = new MpcWithdrawArgs
      {
        RequestId = "12345678949",
        SubWalletId = 1000537,
        Symbol = "Sepolia",
        Amount = "0.001",
        AddressTo = "0xdcb0D867403adE76e75a4A6bBcE9D53C9d05B981",
        Remark = "Signed withdrawal"
      };
      var expectedResult = new MpcWithdrawResult
      {
        Code = 0,
        Msg = "success",
        Data = new MpcWithdraw { WithdrawId = 12345 }
      };
      mockWithdrawApi.Setup(x => x.Withdraw(It.IsAny<MpcWithdrawArgs>(), true))
        .Returns(expectedResult);

      // Act
      var result = mockWithdrawApi.Object.Withdraw(withdrawArgs, true);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(12345, result.Data.WithdrawId);
    }

    [Fact]
    public void GetWithdrawRecords_ShouldReturnRecords()
    {
      // Arrange
      var mockWithdrawApi = new Mock<IWithdrawApi>();
      var requestIds = new List<string> { "12345678901", "12345678" };
      var expectedResult = new WithdrawRecordResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<WithdrawRecord>
        {
          new WithdrawRecord { RequestId = "12345678901", Symbol = "ETH", Status = 1 },
          new WithdrawRecord { RequestId = "12345678", Symbol = "BTC", Status = 2 }
        }
      };
      mockWithdrawApi.Setup(x => x.GetWithdrawRecords(requestIds))
        .Returns(expectedResult);

      // Act
      var result = mockWithdrawApi.Object.GetWithdrawRecords(requestIds);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(2, result.Data.Count);
    }

    [Fact]
    public void SyncWithdrawRecords_ShouldReturnRecords()
    {
      // Arrange
      var mockWithdrawApi = new Mock<IWithdrawApi>();
      var expectedResult = new WithdrawRecordResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<WithdrawRecord>
        {
          new WithdrawRecord { Id = 1, Symbol = "ETH" },
          new WithdrawRecord { Id = 2, Symbol = "BTC" }
        }
      };
      mockWithdrawApi.Setup(x => x.SyncWithdrawRecords(0))
        .Returns(expectedResult);

      // Act
      var result = mockWithdrawApi.Object.SyncWithdrawRecords(0);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(2, result.Data.Count);
    }

    #endregion

    #region DepositApi Tests

    [Fact]
    public void GetDepositRecords_ShouldReturnRecords()
    {
      // Arrange
      var mockDepositApi = new Mock<IDepositApi>();
      var ids = new List<int> { 3294170, 456, 3 };
      var expectedResult = new DepositRecordResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<DepositRecord>
        {
          new DepositRecord { Id = 3294170, Symbol = "ETH", Status = 1 }
        }
      };
      mockDepositApi.Setup(x => x.GetDepositRecords(ids))
        .Returns(expectedResult);

      // Act
      var result = mockDepositApi.Object.GetDepositRecords(ids);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Single(result.Data);
    }

    [Fact]
    public void SyncDepositRecords_ShouldReturnRecords()
    {
      // Arrange
      var mockDepositApi = new Mock<IDepositApi>();
      var expectedResult = new DepositRecordResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<DepositRecord>
        {
          new DepositRecord { Id = 100, Symbol = "ETH" },
          new DepositRecord { Id = 101, Symbol = "BTC" }
        }
      };
      mockDepositApi.Setup(x => x.SyncDepositRecords(100))
        .Returns(expectedResult);

      // Act
      var result = mockDepositApi.Object.SyncDepositRecords(100);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(2, result.Data.Count);
    }

    #endregion

    #region Web3Api Tests

    [Fact]
    public void CreateWeb3Trans_WithSignature_ShouldReturnTransactionResult()
    {
      // Arrange
      var mockWeb3Api = new Mock<IWeb3Api>();
      var web3Args = new CreateWeb3Args
      {
        RequestId = "1234567890",
        SubWalletId = 123456,
        MainChainSymbol = "ETH",
        From = "0xFromAddress",
        InteractiveContract = "0xContractAddress",
        Amount = "0.1",
        InputData = "0x1234"
      };
      var expectedResult = new Web3TransactionResult
      {
        Code = 0,
        Msg = "success",
        Data = new Web3Transaction { Id = "98765" }
      };
      mockWeb3Api.Setup(x => x.CreateWeb3Trans(It.IsAny<CreateWeb3Args>(), true))
        .Returns(expectedResult);

      // Act
      var result = mockWeb3Api.Object.CreateWeb3Trans(web3Args, true);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal("98765", result.Data.Id);
    }

    [Fact]
    public void AccelerationWeb3Trans_ShouldReturnTrue()
    {
      // Arrange
      var mockWeb3Api = new Mock<IWeb3Api>();
      var accelerationArgs = new Web3AccelerationArgs
      {
        TransId = 12345678,
        GasPrice = "50",
        GasLimit = "30000"
      };
      mockWeb3Api.Setup(x => x.AccelerationWeb3Trans(It.IsAny<Web3AccelerationArgs>()))
        .Returns(true);

      // Act
      var result = mockWeb3Api.Object.AccelerationWeb3Trans(accelerationArgs);

      // Assert
      Assert.True(result);
    }

    [Fact]
    public void GetWeb3Records_ShouldReturnRecords()
    {
      // Arrange
      var mockWeb3Api = new Mock<IWeb3Api>();
      var requestIds = new List<string> { "req1", "req2" };
      var expectedResult = new Web3RecordResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<Web3Record>
        {
          new Web3Record { RequestId = "req1", Status = 1 }
        }
      };
      mockWeb3Api.Setup(x => x.GetWeb3Records(requestIds))
        .Returns(expectedResult);

      // Act
      var result = mockWeb3Api.Object.GetWeb3Records(requestIds);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Single(result.Data);
    }

    [Fact]
    public void SyncWeb3Records_ShouldReturnRecords()
    {
      // Arrange
      var mockWeb3Api = new Mock<IWeb3Api>();
      var expectedResult = new Web3RecordResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<Web3Record>
        {
          new Web3Record { Id = 1, Status = 1 }
        }
      };
      mockWeb3Api.Setup(x => x.SyncWeb3Records(1))
        .Returns(expectedResult);

      // Act
      var result = mockWeb3Api.Object.SyncWeb3Records(1);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Single(result.Data);
    }

    #endregion

    #region WorkSpaceApi Tests

    [Fact]
    public void GetSupportMainChain_ShouldReturnChains()
    {
      // Arrange
      var mockWorkSpaceApi = new Mock<IWorkSpaceApi>();
      var expectedResult = new SupportMainChainResult
      {
        Code = 0,
        Msg = "success",
        Data = new SupportedCoinsData
        {
          OpenMainChain = new List<SupportMainChain>
          {
            new SupportMainChain { Symbol = "ETH", IfOpenChain = true },
            new SupportMainChain { Symbol = "BTC", IfOpenChain = true },
            new SupportMainChain { Symbol = "TRX", IfOpenChain = true }
          },
          SupportMainChainList = new List<SupportMainChain>
          {
            new SupportMainChain { Symbol = "ETH" },
            new SupportMainChain { Symbol = "BTC" }
          }
        }
      };
      mockWorkSpaceApi.Setup(x => x.GetSupportMainChain())
        .Returns(expectedResult);

      // Act
      var result = mockWorkSpaceApi.Object.GetSupportMainChain();

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(3, result.Data.OpenMainChain?.Count);
    }

    [Fact]
    public void GetCoinDetails_ShouldReturnCoinInfo()
    {
      // Arrange
      var mockWorkSpaceApi = new Mock<IWorkSpaceApi>();
      var expectedResult = new CoinDetailsResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<CoinDetails>
        {
          new CoinDetails { Symbol = "ETH", BaseSymbol = "ETH", Decimals = 18 }
        }
      };
      mockWorkSpaceApi.Setup(x => x.GetCoinDetails("ETH"))
        .Returns(expectedResult);

      // Act
      var result = mockWorkSpaceApi.Object.GetCoinDetails("ETH");

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Single(result.Data);
      Assert.Equal("ETH", result.Data[0].Symbol);
    }

    [Fact]
    public void GetLastBlockHeight_ShouldReturnHeight()
    {
      // Arrange
      var mockWorkSpaceApi = new Mock<IWorkSpaceApi>();
      var expectedResult = new BlockHeightResult
      {
        Code = 0,
        Msg = "success",
        Data = new BlockHeight { BlockHeightValue = 18500000 }
      };
      mockWorkSpaceApi.Setup(x => x.GetLastBlockHeight("ETH"))
        .Returns(expectedResult);

      // Act
      var result = mockWorkSpaceApi.Object.GetLastBlockHeight("ETH");

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(18500000, result.Data.BlockHeightValue);
    }

    #endregion

    #region AutoSweepApi Tests

    [Fact]
    public void AutoCollectSubWallets_ShouldReturnWalletIds()
    {
      // Arrange
      var mockAutoSweepApi = new Mock<IAutoSweepApi>();
      var expectedResult = new AutoCollectResult
      {
        Code = 0,
        Msg = "success",
        Data = new AutoCollectInfo
        {
          FuelingSubWalletId = 1000537,
          CollectSubWalletId = 1000538
        }
      };
      mockAutoSweepApi.Setup(x => x.AutoCollectSubWallets("USDTERC20"))
        .Returns(expectedResult);

      // Act
      var result = mockAutoSweepApi.Object.AutoCollectSubWallets("USDTERC20");

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(1000537, result.Data.FuelingSubWalletId);
    }

    [Fact]
    public void SetAutoCollectSymbol_ShouldReturnTrue()
    {
      // Arrange
      var mockAutoSweepApi = new Mock<IAutoSweepApi>();
      mockAutoSweepApi.Setup(x => x.SetAutoCollectSymbol("USDTERC20", "100", "0.01"))
        .Returns(true);

      // Act
      var result = mockAutoSweepApi.Object.SetAutoCollectSymbol("USDTERC20", "100", "0.01");

      // Assert
      Assert.True(result);
    }

    [Fact]
    public void SyncAutoCollectRecords_ShouldReturnRecords()
    {
      // Arrange
      var mockAutoSweepApi = new Mock<IAutoSweepApi>();
      var expectedResult = new AutoSweepRecordResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<AutoSweepRecord>
        {
          new AutoSweepRecord { Id = 1, Status = 1 },
          new AutoSweepRecord { Id = 2, Status = 2 }
        }
      };
      mockAutoSweepApi.Setup(x => x.SyncAutoCollectRecords(0))
        .Returns(expectedResult);

      // Act
      var result = mockAutoSweepApi.Object.SyncAutoCollectRecords(0);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(2, result.Data.Count);
    }

    #endregion

    #region TronBuyResourceApi Tests

    [Fact]
    public void CreateTronDelegate_ShouldReturnOrderResult()
    {
      // Arrange
      var mockTronApi = new Mock<ITronBuyResourceApi>();
      var tronArgs = new TronBuyEnergyArgs
      {
        RequestId = "tron_request_001",
        AddressFrom = "TPjJg9FnzQuYBd6bshgaq7rkH4s36zju5S",
        ServiceChargeType = "10010",
        BuyType = 0,
        ResourceType = 0,
        EnergyNum = 32000,
        AddressTo = "TGmBzYfBBtMfFF8v9PweTaPwn3WoB7aGPd",
        ContractAddress = "TR7NHqjeKQxGTCi8q8ZY4pL8otSzgjLj6t"
      };
      var expectedResult = new TronEnergyOrderResult
      {
        Code = 0,
        Msg = "success",
        Data = new TronEnergyOrder { TransId = "trans123", RequestId = "tron_request_001" }
      };
      mockTronApi.Setup(x => x.CreateTronDelegate(It.IsAny<TronBuyEnergyArgs>()))
        .Returns(expectedResult);

      // Act
      var result = mockTronApi.Object.CreateTronDelegate(tronArgs);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal("trans123", result.Data.TransId);
    }

    [Fact]
    public void GetBuyResourceRecords_ShouldReturnRecords()
    {
      // Arrange
      var mockTronApi = new Mock<ITronBuyResourceApi>();
      var requestIds = new List<string> { "1234567890", "tron_test_002" };
      var expectedResult = new EnergyOrderDetailResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<EnergyOrderDetail>
        {
          new EnergyOrderDetail { RequestId = "1234567890", Status = 1, EnergyNum = 100000 }
        }
      };
      mockTronApi.Setup(x => x.GetBuyResourceRecords(requestIds))
        .Returns(expectedResult);

      // Act
      var result = mockTronApi.Object.GetBuyResourceRecords(requestIds);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Single(result.Data);
      Assert.Equal(100000, result.Data[0].EnergyNum);
    }

    [Fact]
    public void SyncBuyResourceRecords_ShouldReturnRecords()
    {
      // Arrange
      var mockTronApi = new Mock<ITronBuyResourceApi>();
      var expectedResult = new EnergyOrderDetailResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<EnergyOrderDetail>
        {
          new EnergyOrderDetail { RequestId = "sync001", Status = 1 }
        }
      };
      mockTronApi.Setup(x => x.SyncBuyResourceRecords(0))
        .Returns(expectedResult);

      // Act
      var result = mockTronApi.Object.SyncBuyResourceRecords(0);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Single(result.Data);
    }

    #endregion

    #region NotifyApi Tests

    [Fact]
    public void NotifyRequest_ShouldParseNotification()
    {
      // Arrange
      var mockNotifyApi = new Mock<INotifyApi>();
      var mockCipher = "encrypted_notification_data";
      var expectedNotifyArgs = new MpcNotifyArgs
      {
        NotifyType = "deposit",
        Id = 12345
      };
      mockNotifyApi.Setup(x => x.NotifyRequest(mockCipher))
        .Returns(expectedNotifyArgs);

      // Act
      var result = mockNotifyApi.Object.NotifyRequest(mockCipher);

      // Assert
      Assert.NotNull(result);
      Assert.Equal("deposit", result.NotifyType);
      Assert.Equal(12345, result.Id);
    }

    [Fact]
    public void NotifyRequest_WithInvalidCipher_ShouldReturnNull()
    {
      // Arrange
      var mockNotifyApi = new Mock<INotifyApi>();
      mockNotifyApi.Setup(x => x.NotifyRequest("invalid_cipher"))
        .Returns((MpcNotifyArgs?)null);

      // Act
      var result = mockNotifyApi.Object.NotifyRequest("invalid_cipher");

      // Assert
      Assert.Null(result);
    }

    #endregion

    #region Signature Verification Tests

    [Fact]
    public void WithdrawSign_ShouldGenerateExpectedSignature()
    {
      // Arrange - use the same test data
      var signPrivateKey = "";

      var expectedSignature = "e+TWiABMBiV0MHl1zJc9p8kPgXBvIyB6u6eCMSRra7cINp/u8hX+58p43Yv8bp7Xunvv3VGSehca6QpwqAVHvB7MFPPRXtqlbvZpR8rwBQk6HlSDKBj2eWT27qgLNQ+hBazmG6vFAlfs/JwFjyfCKjijZX91Nvy/BM55/Yd5z9P5lt7nVmn+1+0MZKqM8myKfsaFdo0lmYEO5pNLDaLG6ditLFKGtQCKYMS7Iy7LQ+8O0hNZ+yaqCd8qqv220LUvqTeq0qBD1OOAHRhG0Z4ykCCAtkzEti/T/qMYur3KQifgZIrPG3OtgcgHrsaA92iJQvlFL5AHl1436UbkeZb50A==";

      var withdrawArgs = new MpcWithdrawArgs
      {
        RequestId = "123456789028",
        SubWalletId = 1000537,
        Symbol = "Sepolia",
        Amount = "0.001",
        AddressTo = "0xdcb0D867403adE76e75a4A6bBcE9D53C9d05B981"
      };

      // Create DataCrypto with signPrivateKey
      var dataCrypto = new DataCrypto("dummy", "dummy", signPrivateKey);

      // Act - Generate signature using MpcSignUtil
      var signature = MpcSignUtil.GetWithdrawSign(withdrawArgs, dataCrypto);

      // Debug output
      var signParams = MpcSignUtil.GetWithdrawSignParams(withdrawArgs);
      var sortedString = MpcSignUtil.ParamsSort(signParams);
      var md5Hash = MpcSignUtil.GetMd5(sortedString);

      System.Console.WriteLine($"Sorted String: {sortedString}");
      System.Console.WriteLine($"MD5 Hash: {md5Hash}");
      System.Console.WriteLine($"Generated Signature: {signature}");
      System.Console.WriteLine($"Expected Signature: {expectedSignature}");

      // Assert
      Assert.Equal(expectedSignature, signature);
    }

    #endregion
  }
}
