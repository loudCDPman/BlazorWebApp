using System;
using System.Security.Cryptography;
using System.Text;

namespace BL
{
    public class Crypto
    {
        protected readonly byte[] _Key;
        protected readonly byte[] _Iv;
        public Crypto(byte[] key, byte[] Iv)
        {
            _Key = key;
            _Iv = Iv;
        }
        public byte[] Encryption(string msg)
        {
            using (Aes aes = Aes.Create())
            {
                //aes.Key = Encoding.ASCII.GetBytes("openworld");
                //aes.IV = 
                aes.KeySize = 256;
                aes.GenerateKey();

                using (AesCryptoServiceProvider aesCrypto = new AesCryptoServiceProvider())
                {
                    aesCrypto.KeySize = 256;
                    aesCrypto.BlockSize = 128;
                    aesCrypto.Key = aes.Key;
                    aesCrypto.IV = aes.IV;
                    aesCrypto.Mode = CipherMode.CBC;
                    aesCrypto.Padding = PaddingMode.PKCS7;

                    ICryptoTransform cryptoTransform = aesCrypto.CreateEncryptor(aesCrypto.Key, aesCrypto.IV);
                    return cryptoTransform.TransformFinalBlock(Encoding.UTF8.GetBytes(msg), 0, msg.Length);
                }
            }
        return null;
        }


        public string DeEncryption(Byte[] msg)
        {
            using (Aes aes = Aes.Create())
            {
                //aes.Key = Encoding.ASCII.GetBytes("openworld");
                //aes.IV = 
                aes.KeySize = 256;
                aes.GenerateKey();

                using (AesCryptoServiceProvider aesCrypto = new AesCryptoServiceProvider())
                {
                    aesCrypto.KeySize = 256;
                    aesCrypto.BlockSize = 128;
                    aesCrypto.Key = aes.Key;
                    aesCrypto.IV = aes.IV;
                    aesCrypto.Mode = CipherMode.CBC;
                    aesCrypto.Padding = PaddingMode.PKCS7;

                    ICryptoTransform cryptoTransform = aesCrypto.CreateDecryptor(aesCrypto.Key, aesCrypto.IV);
                    return Encoding.UTF8.GetString(cryptoTransform.TransformFinalBlock(msg, 0, msg.Length));
                }
            }
            return null;
        }
    }
}
