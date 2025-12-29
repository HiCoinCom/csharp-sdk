# ChainUp Custody C# SDK

[![NuGet](https://img.shields.io/nuget/v/ChainUpCustody.svg)](https://www.nuget.org/packages/ChainUpCustody)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

[English](#english) | [中文](#中文)

---

<a name="english"></a>

# English

ChainUp Custody C# SDK provides a complete set of APIs for interacting with ChainUp Custody services, supporting both WaaS (Wallet as a Service) and MPC (Multi-Party Computation) functionalities.

## Version

v1.0.0

## Requirements

- .NET 6.0 or higher

## Installation

### Via NuGet Package Manager

```bash
dotnet add package ChainUpCustody
```

### Build from Source

```bash
git clone https://github.com/AntChainOpenLabs/csharp-sdk.git
cd csharp-sdk
dotnet build
```

## Quick Start

### Environment Variables

Set the following environment variables before using the SDK:

```bash
# WaaS Configuration
export WAAS_APP_ID="your_app_id"
export WAAS_USER_PRIVATE_KEY="your_rsa_private_key"
export WAAS_PUBLIC_KEY="chainup_waas_public_key"

# MPC Configuration (optional)
export MPC_APP_ID="your_mpc_app_id"
export MPC_USER_PRIVATE_KEY="your_mpc_private_key"
export MPC_PUBLIC_KEY="chainup_mpc_public_key"
export MPC_SIGN_PRIVATE_KEY="your_sign_private_key"
```

### WaaS Client Usage

```csharp
using ChainUpCustody.Client;
using ChainUpCustody.Api.Models.Waas;

// Create WaaS client using Builder pattern
var client = WaasClient.Builder()
    .SetAppId("your_app_id")
    .SetUserPrivateKey("your_private_key")
    .SetWaasPublicKey("waas_public_key")
    .EnableLog(true)
    .Build();
```

#### User Management

```csharp
// Register user by email
var result = client.UserApi.RegisterEmailUser("user@example.com");
Console.WriteLine($"User UID: {result.Data?.Uid}");

// Register user by mobile
var mobileResult = client.UserApi.RegisterMobileUser("86", "13800138000");

// Get user info
var userInfo = client.UserApi.GetEmailUser("user@example.com");

// Sync user list
var userList = client.UserApi.SyncUserList(maxId: 0);
```

#### Account Management

```csharp
// Get user account balance
var account = client.AccountApi.GetUserAccount(uid: 12345, symbol: "ETH");
Console.WriteLine($"Balance: {account.Data?.NormalBalance}");

// Get user deposit address
var address = client.AccountApi.GetUserAddress(uid: 12345, symbol: "ETH");
Console.WriteLine($"Address: {address.Data?.Address}");

// Get company account balance
var companyAccount = client.AccountApi.GetCompanyAccount(symbol: "ETH");

// Sync user address list
var addressList = client.AccountApi.SyncUserAddressList(maxId: 0);
```

#### Coin Management

```csharp
// Get supported coin list
var coins = client.CoinApi.GetCoinList();
foreach (var coin in coins.Data ?? new List<CoinInfo>())
{
    Console.WriteLine($"Symbol: {coin.Symbol}, Network: {coin.CoinNet}");
}
```

#### Billing (Deposit/Withdraw)

```csharp
// Submit withdrawal request
var withdrawArgs = new WithdrawArgs
{
    RequestId = Guid.NewGuid().ToString(),
    FromUid = 12345,
    ToAddress = "0x1234567890abcdef...",
    Amount = 1.5m,
    Symbol = "ETH"
};
var withdrawResult = client.BillingApi.Withdraw(withdrawArgs);

// Query withdrawal records by request_id
var withdrawRecords = client.BillingApi.WithdrawList(new List<string> { "request_id_1" });

// Sync withdrawal records
var syncWithdraw = client.BillingApi.SyncWithdrawList(maxId: 0);

// Sync deposit records
var syncDeposit = client.BillingApi.SyncDepositList(maxId: 0);

// Sync miner fee records
var minerFees = client.BillingApi.SyncMinerFeeList(maxId: 0);
```

#### Internal Transfer

```csharp
// Account transfer between users
var transferArgs = new TransferArgs
{
    RequestId = Guid.NewGuid().ToString(),
    Symbol = "USDT",
    Amount = 100.0m,
    To = "67890",  // Target UID
    Remark = "Transfer from C# SDK"
};
var transferResult = client.TransferApi.AccountTransfer(transferArgs);

// Query transfer records
var transferRecords = client.TransferApi.GetAccountTransferList("request_id", ITransferApi.REQUEST_ID);

// Sync transfer records
var syncTransfer = client.TransferApi.SyncAccountTransferList(maxId: 0);
```

#### Async Notification Handling

```csharp
// Decrypt notification data from webhook
var encryptedData = "..."; // Encrypted data from ChainUp webhook
var notifyData = client.AsyncNotifyApi.NotifyRequest(encryptedData);

if (notifyData != null)
{
    Console.WriteLine($"Side: {notifyData.Side}");  // "deposit" or "withdraw"
    Console.WriteLine($"ID: {notifyData.Id}");
    Console.WriteLine($"Amount: {notifyData.Amount}");
    Console.WriteLine($"Status: {notifyData.Status}");
}

// Decrypt withdrawal verification request
var withdrawRequest = client.AsyncNotifyApi.VerifyRequest(encryptedData);

// Encrypt withdrawal verification response
var response = client.AsyncNotifyApi.VerifyResponse(withdrawArgs);
```

### MPC Client Usage

```csharp
using ChainUpCustody.Client;
using ChainUpCustody.Enums;

// Create MPC client
var mpcClient = MpcClient.Builder()
    .SetAppId("your_app_id")
    .SetUserPrivateKey("your_private_key")
    .SetWaasPublicKey("waas_public_key")
    .SetSignPrivateKey("your_sign_private_key")  // Optional, for transaction signing
    .EnableLog(true)
    .Build();
```

#### Workspace Management

```csharp
// Get supported main chains
var chains = mpcClient.WorkSpaceApi.GetSupportMainChain();

// Get coin details
var coinDetails = mpcClient.WorkSpaceApi.GetCoinDetails("ETH");

// Get last block height
var blockHeight = mpcClient.WorkSpaceApi.GetLastBlockHeight("ETH");
```

#### Wallet Management

```csharp
// Create wallet
var wallet = mpcClient.WalletApi.CreateWallet("MyWallet", AppShowStatus.Show);

// Create wallet address
var address = mpcClient.WalletApi.CreateWalletAddress(
    subWalletId: wallet.Data.SubWalletId,
    symbol: "ETH"
);

// Get address list
var addresses = mpcClient.WalletApi.GetAddressList(
    subWalletId: wallet.Data.SubWalletId,
    symbol: "ETH"
);
```

#### Withdrawal

```csharp
// Submit withdrawal (without signing)
var withdrawResult = mpcClient.WithdrawApi.Withdraw(withdrawArgs);

// Submit withdrawal (with signing - requires SignPrivateKey)
var withdrawResult2 = mpcClient.WithdrawApi.Withdraw(withdrawArgs, needTransactionSign: true);
```

## API Reference

### WaaS APIs

| API Interface   | Description                            |
| --------------- | -------------------------------------- |
| IUserApi        | User management (register, query)      |
| IAccountApi     | Account management (balance, address)  |
| ICoinApi        | Coin information                       |
| IBillingApi     | Billing (withdraw, deposit, miner fee) |
| ITransferApi    | Internal transfer                      |
| IAsyncNotifyApi | Async notification decryption          |

### MPC APIs

| API Interface       | Description                             |
| ------------------- | --------------------------------------- |
| IWorkSpaceApi       | Workspace (chains, coins, block height) |
| IWalletApi          | Wallet management                       |
| IDepositApi         | Deposit records                         |
| IWithdrawApi        | Withdrawal management                   |
| IWeb3Api            | Web3 transactions                       |
| IAutoSweepApi       | Auto sweep                              |
| ITronBuyResourceApi | Tron resource purchase                  |
| INotifyApi          | Notification decryption                 |

## RSA Key Generation

```csharp
using ChainUpCustody.Crypto;

var (privateKey, publicKey) = RsaHelper.GenerateKeys();
Console.WriteLine($"Private Key: {privateKey}");
Console.WriteLine($"Public Key: {publicKey}");
```

## Running Tests

```bash
cd tests/ChainUpCustody.Tests
dotnet test
```

## License

MIT License

---

<a name="中文"></a>

# 中文

ChainUp Custody C# SDK 提供了一套完整的 API，用于与 ChainUp Custody 服务交互，支持 WaaS（钱包即服务）和 MPC（多方计算）功能。

## 版本

v1.0.0

## 系统要求

- .NET 6.0 或更高版本

## 安装

### 从源代码构建

```bash
git clone https://github.com/AntChainOpenLabs/csharp-sdk.git
cd csharp-sdk
dotnet build
```

## 快速开始

### 环境变量配置

使用 SDK 前，请设置以下环境变量：

```bash
# WaaS 配置
export WAAS_APP_ID="您的应用ID"
export WAAS_USER_PRIVATE_KEY="您的RSA私钥"
export WAAS_PUBLIC_KEY="ChainUp WaaS公钥"

# MPC 配置（可选）
export MPC_APP_ID="您的MPC应用ID"
export MPC_USER_PRIVATE_KEY="您的MPC私钥"
export MPC_PUBLIC_KEY="ChainUp MPC公钥"
export MPC_SIGN_PRIVATE_KEY="您的签名私钥"
```

### WaaS 客户端使用

```csharp
using ChainUpCustody.Client;
using ChainUpCustody.Api.Models.Waas;

// 使用 Builder 模式创建 WaaS 客户端
var client = WaasClient.Builder()
    .SetAppId("您的应用ID")
    .SetUserPrivateKey("您的私钥")
    .SetWaasPublicKey("WaaS公钥")
    .EnableLog(true)
    .Build();
```

#### 用户管理

```csharp
// 通过邮箱注册用户
var result = client.UserApi.RegisterEmailUser("user@example.com");
Console.WriteLine($"用户 UID: {result.Data?.Uid}");

// 通过手机号注册用户
var mobileResult = client.UserApi.RegisterMobileUser("86", "13800138000");

// 获取用户信息
var userInfo = client.UserApi.GetEmailUser("user@example.com");

// 同步用户列表
var userList = client.UserApi.SyncUserList(maxId: 0);
```

#### 账户管理

```csharp
// 获取用户账户余额
var account = client.AccountApi.GetUserAccount(uid: 12345, symbol: "ETH");
Console.WriteLine($"余额: {account.Data?.NormalBalance}");

// 获取用户充值地址
var address = client.AccountApi.GetUserAddress(uid: 12345, symbol: "ETH");
Console.WriteLine($"地址: {address.Data?.Address}");

// 获取公司账户余额
var companyAccount = client.AccountApi.GetCompanyAccount(symbol: "ETH");

// 同步用户地址列表
var addressList = client.AccountApi.SyncUserAddressList(maxId: 0);
```

#### 币种管理

```csharp
// 获取支持的币种列表
var coins = client.CoinApi.GetCoinList();
foreach (var coin in coins.Data ?? new List<CoinInfo>())
{
    Console.WriteLine($"币种: {coin.Symbol}, 网络: {coin.CoinNet}");
}
```

#### 账单管理（充值/提现）

```csharp
// 发起提现请求
var withdrawArgs = new WithdrawArgs
{
    RequestId = Guid.NewGuid().ToString(),
    FromUid = 12345,
    ToAddress = "0x1234567890abcdef...",
    Amount = 1.5m,
    Symbol = "ETH"
};
var withdrawResult = client.BillingApi.Withdraw(withdrawArgs);

// 按 request_id 查询提现记录
var withdrawRecords = client.BillingApi.WithdrawList(new List<string> { "request_id_1" });

// 同步提现记录
var syncWithdraw = client.BillingApi.SyncWithdrawList(maxId: 0);

// 同步充值记录
var syncDeposit = client.BillingApi.SyncDepositList(maxId: 0);

// 同步矿工费记录
var minerFees = client.BillingApi.SyncMinerFeeList(maxId: 0);
```

#### 内部转账

```csharp
// 用户间账户转账
var transferArgs = new TransferArgs
{
    RequestId = Guid.NewGuid().ToString(),
    Symbol = "USDT",
    Amount = 100.0m,
    To = "67890",  // 目标用户 UID
    Remark = "C# SDK 转账测试"
};
var transferResult = client.TransferApi.AccountTransfer(transferArgs);

// 查询转账记录
var transferRecords = client.TransferApi.GetAccountTransferList("request_id", ITransferApi.REQUEST_ID);

// 同步转账记录
var syncTransfer = client.TransferApi.SyncAccountTransferList(maxId: 0);
```

#### 异步通知处理

```csharp
// 解密来自 webhook 的通知数据
var encryptedData = "..."; // ChainUp webhook 发送的加密数据
var notifyData = client.AsyncNotifyApi.NotifyRequest(encryptedData);

if (notifyData != null)
{
    Console.WriteLine($"类型: {notifyData.Side}");  // "deposit" 或 "withdraw"
    Console.WriteLine($"ID: {notifyData.Id}");
    Console.WriteLine($"金额: {notifyData.Amount}");
    Console.WriteLine($"状态: {notifyData.Status}");
}

// 解密提现二次验证请求
var withdrawRequest = client.AsyncNotifyApi.VerifyRequest(encryptedData);

// 加密提现验证响应
var response = client.AsyncNotifyApi.VerifyResponse(withdrawArgs);
```

### MPC 客户端使用

```csharp
using ChainUpCustody.Client;
using ChainUpCustody.Enums;

// 创建 MPC 客户端
var mpcClient = MpcClient.Builder()
    .SetAppId("您的应用ID")
    .SetUserPrivateKey("您的私钥")
    .SetWaasPublicKey("WaaS公钥")
    .SetSignPrivateKey("您的签名私钥")  // 可选，用于交易签名
    .EnableLog(true)
    .Build();
```

#### 工作空间管理

```csharp
// 获取支持的主链
var chains = mpcClient.WorkSpaceApi.GetSupportMainChain();

// 获取币种详情
var coinDetails = mpcClient.WorkSpaceApi.GetCoinDetails("ETH");

// 获取最新区块高度
var blockHeight = mpcClient.WorkSpaceApi.GetLastBlockHeight("ETH");
```

#### 钱包管理

```csharp
// 创建钱包
var wallet = mpcClient.WalletApi.CreateWallet("我的钱包", AppShowStatus.Show);

// 创建钱包地址
var address = mpcClient.WalletApi.CreateWalletAddress(
    subWalletId: wallet.Data.SubWalletId,
    symbol: "ETH"
);

// 获取地址列表
var addresses = mpcClient.WalletApi.GetAddressList(
    subWalletId: wallet.Data.SubWalletId,
    symbol: "ETH"
);
```

#### 提现

```csharp
// 发起提现（无需签名）
var withdrawResult = mpcClient.WithdrawApi.Withdraw(withdrawArgs);

// 发起提现（需要签名 - 需设置 SignPrivateKey）
var withdrawResult2 = mpcClient.WithdrawApi.Withdraw(withdrawArgs, needTransactionSign: true);
```

## API 参考

### WaaS API

| API 接口        | 描述                           |
| --------------- | ------------------------------ |
| IUserApi        | 用户管理（注册、查询）         |
| IAccountApi     | 账户管理（余额、地址）         |
| ICoinApi        | 币种信息                       |
| IBillingApi     | 账单管理（提现、充值、矿工费） |
| ITransferApi    | 内部转账                       |
| IAsyncNotifyApi | 异步通知解密                   |

### MPC API

| API 接口            | 描述                       |
| ------------------- | -------------------------- |
| IWorkSpaceApi       | 工作空间（链、币种、区块） |
| IWalletApi          | 钱包管理                   |
| IDepositApi         | 充值记录                   |
| IWithdrawApi        | 提现管理                   |
| IWeb3Api            | Web3 交易                  |
| IAutoSweepApi       | 自动归集                   |
| ITronBuyResourceApi | Tron 资源购买              |
| INotifyApi          | 通知解密                   |

## RSA 密钥生成

```csharp
using ChainUpCustody.Crypto;

var (privateKey, publicKey) = RsaHelper.GenerateKeys();
Console.WriteLine($"私钥: {privateKey}");
Console.WriteLine($"公钥: {publicKey}");
```

## 运行测试

```bash
cd tests/ChainUpCustody.Tests
dotnet test
```

## 许可证

MIT License
