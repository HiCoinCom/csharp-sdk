using com.github.hicoincom;
using com.github.hicoincom.api.bean.mpc;
using Newtonsoft.Json;

namespace com.github.hicoincom
{
    public class Test01
    {
        public static void Main(string[] args)
        {
            string privateKeyBase64 = "";
            string publicKeyBase64 = "";

            MpcConfig cfg = new MpcConfig();
            cfg.AppId = "";
            cfg.UserPrivateKey = privateKeyBase64;
            cfg.WaasPublickKey = publicKeyBase64;
            cfg.SignPrivateKey = privateKeyBase64;
            cfg.EnableLog = true;
            MpcClient client = WaasClientFactory.CreateMpcClient(cfg);

            WithdrawArgs withdrawArgs = new WithdrawArgs
            {
                SubWalletId = 1004697,
                Symbol = "TRX",
                Time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                AddressTo = "TKpMts9nfG9Mg83NeKL73sGcFmD8YWevkD",
                Memo = "",
                Amount = "100.222",
                RequestId = new Random().Next().ToString(),
                Remark = "aaa"
            };
            var withdrawResult = client.WithdrawApi.withdraw(withdrawArgs, true);
            Console.WriteLine(JsonConvert.SerializeObject(withdrawResult));

            // CoinDetailsResult coinDetailsResult = client.WorkSpaceApi.getCoinDetails("", "HECO", true);
            // Console.WriteLine(JsonConvert.SerializeObject(coinDetailsResult));
            //
            // DepositRecordResult depositRecordResult = client.DepositApi.syncDepositRecords(0);
            // Console.WriteLine(JsonConvert.SerializeObject(depositRecordResult));
            //
            // var walletResult = client.WalletApi.createWallet("Test01", AppShowStatus.SHOW);
            // Console.WriteLine(JsonConvert.SerializeObject(walletResult));

            // bool changeWalletShowStatus = client.WalletApi.changeWalletShowStatus(new List<int>(){1004697}, AppShowStatus.SHOW);
            // Console.WriteLine(JsonConvert.SerializeObject(changeWalletShowStatus));

            // var withdrawRecordResult = client.WithdrawApi.syncWithdrawRecords(null);
            // Console.WriteLine(JsonConvert.SerializeObject(withdrawRecordResult));

            var walletAddressListResult = client.WalletApi.getWalletAssets(1004697, "TRX");
            Console.WriteLine(JsonConvert.SerializeObject(walletAddressListResult));
        }

        // public static void Main(string[] args)
        // {
        //     var rsa = RSA.Create();
        //     Console.WriteLine(Convert.ToBase64String(rsa.ExportPkcs8PrivateKey()));
        //     
        //     Console.WriteLine(Convert.ToBase64String(rsa.ExportSubjectPublicKeyInfo()));
        // }
    }
}