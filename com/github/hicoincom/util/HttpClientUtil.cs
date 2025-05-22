namespace api.github.hicoincom.util
{
    public class HttpClientUtil
    {
        private const int DEFAULT_TIMEOUT = 30000;

        private static HttpClientUtil instance;

        private static readonly HttpClient client;
        private int timeout = DEFAULT_TIMEOUT;

        private static readonly int maxConnTotal = 200;
        private static readonly int maxConnPerRoute = 100;

        static HttpClientUtil()
        {
            var handler = new HttpClientHandler
            {
                MaxConnectionsPerServer = maxConnPerRoute
            };
            client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromMilliseconds(DEFAULT_TIMEOUT)
            };
        }
        
        private HttpClientUtil() { }

        public static HttpClientUtil GetInstance()
        {
            if (instance == null)
            {
                lock (typeof(HttpClientUtil))
                {
                    if (instance == null)
                    {
                        instance = new HttpClientUtil();
                    }
                }
            }
            return instance;
        }

        public string DoGetWithJsonResult(string uri)
        {
            try
            {
                HttpResponseMessage response = client.GetAsync(uri).Result; // .Result 是同步的

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result; // .Result 是同步的
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);  // 你可以替换成适当的日志
            }
            return "";
        }

        public string DoPostWithJsonResult(string uri, Dictionary<string, string> paramMap)
        {
            try
            {
                var content = new FormUrlEncodedContent(paramMap);

                HttpResponseMessage response = client.PostAsync(uri, content).Result; // .Result 是同步的

                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result; // .Result 是同步的
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);  // 你可以替换成适当的日志
            }
            return "";
        }
    }
}