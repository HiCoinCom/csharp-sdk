namespace com.github.hicoincom.exception
{
    public class ArgsNullException : Exception
    {
        public ArgsNullException(string message) : base(message)
        {
        }

        public ArgsNullException() : base()
        {

        }
    }
}