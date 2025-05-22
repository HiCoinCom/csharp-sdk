using Newtonsoft.Json;

namespace com.github.hicoincom.api.bean
{
    [Serializable]
    public class Result<T>
    {
        private static readonly string SUCCESS_CODE = "0";

        [JsonProperty("code")]
        private string Code { get; set; }

        [JsonProperty("msg")]
        private string Msg { get; set; }

        [JsonProperty("data")]
        private T Data { get; set; }

        // Getter and Setter for Code
        public string GetCode()
        {
            return Code;
        }

        public void SetCode(string code)
        {
            Code = code;
        }

        // Getter and Setter for Msg
        public string GetMsg()
        {
            return Msg;
        }

        public void SetMsg(string msg)
        {
            Msg = msg;
        }

        // Getter and Setter for Data
        public T GetData()
        {
            return Data;
        }

        public void SetData(T data)
        {
            Data = data;
        }

        // Convert the object to JSON string
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        // Check if the operation is successful
        public bool IsSuccess()
        {
            return SUCCESS_CODE.Equals(this.GetCode());
        }
    }
}