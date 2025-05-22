using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class ChangeShowStatusArgs : BaseArgs
    {
        [JsonProperty("sub_wallet_ids")]
        public string SubWalletIds { get; set; }

        [JsonProperty("app_show_status")]
        public string AppShowStatus { get; set; }
    }
}