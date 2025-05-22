using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class CreateWalletArgs : BaseArgs
    {
        [JsonProperty("sub_wallet_name")]
        public string SubWalletName { get; set; }

        [JsonProperty("app_show_status")]
        public string AppShowStatus { get; set; }
    }
}