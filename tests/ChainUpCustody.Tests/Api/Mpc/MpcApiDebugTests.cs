using System;
using System.Collections.Generic;
using ChainUpCustody.Api.Models.Mpc;
using ChainUpCustody.Client;
using ChainUpCustody.Enums;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace ChainUpCustody.Tests.Api.Mpc
{
  /// <summary>
  /// MPC API 调试测试 - 用于打印解密后的数据并核对返回值类型是否缺失字段
  /// 参考: https://github.com/HiCoinCom/rust-sdk/blob/main/examples/debug_api_response.rs
  /// 
  /// 这些测试会实际调用 API，请配置以下环境变量:
  /// - MPC_APP_ID: 应用 ID
  /// - MPC_USER_PRIVATE_KEY: 用户 RSA 私钥
  /// - MPC_WAAS_PUBLIC_KEY: ChainUp WaaS RSA 公钥
  /// - MPC_SIGN_PRIVATE_KEY: 签名 RSA 私钥 (可选，用于提现/web3)
  /// </summary>
  public class MpcApiDebugTests
  {
    private readonly ITestOutputHelper _output;
    private readonly MpcClient? _client;
    private readonly bool _isConfigured;

    // 测试配置 - 替换为实际值或使用环境变量
    private const int SubWalletId = 1000537;
    private const string TestSymbol = "ETH";
    private const string TestMainChainSymbol = "ETH";
    private const string TestAddress = "0x633A84Ee0ab29d911e5466e5E1CB9cdBf5917E72";

    public MpcApiDebugTests(ITestOutputHelper output)
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
      }
    }

    private void SkipIfNotConfigured()
    {
      if (!_isConfigured)
      {
        throw new SkipException("MPC API 未配置，跳过调试测试。请设置环境变量: MPC_APP_ID, MPC_USER_PRIVATE_KEY, MPC_WAAS_PUBLIC_KEY");
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

    private void PrintError(Exception ex)
    {
      _output.WriteLine($"错误: {ex.Message}");
      if (ex.InnerException != null)
      {
        _output.WriteLine($"内部错误: {ex.InnerException.Message}");
      }
    }

    #region WorkSpace API 调试测试

    /// <summary>
    /// 调试测试: 获取支持的币种
    /// </summary>
    [Fact]
    public void Debug_GetSupportMainChain()
    {
      SkipIfNotConfigured();
      PrintSeparator("获取支持的币种", "/api/mpc/coin/supported");

      try
      {
        var result = _client!.WorkSpaceApi.GetSupportMainChain();
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        _output.WriteLine($"OpenMainChain Count: {result.Data?.OpenMainChain?.Count ?? 0}");
        _output.WriteLine($"SupportMainChainList Count: {result.Data?.SupportMainChainList?.Count ?? 0}");

        if (result.Data?.OpenMainChain != null && result.Data.OpenMainChain.Count > 0)
        {
          var first = result.Data.OpenMainChain[0];
          _output.WriteLine("");
          _output.WriteLine("OpenMainChain 首条数据字段:");
          _output.WriteLine($"  Symbol: {first.Symbol}");
          _output.WriteLine($"  CoinNet: {first.CoinNet}");
          _output.WriteLine($"  RealSymbol: {first.RealSymbol}");
          _output.WriteLine($"  SymbolAlias: {first.SymbolAlias}");
          _output.WriteLine($"  IsSupportMemo: {first.IsSupportMemo}");
          _output.WriteLine($"  ChainId: {first.ChainId}");
          _output.WriteLine($"  EnableWithdraw: {first.EnableWithdraw}");
          _output.WriteLine($"  EnableDeposit: {first.EnableDeposit}");
          _output.WriteLine($"  SupportAcceleration: {first.SupportAcceleration}");
          _output.WriteLine($"  NeedPayment: {first.NeedPayment}");
          _output.WriteLine($"  IfOpenChain: {first.IfOpenChain}");
          _output.WriteLine($"  DisplayOrder: {first.DisplayOrder}");
        }

        if (result.Data?.SupportMainChainList != null && result.Data.SupportMainChainList.Count > 0)
        {
          var first = result.Data.SupportMainChainList[0];
          _output.WriteLine("");
          _output.WriteLine("SupportMainChainList 首条数据字段:");
          _output.WriteLine($"  Symbol: {first.Symbol}");
          _output.WriteLine($"  CoinNet: {first.CoinNet}");
          _output.WriteLine($"  RealSymbol: {first.RealSymbol}");
          _output.WriteLine($"  SymbolAlias: {first.SymbolAlias}");
          _output.WriteLine($"  IsSupportMemo: {first.IsSupportMemo}");
          _output.WriteLine($"  ChainId: {first.ChainId}");
          _output.WriteLine($"  EnableWithdraw: {first.EnableWithdraw}");
          _output.WriteLine($"  EnableDeposit: {first.EnableDeposit}");
          _output.WriteLine($"  SupportAcceleration: {first.SupportAcceleration}");
          _output.WriteLine($"  NeedPayment: {first.NeedPayment}");
          _output.WriteLine($"  IfOpenChain: {first.IfOpenChain}");
          _output.WriteLine($"  DisplayOrder: {first.DisplayOrder}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 获取币种详情
    /// </summary>
    [Fact]
    public void Debug_GetCoinDetails()
    {
      SkipIfNotConfigured();
      PrintSeparator("获取币种详情", "/api/mpc/coin/list");

      try
      {
        var result = _client!.WorkSpaceApi.GetCoinDetails(TestSymbol);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        _output.WriteLine($"Data Count: {result.Data?.Count ?? 0}");

        if (result.Data != null && result.Data.Count > 0)
        {
          var data = result.Data[0];
          _output.WriteLine("");
          _output.WriteLine("首条数据字段:");
          _output.WriteLine($"  Id: {data.Id}");
          _output.WriteLine($"  Symbol: {data.Symbol}");
          _output.WriteLine($"  CoinNet: {data.CoinNet}");
          _output.WriteLine($"  SymbolAlias: {data.SymbolAlias}");
          _output.WriteLine($"  ContractAddress: {data.ContractAddress}");
          _output.WriteLine($"  Decimals: {data.Decimals}");
          _output.WriteLine($"  AddressLink: {data.AddressLink}");
          _output.WriteLine($"  TxidLink: {data.TxidLink}");
          _output.WriteLine($"  DepositConfirmation: {data.DepositConfirmation}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 获取最新区块高度
    /// </summary>
    [Fact]
    public void Debug_GetLastBlockHeight()
    {
      SkipIfNotConfigured();
      PrintSeparator("获取最新区块高度", "/api/mpc/main/coin/last/block/height");

      try
      {
        var result = _client!.WorkSpaceApi.GetLastBlockHeight(TestMainChainSymbol);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");

        if (result.Data != null)
        {
          _output.WriteLine($"Data.BlockHeightValue: {result.Data.BlockHeightValue}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    #endregion

    #region Wallet API 调试测试

    /// <summary>
    /// 调试测试: 创建钱包
    /// </summary>
    [Fact]
    public void Debug_CreateWallet()
    {
      SkipIfNotConfigured();
      PrintSeparator("创建钱包", "/api/mpc/sub_wallet/create");

      try
      {
        var walletName = $"test_wallet_{DateTime.UtcNow:yyyyMMddHHmmss}";
        var result = _client!.WalletApi.CreateWallet(walletName, AppShowStatus.Show);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");

        if (result.Data != null)
        {
          _output.WriteLine($"Data.SubWalletId: {result.Data.SubWalletId}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 创建钱包地址
    /// </summary>
    [Fact]
    public void Debug_CreateWalletAddress()
    {
      SkipIfNotConfigured();
      PrintSeparator("创建钱包地址", "/api/mpc/sub_wallet/create/address");

      try
      {
        var result = _client!.WalletApi.CreateWalletAddress(SubWalletId, TestMainChainSymbol);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");

        if (result.Data != null)
        {
          _output.WriteLine($"Data.Address: {result.Data.Address}");
          _output.WriteLine($"Data.Memo: {result.Data.Memo}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 获取地址列表
    /// </summary>
    [Fact]
    public void Debug_GetAddressList()
    {
      SkipIfNotConfigured();
      PrintSeparator("获取地址列表", "/api/mpc/sub_wallet/get/address/list");

      try
      {
        var result = _client!.WalletApi.GetAddressList(SubWalletId, TestMainChainSymbol);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        _output.WriteLine($"Data Count: {result.Data?.Count ?? 0}");

        if (result.Data != null && result.Data.Count > 0)
        {
          var first = result.Data[0];
          _output.WriteLine("");
          _output.WriteLine("首条数据字段:");
          _output.WriteLine($"  Id: {first.Id}");
          _output.WriteLine($"  Address: {first.Address}");
          _output.WriteLine($"  Memo: {first.Memo}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 获取钱包资产
    /// </summary>
    [Fact]
    public void Debug_GetWalletAssets()
    {
      SkipIfNotConfigured();
      PrintSeparator("获取钱包资产", "/api/mpc/sub_wallet/assets");

      try
      {
        var result = _client!.WalletApi.GetWalletAssets(SubWalletId);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");

        if (result.Data != null)
        {
          var data = result.Data;
          _output.WriteLine("");
          _output.WriteLine("数据字段:");
          _output.WriteLine($"  NormalBalance: {data.NormalBalance}");
          _output.WriteLine($"  LockBalance: {data.LockBalance}");
          _output.WriteLine($"  CollectingBalance: {data.CollectingBalance}");
          _output.WriteLine($"  DepositAddress: {data.DepositAddress}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 修改钱包显示状态
    /// </summary>
    [Fact]
    public void Debug_ChangeShowStatus()
    {
      SkipIfNotConfigured();
      PrintSeparator("修改钱包显示状态", "/api/mpc/sub_wallet/change_show_status");

      try
      {
        var result = _client!.WalletApi.ChangeShowStatus(new long[] { SubWalletId }, 1);
        _output.WriteLine($"结果: {result}");

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"返回值类型: bool");
        _output.WriteLine($"返回值: {result}");
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 获取地址信息
    /// </summary>
    [Fact]
    public void Debug_GetAddressInfo()
    {
      SkipIfNotConfigured();
      PrintSeparator("获取地址信息", "/api/mpc/sub_wallet/address/info");

      try
      {
        var result = _client!.WalletApi.GetAddressInfo(TestAddress);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");

        if (result.Data != null)
        {
          _output.WriteLine($"Data.SubWalletId: {result.Data.SubWalletId}");
          _output.WriteLine($"Data.Address: {result.Data.Address}");
          _output.WriteLine($"Data.Memo: {result.Data.Memo}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    #endregion

    #region Deposit API 调试测试

    /// <summary>
    /// 调试测试: 同步充值记录
    /// </summary>
    [Fact]
    public void Debug_SyncDepositRecords()
    {
      SkipIfNotConfigured();
      PrintSeparator("同步充值记录", "/api/mpc/trans/sync/deposit");

      try
      {
        var result = _client!.DepositApi.SyncDepositRecords(0);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        _output.WriteLine($"Data Count: {result.Data?.Count ?? 0}");

        if (result.Data != null && result.Data.Count > 0)
        {
          var first = result.Data[0];
          _output.WriteLine("");
          _output.WriteLine("首条数据字段:");
          _output.WriteLine($"  Id: {first.Id}");
          _output.WriteLine($"  SubWalletId: {first.SubWalletId}");
          _output.WriteLine($"  Symbol: {first.Symbol}");
          _output.WriteLine($"  BaseSymbol: {first.BaseSymbol}");
          _output.WriteLine($"  ContractAddress: {first.ContractAddress}");
          _output.WriteLine($"  Amount: {first.Amount}");
          _output.WriteLine($"  AddressFrom: {first.AddressFrom}");
          _output.WriteLine($"  AddressTo: {first.AddressTo}");
          _output.WriteLine($"  Memo: {first.Memo}");
          _output.WriteLine($"  Txid: {first.Txid}");
          _output.WriteLine($"  Confirmations: {first.Confirmations}");
          _output.WriteLine($"  TxHeight: {first.TxHeight}");
          _output.WriteLine($"  Status: {first.Status}");
          _output.WriteLine($"  DepositType: {first.DepositType}");
          _output.WriteLine($"  TokenId: {first.TokenId}");
          _output.WriteLine($"  CreatedAt: {first.CreatedAt}");
          _output.WriteLine($"  UpdatedAt: {first.UpdatedAt}");
          _output.WriteLine($"  RefundAmount: {first.RefundAmount}");
          _output.WriteLine($"  KytStatus: {first.KytStatus}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 获取充值记录 (按ID)
    /// </summary>
    [Fact]
    public void Debug_GetDepositRecords()
    {
      SkipIfNotConfigured();
      PrintSeparator("获取充值记录", "/api/mpc/trans/deposit");

      try
      {
        // 先同步获取一些 ID
        var syncResult = _client!.DepositApi.SyncDepositRecords(0);
        if (syncResult.Data == null || syncResult.Data.Count == 0)
        {
          _output.WriteLine("无充值记录可查询");
          return;
        }

        var ids = new List<int>();
        foreach (var record in syncResult.Data)
        {
          if (record.Id.HasValue && ids.Count < 3)
          {
            ids.Add(record.Id.Value);
          }
        }

        if (ids.Count == 0)
        {
          _output.WriteLine("无有效 ID 可查询");
          return;
        }

        var result = _client!.DepositApi.GetDepositRecords(ids);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        _output.WriteLine($"Data Count: {result.Data?.Count ?? 0}");
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    #endregion

    #region Withdraw API 调试测试

    /// <summary>
    /// 调试测试: 同步提现记录
    /// </summary>
    [Fact]
    public void Debug_SyncWithdrawRecords()
    {
      SkipIfNotConfigured();
      PrintSeparator("同步提现记录", "/api/mpc/trans/sync/withdraw");

      try
      {
        var result = _client!.WithdrawApi.SyncWithdrawRecords(0);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        _output.WriteLine($"Data Count: {result.Data?.Count ?? 0}");

        if (result.Data != null && result.Data.Count > 0)
        {
          var first = result.Data[0];
          _output.WriteLine("");
          _output.WriteLine("首条数据字段:");
          _output.WriteLine($"  Id: {first.Id}");
          _output.WriteLine($"  RequestId: {first.RequestId}");
          _output.WriteLine($"  Symbol: {first.Symbol}");
          _output.WriteLine($"  Amount: {first.Amount}");
          _output.WriteLine($"  AddressTo: {first.AddressTo}");
          _output.WriteLine($"  AddressFrom: {first.AddressFrom}");
          _output.WriteLine($"  Txid: {first.Txid}");
          _output.WriteLine($"  Confirmations: {first.Confirmations}");
          _output.WriteLine($"  Status: {first.Status}");
          _output.WriteLine($"  SubWalletId: {first.SubWalletId}");
          _output.WriteLine($"  Fee: {first.Fee}");
          _output.WriteLine($"  FeeSymbol: {first.FeeSymbol}");
          _output.WriteLine($"  RealFee: {first.RealFee}");
          _output.WriteLine($"  CreatedAt: {first.CreatedAt}");
          _output.WriteLine($"  UpdatedAt: {first.UpdatedAt}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 获取提现记录 (按 request_id)
    /// </summary>
    [Fact]
    public void Debug_GetWithdrawRecords()
    {
      SkipIfNotConfigured();
      PrintSeparator("获取提现记录", "/api/mpc/trans/withdraw");

      try
      {
        // 先同步获取一些 request_id
        var syncResult = _client!.WithdrawApi.SyncWithdrawRecords(0);
        if (syncResult.Data == null || syncResult.Data.Count == 0)
        {
          _output.WriteLine("无提现记录可查询");
          return;
        }

        var requestIds = new List<string>();
        foreach (var record in syncResult.Data)
        {
          if (!string.IsNullOrEmpty(record.RequestId) && requestIds.Count < 3)
          {
            requestIds.Add(record.RequestId);
          }
        }

        if (requestIds.Count == 0)
        {
          _output.WriteLine("无有效 request_id 可查询");
          return;
        }

        var result = _client!.WithdrawApi.GetWithdrawRecords(requestIds);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        _output.WriteLine($"Data Count: {result.Data?.Count ?? 0}");
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    #endregion

    #region Auto Sweep API 调试测试

    /// <summary>
    /// 调试测试: 同步自动归集记录
    /// </summary>
    [Fact]
    public void Debug_SyncAutoCollectRecords()
    {
      SkipIfNotConfigured();
      PrintSeparator("同步自动归集记录", "/api/mpc/billing/sync_auto_collect_list");

      try
      {
        var result = _client!.AutoSweepApi.SyncAutoCollectRecords(0);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        _output.WriteLine($"Data Count: {result.Data?.Count ?? 0}");

        if (result.Data != null && result.Data.Count > 0)
        {
          var first = result.Data[0];
          _output.WriteLine("");
          _output.WriteLine("首条数据字段:");
          _output.WriteLine($"  Id: {first.Id}");
          _output.WriteLine($"  Symbol: {first.Symbol}");
          _output.WriteLine($"  Amount: {first.Amount}");
          _output.WriteLine($"  AddressFrom: {first.AddressFrom}");
          _output.WriteLine($"  AddressTo: {first.AddressTo}");
          _output.WriteLine($"  Txid: {first.Txid}");
          _output.WriteLine($"  Memo: {first.Memo}");
          _output.WriteLine($"  Remark: {first.Remark}");
          _output.WriteLine($"  Confirmations: {first.Confirmations}");
          _output.WriteLine($"  Status: {first.Status}");
          _output.WriteLine($"  SubWalletId: {first.SubWalletId}");
          _output.WriteLine($"  Fee: {first.Fee}");
          _output.WriteLine($"  FeeSymbol: {first.FeeSymbol}");
          _output.WriteLine($"  RealFee: {first.RealFee}");
          _output.WriteLine($"  CreatedAt: {first.CreatedAt}");
          _output.WriteLine($"  UpdatedAt: {first.UpdatedAt}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 获取自动归集钱包
    /// </summary>
    [Fact]
    public void Debug_AutoCollectSubWallets()
    {
      SkipIfNotConfigured();
      PrintSeparator("获取自动归集钱包", "/api/mpc/auto_collect/sub_wallets");

      try
      {
        var result = _client!.AutoSweepApi.AutoCollectSubWallets("USDTERC20");
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        if (result.Data != null)
        {
          _output.WriteLine($"FuelingSubWalletId: {result.Data.FuelingSubWalletId}");
          _output.WriteLine($"CollectSubWalletId: {result.Data.CollectSubWalletId}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    #endregion

    #region Web3 API 调试测试

    /// <summary>
    /// 调试测试: 同步 Web3 交易记录
    /// </summary>
    [Fact]
    public void Debug_SyncWeb3Records()
    {
      SkipIfNotConfigured();
      PrintSeparator("同步 Web3 交易记录", "/api/mpc/web3/trans/sync/list");

      try
      {
        var result = _client!.Web3Api.SyncWeb3Records(0);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        _output.WriteLine($"Data Count: {result.Data?.Count ?? 0}");

        if (result.Data != null && result.Data.Count > 0)
        {
          var first = result.Data[0];
          _output.WriteLine("");
          _output.WriteLine("首条数据字段:");
          _output.WriteLine($"  Id: {first.Id}");
          _output.WriteLine($"  RequestId: {first.RequestId}");
          _output.WriteLine($"  SubWalletId: {first.SubWalletId}");
          _output.WriteLine($"  MainChainSymbol: {first.MainChainSymbol}");
          _output.WriteLine($"  Symbol: {first.Symbol}");
          _output.WriteLine($"  InteractiveContract: {first.InteractiveContract}");
          _output.WriteLine($"  Amount: {first.Amount}");
          _output.WriteLine($"  GasPrice: {first.GasPrice}");
          _output.WriteLine($"  GasLimit: {first.GasLimit}");
          _output.WriteLine($"  GasUsed: {first.GasUsed}");
          _output.WriteLine($"  Txid: {first.Txid}");
          _output.WriteLine($"  AddressFrom: {first.AddressFrom}");
          _output.WriteLine($"  AddressTo: {first.AddressTo}");
          _output.WriteLine($"  Fee: {first.Fee}");
          _output.WriteLine($"  RealFee: {first.RealFee}");
          _output.WriteLine($"  FeeSymbol: {first.FeeSymbol}");
          _output.WriteLine($"  Status: {first.Status}");
          _output.WriteLine($"  TransType: {first.TransType}");
          _output.WriteLine($"  TransSource: {first.TransSource}");
          _output.WriteLine($"  Confirmations: {first.Confirmations}");
          _output.WriteLine($"  TxHeight: {first.TxHeight}");
          _output.WriteLine($"  Remark: {first.Remark}");
          _output.WriteLine($"  CreatedAt: {first.CreatedAt}");
          _output.WriteLine($"  UpdatedAt: {first.UpdatedAt}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 获取 Web3 交易记录 (按 request_id)
    /// </summary>
    [Fact]
    public void Debug_GetWeb3Records()
    {
      SkipIfNotConfigured();
      PrintSeparator("获取 Web3 交易记录", "/api/mpc/web3/trans/list");

      try
      {
        // 先同步获取一些 request_id
        var syncResult = _client!.Web3Api.SyncWeb3Records(0);
        if (syncResult.Data == null || syncResult.Data.Count == 0)
        {
          _output.WriteLine("无 Web3 交易记录可查询");
          return;
        }

        var requestIds = new List<string>();
        foreach (var record in syncResult.Data)
        {
          if (!string.IsNullOrEmpty(record.RequestId) && requestIds.Count < 3)
          {
            requestIds.Add(record.RequestId);
          }
        }

        if (requestIds.Count == 0)
        {
          _output.WriteLine("无有效 request_id 可查询");
          return;
        }

        var result = _client!.Web3Api.GetWeb3Records(requestIds);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        _output.WriteLine($"Data Count: {result.Data?.Count ?? 0}");
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    #endregion

    #region Tron Buy Resource API 调试测试

    /// <summary>
    /// 调试测试: 同步 TRON 资源购买记录
    /// </summary>
    [Fact]
    public void Debug_SyncBuyResourceRecords()
    {
      SkipIfNotConfigured();
      PrintSeparator("同步 TRON 资源购买记录", "/api/mpc/tron/buy_resource/sync/list");

      try
      {
        var result = _client!.TronBuyResourceApi.SyncBuyResourceRecords(0);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        _output.WriteLine($"Data Count: {result.Data?.Count ?? 0}");

        if (result.Data != null && result.Data.Count > 0)
        {
          var data = result.Data[0];
          _output.WriteLine("");
          _output.WriteLine("首条数据字段:");
          _output.WriteLine($"  Id: {data.Id}");
          _output.WriteLine($"  RequestId: {data.RequestId}");
          _output.WriteLine($"  BuyType: {data.BuyType}");
          _output.WriteLine($"  ResourceType: {data.ResourceType}");
          _output.WriteLine($"  ServiceChargeRate: {data.ServiceChargeRate}");
          _output.WriteLine($"  ServiceCharge: {data.ServiceCharge}");
          _output.WriteLine($"  EnergyNum: {data.EnergyNum}");
          _output.WriteLine($"  NetNum: {data.NetNum}");
          _output.WriteLine($"  AddressFrom: {data.AddressFrom}");
          _output.WriteLine($"  AddressTo: {data.AddressTo}");
          _output.WriteLine($"  ContractAddress: {data.ContractAddress}");
          _output.WriteLine($"  EnergyTxid: {data.EnergyTxid}");
          _output.WriteLine($"  NetTxid: {data.NetTxid}");
          _output.WriteLine($"  EnergyPrice: {data.EnergyPrice}");
          _output.WriteLine($"  NetPrice: {data.NetPrice}");
          _output.WriteLine($"  Status: {data.Status}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    /// <summary>
    /// 调试测试: 获取 TRON 资源购买记录 (按 request_id)
    /// </summary>
    [Fact]
    public void Debug_GetBuyResourceRecords()
    {
      SkipIfNotConfigured();
      PrintSeparator("获取 TRON 资源购买记录", "/api/mpc/tron/delegate/sync_trans_list");

      try
      {
        // 先同步获取一些 request_id
        var syncResult = _client!.TronBuyResourceApi.SyncBuyResourceRecords(0);
        if (syncResult.Data == null || syncResult.Data.Count == 0)
        {
          _output.WriteLine("无 TRON 资源购买记录可查询");
          return;
        }

        var requestIds = new List<string>();
        foreach (var record in syncResult.Data.Take(3))
        {
          if (!string.IsNullOrEmpty(record.RequestId))
          {
            requestIds.Add(record.RequestId);
          }
        }

        if (requestIds.Count == 0)
        {
          _output.WriteLine("无有效 request_id 可查询");
          return;
        }

        var result = _client!.TronBuyResourceApi.GetBuyResourceRecords(requestIds);
        PrintResult(result);

        _output.WriteLine("");
        _output.WriteLine("===== 字段核对 =====");
        _output.WriteLine($"Code: {result.Code}");
        _output.WriteLine($"Msg: {result.Msg}");
        _output.WriteLine($"Data Count: {result.Data?.Count ?? 0}");

        if (result.Data != null && result.Data.Count > 0)
        {
          var data = result.Data[0];
          _output.WriteLine("");
          _output.WriteLine("首条数据字段:");
          _output.WriteLine($"  Id: {data.Id}");
          _output.WriteLine($"  RequestId: {data.RequestId}");
          _output.WriteLine($"  BuyType: {data.BuyType}");
          _output.WriteLine($"  ResourceType: {data.ResourceType}");
          _output.WriteLine($"  ServiceChargeRate: {data.ServiceChargeRate}");
          _output.WriteLine($"  ServiceCharge: {data.ServiceCharge}");
          _output.WriteLine($"  EnergyNum: {data.EnergyNum}");
          _output.WriteLine($"  NetNum: {data.NetNum}");
          _output.WriteLine($"  AddressFrom: {data.AddressFrom}");
          _output.WriteLine($"  AddressTo: {data.AddressTo}");
          _output.WriteLine($"  ContractAddress: {data.ContractAddress}");
          _output.WriteLine($"  EnergyTxid: {data.EnergyTxid}");
          _output.WriteLine($"  NetTxid: {data.NetTxid}");
          _output.WriteLine($"  EnergyPrice: {data.EnergyPrice}");
          _output.WriteLine($"  NetPrice: {data.NetPrice}");
          _output.WriteLine($"  Status: {data.Status}");
        }
      }
      catch (Exception ex)
      {
        PrintError(ex);
        throw;
      }
    }

    #endregion
  }
}
