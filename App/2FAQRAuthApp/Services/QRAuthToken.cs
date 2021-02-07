using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _2FAQRAuthApp.Services
{
    public class QRAuthToken
    {
        private QRAuthToken(){}

        public string Code { get; private set; }
        public string Uid { get; private set; }

        public static async Task<bool> AuthenticateCode(string uid, string qrcode)
        {
            bool ret = false;

            var code = new QRAuthToken()
            {
                Uid = uid, Code = qrcode
            };


            using (var handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(handler))
                {
                    var url = $"{App.PORTAL_SCHEMA}://{App.PORTAL_HOST}:{App.PORTAL_PORT}";
                    client.BaseAddress = new Uri(url); 
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                    var json = System.Text.Json.JsonSerializer.Serialize(code);
                    var request = new StringContent(json, Encoding.UTF8, "application/json");
                    var req = await client.PostAsync("api/authenticate", request);

                    if (req.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ret = true;

                    }

                }
            }

            return ret;
        }
    }


}
