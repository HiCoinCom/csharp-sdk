using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    /// <summary>
    /// Represents a record of an auto-collection transaction.
    /// </summary>
    [Serializable]
    public class AutoCollectRecord
    {
        /// <summary>
        /// Consolidation ID.
        /// </summary>
        [JsonProperty("id")]
        public int? Id { get; set; }

        /// <summary>
        /// Wallet ID.
        /// </summary>
        [JsonProperty("sub_wallet_id")]
        public int? SubWalletId { get; set; }

        /// <summary>
        /// Unique identifier for the coin, used for transfers, e.g., USDTERC20.
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Consolidation amount.
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// Fee currency, e.g., ETH.
        /// </summary>
        [JsonProperty("fee_symbol")]
        public string FeeSymbol { get; set; }

        /// <summary>
        /// Fee amount, e.g., 0.00123.
        /// </summary>
        [JsonProperty("fee")]
        public string Fee { get; set; }

        /// <summary>
        /// Actual consumed fee, e.g., 0.00111.
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
        /// Sender's address.
        /// </summary>
        [JsonProperty("address_from")]
        public string AddressFrom { get; set; }

        /// <summary>
        /// Consolidation address.
        /// </summary>
        [JsonProperty("address_to")]
        public string AddressTo { get; set; }

        /// <summary>
        /// Transaction hash.
        /// </summary>
        [JsonProperty("txid")]
        public string Txid { get; set; }

        /// <summary>
        /// Number of block confirmations, e.g., 10.
        /// </summary>
        [JsonProperty("confirmations")]
        public int? Confirmations { get; set; }

        /// <summary>
        /// Consolidation status:
        /// 1000 Unapproved,
        /// 1100 Approved, Pending Signature,
        /// 1200 In Progress,
        /// 2000 Completed,
        /// 2100 Rejected,
        /// 2200 Rejected,
        /// 2201 System Rejected,
        /// 2202 Auto Cancelled,
        /// 2300 Transaction Discarded,
        /// 2400 Payment Failed.
        /// </summary>
        [JsonProperty("status")]
        public int? Status { get; set; }

        /// <summary>
        /// Consolidation type:
        /// 10 - Consolidation Transaction,
        /// 11 - Consolidation Gas Transaction.
        /// </summary>
        [JsonProperty("trans_type")]
        public int? TransType { get; set; }

        /// <summary>
        /// Base currency unique identifier on the main chain, e.g., ETH.
        /// </summary>
        [JsonProperty("base_symbol")]
        public string BaseSymbol { get; set; }

        /// <summary>
        /// Contract address for the consolidation currency.
        /// </summary>
        [JsonProperty("contract_address")]
        public string ContractAddress { get; set; }
    }
}
