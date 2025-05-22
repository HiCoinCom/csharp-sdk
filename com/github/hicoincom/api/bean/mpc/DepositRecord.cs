using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    /// <summary>
    /// Represents a deposit record.
    /// </summary>
    [Serializable]
    public class DepositRecord
    {
        /// <summary>
        /// Receiving ID.
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
        /// The unique identifier of the main chain of the coin to be received, e.g., ETH.
        /// </summary>
        [JsonProperty("base_symbol")]
        public string BaseSymbol { get; set; }

        /// <summary>
        /// Received coin’s contract address.
        /// </summary>
        [JsonProperty("contract_address")]
        public string ContractAddress { get; set; }

        /// <summary>
        /// Receiving amount.
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// Transfer from address.
        /// </summary>
        [JsonProperty("address_from")]
        public string AddressFrom { get; set; }

        /// <summary>
        /// Receiving address.
        /// </summary>
        [JsonProperty("address_to")]
        public string AddressTo { get; set; }

        /// <summary>
        /// Receiving address memo.
        /// </summary>
        [JsonProperty("memo")]
        public string Memo { get; set; }

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
        /// Block height at which the transaction is completed.
        /// </summary>
        [JsonProperty("tx_height")]
        public long? TxHeight { get; set; }

        /// <summary>
        /// Receiving status: 
        /// 1000 unconfirmed, 1100 confirmed (transaction block confirmed),
        /// 2000 completed (shown at account), 3000 abnormal.
        /// </summary>
        [JsonProperty("status")]
        public int? Status { get; set; }

        /// <summary>
        /// Deposit Type: 1 for regular coin deposit, 2 for NFT deposit.
        /// </summary>
        [JsonProperty("deposit_type")]
        public int? DepositType { get; set; }

        /// <summary>
        /// TokenId for NFT deposit. This value is not empty when deposit_type = 2.
        /// </summary>
        [JsonProperty("token_id")]
        public string TokenId { get; set; }

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
