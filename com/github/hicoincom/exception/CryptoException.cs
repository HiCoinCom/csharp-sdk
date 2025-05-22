namespace com.github.hicoincom.exception
{

    public class CryptoException : Exception
    {
        public CryptoException(string message) : base(message)
        {
        }

        public CryptoException() : base()
        {
        }
    }
}