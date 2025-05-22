using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class WalletAddressInfo
    {
        [JsonProperty("sub_wallet_id")]
        public int? SubWalletId { get; set; }


        [JsonProperty("addr_type")]
        public int? AddrType { get; set; }

        [JsonProperty("merge_address_symbol")]
        public string MergeAddressSymbol { get; set; }
    }
}