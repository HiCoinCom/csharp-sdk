using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    /// <summary>
    /// Represents the arguments for creating a Web3 transaction.
    /// API Reference: https://custodydocs-en.chainup.com/api-references/mpc-apis/apis/web3/web3-create
    /// </summary>
    [Serializable]
    public class CreateWeb3Args : BaseArgs
    {
        /// <summary>
        /// Wallet ID (required).
        /// </summary>
        [JsonProperty("sub_wallet_id")]
        public int? SubWalletId { get; set; }

        /// <summary>
        /// The unique identifier for transferring coins (required).
        /// </summary>
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        /// <summary>
        /// Main chain coin symbol, unique identifier for the coin, e.g., ETH (required).
        /// </summary>
        [JsonProperty("main_chain_symbol")]
        public string MainChainSymbol { get; set; }

        /// <summary>
        /// Transaction initiation address (optional).
        /// If not specified, the wallet’s “commonly used address” will be used by default.
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        /// Interactive contract (required).
        /// </summary>
        [JsonProperty("interactive_contract")]
        public string InteractiveContract { get; set; }

        /// <summary>
        /// Transfer amount (required).
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// Gas fee in Gwei (required).
        /// </summary>
        [JsonProperty("gas_price")]
        public string GasPrice { get; set; }

        /// <summary>
        /// Gas limit fee (required).
        /// </summary>
        [JsonProperty("gas_limit")]
        public string GasLimit { get; set; }

        /// <summary>
        /// Hexadecimal data for contract transaction (required).
        /// </summary>
        [JsonProperty("input_data")]
        public string InputData { get; set; }

        /// <summary>
        /// Transaction type: 0 for authorization transaction, 1 for other transaction. If 0, the amount field is invalid (required).
        /// </summary>
        [JsonProperty("trans_type")]
        public string TransType { get; set; }

        /// <summary>
        /// Dapp name (optional).
        /// </summary>
        [JsonProperty("dapp_name")]
        public string DappName { get; set; }

        /// <summary>
        /// Dapp URL (optional).
        /// </summary>
        [JsonProperty("dapp_url")]
        public string DappUrl { get; set; }

        /// <summary>
        /// Dapp image (optional).
        /// </summary>
        [JsonProperty("dapp_img")]
        public string DappImg { get; set; }

        /// <summary>
        /// RSA private key signature (optional).
        /// Parameters involved in the signature: "request_id", "sub_wallet_id", "main_chain_symbol", "interactive_contract", "amount", "input_data".
        /// Signature rules: https://custodydocs-en.chainup.com/api-references/mpc-apis/co-signer/sign-verify
        /// </summary>
        [JsonProperty("sign")]
        public string Sign { get; set; }
    }
}
