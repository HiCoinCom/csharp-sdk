using com.github.hicoincom.api.bean;
using com.github.hicoincom.api.bean.mpc;
using com.github.hicoincom.crypto;
using com.github.hicoincom.enums;
using com.github.hicoincom.exception;

namespace com.github.hicoincom.api.mpc.impl
{
    public class WorkSpaceApi : WaasApi, IWorkSpaceApi
    {
        public WorkSpaceApi(WaasConfig config, IDataCrypto dataCrypto) : base(config, dataCrypto)
        {
            Config = config;
            DataCrypto = dataCrypto;
        }

        public CoinDetailsResult getCoinDetails(string symbol, string baseSymbol, bool openChain)
        {
            var args = new GetCoinDetailsArgs()
            {
                Symbol = symbol,
                BaseSymbol = baseSymbol,
                OpenChain = openChain
            };
            return Invoke<CoinDetailsResult>(MpcApiUri.COIN_DETAILS, args, typeof(CoinDetailsResult));
        }

        public GetLastBlockHeightResult getLastBlockHeight(string baseSymbol)
        {
            if (string.IsNullOrEmpty(baseSymbol))
            {
                throw new ArgsNullException("get last block height, the request parameter 'bas_sSymbol' empty");
            }
            var args = new GetLastBlockHeightArgs()
            {
                BaseSymbol = baseSymbol
            };
            return Invoke<GetLastBlockHeightResult>(MpcApiUri.CHAIN_HEIGHT, args, typeof(GetLastBlockHeightResult));
        }

        public SupportMainChainResult getSupportMainChain()
        {
            return Invoke<SupportMainChainResult>(MpcApiUri.SUPPORT_MAIN_CHAIN, new BaseArgs(), typeof(SupportMainChainResult));
        }
    }
}