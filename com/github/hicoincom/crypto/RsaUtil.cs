using System.Security.Cryptography;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto;
using System.Text;
using System.Runtime.Intrinsics.Arm;

namespace com.github.hicoincom.crypto
{
    class RsaUtil
    {

        private const int MAX_ENCRYPT_BLOCK = 234;

        private const int MAX_DECRYPT_BLOCK = 256;

        public static string PrivateKeyEncrypt(string privateKey, string strEncryptString)
        {
            byte[] keyBytes = Convert.FromBase64String(privateKey);
            //加载私钥
            RSACryptoServiceProvider privateRsa = new RSACryptoServiceProvider();
            privateRsa.ImportPkcs8PrivateKey(keyBytes, out _);

            //转换密钥
            AsymmetricCipherKeyPair keyPair = DotNetUtilities.GetKeyPair(privateRsa);
            //使用RSA/ECB/PKCS1Padding格式
            IBufferedCipher c = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");

            //第一个参数为true表示加密，为false表示解密；第二个参数表示密钥
            c.Init(true, keyPair.Private);
            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(strEncryptString);

            byte[] cache;
            int time = 0;//次数
            int inputLen = dataToEncrypt.Length;
            int offSet = 0;

            MemoryStream outStream = new MemoryStream();
            while (inputLen - offSet > 0)
            {
                if (inputLen - offSet > MAX_ENCRYPT_BLOCK)
                {
                    cache = c.DoFinal(dataToEncrypt, offSet, MAX_ENCRYPT_BLOCK);
                }
                else
                {
                    cache = c.DoFinal(dataToEncrypt, offSet, inputLen - offSet);
                }
                outStream.Write(cache, 0, cache.Length);

                time++;
                offSet = time * MAX_ENCRYPT_BLOCK;
            }

            byte[] resData = outStream.ToArray();

            string strBase64 = Convert.ToBase64String(resData)
                .Replace("\r", "")
                .Replace("\n", "")
                .Replace("+", "-")
                .Replace("/", "_");
            outStream.Close();
            return strBase64;
        }

        public static string PublicKeyDecrypt(string publicKey, string data)
        {
            //加载公钥
            RSACryptoServiceProvider publicRsa = new RSACryptoServiceProvider();
            publicRsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(publicKey), out _);
            RSAParameters rp = publicRsa.ExportParameters(false);

            //转换密钥
            AsymmetricKeyParameter pbk = DotNetUtilities.GetRsaPublicKey(rp);

            IBufferedCipher c = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");
            //第一个参数为true表示加密，为false表示解密；第二个参数表示密钥
            c.Init(false, pbk);
            string newData = data
                .Replace("\r", "")
                .Replace("\n", "")
                .Replace("-", "+")
                .Replace("_", "/");
            // 转回之后需要在末尾补=
            int paddingNeeded = 4 - (newData.Length % 4);
            if (paddingNeeded < 4)
            {
                newData = newData.PadRight(newData.Length + paddingNeeded, '=');
            }
            byte[] dataToDecrypt = Convert.FromBase64String(newData);

            byte[] cache;
            int time = 0;//次数
            int inputLen = dataToDecrypt.Length;
            int offSet = 0;
            MemoryStream outStream = new MemoryStream();
            while (inputLen - offSet > 0)
            {
                if (inputLen - offSet > MAX_DECRYPT_BLOCK)
                {
                    cache = c.DoFinal(dataToDecrypt, offSet, MAX_DECRYPT_BLOCK);
                }
                else
                {
                    cache = c.DoFinal(dataToDecrypt, offSet, inputLen - offSet);
                }
                outStream.Write(cache, 0, cache.Length);

                time++;
                offSet = time * MAX_DECRYPT_BLOCK;
            }
            byte[] resData = outStream.ToArray();

            string strDec = Encoding.UTF8.GetString(resData);
            return strDec;
        }

        public static string Sign(string str, string privateKey)
        {
            //根据需要加签时的哈希算法转化成对应的hash字符节
            byte[] bt = Encoding.GetEncoding("utf-8").GetBytes(str);
            byte[] rgbHash;
                    
            var csp = SHA256.Create();
            rgbHash = csp.ComputeHash(bt);
            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            key.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKey), out _);
            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
            formatter.SetHashAlgorithm("SHA256");
            byte[] inArray = formatter.CreateSignature(rgbHash);
            return Convert.ToBase64String(inArray);
        }
        /**
         * 生成公司呀
         */
        public static Dictionary<string, string> generateRsaPriAndPub()
        {
            Dictionary<string, string> keyMap = new Dictionary<string, string>();
            var rsa = RSA.Create();
            keyMap["privateKey"] = Convert.ToBase64String(rsa.ExportPkcs8PrivateKey());
            keyMap["publicKey"] = Convert.ToBase64String(rsa.ExportSubjectPublicKeyInfo());
            return keyMap;
        }
    }
}