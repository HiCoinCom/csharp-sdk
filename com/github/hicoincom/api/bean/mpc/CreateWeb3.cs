using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class CreateWeb3 : BaseArgs
    {
        [JsonProperty("trans_id")]
        public int? TransId { get; set; }
    }
}