using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PortalExample.Services
{
    public class CustomCypher : ISimpleCypher
    {
        private const string Key = "SimpleSampleKey";

        public string RandomDecrypt(string textToDecrypt)
        {
            var ttd = Decrypt(textToDecrypt);
            return ttd.Split("#")[0];
        }

        public string RandomEncrypt(string textToEncrypt)
        {
            var tte = $"{textToEncrypt}#{DateTime.Now.Ticks}";
            return Encrypt(tte);
        }


        public string Decrypt(string textToDecrypt)
        {
            byte[] MyDecryptArray = Convert.FromBase64String(textToDecrypt);

            MD5CryptoServiceProvider MyMD5CryptoService = new MD5CryptoServiceProvider();

            byte[] MysecurityKeyArray = MyMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));

            MyMD5CryptoService.Clear();

            var MyTripleDESCryptoService = new TripleDESCryptoServiceProvider();

            MyTripleDESCryptoService.Key = MysecurityKeyArray;

            MyTripleDESCryptoService.Mode = CipherMode.ECB;

            MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var MyCrytpoTransform = MyTripleDESCryptoService.CreateDecryptor();

            byte[] MyresultArray = MyCrytpoTransform
               .TransformFinalBlock(MyDecryptArray, 0,
               MyDecryptArray.Length);

            MyTripleDESCryptoService.Clear();

            return UTF8Encoding.UTF8.GetString(MyresultArray);
        }

        public string Encrypt(string textToEncrypt)
        {
            byte[] MyEncryptedArray = UTF8Encoding.UTF8.GetBytes(textToEncrypt);

            MD5CryptoServiceProvider MyMD5CryptoService = new MD5CryptoServiceProvider();

            byte[] MysecurityKeyArray = MyMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));

            MyMD5CryptoService.Clear();

            var MyTripleDESCryptoService = new TripleDESCryptoServiceProvider();

            MyTripleDESCryptoService.Key = MysecurityKeyArray;

            MyTripleDESCryptoService.Mode = CipherMode.ECB;

            MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var MyCrytpoTransform = MyTripleDESCryptoService
               .CreateEncryptor();

            byte[] MyresultArray = MyCrytpoTransform
               .TransformFinalBlock(MyEncryptedArray, 0,
               MyEncryptedArray.Length);

            MyTripleDESCryptoService.Clear();

            return Convert.ToBase64String(MyresultArray, 0, MyresultArray.Length);
        }

      
    }
}
