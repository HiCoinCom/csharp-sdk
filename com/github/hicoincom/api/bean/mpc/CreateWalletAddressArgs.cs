using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean.mpc
{
    [Serializable]
    public class CreateWalletAddressArgs : BaseArgs
    {
        [JsonProperty("sub_wallet_id")]
        public int? SubWalletId { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}