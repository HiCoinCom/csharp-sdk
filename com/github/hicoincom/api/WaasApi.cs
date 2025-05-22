using api.github.hicoincom.util;
using com.github.hicoincom.api.bean;
using com.github.hicoincom.crypto;
using com.github.hicoincom.enums;
using com.github.hicoincom.exception;
using Newtonsoft.Json;
using NLog;

namespace com.github.hicoincom.api
{
    public class WaasApi
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public WaasConfig Config { get; set; }
        public IDataCrypto DataCrypto { get; set; }

        public WaasApi(WaasConfig config, IDataCrypto dataCrypto)
        {
            Config = config;
            Config.Domain = config.Domain;
            DataCrypto = dataCrypto;
        }

       protected T Invoke<T>(MpcApiUri uri, BaseArgs args, Type clazz)
        {
            return Invoke<T>(uri.Value, uri.Method, args, clazz);
        }

        protected T Invoke<T>(string uri, string requestMethod, BaseArgs args, Type clazz)
        {
            // 设置默认参数
            args.Charset = this.Config.Charset;
            args.Time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            // 加密参数
            string raw = args.ToJson();
            LogInfo("request api:{0}, request args:{1}", uri, raw);
            string data = this.DataCrypto.Encrypt(raw).Replace(" ", "");
            LogInfo("request api: {0}, encode args:{1}", uri, data);

            if (string.IsNullOrWhiteSpace(data))
            {
                logger.Error("request api:{0}, encode args return null", uri);
                throw new CryptoException("data crypto return null");
            }

            // 发起请求
            string url = $"{Config.Domain}{uri}";
            var paramsObj = new Args(Config.AppId, data);
            string resp = null;

            if (requestMethod.Equals(HttpMethod.Get.Method, StringComparison.OrdinalIgnoreCase))
            {
                url += "?" + paramsObj.ToString();
                resp = HttpClientUtil.GetInstance().DoGetWithJsonResult(url);
            }
            else
            {
                resp = HttpClientUtil.GetInstance().DoPostWithJsonResult(url, paramsObj.ToMap());
            }

            LogInfo("request api: {0} raw result:{1}", uri, resp);

            // 处理空响应
            if (string.IsNullOrWhiteSpace(resp))
            {
                logger.Error("request api: {0} api return null", uri);
                return default;
            }

            var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(resp);
            if (jsonObject == null || !jsonObject.ContainsKey("data") || string.IsNullOrWhiteSpace(jsonObject["data"]?.ToString()))
            {
                logger.Error("request api: {0}, result do not found data field or data field is empty", uri);
                return default;
            }

            // 解密响应数据
            string respRaw = this.DataCrypto.Decrypt(jsonObject["data"].ToString());
            LogInfo("request api:{0} decode result :{1}", uri, respRaw);

            if (string.IsNullOrWhiteSpace(respRaw))
            {
                logger.Error("request api:{0}, decode result return null", uri);
                return default;
            }

            try
            {
                var result = JsonConvert.DeserializeObject(respRaw, clazz);
                if (result == null)
                {
                    logger.Error("request api:{0}, result parse json to object error, json:{1}", uri, respRaw);
                    return default;
                }
                return (T)result;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error parsing result.");
                return default;
            }
        }

        private void LogInfo(string message, params object[] args)
        {
            if (Config.EnableLog)
            {
                logger.Info(message, args);
            }
        }
    }
}