namespace com.github.hicoincom.enums
{

    public class MpcApiUri
    {
        public string Value { get; set; }
        public string Method { get; set; }

        public MpcApiUri(string value, string method)
        {
            this.Value = value;
            this.Method = method;
        }

        public static readonly MpcApiUri SUPPORT_MAIN_CHAIN = new MpcApiUri("/mpc/wallet/open_coin", "GET");
        public static readonly MpcApiUri COIN_DETAILS = new MpcApiUri("/mpc/coin_list", "GET");
        public static readonly MpcApiUri CHAIN_HEIGHT = new MpcApiUri("/mpc/chain_height", "GET");
        public static readonly MpcApiUri CREATE_WALLET = new MpcApiUri("/mpc/sub_wallet/create", "POST");
        public static readonly MpcApiUri CREATE_WALLET_ADDRESS = new MpcApiUri("/mpc/sub_wallet/create/address", "POST");
        public static readonly MpcApiUri GET_ADDRESS_LIST = new MpcApiUri("/mpc/sub_wallet/get/address/list", "POST");
        public static readonly MpcApiUri GET_WALLET_ASSETS = new MpcApiUri("/mpc/sub_wallet/assets", "GET");
        public static readonly MpcApiUri CHANGE_SHOW_STATUS = new MpcApiUri("/mpc/sub_wallet/change_show_status", "POST");
        public static readonly MpcApiUri ADDRESS_INFO = new MpcApiUri("/mpc/sub_wallet/address/info", "GET");
        public static readonly MpcApiUri BILLING_WITHDRAW = new MpcApiUri("/mpc/billing/withdraw", "POST");
        public static readonly MpcApiUri WITHDRAW_RECORDS = new MpcApiUri("/mpc/billing/withdraw_list", "GET");
        public static readonly MpcApiUri SYNC_WITHDRAW_RECORDS = new MpcApiUri("/mpc/billing/sync_withdraw_list", "GET");
        public static readonly MpcApiUri DEPOSIT_RECORDS = new MpcApiUri("/mpc/billing/deposit_list", "POST");
        public static readonly MpcApiUri SYNC_DEPOSIT_RECORDS = new MpcApiUri("/mpc/billing/sync_deposit_list", "GET");
        public static readonly MpcApiUri CREATE_WEB3_TRANSACTION = new MpcApiUri("/mpc/web3/trans/create", "POST");
        public static readonly MpcApiUri WEB3_TRANS_ACCELERATION = new MpcApiUri("/mpc/web3/pending", "POST");
        public static readonly MpcApiUri WEB3_TRANS_RECORDS = new MpcApiUri("/mpc/web3/trans_list", "GET");
        public static readonly MpcApiUri SYNC_WEB3_RECORDS = new MpcApiUri("/mpc/web3/sync_trans_list", "GET");
        public static readonly MpcApiUri AUTO_COLLECT_WALLETS = new MpcApiUri("/mpc/auto_collect/sub_wallets", "GET");
        public static readonly MpcApiUri SET_AUTO_COLLECT_SYMBOL = new MpcApiUri("/mpc/billing/auto_collect/symbol/set", "POST");
        public static readonly MpcApiUri SYNC_AUTO_SWEEP_RECORDS = new MpcApiUri("/mpc/billing/sync_auto_collect_list", "GET");

        // 迭代器
        public static IEnumerable<MpcApiUri> Values()
        {
            return new[]
            {
                SUPPORT_MAIN_CHAIN,
                COIN_DETAILS,
                CHAIN_HEIGHT,
                CREATE_WALLET,
                CREATE_WALLET_ADDRESS,
                GET_ADDRESS_LIST,
                GET_WALLET_ASSETS,
                CHANGE_SHOW_STATUS,
                ADDRESS_INFO,
                BILLING_WITHDRAW,
                WITHDRAW_RECORDS,
                SYNC_WITHDRAW_RECORDS,
                DEPOSIT_RECORDS,
                CREATE_WEB3_TRANSACTION,
                WEB3_TRANS_ACCELERATION,
                WEB3_TRANS_RECORDS,
                SYNC_WEB3_RECORDS,
                AUTO_COLLECT_WALLETS,
                SET_AUTO_COLLECT_SYMBOL,
                SYNC_AUTO_SWEEP_RECORDS
            };
        }

        public static IEnumerable<MpcApiUri> findByMethod(string name)
        {
            foreach (var item in Values())
            {
                if (item.Method == name)
                {
                    yield return item;
                }
            }
        }

        public override string ToString()
        {
            return $"{Value} ({Method})";
        }
    }
}