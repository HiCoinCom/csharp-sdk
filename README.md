# ChainUp Custody C# SDK

ChainUp Custody C# SDK 是基于 [Java SDK](https://github.com/HiCoinCom/java-sdk) 移植的 C# 版本，提供与 ChainUp Custody 服务交互的完整功能。

## 版本

v2.2.0

## 系统要求

- .NET 6.0 或更高版本

## 安装

### 通过 NuGet 包管理器

```bash
dotnet add package ChainUpCustody
```

### 从源代码构建

```bash
cd src/ChainUpCustody
dotnet build
```

## 快速开始

### WaaS 客户端使用

```csharp
using ChainUpCustody.Client;
using ChainUpCustody.Api.Bean.Waas;

// 使用 Builder 模式创建客户端
var waasClient = WaasClient.Builder()
    .SetAppId("your_app_id")
    .SetUserPrivateKey("your_private_key")
    .SetWaasPublicKey("waas_public_key")
    .EnableLog(true)
    .Build();

// 创建用户
var registerArgs = new RegisterArgs
{
    Country = "US",
    Email = "user@example.com"
};
var result = waasClient.UserApi.RegisterEmailUser(registerArgs);
Console.WriteLine($"User created: {result.Code}");

// 获取币种列表
var coinResult = waasClient.CoinApi.GetCoinList();

// 提币
var withdrawArgs = new WithdrawArgs
{
    RequestId = Guid.NewGuid().ToString(),
    Symbol = "ETH",
    To = "0x...",
    Amount = "1.0"
};
var withdrawResult = waasClient.BillingApi.Withdraw(withdrawArgs);

// 内部转账
var transferArgs = new TransferArgs
{
    RequestId = Guid.NewGuid().ToString(),
    Symbol = "ETH",
    FromUid = "user1",
    ToUid = "user2",
    Amount = "1.0"
};
var transferResult = waasClient.TransferApi.AccountTransfer(transferArgs);
```

### MPC 客户端使用

```csharp
using ChainUpCustody.Client;
using ChainUpCustody.Api.Bean.Mpc;
using ChainUpCustody.Enums;

// 使用 Builder 模式创建客户端
var mpcClient = MpcClient.Builder()
    .SetAppId("your_app_id")
    .SetUserPrivateKey("your_private_key")
    .SetWaasPublicKey("waas_public_key")
    .SetSignPrivateKey("your_sign_private_key") // 可选，用于交易签名
    .EnableLog(true)
    .Build();

// 获取支持的主链
var chainResult = mpcClient.WorkSpaceApi.GetSupportMainChain();

// 获取币种详情
var coinResult = mpcClient.WorkSpaceApi.GetCoinDetails("ETH");

// 创建钱包
var walletResult = mpcClient.WalletApi.CreateWallet("MyWallet", AppShowStatus.Show);

// 创建钱包地址
var addressResult = mpcClient.WalletApi.CreateWalletAddress(
    subWalletId: walletResult.Data.SubWalletId,
    symbol: "ETH"
);

// 获取地址列表
var addressListResult = mpcClient.WalletApi.GetAddressList(
    subWalletId: walletResult.Data.SubWalletId,
    symbol: "ETH"
);

// 提币（不需要签名）
var withdrawArgs = new MpcWithdrawArgs
{
    RequestId = Guid.NewGuid().ToString(),
    SubWalletId = walletResult.Data.SubWalletId,
    Symbol = "ETH",
    AddressTo = "0x...",
    Amount = "1.0"
};
var withdrawResult = mpcClient.WithdrawApi.Withdraw(withdrawArgs);

// 提币（需要签名 - 设置 needTransactionSign 为 true）
// 需要在创建客户端时设置 SignPrivateKey
var withdrawResult2 = mpcClient.WithdrawApi.Withdraw(withdrawArgs, needTransactionSign: true);

// 创建 Web3 交易
var web3Args = new CreateWeb3Args
{
    RequestId = Guid.NewGuid().ToString(),
    SubWalletId = walletResult.Data.SubWalletId,
    MainChainSymbol = "ETH",
    InteractiveContract = "0x...",
    Amount = "0",
    InputData = "0x..."
};
var web3Result = mpcClient.Web3Api.CreateWeb3Trans(web3Args);

// 创建 Web3 交易（需要签名）
var web3Result2 = mpcClient.Web3Api.CreateWeb3Trans(web3Args, needTransactionSign: true);

// 获取充值记录
var depositRecords = mpcClient.DepositApi.GetDepositRecords(new List<int> { 1, 2, 3 });

// 同步提现记录
var withdrawRecords = mpcClient.WithdrawApi.SyncWithdrawRecords(maxId: 0);
```

### 使用工厂方法

```csharp
using ChainUpCustody.Client;

// 创建 WaaS 客户端
var waasClient = WaasClientFactory.CreateWaasClient(
    appId: "your_app_id",
    userPrivateKey: "your_private_key",
    waasPublicKey: "waas_public_key",
    enableLog: true
);

// 创建 MPC 客户端
var mpcClient = WaasClientFactory.CreateMpcClient(
    appId: "your_app_id",
    userPrivateKey: "your_private_key",
    waasPublicKey: "waas_public_key",
    signPrivateKey: "your_sign_private_key"
);
```

## 项目结构

```
ChainUpCustody/
├── Api/
│   ├── Bean/                # 请求/响应模型
│   │   ├── Waas/            # WaaS Bean 类
│   │   └── Mpc/             # MPC Bean 类
│   ├── Mpc/                 # MPC API 接口
│   │   └── Impl/            # MPC API 实现
│   ├── Waas/                # WaaS API 接口
│   │   └── Impl/            # WaaS API 实现
│   ├── MpcApiBase.cs        # MPC API 基类
│   └── WaasApiBase.cs       # WaaS API 基类
├── Client/
│   ├── MpcClient.cs         # MPC 客户端
│   ├── WaasClient.cs        # WaaS 客户端
│   └── WaasClientFactory.cs # 客户端工厂
├── Config/
│   ├── MpcConfig.cs         # MPC 配置
│   └── WaasConfig.cs        # WaaS 配置
├── Crypto/
│   ├── DataCrypto.cs        # 数据加解密
│   ├── IDataCrypto.cs       # 加解密接口
│   └── RsaHelper.cs         # RSA 工具类
├── Enums/
│   ├── ApiUri.cs            # WaaS API 端点枚举
│   ├── AppShowStatus.cs
│   └── MpcApiUri.cs         # MPC API 端点枚举
└── Utils/
    └── HttpClientUtil.cs    # HTTP 工具类
```

## API 列表

### WaaS API

| API 接口        | 描述                               |
| --------------- | ---------------------------------- |
| IUserApi        | 用户管理（注册用户、查询用户信息） |
| IAccountApi     | 账户管理（查询余额、获取地址）     |
| ICoinApi        | 币种查询                           |
| IBillingApi     | 账单管理（提现、充值、矿工费记录） |
| ITransferApi    | 内部转账（商户间转账）             |
| IAsyncNotifyApi | 异步通知解密                       |

### MPC API

| API 接口            | 描述                             |
| ------------------- | -------------------------------- |
| IWorkSpaceApi       | 工作空间（主链、币种、区块高度） |
| IWalletApi          | 钱包管理（创建钱包、地址管理）   |
| IDepositApi         | 充值记录                         |
| IWithdrawApi        | 提现管理                         |
| IWeb3Api            | Web3 交易                        |
| IAutoSweepApi       | 自动归集                         |
| ITronBuyResourceApi | Tron 资源购买                    |
| INotifyApi          | 通知解密                         |

## RSA 密钥生成

```csharp
using ChainUpCustody.Crypto;

// 生成 RSA 密钥对
var (privateKey, publicKey) = RsaHelper.GenerateKeys();
Console.WriteLine("Private Key:");
Console.WriteLine(privateKey);
Console.WriteLine("Public Key:");
Console.WriteLine(publicKey);
```

## 运行测试

```bash
cd tests/ChainUpCustody.Tests
dotnet test
```

## 日志记录

SDK 支持通过 Microsoft.Extensions.Logging 进行日志记录：

```csharp
using Microsoft.Extensions.Logging;

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Debug);
});

var logger = loggerFactory.CreateLogger<WaasClient>();

var client = WaasClient.Builder()
    .SetAppId("your_app_id")
    .SetUserPrivateKey("your_private_key")
    .SetWaasPublicKey("waas_public_key")
    .SetLogger(logger)
    .EnableLog(true)
    .Build();
```

## 错误处理

```csharp
var result = await waasClient.BillingApi.WithdrawAsync(...);

if (result.IsSuccess)
{
    // 处理成功响应
    Console.WriteLine($"Success: {result.Data}");
}
else
{
    // 处理错误
    Console.WriteLine($"Error {result.Code}: {result.Msg}");
}
```

## 许可证

MIT License
