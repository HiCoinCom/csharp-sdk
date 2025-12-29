using System;
using System.Collections.Generic;
using ChainUpCustody.Api.Models.Waas;
using ChainUpCustody.Api.Waas;
using ChainUpCustody.Client;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace ChainUpCustody.Tests.Api.Waas
{
  /// <summary>
  /// WaaS API 调试测试 - 用于打印解密后的数据并核对返回值类型是否缺失字段
  /// 
  /// 这些测试会实际调用 API，请配置以下环境变量:
  /// - WAAS_APP_ID: 应用 ID
  /// - WAAS_USER_PRIVATE_KEY: 用户 RSA 私钥
  /// - WAAS_PUBLIC_KEY: ChainUp WaaS RSA 公钥
  /// </summary>
  public class WaasApiDebugTests
  {
    private readonly ITestOutputHelper _output;
    private readonly WaasClient? _client;
    private readonly bool _isConfigured;

    // 测试配置 - 可替换为实际值
    private const string TestEmail = "test@example.com";
    private const string TestSymbol = "ETH";
    private const int TestUid = 1;

    public WaasApiDebugTests(ITestOutputHelper output)
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
      }
    }

    private void SkipIfNotConfigured()
    {
      if (!_isConfigured)
      {
        throw new SkipException("WaaS API 未配置，跳过调试测试。请设置环境变量: WAAS_APP_ID, WAAS_USER_PRIVATE_KEY, WAAS_PUBLIC_KEY");
      }
    }

    private void PrintSeparator(string apiName, string apiPath)
    {
      _output.WriteLine("");
      _output.WriteLine(new string('=', 60));
      _output.WriteLine($"{apiName} - {apiPath}");
      _output.WriteLine(new string('=', 60));
    }

    private void PrintResult<T>(T result, string description = "结果")
    {
      var json = JsonConvert.SerializeObject(result, Formatting.Indented);
      _output.WriteLine($"{description}:");
      _output.WriteLine(json);
    }

    private void PrintListResult<T>(IList<T>? list, string listName)
    {
      if (list != null)
      {
        _output.WriteLine($"{listName} 长度: {list.Count}");
        if (list.Count > 0)
        {
          _output.WriteLine($"{listName} 首条数据:");
          var json = JsonConvert.SerializeObject(list[0], Formatting.Indented);
          _output.WriteLine(json);
        }
      }
      else
      {
        _output.WriteLine($"{listName} 为 null");
      }
    }

    private void PrintError(Exception ex)
    {
      _output.WriteLine($"错误: {ex.Message}");
      if (ex.InnerException != null)
      {
        _output.WriteLine($"内部错误: {ex.InnerException.Message}");
      }
    }

    #region UserApi 调试测试

    /// <summary>
    /// 调试测试: 注册邮箱用户
    /// </summary>
    [Fact]
    public void Debug_RegisterEmailUser()
    {
      SkipIfNotConfigured();
      PrintSeparator("注册邮箱用户", "/api/v2/user/createUserEmail");

      try
      {
        var email = $"test_{DateTime.Now:yyyyMMddHHmmss}@example.com";
        var result = _client!.UserApi.RegisterEmailUser(email);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");

        if (result.Data != null)
        {
          _output.WriteLine("");
          _output.WriteLine("UserInfo 字段:");
          _output.WriteLine($"  Uid: {result.Data.Uid}");
          _output.WriteLine($"  Email: {result.Data.Email}");
          _output.WriteLine($"  Nickname: {result.Data.Nickname}");
          _output.WriteLine($"  Mobile: {result.Data.Mobile}");
          _output.WriteLine($"  Country: {result.Data.Country}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 获取邮箱用户信息
    /// </summary>
    [Fact]
    public void Debug_GetEmailUser()
    {
      SkipIfNotConfigured();
      PrintSeparator("获取邮箱用户信息", "/api/v2/user/getUserInfo");

      try
      {
        var result = _client!.UserApi.GetEmailUser(TestEmail);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");

        if (result.Data != null)
        {
          _output.WriteLine("");
          _output.WriteLine("UserInfo 字段:");
          _output.WriteLine($"  Uid: {result.Data.Uid}");
          _output.WriteLine($"  Email: {result.Data.Email}");
          _output.WriteLine($"  Nickname: {result.Data.Nickname}");
          _output.WriteLine($"  Mobile: {result.Data.Mobile}");
          _output.WriteLine($"  Country: {result.Data.Country}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 同步用户列表
    /// </summary>
    [Fact]
    public void Debug_SyncUserList()
    {
      SkipIfNotConfigured();
      PrintSeparator("同步用户列表", "/api/v2/user/getUserList");

      try
      {
        var result = _client!.UserApi.SyncUserList(0);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        PrintListResult(result.Data, "UserList");

        if (result.Data != null && result.Data.Count > 0)
        {
          var user = result.Data[0];
          _output.WriteLine("");
          _output.WriteLine("首条 UserInfo 字段详情:");
          _output.WriteLine($"  Uid: {user.Uid}");
          _output.WriteLine($"  Email: {user.Email}");
          _output.WriteLine($"  Nickname: {user.Nickname}");
          _output.WriteLine($"  Mobile: {user.Mobile}");
          _output.WriteLine($"  Country: {user.Country}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    #endregion

    #region AccountApi 调试测试

    /// <summary>
    /// 调试测试: 获取用户账户
    /// </summary>
    [Fact]
    public void Debug_GetUserAccount()
    {
      SkipIfNotConfigured();
      PrintSeparator("获取用户账户", "/api/v2/account/getUserSymbolAccount");

      try
      {
        var result = _client!.AccountApi.GetUserAccount(TestUid, TestSymbol);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");

        if (result.Data != null)
        {
          _output.WriteLine("");
          _output.WriteLine("UserAccount 字段:");
          _output.WriteLine($"  NormalBalance: {result.Data.NormalBalance}");
          _output.WriteLine($"  LockBalance: {result.Data.LockBalance}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 获取用户充值地址
    /// </summary>
    [Fact]
    public void Debug_GetUserAddress()
    {
      SkipIfNotConfigured();
      PrintSeparator("获取用户充值地址", "/api/v2/account/getUserDepositAddress");

      try
      {
        var result = _client!.AccountApi.GetUserAddress(TestUid, TestSymbol);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");

        if (result.Data != null)
        {
          _output.WriteLine("");
          _output.WriteLine("UserAddress 字段:");
          _output.WriteLine($"  Id: {result.Data.Id}");
          _output.WriteLine($"  Uid: {result.Data.Uid}");
          _output.WriteLine($"  Address: {result.Data.Address}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 同步用户地址列表
    /// </summary>
    [Fact]
    public void Debug_SyncUserAddressList()
    {
      SkipIfNotConfigured();
      PrintSeparator("同步用户地址列表", "/api/v2/account/getUserDepositAddressList");

      try
      {
        var result = _client!.AccountApi.SyncUserAddressList(0);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        PrintListResult(result.Data, "AddressList");

        if (result.Data != null && result.Data.Count > 0)
        {
          var addr = result.Data[0];
          _output.WriteLine("");
          _output.WriteLine("首条 UserAddress 字段详情:");
          _output.WriteLine($"  Id: {addr.Id}");
          _output.WriteLine($"  Uid: {addr.Uid}");
          _output.WriteLine($"  Address: {addr.Address}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 获取公司账户
    /// </summary>
    [Fact]
    public void Debug_GetCompanyAccount()
    {
      SkipIfNotConfigured();
      PrintSeparator("获取公司账户", "/api/v2/account/getCompanySymbolAccount");

      try
      {
        var result = _client!.AccountApi.GetCompanyAccount(TestSymbol);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");

        if (result.Data != null)
        {
          _output.WriteLine("");
          _output.WriteLine("Account 字段:");
          _output.WriteLine($"  Symbol: {result.Data.Symbol}");
          _output.WriteLine($"  Balance: {result.Data.Balance}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    #endregion

    #region CoinApi 调试测试

    /// <summary>
    /// 调试测试: 获取币种列表
    /// </summary>
    [Fact]
    public void Debug_GetCoinList()
    {
      SkipIfNotConfigured();
      PrintSeparator("获取币种列表", "/api/v2/user/getCoinList");

      try
      {
        var result = _client!.CoinApi.GetCoinList();
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        PrintListResult(result.Data, "CoinList");

        if (result.Data != null && result.Data.Count > 0)
        {
          var coin = result.Data[0];
          _output.WriteLine("");
          _output.WriteLine("首条 CoinInfo 字段详情:");
          _output.WriteLine($"  Symbol: {coin.Symbol}");
          _output.WriteLine($"  Icon: {coin.Icon}");
          _output.WriteLine($"  RealSymbol: {coin.RealSymbol}");
          _output.WriteLine($"  BaseSymbol: {coin.BaseSymbol}");
          _output.WriteLine($"  Decimals: {coin.Decimals}");
          _output.WriteLine($"  ContractAddress: {coin.ContractAddress}");
          _output.WriteLine($"  DepositConfirmation: {coin.DepositConfirmation}");
          _output.WriteLine($"  SupportMemo: {coin.SupportMemo}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    #endregion

    #region BillingApi 调试测试

    /// <summary>
    /// 调试测试: 同步提现列表
    /// </summary>
    [Fact]
    public void Debug_SyncWithdrawList()
    {
      SkipIfNotConfigured();
      PrintSeparator("同步提现列表", "/api/v2/billing/syncWithdrawList");

      try
      {
        var result = _client!.BillingApi.SyncWithdrawList(0);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        PrintListResult(result.Data, "WithdrawList");

        if (result.Data != null && result.Data.Count > 0)
        {
          var withdraw = result.Data[0];
          _output.WriteLine("");
          _output.WriteLine("首条 Withdraw 字段详情:");
          _output.WriteLine($"  Id: {withdraw.Id}");
          _output.WriteLine($"  RequestId: {withdraw.RequestId}");
          _output.WriteLine($"  Uid: {withdraw.Uid}");
          _output.WriteLine($"  Symbol: {withdraw.Symbol}");
          _output.WriteLine($"  Amount: {withdraw.Amount}");
          _output.WriteLine($"  AddressTo: {withdraw.AddressTo}");
          _output.WriteLine($"  Status: {withdraw.Status}");
          _output.WriteLine($"  WithdrawFee: {withdraw.WithdrawFee}");
          _output.WriteLine($"  WithdrawFeeSymbol: {withdraw.WithdrawFeeSymbol}");
          _output.WriteLine($"  RealFee: {withdraw.RealFee}");
          _output.WriteLine($"  FeeSymbol: {withdraw.FeeSymbol}");
          _output.WriteLine($"  CreatedAt: {withdraw.CreatedAt}");
          _output.WriteLine($"  UpdatedAt: {withdraw.UpdatedAt}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 根据请求ID获取提现列表
    /// </summary>
    [Fact]
    public void Debug_WithdrawList()
    {
      SkipIfNotConfigured();
      PrintSeparator("根据请求ID获取提现列表", "/api/v2/billing/withdrawList");

      try
      {
        var requestIds = new List<string> { "test_request_1" };
        var result = _client!.BillingApi.WithdrawList(requestIds);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        PrintListResult(result.Data, "WithdrawList");
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 同步充值列表
    /// </summary>
    [Fact]
    public void Debug_SyncDepositList()
    {
      SkipIfNotConfigured();
      PrintSeparator("同步充值列表", "/api/v2/billing/syncDepositList");

      try
      {
        var result = _client!.BillingApi.SyncDepositList(0);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        PrintListResult(result.Data, "DepositList");

        if (result.Data != null && result.Data.Count > 0)
        {
          var deposit = result.Data[0];
          _output.WriteLine("");
          _output.WriteLine("首条 Deposit 字段详情:");
          _output.WriteLine($"  Id: {deposit.Id}");
          _output.WriteLine($"  Uid: {deposit.Uid}");
          _output.WriteLine($"  Symbol: {deposit.Symbol}");
          _output.WriteLine($"  Amount: {deposit.Amount}");
          _output.WriteLine($"  AddressFrom: {deposit.AddressFrom}");
          _output.WriteLine($"  AddressTo: {deposit.AddressTo}");
          _output.WriteLine($"  Txid: {deposit.Txid}");
          _output.WriteLine($"  Confirmations: {deposit.Confirmations}");
          _output.WriteLine($"  Status: {deposit.Status}");
          _output.WriteLine($"  CreatedAt: {deposit.CreatedAt}");
          _output.WriteLine($"  UpdatedAt: {deposit.UpdatedAt}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 根据ID获取充值列表
    /// </summary>
    [Fact]
    public void Debug_DepositList()
    {
      SkipIfNotConfigured();
      PrintSeparator("根据ID获取充值列表", "/api/v2/billing/depositList");

      try
      {
        var ids = new List<int> { 1 };
        var result = _client!.BillingApi.DepositList(ids);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        PrintListResult(result.Data, "DepositList");
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 同步矿工费列表
    /// </summary>
    [Fact]
    public void Debug_SyncMinerFeeList()
    {
      SkipIfNotConfigured();
      PrintSeparator("同步矿工费列表", "/api/v2/billing/syncMinerFeeList");

      try
      {
        var result = _client!.BillingApi.SyncMinerFeeList(0);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        PrintListResult(result.Data, "MinerFeeList");

        if (result.Data != null && result.Data.Count > 0)
        {
          var fee = result.Data[0];
          _output.WriteLine("");
          _output.WriteLine("首条 MinerFee 字段详情:");
          _output.WriteLine($"  Id: {fee.Id}");
          _output.WriteLine($"  Symbol: {fee.Symbol}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 根据ID获取矿工费列表
    /// </summary>
    [Fact]
    public void Debug_MinerFeeList()
    {
      SkipIfNotConfigured();
      PrintSeparator("根据ID获取矿工费列表", "/api/v2/billing/minerFeeList");

      try
      {
        var ids = new List<int> { 1 };
        var result = _client!.BillingApi.MinerFeeList(ids);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        PrintListResult(result.Data, "MinerFeeList");
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 提现 (注意: 实际调用会创建提现请求)
    /// </summary>
    [Fact]
    public void Debug_Withdraw()
    {
      SkipIfNotConfigured();
      PrintSeparator("提现", "/api/v2/billing/withdraw");

      try
      {
        var args = new WithdrawArgs
        {
          RequestId = "1234567890",
          Symbol = TestSymbol,
          Amount = 0.001m,
          ToAddress = "0x0000000000000000000000000000000000000000"
        };

        _output.WriteLine("请求参数:");
        PrintResult(args);
        _output.WriteLine("");
        _output.WriteLine("注意: 此测试默认跳过，避免实际创建提现请求");
        _output.WriteLine("如需测试，请手动调用 _client.BillingApi.Withdraw(args)");

        // 默认不实际调用，避免创建真实提现
        // var result = _client!.BillingApi.Withdraw(args);
        // PrintResult(result);
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    #endregion

    #region TransferApi 调试测试

    /// <summary>
    /// 调试测试: 同步转账列表
    /// </summary>
    [Fact]
    public void Debug_SyncAccountTransferList()
    {
      SkipIfNotConfigured();
      PrintSeparator("同步转账列表", "/api/v2/account/syncAccountTransferList");

      try
      {
        var result = _client!.TransferApi.SyncAccountTransferList(0);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        PrintListResult(result.Data, "TransferList");

        if (result.Data != null && result.Data.Count > 0)
        {
          var transfer = result.Data[0];
          _output.WriteLine("");
          _output.WriteLine("首条 Transfer 字段详情:");
          _output.WriteLine($"  Id: {transfer.Id}");
          _output.WriteLine($"  RequestId: {transfer.RequestId}");
          _output.WriteLine($"  Receipt: {transfer.Receipt}");
          _output.WriteLine($"  Symbol: {transfer.Symbol}");
          _output.WriteLine($"  Amount: {transfer.Amount}");
          _output.WriteLine($"  From: {transfer.From}");
          _output.WriteLine($"  To: {transfer.To}");
          _output.WriteLine($"  CreatedAt: {transfer.CreatedAt}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 根据ID获取转账列表
    /// </summary>
    [Fact]
    public void Debug_GetAccountTransferList()
    {
      SkipIfNotConfigured();
      PrintSeparator("根据ID获取转账列表", "/api/v2/account/getAccountTransferList");

      try
      {
        var result = _client!.TransferApi.GetAccountTransferList("test_id_1", ITransferApi.REQUEST_ID);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        PrintListResult(result.Data, "TransferList");
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 内部转账 (注意: 实际调用会创建转账请求)
    /// </summary>
    [Fact]
    public void Debug_AccountTransfer()
    {
      SkipIfNotConfigured();
      PrintSeparator("内部转账", "/api/v2/account/accountTransfer");

      try
      {
        var args = new TransferArgs
        {
          RequestId = $"test_{DateTime.Now:yyyyMMddHHmmss}",
          Symbol = TestSymbol,
          Amount = 0.001m,
          To = "recipient_uid"
        };

        _output.WriteLine("请求参数:");
        PrintResult(args);
        _output.WriteLine("");
        _output.WriteLine("注意: 此测试默认跳过，避免实际创建转账请求");
        _output.WriteLine("如需测试，请手动调用 _client.TransferApi.AccountTransfer(args)");

        // 默认不实际调用，避免创建真实转账
        // var result = _client!.TransferApi.AccountTransfer(args);
        // PrintResult(result);
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    #endregion

    #region 综合测试 - 打印所有 API 结果

    /// <summary>
    /// 综合调试测试: 调用所有可安全调用的 WaaS API 并打印结果
    /// </summary>
    [Fact]
    public void Debug_AllWaasApis()
    {
      SkipIfNotConfigured();

      _output.WriteLine("╔════════════════════════════════════════════════════════════╗");
      _output.WriteLine("║                   WaaS API 综合调试测试                     ║");
      _output.WriteLine("╚════════════════════════════════════════════════════════════╝");
      _output.WriteLine("");

      // 1. UserApi - SyncUserList
      try
      {
        PrintSeparator("1. 同步用户列表", "UserApi.SyncUserList");
        var userListResult = _client!.UserApi.SyncUserList(0);
        _output.WriteLine($"Code: {userListResult.Code}, Msg: {userListResult.Msg}");
        PrintListResult(userListResult.Data, "UserList");
      }
      catch (Exception ex)
      {
        _output.WriteLine($"调用失败: {ex.Message}");
      }

      // 2. AccountApi - GetCompanyAccount
      try
      {
        PrintSeparator("2. 获取公司账户", "AccountApi.GetCompanyAccount");
        var companyAccountResult = _client!.AccountApi.GetCompanyAccount(TestSymbol);
        _output.WriteLine($"Code: {companyAccountResult.Code}, Msg: {companyAccountResult.Msg}");
        if (companyAccountResult.Data != null)
        {
          _output.WriteLine($"Symbol: {companyAccountResult.Data.Symbol}, Balance: {companyAccountResult.Data.Balance}");
        }
      }
      catch (Exception ex)
      {
        _output.WriteLine($"调用失败: {ex.Message}");
      }

      // 3. AccountApi - SyncUserAddressList
      try
      {
        PrintSeparator("3. 同步用户地址列表", "AccountApi.SyncUserAddressList");
        var addressListResult = _client!.AccountApi.SyncUserAddressList(0);
        _output.WriteLine($"Code: {addressListResult.Code}, Msg: {addressListResult.Msg}");
        PrintListResult(addressListResult.Data, "AddressList");
      }
      catch (Exception ex)
      {
        _output.WriteLine($"调用失败: {ex.Message}");
      }

      // 4. CoinApi - GetCoinList
      try
      {
        PrintSeparator("4. 获取币种列表", "CoinApi.GetCoinList");
        var coinListResult = _client!.CoinApi.GetCoinList();
        _output.WriteLine($"Code: {coinListResult.Code}, Msg: {coinListResult.Msg}");
        PrintListResult(coinListResult.Data, "CoinList");
      }
      catch (Exception ex)
      {
        _output.WriteLine($"调用失败: {ex.Message}");
      }

      // 5. BillingApi - SyncDepositList
      try
      {
        PrintSeparator("5. 同步充值列表", "BillingApi.SyncDepositList");
        var depositListResult = _client!.BillingApi.SyncDepositList(0);
        _output.WriteLine($"Code: {depositListResult.Code}, Msg: {depositListResult.Msg}");
        PrintListResult(depositListResult.Data, "DepositList");
      }
      catch (Exception ex)
      {
        _output.WriteLine($"调用失败: {ex.Message}");
      }

      // 6. BillingApi - SyncWithdrawList
      try
      {
        PrintSeparator("6. 同步提现列表", "BillingApi.SyncWithdrawList");
        var withdrawListResult = _client!.BillingApi.SyncWithdrawList(0);
        _output.WriteLine($"Code: {withdrawListResult.Code}, Msg: {withdrawListResult.Msg}");
        PrintListResult(withdrawListResult.Data, "WithdrawList");
      }
      catch (Exception ex)
      {
        _output.WriteLine($"调用失败: {ex.Message}");
      }

      // 7. BillingApi - SyncMinerFeeList
      try
      {
        PrintSeparator("7. 同步矿工费列表", "BillingApi.SyncMinerFeeList");
        var minerFeeListResult = _client!.BillingApi.SyncMinerFeeList(0);
        _output.WriteLine($"Code: {minerFeeListResult.Code}, Msg: {minerFeeListResult.Msg}");
        PrintListResult(minerFeeListResult.Data, "MinerFeeList");
      }
      catch (Exception ex)
      {
        _output.WriteLine($"调用失败: {ex.Message}");
      }

      // 8. TransferApi - SyncAccountTransferList
      try
      {
        PrintSeparator("8. 同步转账列表", "TransferApi.SyncAccountTransferList");
        var transferListResult = _client!.TransferApi.SyncAccountTransferList(0);
        _output.WriteLine($"Code: {transferListResult.Code}, Msg: {transferListResult.Msg}");
        PrintListResult(transferListResult.Data, "TransferList");
      }
      catch (Exception ex)
      {
        _output.WriteLine($"调用失败: {ex.Message}");
      }

      _output.WriteLine("");
      _output.WriteLine("╔════════════════════════════════════════════════════════════╗");
      _output.WriteLine("║                     综合调试测试完成                        ║");
      _output.WriteLine("╚════════════════════════════════════════════════════════════╝");
    }

    #endregion
  }
}
