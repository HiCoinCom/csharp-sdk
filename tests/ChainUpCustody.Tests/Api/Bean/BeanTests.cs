using ChainUpCustody.Api.Models;
using ChainUpCustody.Api.Models.Waas;
using ChainUpCustody.Api.Models.Mpc;
using Newtonsoft.Json;
using Xunit;

namespace ChainUpCustody.Tests.Api.Bean
{
  /// <summary>
  /// Unit tests for API Bean classes
  /// </summary>
  public class BeanTests
  {
    [Fact]
    public void BaseArgs_Create_ShouldSetValues()
    {
      // Act
      var args = BaseArgs.Create("app123", "encrypted_data");

      // Assert
      Assert.Equal("app123", args.AppId);
      Assert.Equal("encrypted_data", args.Data);
      Assert.True(args.Time > 0);
    }

    [Fact]
    public void BaseArgs_ToDictionary_ShouldContainAllFields()
    {
      // Arrange
      var args = new BaseArgs
      {
        AppId = "app123",
        Data = "test_data",
        Time = 1234567890
      };

      // Act
      var dict = args.ToDictionary();

      // Assert
      Assert.Equal("app123", dict["app_id"]);
      Assert.Equal("test_data", dict["data"]);
      Assert.Equal("1234567890", dict["time"]);
    }

    [Fact]
    public void Args_Create_ShouldSetValuesWithSign()
    {
      // Act
      var args = Args.Create("app123", "encrypted_data", "signature");

      // Assert
      Assert.Equal("app123", args.AppId);
      Assert.Equal("encrypted_data", args.Data);
      Assert.Equal("signature", args.Sign);
      Assert.True(args.Time > 0);
    }

    [Fact]
    public void Args_ToDictionary_ShouldContainSign()
    {
      // Arrange
      var args = new Args
      {
        AppId = "app123",
        Data = "test_data",
        Time = 1234567890,
        Sign = "signature"
      };

      // Act
      var dict = args.ToDictionary();

      // Assert
      Assert.Equal("app123", dict["app_id"]);
      Assert.Equal("test_data", dict["data"]);
      Assert.Equal("1234567890", dict["time"]);
      Assert.Equal("signature", dict["sign"]);
    }

    [Fact]
    public void Args_ToDictionary_WithoutSign_ShouldNotContainSign()
    {
      // Arrange
      var args = new Args
      {
        AppId = "app123",
        Data = "test_data",
        Time = 1234567890,
        Sign = null
      };

      // Act
      var dict = args.ToDictionary();

      // Assert
      Assert.False(dict.ContainsKey("sign"));
    }

    [Fact]
    public void Result_Success_ShouldCreateSuccessResult()
    {
      // Act
      var result = Result<string>.Success("test_data");

      // Assert
      Assert.Equal(0, result.Code);
      Assert.Equal("success", result.Msg);
      Assert.Equal("test_data", result.Data);
      Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Result_Error_ShouldCreateErrorResult()
    {
      // Act
      var result = Result<string>.Error(100, "Error message");

      // Assert
      Assert.Equal(100, result.Code);
      Assert.Equal("Error message", result.Msg);
      Assert.Null(result.Data);
      Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Result_Serialization_ShouldUseCorrectJsonNames()
    {
      // Arrange
      var result = new Result<string>
      {
        Code = 0,
        Msg = "success",
        Data = "test"
      };

      // Act
      var json = JsonConvert.SerializeObject(result);

      // Assert
      Assert.Contains("\"code\"", json);
      Assert.Contains("\"msg\"", json);
      Assert.Contains("\"data\"", json);
    }

    #region WaaS Bean Tests

    [Fact]
    public void RegisterArgs_Serialization_ShouldUseCorrectJsonNames()
    {
      // Arrange
      var args = new RegisterArgs
      {
        Country = "US",
        Email = "test@example.com"
      };

      // Act
      var json = args.ToJson();

      // Assert
      Assert.Contains("\"country\"", json);
      Assert.Contains("\"email\"", json);
    }

    [Fact]
    public void WithdrawArgs_Serialization_ShouldUseCorrectJsonNames()
    {
      // Arrange
      var args = new WithdrawArgs
      {
        RequestId = "req123",
        Symbol = "ETH",
        ToAddress = "0x1234567890",
        Amount = 1.5m,
        FromUid = 123
      };

      // Act
      var json = args.ToJson();

      // Assert
      Assert.Contains("\"request_id\"", json);
      Assert.Contains("\"symbol\"", json);
      Assert.Contains("\"to_address\"", json);
      Assert.Contains("\"amount\"", json);
      Assert.Contains("\"from_uid\"", json);
    }

    [Fact]
    public void UserInfo_Deserialization_ShouldWork()
    {
      // Arrange
      var json = @"{""uid"":123,""email"":""test@example.com""}";

      // Act
      var userInfo = JsonConvert.DeserializeObject<UserInfo>(json);

      // Assert
      Assert.NotNull(userInfo);
      Assert.Equal(123, userInfo.Uid);
      Assert.Equal("test@example.com", userInfo.Email);
    }

    [Fact]
    public void UserInfoResult_Deserialization_ShouldWork()
    {
      // Arrange
      var json = @"{""code"":0,""msg"":""success"",""data"":{""uid"":123}}";

      // Act
      var result = JsonConvert.DeserializeObject<UserInfoResult>(json);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(123, result.Data.Uid);
    }

    [Fact]
    public void TransferArgs_Serialization_ShouldUseCorrectJsonNames()
    {
      // Arrange
      var args = new TransferArgs
      {
        RequestId = "req123",
        Symbol = "ETH",
        To = "user2",
        Remark = "test remark",
        Amount = 10.5m
      };

      // Act
      var json = args.ToJson();

      // Assert
      Assert.Contains("\"request_id\"", json);
      Assert.Contains("\"symbol\"", json);
      Assert.Contains("\"to\"", json);
      Assert.Contains("\"remark\"", json);
      Assert.Contains("\"amount\"", json);
    }

    #endregion

    #region MPC Bean Tests

    [Fact]
    public void CreateWalletArgs_Serialization_ShouldUseCorrectJsonNames()
    {
      // Arrange
      var args = new CreateWalletArgs
      {
        SubWalletName = "TestWallet",
        ShowStatus = 0
      };

      // Act
      var json = args.ToJson();

      // Assert
      Assert.Contains("\"sub_wallet_name\"", json);
      Assert.Contains("\"app_show_status\"", json);
    }

    [Fact]
    public void MpcWithdrawArgs_Serialization_ShouldUseCorrectJsonNames()
    {
      // Arrange
      var args = new MpcWithdrawArgs
      {
        RequestId = "req123",
        SubWalletId = 123,
        Symbol = "ETH",
        AddressTo = "0x1234567890",
        Amount = "1.5"
      };

      // Act
      var json = args.ToJson();

      // Assert
      Assert.Contains("\"request_id\"", json);
      Assert.Contains("\"sub_wallet_id\"", json);
      Assert.Contains("\"symbol\"", json);
      Assert.Contains("\"address_to\"", json);
      Assert.Contains("\"amount\"", json);
    }

    [Fact]
    public void WalletResult_Deserialization_ShouldWork()
    {
      // Arrange
      var json = @"{""code"":0,""msg"":""success"",""data"":{""sub_wallet_id"":123}}";

      // Act
      var result = JsonConvert.DeserializeObject<WalletResult>(json);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(0, result.Code);
      Assert.NotNull(result.Data);
      Assert.Equal(123, result.Data.SubWalletId);
    }

    [Fact]
    public void CreateWeb3Args_Serialization_ShouldUseCorrectJsonNames()
    {
      // Arrange
      var args = new CreateWeb3Args
      {
        RequestId = "req123",
        SubWalletId = 123,
        From = "0xfrom",
        InteractiveContract = "0xcontract",
        MainChainSymbol = "ETH",
        InputData = "0x1234"
      };

      // Act
      var json = args.ToJson();

      // Assert
      Assert.Contains("\"request_id\"", json);
      Assert.Contains("\"sub_wallet_id\"", json);
      Assert.Contains("\"from\"", json);
      Assert.Contains("\"interactive_contract\"", json);
      Assert.Contains("\"main_chain_symbol\"", json);
      Assert.Contains("\"input_data\"", json);
    }

    [Fact]
    public void TronBuyEnergyArgs_Serialization_ShouldUseCorrectJsonNames()
    {
      // Arrange
      // Reference: https://github.com/HiCoinCom/rust-sdk/blob/main/src/mpc/api/tron_resource_api.rs
      var args = new TronBuyEnergyArgs
      {
        RequestId = "req123",
        AddressFrom = "TPjJg9FnzQuYBd6bshgaq7rkH4s36zju5S",
        ServiceChargeType = "10010",
        BuyType = 0,
        ResourceType = 0,
        EnergyNum = 32000,
        NetNum = 0,
        AddressTo = "TGmBzYfBBtMfFF8v9PweTaPwn3WoB7aGPd",
        ContractAddress = "TR7NHqjeKQxGTCi8q8ZY4pL8otSzgjLj6t"
      };

      // Act
      var json = args.ToJson();

      // Assert
      Assert.Contains("\"request_id\"", json);
      Assert.Contains("\"address_from\"", json);
      Assert.Contains("\"service_charge_type\"", json);
      Assert.Contains("\"buy_type\"", json);
      Assert.Contains("\"resource_type\"", json);
      Assert.Contains("\"energy_num\"", json);
      Assert.Contains("\"address_to\"", json);
      Assert.Contains("\"contract_address\"", json);
    }

    #endregion
  }
}
