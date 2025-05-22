using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class CoinDetails
    {
        /// <summary>
        /// Unique identifier of the base coin of the main chain, e.g., MATIC1.
        /// </summary>
        [JsonProperty("base_symbol")]
        public string BaseSymbol { get; set; }

        /// <summary>
        /// Coin network.
        /// </summary>
        [JsonProperty("coin_net")]
        public string CoinNet { get; set; }

        /// <summary>
        /// Unique identifier for the coin, e.g., MATIC1.
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Real name of the coin.
        /// </summary>
        [JsonProperty("symbol_alias")]
        public string SymbolAlias { get; set; }

        /// <summary>
        /// Address regex pattern, e.g., .*.
        /// </summary>
        [JsonProperty("address_regex")]
        public string AddressRegex { get; set; }

        /// <summary>
        /// Address tag regex pattern.
        /// </summary>
        [JsonProperty("address_tag_regex")]
        public string AddressTagRegex { get; set; }

        /// <summary>
        /// Contract address supported by the MPC main chain.
        /// </summary>
        [JsonProperty("contract_address")]
        public string ContractAddress { get; set; }

        /// <summary>
        /// Coin precision.
        /// </summary>
        [JsonProperty("decimals")]
        public int? Decimals { get; set; }

        /// <summary>
        /// Number of confirmations for deposits.
        /// </summary>
        [JsonProperty("deposit_confirmation")]
        public int? DepositConfirmation { get; set; }

        /// <summary>
        /// Prefix for the block explorer address query link.
        /// </summary>
        [JsonProperty("address_link")]
        public string AddressLink { get; set; }

        /// <summary>
        /// Prefix for the block explorer transaction ID query link.
        /// </summary>
        [JsonProperty("txid_link")]
        public string TxidLink { get; set; }

        /// <summary>
        /// Coin icon.
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Indicates if the main chain is open (true/false).
        /// </summary>
        [JsonProperty("if_open_chain")]
        public bool? IfOpenChain { get; set; }

        /// <summary>
        /// Name of the coin on the blockchain.
        /// </summary>
        [JsonProperty("real_symbol")]
        public string RealSymbol { get; set; }

        /// <summary>
        /// Indicates if memo is supported (0: not supported, 1: supported).
        /// </summary>
        [JsonProperty("support_memo")]
        public string SupportMemo { get; set; }

        /// <summary>
        /// Indicates if token coins are supported (0: not supported, 1: supported for main chain coins, empty for tokens).
        /// </summary>
        [JsonProperty("support_token")]
        public string SupportToken { get; set; }

        /// <summary>
        /// Indicates if acceleration is supported (true/false).
        /// </summary>
        [JsonProperty("support_acceleration")]
        public bool? SupportAcceleration { get; set; }

        /// <summary>
        /// Support for multiple addresses (true: supported, false: not supported).
        /// </summary>
        [JsonProperty("support_multi_addr")]
        public bool? SupportMultiAddr { get; set; }

        /// <summary>
        /// Merged address main chain coin, unique identifier for the coin.
        /// </summary>
        [JsonProperty("merge_address_symbol")]
        public string MergeAddressSymbol { get; set; }

        /// <summary>
        /// Belongs to the main chain coin type (0: account type, 1: UTXO-type main chain, 2: Memo type).
        /// </summary>
        [JsonProperty("coin_type")]
        public int? CoinType { get; set; }
    }
}