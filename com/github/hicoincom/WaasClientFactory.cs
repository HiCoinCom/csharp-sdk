using System.Reflection.Metadata;
using com.github.hicoincom.api.mpc.impl;
using com.github.hicoincom.crypto;

namespace com.github.hicoincom{
    public class WaasClientFactory
    {
        public static MpcClient CreateMpcClient(WaasConfig config)
        {
            DataCrypto dataCrypto = new DataCrypto(config.UserPrivateKey, config.WaasPublickKey);
            return CreateMpcClient(config, dataCrypto);
        }

        public static MpcClient CreateMpcClient(WaasConfig config, IDataCrypto dataCrypto)
        {
            return new MpcClient
            {
                WorkSpaceApi = new WorkSpaceApi(config, dataCrypto),
                WalletApi = new WalletApi(config, dataCrypto),
                WithdrawApi = new WithdrawApi(config, dataCrypto),
                AutoSweepApi = new AutoSweepApi(config, dataCrypto),
                Web3Api = new Web3Api(config, dataCrypto),
                DepositApi = new DepositApi(config, dataCrypto)
            };
        }
    }
}