using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace com.github.hicoincom.api.bean
{
    public class BaseArgs
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("charset")]
        public string Charset { get; set; }

        [JsonProperty("time")]
        public long? Time { get; set; }

        public string ToJson()
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(this);
        }
    }
}