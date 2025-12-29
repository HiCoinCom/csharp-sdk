using System;
using System.Collections.Generic;
using ChainUpCustody.Api.Models.Waas;
using ChainUpCustody.Api.Waas;
using Moq;
using Xunit;

namespace ChainUpCustody.Tests.Api.Waas
{
  /// <summary>
  /// Unit tests for WaaS API functions
  /// Reference: https://github.com/HiCoinCom/go-sdk/blob/main/examples/waas_example.go
  /// </summary>
  public class WaasApiTests
  {
    #region UserApi Tests

    [Fact]
    public void RegisterEmailUser_ShouldReturnUserInfo()
    {
      // Arrange
      var mockUserApi = new Mock<IUserApi>();
      var expectedResult = new UserInfoResult
      {
        Code = 0,
        Msg = "success",
        Data = new UserInfo
        {
          Uid = 12345,
          Email = "test@example.com"
        }
      };
      mockUserApi.Setup(x => x.RegisterEmailUser("test@example.com"))
        .Returns(expectedResult);

      // Act
      var result = mockUserApi.Object.RegisterEmailUser("test@example.com");

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(12345, result.Data.Uid);
      Assert.Equal("test@example.com", result.Data.Email);
    }

    [Fact]
    public void GetEmailUser_ShouldReturnUserInfo()
    {
      // Arrange
      var mockUserApi = new Mock<IUserApi>();
      var expectedResult = new UserInfoResult
      {
        Code = 0,
        Msg = "success",
        Data = new UserInfo
        {
          Uid = 12345,
          Email = "user@example.com",
          Nickname = "TestUser"
        }
      };
      mockUserApi.Setup(x => x.GetEmailUser("user@example.com"))
        .Returns(expectedResult);

      // Act
      var result = mockUserApi.Object.GetEmailUser("user@example.com");

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(12345, result.Data.Uid);
    }

    [Fact]
    public void SyncUserList_ShouldReturnUserList()
    {
      // Arrange
      var mockUserApi = new Mock<IUserApi>();
      var expectedResult = new UserInfoListResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<UserInfo>
        {
          new UserInfo { Uid = 1, Email = "user1@example.com" },
          new UserInfo { Uid = 2, Email = "user2@example.com" }
        }
      };
      mockUserApi.Setup(x => x.SyncUserList(0))
        .Returns(expectedResult);

      // Act
      var result = mockUserApi.Object.SyncUserList(0);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(2, result.Data.Count);
    }

    #endregion

    #region AccountApi Tests

    [Fact]
    public void GetUserAccount_ShouldReturnAccountInfo()
    {
      // Arrange
      var mockAccountApi = new Mock<IAccountApi>();
      var expectedResult = new UserAccountResult
      {
        Code = 0,
        Msg = "success",
        Data = new UserAccount
        {
          NormalBalance = 100.5m,
          LockBalance = 10.0m
        }
      };
      mockAccountApi.Setup(x => x.GetUserAccount(1001, "ETH"))
        .Returns(expectedResult);

      // Act
      var result = mockAccountApi.Object.GetUserAccount(1001, "ETH");

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(100.5m, result.Data.NormalBalance);
      Assert.Equal(10.0m, result.Data.LockBalance);
    }

    [Fact]
    public void GetUserAddress_ShouldReturnAddress()
    {
      // Arrange
      var mockAccountApi = new Mock<IAccountApi>();
      var expectedResult = new UserAddressResult
      {
        Code = 0,
        Msg = "success",
        Data = new UserAddress
        {
          Id = 1,
          Uid = 1001,
          Address = "0x1234567890abcdef1234567890abcdef12345678"
        }
      };
      mockAccountApi.Setup(x => x.GetUserAddress(1001, "ETH"))
        .Returns(expectedResult);

      // Act
      var result = mockAccountApi.Object.GetUserAddress(1001, "ETH");

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.NotNull(result.Data.Address);
    }

    [Fact]
    public void SyncUserAddressList_ShouldReturnAddresses()
    {
      // Arrange
      var mockAccountApi = new Mock<IAccountApi>();
      var expectedResult = new UserAddressListResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<UserAddress>
        {
          new UserAddress { Id = 1, Uid = 1001, Address = "0xabc123" },
          new UserAddress { Id = 2, Uid = 1002, Address = "0xdef456" }
        }
      };
      mockAccountApi.Setup(x => x.SyncUserAddressList(0))
        .Returns(expectedResult);

      // Act
      var result = mockAccountApi.Object.SyncUserAddressList(0);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(2, result.Data.Count);
    }

    [Fact]
    public void GetCompanyAccount_ShouldReturnBalance()
    {
      // Arrange
      var mockAccountApi = new Mock<IAccountApi>();
      var expectedResult = new AccountResult
      {
        Code = 0,
        Msg = "success",
        Data = new Account
        {
          Symbol = "ETH",
          Balance = 1000.5m
        }
      };
      mockAccountApi.Setup(x => x.GetCompanyAccount("ETH"))
        .Returns(expectedResult);

      // Act
      var result = mockAccountApi.Object.GetCompanyAccount("ETH");

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal("ETH", result.Data.Symbol);
      Assert.Equal(1000.5m, result.Data.Balance);
    }

    #endregion

    #region BillingApi Tests

    [Fact]
    public void Withdraw_ShouldReturnWithdrawResult()
    {
      // Arrange
      var mockBillingApi = new Mock<IBillingApi>();
      var withdrawArgs = new WithdrawArgs
      {
        RequestId = "withdraw_001",
        Symbol = "ETH",
        Amount = 1.5m,
        ToAddress = "0xReceiverAddress"
      };
      var expectedResult = new WithdrawResult
      {
        Code = 0,
        Msg = "success",
        Data = new Withdraw { Id = 12345 }
      };
      mockBillingApi.Setup(x => x.Withdraw(It.IsAny<WithdrawArgs>()))
        .Returns(expectedResult);

      // Act
      var result = mockBillingApi.Object.Withdraw(withdrawArgs);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(12345, result.Data.Id);
    }

    [Fact]
    public void WithdrawList_ShouldReturnRecords()
    {
      // Arrange
      var mockBillingApi = new Mock<IBillingApi>();
      var requestIds = new List<string> { "req1", "req2", "req3" };
      var expectedResult = new WithdrawListResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<Withdraw>
        {
          new Withdraw { Id = 1, Symbol = "ETH", Amount = 1.5m, Status = 1 },
          new Withdraw { Id = 2, Symbol = "BTC", Amount = 0.5m, Status = 2 }
        }
      };
      mockBillingApi.Setup(x => x.WithdrawList(requestIds))
        .Returns(expectedResult);

      // Act
      var result = mockBillingApi.Object.WithdrawList(requestIds);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(2, result.Data.Count);
    }

    [Fact]
    public void SyncWithdrawList_ShouldReturnRecords()
    {
      // Arrange
      var mockBillingApi = new Mock<IBillingApi>();
      var expectedResult = new WithdrawListResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<Withdraw>
        {
          new Withdraw { Id = 1, Symbol = "ETH" },
          new Withdraw { Id = 2, Symbol = "BTC" }
        }
      };
      mockBillingApi.Setup(x => x.SyncWithdrawList(0))
        .Returns(expectedResult);

      // Act
      var result = mockBillingApi.Object.SyncWithdrawList(0);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(2, result.Data.Count);
    }

    [Fact]
    public void DepositList_ShouldReturnRecords()
    {
      // Arrange
      var mockBillingApi = new Mock<IBillingApi>();
      var ids = new List<int> { 100, 101 };
      var expectedResult = new DepositListResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<Deposit>
        {
          new Deposit { Id = 100, Symbol = "ETH", Amount = 2.0m, Status = 1 }
        }
      };
      mockBillingApi.Setup(x => x.DepositList(ids))
        .Returns(expectedResult);

      // Act
      var result = mockBillingApi.Object.DepositList(ids);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Single(result.Data);
    }

    [Fact]
    public void SyncDepositList_ShouldReturnRecords()
    {
      // Arrange
      var mockBillingApi = new Mock<IBillingApi>();
      var expectedResult = new DepositListResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<Deposit>
        {
          new Deposit { Id = 100, Symbol = "ETH" },
          new Deposit { Id = 101, Symbol = "BTC" }
        }
      };
      mockBillingApi.Setup(x => x.SyncDepositList(0))
        .Returns(expectedResult);

      // Act
      var result = mockBillingApi.Object.SyncDepositList(0);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(2, result.Data.Count);
    }

    [Fact]
    public void MinerFeeList_ShouldReturnRecords()
    {
      // Arrange
      var mockBillingApi = new Mock<IBillingApi>();
      var ids = new List<int> { 200, 201 };
      var expectedResult = new MinerFeeListResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<MinerFee>
        {
          new MinerFee { Id = 200, Symbol = "ETH" }
        }
      };
      mockBillingApi.Setup(x => x.MinerFeeList(ids))
        .Returns(expectedResult);

      // Act
      var result = mockBillingApi.Object.MinerFeeList(ids);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Single(result.Data);
    }

    [Fact]
    public void SyncMinerFeeList_ShouldReturnRecords()
    {
      // Arrange
      var mockBillingApi = new Mock<IBillingApi>();
      var expectedResult = new MinerFeeListResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<MinerFee>
        {
          new MinerFee { Id = 200, Symbol = "ETH" },
          new MinerFee { Id = 201, Symbol = "BTC" }
        }
      };
      mockBillingApi.Setup(x => x.SyncMinerFeeList(0))
        .Returns(expectedResult);

      // Act
      var result = mockBillingApi.Object.SyncMinerFeeList(0);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(2, result.Data.Count);
    }

    #endregion

    #region TransferApi Tests

    [Fact]
    public void AccountTransfer_ShouldReturnTransferId()
    {
      // Arrange
      var mockTransferApi = new Mock<ITransferApi>();
      var transferArgs = new TransferArgs
      {
        RequestId = "transfer_001",
        Symbol = "ETH",
        Amount = 1.0m,
        To = "recipient_user_id"
      };
      var expectedResult = new TransferResult
      {
        Code = 0,
        Msg = "success",
        Data = new Transfer { Id = 98765 }
      };
      mockTransferApi.Setup(x => x.AccountTransfer(It.IsAny<TransferArgs>()))
        .Returns(expectedResult);

      // Act
      var result = mockTransferApi.Object.AccountTransfer(transferArgs);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(98765, result.Data.Id);
    }

    [Fact]
    public void GetAccountTransferList_ShouldReturnRecords()
    {
      // Arrange
      var mockTransferApi = new Mock<ITransferApi>();
      var expectedResult = new TransferListResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<Transfer>
        {
          new Transfer { Id = 200, Symbol = "ETH", Amount = 1.0m },
          new Transfer { Id = 201, Symbol = "BTC", Amount = 0.5m }
        }
      };
      mockTransferApi.Setup(x => x.GetAccountTransferList("200,201", ITransferApi.REQUEST_ID))
        .Returns(expectedResult);

      // Act
      var result = mockTransferApi.Object.GetAccountTransferList("200,201", ITransferApi.REQUEST_ID);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(2, result.Data.Count);
    }

    [Fact]
    public void SyncAccountTransferList_ShouldReturnRecords()
    {
      // Arrange
      var mockTransferApi = new Mock<ITransferApi>();
      var expectedResult = new TransferListResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<Transfer>
        {
          new Transfer { Id = 200, Symbol = "ETH" },
          new Transfer { Id = 201, Symbol = "BTC" }
        }
      };
      mockTransferApi.Setup(x => x.SyncAccountTransferList(0))
        .Returns(expectedResult);

      // Act
      var result = mockTransferApi.Object.SyncAccountTransferList(0);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(2, result.Data.Count);
    }

    #endregion

    #region CoinApi Tests

    [Fact]
    public void GetCoinList_ShouldReturnCoins()
    {
      // Arrange
      var mockCoinApi = new Mock<ICoinApi>();
      var expectedResult = new CoinInfoListResult
      {
        Code = 0,
        Msg = "success",
        Data = new List<CoinInfo>
        {
          new CoinInfo { Symbol = "ETH", BaseSymbol = "ETH" },
          new CoinInfo { Symbol = "BTC", BaseSymbol = "BTC" },
          new CoinInfo { Symbol = "USDT", BaseSymbol = "TRX" }
        }
      };
      mockCoinApi.Setup(x => x.GetCoinList())
        .Returns(expectedResult);

      // Act
      var result = mockCoinApi.Object.GetCoinList();

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(3, result.Data.Count);
    }

    #endregion

    #region AsyncNotifyApi Tests

    [Fact]
    public void NotifyRequest_ShouldParseNotification()
    {
      // Arrange
      var mockAsyncNotifyApi = new Mock<IAsyncNotifyApi>();
      var mockCipher = "encrypted_notification_data";
      var expectedNotifyArgs = new AsyncNotifyArgs
      {
        Side = "1",
        Symbol = "ETH",
        Amount = 1.5m,
        Status = 1
      };
      mockAsyncNotifyApi.Setup(x => x.NotifyRequest(mockCipher))
        .Returns(expectedNotifyArgs);

      // Act
      var result = mockAsyncNotifyApi.Object.NotifyRequest(mockCipher);

      // Assert
      Assert.NotNull(result);
      Assert.Equal("1", result.Side);
      Assert.Equal("ETH", result.Symbol);
      Assert.Equal(1.5m, result.Amount);
      Assert.Equal(1, result.Status);
    }

    [Fact]
    public void NotifyRequest_WithInvalidCipher_ShouldReturnNull()
    {
      // Arrange
      var mockAsyncNotifyApi = new Mock<IAsyncNotifyApi>();
      mockAsyncNotifyApi.Setup(x => x.NotifyRequest("invalid_cipher"))
        .Returns((AsyncNotifyArgs?)null);

      // Act
      var result = mockAsyncNotifyApi.Object.NotifyRequest("invalid_cipher");

      // Assert
      Assert.Null(result);
    }

    #endregion
  }
}
