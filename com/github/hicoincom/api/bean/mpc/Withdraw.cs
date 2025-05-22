using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class Withdraw
    {
        [JsonProperty("withdraw_id")]
        public int? WithdrawId { get; set; }
    }
}