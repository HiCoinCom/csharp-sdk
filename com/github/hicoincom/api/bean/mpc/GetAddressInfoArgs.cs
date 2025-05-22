using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class GetAddressInfoArgs : BaseArgs
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("memo")]
        public string Memo { get; set; }
    }
}