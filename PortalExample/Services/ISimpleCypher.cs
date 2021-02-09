using System;
using System.Threading.Tasks;

namespace PortalExample.Services
{
    public interface ISimpleCypher
    {
        public string Encrypt(string textToEncrypt);
        public string RandomEncrypt(string textToEncrypt);


        public string Decrypt(string textToDecrypt);
        public string RandomDecrypt(string textToDecrypt);


    }
}
