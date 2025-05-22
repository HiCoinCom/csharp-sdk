using Newtonsoft.Json;
using System;

namespace com.github.hicoincom.api.bean.mpc
{
    /// <summary>
    /// Represents a record of a withdrawal transaction.
    /// </summary>
    [Serializable]
    public class WithdrawRecord
    {
        /// <summary>
        /// Withdrawal ID.
        /// </summary>
        [JsonProperty("id")]
        public int? Id { get; set; }

        /// <summary>
        /// The unique identifier for transferring coins.
        /// </summary>
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

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
        /// Transfer coin's contract address.
        /// </summary>
        [JsonProperty("contract_address")]
        public string ContractAddress { get; set; }

        /// <summary>
        /// The unique identifier of the main chain of the coin to be transferred, e.g., ETH.
        /// </summary>
        [JsonProperty("base_symbol")]
        public string BaseSymbol { get; set; }

        /// <summary>
        /// From account address.
        /// </summary>
        [JsonProperty("address_from")]
        public string AddressFrom { get; set; }

        /// <summary>
        /// To account address.
        /// </summary>
        [JsonProperty("address_to")]
        public string AddressTo { get; set; }

        /// <summary>
        /// To account address memo.
        /// </summary>
        [JsonProperty("memo")]
        public string Memo { get; set; }

        /// <summary>
        /// Transfer amount.
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// Transaction hash.
        /// </summary>
        [JsonProperty("txid")]
        public string Txid { get; set; }

        /// <summary>
        /// Gas fee coin, e.g., ETH.
        /// </summary>
        [JsonProperty("fee_symbol")]
        public string FeeSymbol { get; set; }

        /// <summary>
        /// Gas fee, e.g., 0.00123.
        /// </summary>
        [JsonProperty("fee")]
        public string Fee { get; set; }

        /// <summary>
        /// The actual gas fee consumed, e.g., 0.00111.
        /// </summary>
        [JsonProperty("real_fee")]
        public string RealFee { get; set; }

        /// <summary>
        /// Transfer status:
        /// 1000 Awaiting approval,
        /// 1100 Approved while awaiting signature,
        /// 1200 Payment in progress,
        /// 2000 Payment completed,
        /// 2100 Not Approved,
        /// 2200 Rejected,
        /// 2300 Transaction discarded,
        /// 2400 Payment failed.
        /// </summary>
        [JsonProperty("status")]
        public int? Status { get; set; }

        /// <summary>
        /// Number of block confirmations, e.g., 10.
        /// </summary>
        [JsonProperty("confirmations")]
        public int? Confirmations { get; set; }

        /// <summary>
        /// Block height at which the transaction is completed.
        /// </summary>
        [JsonProperty("tx_height")]
        public long? TxHeight { get; set; }

        /// <summary>
        /// Transfer type:
        /// 1 - App,
        /// 2 - OpenAPI,
        /// 3 - Web.
        /// </summary>
        [JsonProperty("withdraw_source")]
        public int? WithdrawSource { get; set; }

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
    }
}
