namespace com.github.hicoincom.crypto
{

    public class DataCrypto : IDataCrypto
    {

        string privateKey;

        string publicKey;

        public DataCrypto(string privateKey, string publicKey)
        {
            this.privateKey = privateKey;
            this.publicKey = publicKey;
        }

        public DataCrypto()
        {

        }

        public string Decrypt(string data)
        {
            return RsaUtil.PublicKeyDecrypt(publicKey, data.Replace(" ", ""));
        }

        public string Encrypt(string data)
        {
            return RsaUtil.PrivateKeyEncrypt(privateKey, data);
        }
    }
}