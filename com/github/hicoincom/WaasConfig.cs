namespace com.github.hicoincom
{
    public class WaasConfig {
        public string AppId { get; set; }
        public string UserPrivateKey { get; set; }
        public string WaasPublickKey { get; set; }
        public string Domain { get; set; } = "https://openapi.hicoin.vip/api";
        public string version { get; set; } = "v2";
        public bool EnableLog { get; set; } = false;
        public string Charset { get; set; } = "utf-8";

    }
}