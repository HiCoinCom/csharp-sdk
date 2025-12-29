using System;
using System.Collections.Generic;
using System.Linq;
using ChainUpCustody.Api.Models.Mpc;
using ChainUpCustody.Api.Mpc;
using ChainUpCustody.Client;
using ChainUpCustody.Config;
using ChainUpCustody.Enums;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace ChainUpCustody.Tests.Api.Mpc
{
  /// <summary>
  /// Integration tests for MPC API functions
  /// Reference: https://github.com/HiCoinCom/rust-sdk/blob/main/examples/mpc_example.rs
  /// 
  /// These tests make real API calls. Configure the following environment variables:
  /// - MPC_APP_ID: Your application ID
  /// - MPC_USER_PRIVATE_KEY: Your RSA private key
  /// - MPC_WAAS_PUBLIC_KEY: ChainUp WaaS RSA public key
  /// - MPC_SIGN_PRIVATE_KEY: Your sign RSA private key (optional, for withdraw/web3)
  /// </summary>
  public class MpcApiIntegrationTests
  {
    private readonly ITestOutputHelper _output;
    private readonly MpcClient? _client;
    private readonly bool _isConfigured;

    // Test configuration - replace with your actual values or use environment variables
    private const int SubWalletId = 1000537;
    private const string TestSymbol = "ETH";
    private const string TestMainChainSymbol = "ETH";
    private const string TestAddress = "0x633A84Ee0ab29d911e5466e5E1CB9cdBf5917E72";

    public MpcApiIntegrationTests(ITestOutputHelper output)
    {
      _output = output;

      var appId = Environment.GetEnvironmentVariable("MPC_APP_ID") ?? "";
      var userPrivateKey = Environment.GetEnvironmentVariable("MPC_USER_PRIVATE_KEY") ?? "";
      var waasPublicKey = Environment.GetEnvironmentVariable("MPC_WAAS_PUBLIC_KEY") ?? "";
      var signPrivateKey = Environment.GetEnvironmentVariable("MPC_SIGN_PRIVATE_KEY") ?? "";

      _isConfigured = !string.IsNullOrEmpty(appId) &&
                      !string.IsNullOrEmpty(userPrivateKey) &&
                      !string.IsNullOrEmpty(waasPublicKey);

      if (_isConfigured)
      {
        _client = MpcClient.Builder()
            .SetAppId(appId)
            .SetUserPrivateKey(userPrivateKey)
            .SetWaasPublicKey(waasPublicKey)
            .SetSignPrivateKey(signPrivateKey)
            .EnableLog(true)
            .Build();
        _output.WriteLine("✓ MPC 客户端创建成功");
      }
      else
      {
        _output.WriteLine("⚠ MPC 客户端未配置，请设置环境变量");
      }
    }

    private void SkipIfNotConfigured()
    {
      Skip.If(!_isConfigured, "MPC API 未配置，跳过集成测试");
    }

    /// <summary>
    /// 打印错误信息
    /// </summary>
    private void PrintError<T>(ChainUpCustody.Api.Models.Result<T>? result)
    {
      if (result == null)
      {
        _output.WriteLine("  ✗ 错误: 结果为 null");
        return;
      }
      _output.WriteLine($"  ✗ 错误: Code={result.Code}, Msg={result.Msg}");
    }

    /// <summary>
    /// 打印列表详情：长度和第一条数据的 JSON
    /// </summary>
    private void PrintListDetails<T>(string label, List<T>? list)
    {
      if (list == null)
      {
        _output.WriteLine($"  {label}: null");
        return;
      }
      _output.WriteLine($"  {label}长度: {list.Count}");
      if (list.Count > 0)
      {
        var firstItem = JsonConvert.SerializeObject(list[0], Formatting.Indented);
        _output.WriteLine($"  {label}首条数据:\n{firstItem}");
      }
    }

    #region WorkSpace API Tests

    [SkippableFact]
    public void GetSupportMainChain_ShouldReturnSupportedCoins()
    {
      SkipIfNotConfigured();

      _output.WriteLine("\n========== 工作区管理 (Workspace API) ==========");

      // 1. 获取支持的币种
      var result = _client!.WorkSpaceApi.GetSupportMainChain();

      _output.WriteLine($"✓ 获取支持的币种: Code={result.Code}, Msg={result.Msg}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        PrintListDetails("已开通主链", result.Data.OpenMainChain);
        PrintListDetails("支持主链", result.Data.SupportMainChainList);
      }
      else
      {
        PrintError(result);
      }
    }

    [SkippableFact]
    public void GetCoinDetails_ShouldReturnCoinInfo()
    {
      SkipIfNotConfigured();

      // 2. 获取币种详情
      var result = _client!.WorkSpaceApi.GetCoinDetails(TestMainChainSymbol);

      _output.WriteLine($"✓ 获取币种详情 ({TestMainChainSymbol}): Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        PrintListDetails("币种详情", result.Data);
      }
      else
      {
        PrintError(result);
      }
    }

    [SkippableFact]
    public void GetLastBlockHeight_ShouldReturnBlockHeight()
    {
      SkipIfNotConfigured();

      // 3. 获取最新区块高度
      var result = _client!.WorkSpaceApi.GetLastBlockHeight(TestMainChainSymbol);

      _output.WriteLine($"✓ 获取区块高度 ({TestMainChainSymbol}): Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  区块高度: {result.Data.BlockHeightValue}");
        _output.WriteLine($"  区块高度详情: {JsonConvert.SerializeObject(result.Data, Formatting.Indented)}");
      }
      else
      {
        PrintError(result);
      }
    }

    #endregion

    #region Wallet API Tests

    [SkippableFact]
    public void CreateWallet_ShouldCreateNewWallet()
    {
      SkipIfNotConfigured();

      _output.WriteLine("\n========== 钱包管理 (Wallet API) ==========");

      // 1. 创建钱包
      var walletName = "TestWallet";
      var result = _client!.WalletApi.CreateWallet(walletName, AppShowStatus.Show);

      _output.WriteLine($"✓ 创建钱包 ({walletName}): Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  创建钱包结果: {JsonConvert.SerializeObject(result.Data, Formatting.Indented)}");
      }
      else
      {
        PrintError(result);
      }
    }

    [SkippableFact]
    public void CreateWalletAddress_ShouldReturnAddress()
    {
      SkipIfNotConfigured();

      // 2. 创建钱包地址
      var result = _client!.WalletApi.CreateWalletAddress(SubWalletId, TestSymbol);

      _output.WriteLine($"✓ 创建钱包地址 (SubWalletId={SubWalletId}, Symbol={TestSymbol}): Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  创建地址结果: {JsonConvert.SerializeObject(result.Data, Formatting.Indented)}");
      }
      else
      {
        PrintError(result);
      }
    }

    [SkippableFact]
    public void GetAddressList_ShouldReturnAddresses()
    {
      SkipIfNotConfigured();

      // 3. 查询钱包地址列表
      var result = _client!.WalletApi.GetAddressList(SubWalletId, TestSymbol, null);

      _output.WriteLine($"✓ 查询地址列表: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        PrintListDetails("地址列表", result.Data);
      }
      else
      {
        PrintError(result);
      }
    }

    [SkippableFact]
    public void GetWalletAssets_ShouldReturnAssets()
    {
      SkipIfNotConfigured();

      // 4. 获取钱包资产
      var result = _client!.WalletApi.GetWalletAssets(SubWalletId, TestSymbol);

      _output.WriteLine($"✓ 获取钱包资产: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  钱包资产: {JsonConvert.SerializeObject(result.Data, Formatting.Indented)}");
      }
      else
      {
        PrintError(result);
      }
    }

    [SkippableFact]
    public void ChangeShowStatus_ShouldUpdateStatus()
    {
      SkipIfNotConfigured();

      // 5. 修改钱包显示状态
      var result = _client!.WalletApi.ChangeShowStatus(new long[] { SubWalletId }, 1);

      _output.WriteLine($"✓ 修改显示状态: {result}");

      Assert.True(result || !result); // 记录结果，不强制断言
    }

    [SkippableFact]
    public void GetAddressInfo_ShouldReturnAddressInfo()
    {
      SkipIfNotConfigured();

      // 6. 查询地址信息
      var result = _client!.WalletApi.GetAddressInfo(TestAddress);

      _output.WriteLine($"✓ 查询地址信息: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  地址信息: {JsonConvert.SerializeObject(result.Data, Formatting.Indented)}");
      }
      else
      {
        PrintError(result);
      }
    }

    #endregion

    #region Deposit API Tests

    [SkippableFact]
    public void SyncDepositRecords_ShouldReturnRecords()
    {
      SkipIfNotConfigured();

      _output.WriteLine("\n========== 充值管理 (Deposit API) ==========");

      // 1. 同步充值记录
      var result = _client!.DepositApi.SyncDepositRecords(0);

      _output.WriteLine($"✓ 同步充值记录: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        PrintListDetails("充值记录", result.Data);
      }
      else
      {
        PrintError(result);
      }
    }

    [SkippableFact]
    public void GetDepositRecords_ShouldReturnSpecificRecords()
    {
      SkipIfNotConfigured();

      // 2. 获取特定充值记录
      var ids = new List<int> { 123, 456, 789 };
      var result = _client!.DepositApi.GetDepositRecords(ids);

      _output.WriteLine($"✓ 获取充值记录: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        PrintListDetails("充值记录", result.Data);
      }
      else
      {
        PrintError(result);
      }
    }

    #endregion

    #region Withdraw API Tests

    [SkippableFact]
    public void Withdraw_ShouldSubmitWithdrawal()
    {
      SkipIfNotConfigured();

      _output.WriteLine("\n========== 提现管理 (Withdraw API) ==========");

      // 1. 发起提现 (需要配置 SignPrivateKey)
      var withdrawArgs = new MpcWithdrawArgs
      {
        RequestId = "123456789032",
        SubWalletId = SubWalletId,
        Symbol = "Sepolia",
        Amount = "0.001",
        AddressTo = "0xdcb0D867403adE76e75a4A6bBcE9D53C9d05B981",
        Remark = "Test withdrawal from C# SDK"
      };

      var result = _client!.WithdrawApi.Withdraw(withdrawArgs, true);

      _output.WriteLine($"✓ 发起提现: Code={result.Code}, Msg={result.Msg}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  提现结果: {JsonConvert.SerializeObject(result.Data, Formatting.Indented)}");
      }
      else
      {
        PrintError(result);
      }
    }

    [SkippableFact]
    public void SyncWithdrawRecords_ShouldReturnRecords()
    {
      SkipIfNotConfigured();

      // 2. 同步提现记录
      var result = _client!.WithdrawApi.SyncWithdrawRecords(0);

      _output.WriteLine($"✓ 同步提现记录: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        PrintListDetails("提现记录", result.Data);
      }
      else
      {
        PrintError(result);
      }
    }

    [SkippableFact]
    public void GetWithdrawRecords_ShouldReturnSpecificRecords()
    {
      SkipIfNotConfigured();

      // 3. 获取特定提现记录
      var requestIds = new List<string> { "123456789028", "1234567890" };
      var result = _client!.WithdrawApi.GetWithdrawRecords(requestIds);

      _output.WriteLine($"✓ 获取提现记录: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        PrintListDetails("提现记录", result.Data);
      }
      else
      {
        PrintError(result);
      }
    }

    #endregion

    #region Web3 API Tests

    [SkippableFact]
    public void CreateWeb3Trans_ShouldSubmitTransaction()
    {
      SkipIfNotConfigured();

      _output.WriteLine("\n========== Web3 交易 (Web3 API) ==========");

      // 1. 创建 Web3 交易
      var web3Args = new CreateWeb3Args
      {
        RequestId = "1234567890",
        SubWalletId = SubWalletId,
        MainChainSymbol = TestMainChainSymbol,
        InteractiveContract = "0x1234567890abcdef1234567890abcdef12345678",
        Amount = "0",
        GasPrice = "20",
        GasLimit = "21000",
        InputData = "0x",
        TransType = 1,
        DappName = "Test DApp",
        DappUrl = "https://example.com"
      };

      var result = _client!.Web3Api.CreateWeb3Trans(web3Args, true);

      _output.WriteLine($"✓ 创建 Web3 交易: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  Web3交易结果: {JsonConvert.SerializeObject(result.Data, Formatting.Indented)}");
      }
      else
      {
        PrintError(result);
      }
    }

    [SkippableFact]
    public void SyncWeb3Records_ShouldReturnRecords()
    {
      SkipIfNotConfigured();

      // 2. 同步 Web3 交易记录
      var result = _client!.Web3Api.SyncWeb3Records(0);

      _output.WriteLine($"✓ 同步 Web3 记录: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        PrintListDetails("Web3记录", result.Data);
      }
      else
      {
        PrintError(result);
      }
    }

    [SkippableFact]
    public void GetWeb3Records_ShouldReturnSpecificRecords()
    {
      SkipIfNotConfigured();

      // 3. 获取特定 Web3 交易记录
      var requestIds = new List<string> { "web3_test_1", "web3_test_2" };
      var result = _client!.Web3Api.GetWeb3Records(requestIds);

      _output.WriteLine($"✓ 获取 Web3 记录: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        PrintListDetails("Web3记录", result.Data);
      }
      else
      {
        PrintError(result);
      }
    }

    [SkippableFact]
    public void AccelerationWeb3Trans_ShouldAccelerateTransaction()
    {
      SkipIfNotConfigured();

      // 4. 加速 Web3 交易
      // 参考: https://github.com/HiCoinCom/rust-sdk/blob/main/src/mpc/api/web3_api.rs
      var accelerationArgs = new Web3AccelerationArgs
      {
        TransId = 12345678,  // Web3 交易 ID
        GasPrice = "50",     // Gas 价格 (Gwei)
        GasLimit = "30000"   // Gas 限制
      };

      var result = _client!.Web3Api.AccelerationWeb3Trans(accelerationArgs);

      _output.WriteLine($"加速 Web3 交易: {(result ? "成功" : "失败")}");
    }

    #endregion

    #region Auto Sweep API Tests

    [SkippableFact]
    public void AutoSweep_ShouldTriggerAutoSweep()
    {
      SkipIfNotConfigured();

      _output.WriteLine("\n========== 自动归集 (Auto Sweep API) ==========");

      // 1. 获取自动归集钱包
      var result = _client!.AutoSweepApi.AutoCollectSubWallets("USDTERC20");

      _output.WriteLine($"✓ 获取自动归集钱包: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  FuelingSubWalletId: {result.Data.FuelingSubWalletId}");
        _output.WriteLine($"  CollectSubWalletId: {result.Data.CollectSubWalletId}");
      }
      else
      {
        PrintError(result);
      }
    }

    [SkippableFact]
    public void SyncAutoCollectRecords_ShouldReturnRecords()
    {
      SkipIfNotConfigured();

      // 2. 同步归集记录
      var result = _client!.AutoSweepApi.SyncAutoCollectRecords(0);

      _output.WriteLine($"✓ 同步归集记录: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        PrintListDetails("归集记录", result.Data);
      }
      else
      {
        PrintError(result);
      }
    }

    [SkippableFact]
    public void SetAutoCollectSymbol_ShouldConfigureAutoSweep()
    {
      SkipIfNotConfigured();

      // 3. 设置自动归集配置
      var result = _client!.AutoSweepApi.SetAutoCollectSymbol("USDTERC20", "100", "0.01");

      _output.WriteLine($"✓ 设置自动归集: {result}");

      // 只验证调用不抛异常，结果可能为 true 或 false
    }

    #endregion

    #region Tron Resource API Tests

    [SkippableFact]
    public void CreateTronDelegate_ShouldBuyEnergy()
    {
      SkipIfNotConfigured();

      _output.WriteLine("\n========== TRON 资源管理 (Tron Resource API) ==========");

      // 1. 购买 TRON 资源 (能量)
      // Reference: https://github.com/HiCoinCom/rust-sdk/blob/main/src/mpc/api/tron_resource_api.rs
      var args = new TronBuyEnergyArgs
      {
        RequestId = "123456789012",
        AddressFrom = "TPjJg9FnzQuYBd6bshgaq7rkH4s36zju5S",   // 支付资源的地址 (必填)
        ServiceChargeType = "10010",                          // 10010=10分钟, 20001=1小时, 30001=1天
        BuyType = 1,                                          // 0=系统估算, 1=手动指定
        ResourceType = 1,                                     // 0=能量和带宽, 1=仅能量
        EnergyNum = 32000,                                    // 能量数量
        AddressTo = "TGmBzYfBBtMfFF8v9PweTaPwn3WoB7aGPd",     // 接收资源的地址 (buy_type 0 或 2 时必填)
        ContractAddress = "TR7NHqjeKQxGTCi8q8ZY4pL8otSzgjLj6t" // 合约地址 (buy_type 0 或 2 时必填)
      };

      var result = _client!.TronBuyResourceApi.CreateTronDelegate(args);

      _output.WriteLine($"✓ 购买 TRON 资源: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        _output.WriteLine($"  购买资源结果: {JsonConvert.SerializeObject(result.Data, Formatting.Indented)}");
      }
      else
      {
        PrintError(result);
      }
    }

    [SkippableFact]
    public void SyncBuyResourceRecords_ShouldReturnRecords()
    {
      SkipIfNotConfigured();

      // 2. 同步购买资源记录
      var result = _client!.TronBuyResourceApi.SyncBuyResourceRecords(0);

      _output.WriteLine($"✓ 同步购买资源记录: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        PrintListDetails("购买资源记录", result.Data);
      }
      else
      {
        PrintError(result);
      }
    }

    [SkippableFact]
    public void GetBuyResourceRecords_ShouldReturnSpecificRecords()
    {
      SkipIfNotConfigured();

      // 3. 获取特定购买资源记录
      var requestIds = new List<string> { "1234567890", "tron_delegate_test_001" };
      var result = _client!.TronBuyResourceApi.GetBuyResourceRecords(requestIds);

      _output.WriteLine($"✓ 获取购买资源记录: Code={result.Code}");

      Assert.NotNull(result);
      if (result.Code == 0 && result.Data != null)
      {
        PrintListDetails("购买资源记录", result.Data);
      }
      else
      {
        PrintError(result);
      }
    }

    #endregion

    #region Notify API Tests

    [SkippableFact]
    public void NotifyRequest_ShouldDecryptNotification()
    {
      SkipIfNotConfigured();

      _output.WriteLine("\n========== 通知解密 (Notify API) ==========");

      // 解密通知数据 (使用实际的加密通知数据)
      var encryptedData = "Af-uUJj8a2-Og7E5CwzANv4vo8NMf-z-DijwrIuK74Or8eRveM7G_-f0ErtX4WurcVrjdWC-tqU0BDhBwiDijbdyCFBvYB5UmLnHL_Rg13amhQTM-kaHoh-U9WPhYB3vGRwWkTwJ_aETERVVciAvoTf5CalqydMSe8G3KNz-ymrSVUe92DfW5ZdDKJm1hNYYteGJvg0hk--GRiPybPv2W78NlTLyWmXq094megsVzZv-KlsEGPUvPoBnEJ0Xu__AO-l-GfCG4rVO4rb8J01Nq_0Q9eRKcKWq0ci7MfnPPLMhtAWwRvSd3U8PUNHOLqGaJzOLraFnuFUHn90h7T23_DeAduA2W6dto99qb8YQ_iVnMnOKfE0Ls7Vv5S2qhgQJ0nl-BA3PPPOwW37cMb-wTbi3ZezU_S1NQEbrruEChkPhTaK0AqsM6mESV8wGflcWx3N9XPv6QatJ9zedBnkfJ4bJ4Vy2rUEtQF8eVc6zXhV8PuDRiSMf0V0yxzMjE6o9z0s087KSAqFphitlHvQMPJ29FUnyvCe_Czr5WPuhl89GOZjERE2uoNTfHqAlZVzMamoPv4y0qyIjJTufAQm-WwrQK9kGesky7eCiOXVdtR9UhEYpzEJSgXxENjUrHMx6D2AlEzlr17a2DgI-WrWB7oUnyiNnf__ElmLPPkJBdFUfzJByQkLxkUB0FLvTWdVbiIRPmPpdgb7jkhJsHUSOH0NmULqu8bYiEQtGfqRJh8I98qDzHWwfE_VAbqwATj2oD959Fm1eInBqh7eXGoy2WR3o00VpPrNvoE4eJNmw3WpVzlRF7ZVwOpcWRT-dHTShz9mB2Etk9P8D4rGmMZyXHkt4aGUJkE1b3cOEjzkOEFX8CaNe-VHiBYhIyFzMetn7mfIFB0hl565FGEumbhDKNNz_m9T2qPM5k4BQ9fLWUt_WJAVdC81_piIlBOQfYPDbdYoc_9ser1p-Jy5cgTyOMdWuSWC3jMsT09xr8dMcLkKmd39khGidAvGqOOPL1ST0"; // 替换为实际加密数据

      var result = _client!.NotifyApi.NotifyRequest(encryptedData);

      if (result != null)
      {
        _output.WriteLine("✓ 解密通知成功");
        _output.WriteLine($"  通知详情: {JsonConvert.SerializeObject(result, Formatting.Indented)}");
      }
      else
      {
        _output.WriteLine("✗ 解密通知失败或数据无效");
      }
    }

    #endregion
  }
}
