using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class Web3Record
    {
        /// <summary>
        /// Web3 transaction ID.
        /// </summary>
        [JsonProperty("id")]
        public int? Id { get; set; }

        /// <summary>
        /// Unique identifier for the transaction.
        /// </summary>
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        /// <summary>
        /// Wallet ID.
        /// </summary>
        [JsonProperty("sub_wallet_id")]
        public int? SubWalletId { get; set; }

        /// <summary>
        /// Transaction hash.
        /// </summary>
        [JsonProperty("txid")]
        public string Txid { get; set; }

        /// <summary>
        /// Transaction coin.
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Main chain coin symbol, e.g., ETH.
        /// </summary>
        [JsonProperty("main_chain_symbol")]
        public string MainChainSymbol { get; set; }

        /// <summary>
        /// Transaction amount. -1 indicates infinite.
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// Fee currency, e.g., ETH.
        /// </summary>
        [JsonProperty("fee_symbol")]
        public string FeeSymbol { get; set; }

        /// <summary>
        /// Actual fee consumed, e.g., 0.00111.
        /// </summary>
        [JsonProperty("fee")]
        public string Fee { get; set; }

        /// <summary>
        /// Actual fee consumed, e.g., 0.00111.
        /// </summary>
        [JsonProperty("real_fee")]
        public string RealFee { get; set; }

        /// <summary>
        /// Creation time timestamp.
        /// </summary>
        [JsonProperty("created_at")]
        public long? CreatedAt { get; set; }

        /// <summary>
        /// Modification time timestamp.
        /// </summary>
        [JsonProperty("updated_at")]
        public long? UpdatedAt { get; set; }

        /// <summary>
        /// Transaction source address.
        /// </summary>
        [JsonProperty("address_from")]
        public string AddressFrom { get; set; }

        /// <summary>
        /// Transaction destination address.
        /// </summary>
        [JsonProperty("address_to")]
        public string AddressTo { get; set; }

        /// <summary>
        /// Interactive contract.
        /// </summary>
        [JsonProperty("interactive_contract")]
        public string InteractiveContract { get; set; }

        /// <summary>
        /// Confirmations, e.g., 10.
        /// </summary>
        [JsonProperty("confirmations")]
        public int? Confirmations { get; set; }

        /// <summary>
        /// Block height at which the transaction is completed.
        /// </summary>
        [JsonProperty("tx_height")]
        public long? TxHeight { get; set; }

        /// <summary>
        /// Hexadecimal data for contract transaction.
        /// </summary>
        [JsonProperty("input_data")]
        public string InputData { get; set; }

        /// <summary>
        /// Transaction status: 
        /// 1100 - Pending Signature, 1200 - Payment Processing, 
        /// 2000 - Payment Complete, 2100 - Approval Rejected, 
        /// 2200 - Rejected, 2300 - Transaction Discarded, 
        /// 2400 - Payment Failed.
        /// </summary>
        [JsonProperty("status")]
        public int? Status { get; set; }

        /// <summary>
        /// Transaction type: 1 for app, 2 for openapi.
        /// </summary>
        [JsonProperty("trans_source")]
        public int? TransSource { get; set; }
    }
}