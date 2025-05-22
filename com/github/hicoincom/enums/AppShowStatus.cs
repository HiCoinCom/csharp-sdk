using System.ComponentModel;

namespace api.github.hicoincom.enums
{

    public class AppShowStatus
    {
        public static readonly AppShowStatus HIDDEN = new AppShowStatus("2", "don’t show wallet");
        public static readonly AppShowStatus SHOW = new AppShowStatus("1", "show wallet");

        public string Value { get; set; }
        public string Desc { get; set; }

        public AppShowStatus(string value, string desc) {
            Value = value;
            Desc = desc;
        }
    }
}