using System;
using System.Collections.Generic;
using ChainUpCustody.Api.Models.Waas;
using ChainUpCustody.Api.Waas;
using ChainUpCustody.Client;
using ChainUpCustody.Config;
using Xunit;
using Xunit.Abstractions;

namespace ChainUpCustody.Tests.Api.Waas
{
  /// <summary>
  /// Integration tests for WaaS API functions
  /// Reference: https://github.com/HiCoinCom/rust-sdk/blob/main/examples/waas_example.rs
  /// 
  /// These tests make real API calls. Configure the following environment variables:
  /// - WAAS_APP_ID: Your application ID
  /// - WAAS_USER_PRIVATE_KEY: Your RSA private key
  /// - WAAS_PUBLIC_KEY: ChainUp WaaS RSA public key
  /// </summary>
  public class WaasApiIntegrationTests
  {
    private readonly ITestOutputHelper _output;
    private readonly WaasClient? _client;
    private readonly bool _isConfigured;

    // Test configuration - replace with your actual values or use environment variables
    private const int TestUid = 15036904;
    private const string TestSymbol = "ETH";
    private const string TestEmail = "test@example.com";
    private const string TestAddress = "0xd4036730fd450237b8fea382bd887c4c96a8453a";

    public WaasApiIntegrationTests(ITestOutputHelper output)
    {
      _output = output;

      var appId = Environment.GetEnvironmentVariable("WAAS_APP_ID") ?? "";
      var userPrivateKey = Environment.GetEnvironmentVariable("WAAS_USER_PRIVATE_KEY") ?? "";
      var waasPublicKey = Environment.GetEnvironmentVariable("WAAS_PUBLIC_KEY") ?? "";

      _isConfigured = !string.IsNullOrEmpty(appId) &&
                      !string.IsNullOrEmpty(userPrivateKey) &&
                      !string.IsNullOrEmpty(waasPublicKey);

      if (_isConfigured)
      {
        _client = WaasClient.Builder()
            .SetAppId(appId)
            .SetUserPrivateKey(userPrivateKey)
            .SetWaasPublicKey(waasPublicKey)
            .EnableLog(true)
            .Build();
        _output.WriteLine("✓ WaaS 客户端创建成功");
      }
      else
      {
        _output.WriteLine("⚠ WaaS 客户端未配置，请设置环境变量");
      }
    }

    private void SkipIfNotConfigured()
    {
      Skip.If(!_isConfigured, "WaaS API 未配置，跳过集成测试");
    }

    #region User API Tests

    [SkippableFact]
    public void RegisterEmailUser_ShouldRegisterUser()
    {
      SkipIfNotConfigured();

      _output.WriteLine("\n========== 用户管理 (User API) ==========");

      // 1. 注册邮箱用户
      var email = $"test_{DateTime.Now:yyyyMMddHHmmss}@example.com";
      var result = _client!.UserApi.RegisterEmailUser(email);

      _output.WriteLine($"✓ 注册邮箱用户 ({email}): Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  UID: {result.Data.Uid}");
        _output.WriteLine($"  邮箱: {result.Data.Email}");
      }
    }

    [SkippableFact]
    public void GetEmailUser_ShouldReturnUserInfo()
    {
      SkipIfNotConfigured();

      // 2. 获取邮箱用户信息
      var result = _client!.UserApi.GetEmailUser(TestEmail);

      _output.WriteLine($"✓ 获取邮箱用户 ({TestEmail}): Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  UID: {result.Data.Uid}");
        _output.WriteLine($"  昵称: {result.Data.Nickname}");
      }
    }

    [SkippableFact]
    public void SyncUserList_ShouldReturnUsers()
    {
      SkipIfNotConfigured();

      // 3. 同步用户列表
      var result = _client!.UserApi.SyncUserList(0);

      _output.WriteLine($"✓ 同步用户列表: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  用户数量: {result.Data.Count}");
        if (result.Data.Count > 0)
        {
          _output.WriteLine($"  首条UID: {result.Data[0].Uid}");
        }
      }
    }

    #endregion

    #region Account API Tests

    [SkippableFact]
    public void GetUserAccount_ShouldReturnAccount()
    {
      SkipIfNotConfigured();

      _output.WriteLine("\n========== 账户管理 (Account API) ==========");

      // 1. 获取用户账户
      var result = _client!.AccountApi.GetUserAccount(TestUid, TestSymbol);

      _output.WriteLine($"✓ 获取用户账户 (UID={TestUid}, Symbol={TestSymbol}): Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  普通余额: {result.Data.NormalBalance}");
        _output.WriteLine($"  锁定余额: {result.Data.LockBalance}");
      }
    }

    [SkippableFact]
    public void GetUserAddress_ShouldReturnAddress()
    {
      SkipIfNotConfigured();

      // 2. 获取用户充值地址
      var result = _client!.AccountApi.GetUserAddress(TestUid, TestSymbol);

      _output.WriteLine($"✓ 获取用户充值地址: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  地址: {result.Data.Address}");
      }
    }

    [SkippableFact]
    public void GetCompanyAccount_ShouldReturnCompanyBalance()
    {
      SkipIfNotConfigured();

      // 3. 获取公司账户余额
      var result = _client!.AccountApi.GetCompanyAccount(TestSymbol);

      _output.WriteLine($"✓ 获取公司账户余额 ({TestSymbol}): Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  币种: {result.Data.Symbol}");
        _output.WriteLine($"  余额: {result.Data.Balance}");
      }
    }

    [SkippableFact]
    public void SyncUserAddressList_ShouldReturnAddresses()
    {
      SkipIfNotConfigured();

      // 4. 同步用户地址列表
      var result = _client!.AccountApi.SyncUserAddressList(0);

      _output.WriteLine($"✓ 同步用户地址列表: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  地址数量: {result.Data.Count}");
        if (result.Data.Count > 0)
        {
          _output.WriteLine($"  首条地址: {result.Data[0].Address}");
        }
      }
    }

    #endregion

    #region Coin API Tests

    [SkippableFact]
    public void GetCoinList_ShouldReturnCoins()
    {
      SkipIfNotConfigured();

      _output.WriteLine("\n========== 币种管理 (Coin API) ==========");

      // 获取支持的币种列表
      var result = _client!.CoinApi.GetCoinList();

      _output.WriteLine($"✓ 获取币种列表: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  支持币种数量: {result.Data.Count}");
        var count = 0;
        foreach (var coin in result.Data)
        {
          if (count++ >= 5) break;
          _output.WriteLine($"  - {coin.Symbol}: {coin.RealSymbol}");
        }
        if (result.Data.Count > 5)
        {
          _output.WriteLine($"  ... 还有 {result.Data.Count - 5} 个币种");
        }
      }
    }

    #endregion

    #region Billing API Tests

    [SkippableFact]
    public void Withdraw_ShouldSubmitWithdrawal()
    {
      SkipIfNotConfigured();

      _output.WriteLine("\n========== 账单管理 (Billing API) ==========");

      // 1. 发起提现
      var withdrawArgs = new WithdrawArgs
      {
        RequestId = $"withdraw_{DateTime.Now:yyyyMMddHHmmss}_{new Random().Next(1000, 9999)}",
        FromUid = TestUid,
        ToAddress = "0x0f1dc222af5ea2660ff84ae91adc48f1cb2d4991f1e6569dd24d94599c335a06",
        Amount = 0.001m,
        Symbol = "APTOS"
      };

      var result = _client!.BillingApi.Withdraw(withdrawArgs);

      _output.WriteLine($"✓ 发起提现: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  提现ID: {result.Data.Id}");
        _output.WriteLine($"  Request ID: {result.Data.RequestId}");
      }
    }

    [SkippableFact]
    public void WithdrawList_ShouldReturnWithdrawRecords()
    {
      SkipIfNotConfigured();

      // 2. 查询提现记录 (按 request_id)
      var requestIds = new List<string> { "test_request_1", "test_request_2" };
      var result = _client!.BillingApi.WithdrawList(requestIds);

      _output.WriteLine($"✓ 查询提现记录: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  提现记录数量: {result.Data.Count}");
      }
    }

    [SkippableFact]
    public void SyncWithdrawList_ShouldReturnWithdrawRecords()
    {
      SkipIfNotConfigured();

      // 3. 同步提现记录
      var result = _client!.BillingApi.SyncWithdrawList(0);

      _output.WriteLine($"✓ 同步提现记录: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  提现记录数量: {result.Data.Count}");
        if (result.Data.Count > 0)
        {
          _output.WriteLine($"  首条RequestId: {result.Data[0].RequestId}");
        }
      }
    }

    [SkippableFact]
    public void DepositList_ShouldReturnDepositRecords()
    {
      SkipIfNotConfigured();

      // 4. 查询充值记录 (按 WaaS ID)
      var waasIds = new List<int> { 123, 456 };
      var result = _client!.BillingApi.DepositList(waasIds);

      _output.WriteLine($"✓ 查询充值记录: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  充值记录数量: {result.Data.Count}");
      }
    }

    [SkippableFact]
    public void SyncDepositList_ShouldReturnDepositRecords()
    {
      SkipIfNotConfigured();

      // 5. 同步充值记录
      var result = _client!.BillingApi.SyncDepositList(0);

      _output.WriteLine($"✓ 同步充值记录: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  充值记录数量: {result.Data.Count}");
        if (result.Data.Count > 0)
        {
          _output.WriteLine($"  首条ID: {result.Data[0].Id}");
        }
      }
    }

    [SkippableFact]
    public void MinerFeeList_ShouldReturnMinerFeeRecords()
    {
      SkipIfNotConfigured();

      // 6. 查询矿工费记录 (按 WaaS ID)
      var waasIds = new List<int> { 123, 456 };
      var result = _client!.BillingApi.MinerFeeList(waasIds);

      _output.WriteLine($"✓ 查询矿工费记录: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  矿工费记录数量: {result.Data.Count}");
      }
    }

    [SkippableFact]
    public void SyncMinerFeeList_ShouldReturnMinerFeeRecords()
    {
      SkipIfNotConfigured();

      // 7. 同步矿工费记录
      var result = _client!.BillingApi.SyncMinerFeeList(0);

      _output.WriteLine($"✓ 同步矿工费记录: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  矿工费记录数量: {result.Data.Count}");
        if (result.Data.Count > 0)
        {
          _output.WriteLine($"  首条ID: {result.Data[0].Id}");
        }
      }
    }

    #endregion

    #region Transfer API Tests

    [SkippableFact]
    public void AccountTransfer_ShouldTransferFunds()
    {
      SkipIfNotConfigured();

      _output.WriteLine("\n========== 转账管理 (Transfer API) ==========");

      // 1. 内部转账
      var transferArgs = new TransferArgs
      {
        RequestId = $"transfer_{DateTime.Now:yyyyMMddHHmmss}_{new Random().Next(1000, 9999)}",
        Symbol = "USDT",
        Amount = 100.5m,
        To = "67890",
        Remark = "Test transfer from C# SDK"
      };

      var result = _client!.TransferApi.AccountTransfer(transferArgs);

      _output.WriteLine($"✓ 内部转账: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  转账ID: {result.Data.Id}");
        _output.WriteLine($"  Request ID: {result.Data.RequestId}");
      }
    }

    [SkippableFact]
    public void GetAccountTransferList_ByRequestId_ShouldReturnRecords()
    {
      SkipIfNotConfigured();

      // 2. 按 request_id 查询转账记录
      var result = _client!.TransferApi.GetAccountTransferList("123,456", ITransferApi.REQUEST_ID);

      _output.WriteLine($"✓ 按 request_id 查询转账记录: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  转账记录数量: {result.Data.Count}");
      }
    }

    [SkippableFact]
    public void GetAccountTransferList_ByReceipt_ShouldReturnRecords()
    {
      SkipIfNotConfigured();

      // 3. 按 receipt 查询转账记录
      var result = _client!.TransferApi.GetAccountTransferList("123,456", ITransferApi.RECEIPT);

      _output.WriteLine($"✓ 按 receipt 查询转账记录: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  转账记录数量: {result.Data.Count}");
      }
    }

    [SkippableFact]
    public void SyncAccountTransferList_ShouldReturnRecords()
    {
      SkipIfNotConfigured();

      // 4. 同步转账记录
      var result = _client!.TransferApi.SyncAccountTransferList(0);

      _output.WriteLine($"✓ 同步转账记录: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  转账记录数量: {result.Data.Count}");
        if (result.Data.Count > 0)
        {
          _output.WriteLine($"  首条RequestId: {result.Data[0].RequestId}");
        }
      }
    }

    #endregion

    #region Async Notify API Tests

    [SkippableFact]
    public void NotifyRequest_ShouldDecryptNotification()
    {
      SkipIfNotConfigured();

      _output.WriteLine("\n========== 异步通知 (Async Notify API) ==========");

      // 解密通知数据 (使用实际的加密通知数据)
      var encryptedNotify = "jhoA9MtGotqWxqEtB27SwCtJCo9JSIxh2B6m8CItrPQj2gsm6rw-..."; // 替换为实际加密数据

      var result = _client!.AsyncNotifyApi.NotifyRequest(encryptedNotify);

      if (result != null)
      {
        _output.WriteLine("✓ 解密通知成功");
        _output.WriteLine($"  ID: {result.Id}");
        _output.WriteLine($"  Side: {result.Side}");
      }
      else
      {
        _output.WriteLine("✗ 解密通知失败或数据无效");
      }
    }

    [SkippableFact]
    public void VerifyRequest_ShouldDecryptWithdrawRequest()
    {
      SkipIfNotConfigured();

      // 解密提现二次验证请求
      var encryptedRequest = "encrypted_withdraw_request_data"; // 替换为实际加密数据

      var result = _client!.AsyncNotifyApi.VerifyRequest(encryptedRequest);

      if (result != null)
      {
        _output.WriteLine("✓ 解密提现验证请求成功");
        _output.WriteLine($"  Request ID: {result.RequestId}");
        _output.WriteLine($"  From UID: {result.FromUid}");
        _output.WriteLine($"  To Address: {result.ToAddress}");
        _output.WriteLine($"  Amount: {result.Amount}");
        _output.WriteLine($"  Symbol: {result.Symbol}");
      }
      else
      {
        _output.WriteLine("✗ 解密提现验证请求失败或数据无效");
      }
    }

    [SkippableFact]
    public void VerifyResponse_ShouldEncryptWithdrawParams()
    {
      SkipIfNotConfigured();

      // 加密提现验证响应
      var withdrawArgs = new WithdrawArgs
      {
        RequestId = "verify_test_001",
        FromUid = TestUid,
        ToAddress = "0x1234567890abcdef1234567890abcdef12345678",
        Amount = 0.01m,
        Symbol = "ETH",
        CheckSum = "abc123"
      };

      var result = _client!.AsyncNotifyApi.VerifyResponse(withdrawArgs);

      if (!string.IsNullOrEmpty(result))
      {
        _output.WriteLine("✓ 加密提现验证响应成功");
        _output.WriteLine($"  加密数据: {result.Substring(0, Math.Min(50, result.Length))}...");
      }
      else
      {
        _output.WriteLine("✗ 加密提现验证响应失败");
      }

      Assert.False(string.IsNullOrEmpty(result));
    }

    #endregion
  }
}
