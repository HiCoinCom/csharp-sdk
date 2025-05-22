using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class WalletAddress
    {
        [JsonProperty("address")]
        public string Address { get; set; }
        
        [JsonProperty("addr_type")]
        public int? AddrType { get; set; }

        [JsonProperty("memo")]
        public string Memo { get; set; }
    }
}