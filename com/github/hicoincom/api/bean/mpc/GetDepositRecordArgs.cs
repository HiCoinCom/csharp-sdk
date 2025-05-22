using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class GetDepositRecordArgs : BaseArgs
    {
        [JsonProperty("ids")]
        public string Ids { get; set; }
    }
}