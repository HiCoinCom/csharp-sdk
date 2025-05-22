namespace com.github.hicoincom.crypto
{
    public interface IDataCrypto
    {
        string Encrypt(string data);
        string Decrypt(string data);
    }
}