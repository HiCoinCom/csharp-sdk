namespace com.github.hicoincom.exception
{

    public class ConfigException : Exception
    {
        public ConfigException(string message) : base(message)
        {
        }

        public ConfigException() : base()
        {
        }
    }
}