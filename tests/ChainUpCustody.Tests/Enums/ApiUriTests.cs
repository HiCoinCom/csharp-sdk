using ChainUpCustody.Enums;
using Xunit;

namespace ChainUpCustody.Tests.Enums
{
  /// <summary>
  /// Unit tests for API URI enums
  /// </summary>
  public class ApiUriTests
  {
    [Fact]
    public void ApiUri_CreateUserMobile_ShouldHaveCorrectValues()
    {
      // Assert
      Assert.Equal("/user/createUser", ApiUri.CreateUserMobile.Value);
      Assert.Equal("POST", ApiUri.CreateUserMobile.Method);
    }

    [Fact]
    public void ApiUri_CreateUserEmail_ShouldHaveCorrectValues()
    {
      // Assert
      Assert.Equal("/user/registerEmail", ApiUri.CreateUserEmail.Value);
      Assert.Equal("POST", ApiUri.CreateUserEmail.Method);
    }

    [Fact]
    public void ApiUri_BillingWithdraw_ShouldHaveCorrectValues()
    {
      // Assert
      Assert.Equal("/billing/withdraw", ApiUri.BillingWithdraw.Value);
      Assert.Equal("POST", ApiUri.BillingWithdraw.Method);
    }

    [Fact]
    public void ApiUri_CoinList_ShouldHaveCorrectValues()
    {
      // Assert
      Assert.Equal("/user/getCoinList", ApiUri.CoinList.Value);
      Assert.Equal("POST", ApiUri.CoinList.Method);
    }

    [Fact]
    public void MpcApiUri_SupportMainChain_ShouldHaveCorrectValues()
    {
      // Assert
      Assert.Equal("/api/mpc/wallet/open_coin", MpcApiUri.SupportMainChain.Value);
      Assert.Equal("GET", MpcApiUri.SupportMainChain.Method);
    }

    [Fact]
    public void MpcApiUri_CreateWallet_ShouldHaveCorrectValues()
    {
      // Assert
      Assert.Equal("/api/mpc/sub_wallet/create", MpcApiUri.CreateWallet.Value);
      Assert.Equal("POST", MpcApiUri.CreateWallet.Method);
    }

    [Fact]
    public void MpcApiUri_BillingWithdraw_ShouldHaveCorrectValues()
    {
      // Assert
      Assert.Equal("/api/mpc/billing/withdraw", MpcApiUri.BillingWithdraw.Value);
      Assert.Equal("POST", MpcApiUri.BillingWithdraw.Method);
    }

    [Fact]
    public void MpcApiUri_CreateWeb3Transaction_ShouldHaveCorrectValues()
    {
      // Assert
      Assert.Equal("/api/mpc/web3/trans/create", MpcApiUri.CreateWeb3Transaction.Value);
      Assert.Equal("POST", MpcApiUri.CreateWeb3Transaction.Method);
    }

    [Fact]
    public void AppShowStatus_ShouldHaveCorrectValues()
    {
      // Assert - Per Rust SDK: Visible = 1, Hidden = 2
      Assert.Equal(1, (int)AppShowStatus.Show);
      Assert.Equal(2, (int)AppShowStatus.Hidden);
    }
  }
}
