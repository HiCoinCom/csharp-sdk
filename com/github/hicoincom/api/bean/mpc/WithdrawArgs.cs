using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class WithdrawArgs : BaseArgs
    {

        [JsonProperty("sub_wallet_id")]
        public int? SubWalletId { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        
        [JsonProperty("from")]
        public string From { get; set; }
        
        [JsonProperty("address_to")]
        public string AddressTo { get; set; }

        [JsonProperty("memo")]
        public string Memo { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("request_id")]
        public string RequestId { get; set; }
        
        [JsonProperty("remark")]
        public string Remark { get; set; }

        [JsonProperty("sign")]
        public string Sign { get; set; }
    }
}