using api.github.hicoincom.enums;
using com.github.hicoincom.api.bean;
using com.github.hicoincom.api.bean.mpc;
using com.github.hicoincom.crypto;
using com.github.hicoincom.enums;

namespace com.github.hicoincom.api.mpc.impl
{
    public class WalletApi : WaasApi, IWalletApi
    {
        public WalletApi(WaasConfig config, IDataCrypto dataCrypto) : base(config, dataCrypto)
        {
        }

        public bool changeWalletShowStatus(List<int> walletIds, AppShowStatus? showStatus)
        {
            if (walletIds == null || !walletIds.Any()) {
                throw new ArgumentException("the request parameter 'sub_wallet_name' is required");
            }
            if (walletIds.Count > 50) {
                throw new ArgumentException("the length of parameter 'sub_wallet_name' cannot be greater than 50");
            }
            showStatus ??= AppShowStatus.HIDDEN;
            var args = new ChangeShowStatusArgs() {
                SubWalletIds = string.Join(",", walletIds),
                AppShowStatus = showStatus.Value
            };
            var result = Invoke<Result<object>>(MpcApiUri.CHANGE_SHOW_STATUS, args, typeof(Result<object>));
            return result != null && result.GetCode().Equals("0", StringComparison.OrdinalIgnoreCase);
        }

        public WalletResult createWallet(string walletName, AppShowStatus? showStatus)
        {
            if (string.IsNullOrEmpty(walletName)) {
                throw new ArgumentException("the request parameter 'sub_wallet_name' is required");
            }
            if (walletName.Length > 50) {
                throw new ArgumentException("the length of parameter 'sub_wallet_name' cannot be greater than 50");
            }
            showStatus ??= AppShowStatus.HIDDEN;
            var args = new CreateWalletArgs {
                SubWalletName = walletName,
                AppShowStatus = showStatus.Value
            };
            return Invoke<WalletResult>(MpcApiUri.CREATE_WALLET, args, typeof(WalletResult));
        }

        public WalletAddressResult createWalletAddress(int? walletId, string symbol)
        {
            if (walletId == null) {
                throw new ArgumentException("the request parameter 'sub_wallet_id' is required");
            }

            if (string.IsNullOrEmpty(symbol)) {
                throw new ArgumentException("the request parameter 'symbol' is required");
            }

            var args = new CreateWalletAddressArgs {
                SubWalletId = walletId,
                Symbol = symbol
            };

            return Invoke<WalletAddressResult>(MpcApiUri.CREATE_WALLET_ADDRESS, args, typeof(WalletAddressResult));
        }

        public WalletAssetsResult getWalletAssets(int? walletId, string symbol)
        {
            if (walletId == null) {
                throw new ArgumentException("the request parameter 'sub_wallet_id' is required");
            }
            if (string.IsNullOrEmpty(symbol)) {
                throw new ArgumentException("the request parameter 'symbol' is required");
            }
            var args = new GetWalletAssetsArgs {
                SubWalletId = walletId,
                Symbol = symbol
            };
            return Invoke<WalletAssetsResult>(MpcApiUri.GET_WALLET_ASSETS, args, typeof(WalletAssetsResult));
        }

        public WalletAddressListResult queryWalletAddress(int? walletId, string symbol, int? maxId)
        {
            if (walletId == null) {
                throw new ArgumentException("the request parameter 'sub_wallet_id' is required");
            }
            if (string.IsNullOrEmpty(symbol)) {
                throw new ArgumentException("the request parameter 'symbol' is required");
            }
            var args = new QueryAddressArgs {
                SubWalletId = walletId,
                Symbol = symbol,
                MaxId = maxId ?? 0
            };
            return Invoke<WalletAddressListResult>(MpcApiUri.GET_ADDRESS_LIST, args, typeof(WalletAddressListResult));
        }

        public WalletAddressInfoResult walletAddressInfo(string address, string memo)
        {
            if (string.IsNullOrEmpty(address)) {
                throw new ArgumentException("the request parameter 'address' is required");
            }
            var args = new GetAddressInfoArgs {
                Address = address,
                Memo = memo
            };
            return Invoke<WalletAddressInfoResult>(MpcApiUri.ADDRESS_INFO, args, typeof(WalletAddressInfoResult));
        }
    }
}