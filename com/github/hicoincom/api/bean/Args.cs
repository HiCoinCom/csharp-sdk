namespace com.github.hicoincom.api.bean
{

    public class Args
    {
        public string AppId { get; set; }
        public string Data { get; set; }

        public Args()
        {
        }
        
        public Args(string appId, string data)
        {
            AppId = appId;
            Data = data;
        }

        public Dictionary<string, string> ToMap()
        {
            var map = new Dictionary<string, string>(2)
        {
            { "app_id", AppId },
            { "data", Data }
        };
            return map;
        }

        public override string ToString()
        {
            return $"app_id={AppId}&data={Data}";
        }
    }
}