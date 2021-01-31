using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var code = new QRAuthToken()
            {
                Code = "kkkkkkkkkk",
                Uid = "jc4st3lls"

            };

            using(var handler=new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(handler))
                {

                    client.BaseAddress = new Uri("https://localhost:5001");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                    var json = System.Text.Json.JsonSerializer.Serialize(code);
                    var request = new StringContent(json, Encoding.UTF8, "application/json");
                    var req = client.PostAsync("api/Authenticate", request).GetAwaiter().GetResult();

                    if (req.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var content = req.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    }

                }
            }

            
        }
    }


    public class QRAuthToken
    {
        public string Code { get; set; }
        public string Uid { get; set; }
    }
}
