using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
     /// <summary>
    /// Represents a supported coin.
    /// </summary>
    [Serializable]
    public class SupportCoin
    {
        /// <summary>
        /// Unique identifier for the coin, e.g., ETH.
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Coin network, e.g., ETH.
        /// </summary>
        [JsonProperty("coin_net")]
        public string CoinNet { get; set; }

        /// <summary>
        /// Real name of the coin, e.g., ETH.
        /// </summary>
        [JsonProperty("symbol_alias")]
        public string SymbolAlias { get; set; }

        /// <summary>
        /// Indicates if acceleration is supported (true/false).
        /// </summary>
        [JsonProperty("support_acceleration")]
        public bool? SupportAcceleration { get; set; }

        /// <summary>
        /// Indicates if the main chain is opened (false/true).
        /// </summary>
        [JsonProperty("if_open_chain")]
        public bool? IfOpenChain { get; set; }
    }
}