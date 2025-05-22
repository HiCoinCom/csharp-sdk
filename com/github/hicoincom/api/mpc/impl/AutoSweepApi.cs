using com.github.hicoincom.api.bean.mpc;
using com.github.hicoincom.crypto;
using com.github.hicoincom.enums;
using com.github.hicoincom.exception;

namespace com.github.hicoincom.api.mpc.impl
{
    public class AutoSweepApi : WaasApi, IAutoSweepApi
    {

        public AutoSweepApi(WaasConfig config, IDataCrypto dataCrypto) : base(config, dataCrypto)
        {
        }

        public AutoCollectWalletsResult AutoCollectSubWallets(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgsNullException("The request parameter 'symbol' is empty.");
            }

            var args = new GetAutoCollectWalletArgs
            {
                Symbol = symbol
            };
            return Invoke<AutoCollectWalletsResult>(MpcApiUri.AUTO_COLLECT_WALLETS, args, typeof(AutoCollectWalletsResult));
        }

        public AutoCollectWalletsResult autoCollectSubWallets(string symbol)
        {
            throw new NotImplementedException();
        }

        public SetAutoCollectSymbolResult setAutoCollectSymbol(string symbol, string collectMin, string fuelingLimit)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgsNullException("The request parameter 'symbol' is empty.");
            }

            if (string.IsNullOrWhiteSpace(collectMin))
            {
                throw new ArgsNullException("The request parameter 'collect_min' is empty.");
            }

            if (string.IsNullOrWhiteSpace(fuelingLimit))
            {
                throw new ArgsNullException("The request parameter 'fueling_limit' is empty.");
            }
            var args = new SetAutoCollectSymbolArgs
            {
                Symbol = symbol,
                CollectMin = collectMin,
                FuelingLimit = fuelingLimit
            };

            return Invoke<SetAutoCollectSymbolResult>(MpcApiUri.SET_AUTO_COLLECT_SYMBOL, args, typeof(SetAutoCollectSymbolResult));
        }

        public AutoCollectRecordResult syncAutoCollectRecords(int? maxId)
        {
            var args = new SyncTransactionRecordArgs
            {
                MaxId = maxId ?? 0
            };
            return Invoke<AutoCollectRecordResult>(MpcApiUri.SYNC_AUTO_SWEEP_RECORDS, args, typeof(AutoCollectRecordResult));
        }
    }
}