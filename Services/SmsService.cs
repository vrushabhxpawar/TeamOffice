using System.Net;

namespace DemoDownloadPage.Services
    {
    public class SmsService
        {

        public readonly IConfiguration _config;
        public SmsService (IConfiguration config)
            {
            _config = config;
            }

        public async Task<bool> SendSmsAsync (string mobile, string OTP)
            {
            try
                {
                string baseUrl = _config["DLTLink"];

                string msg = OTP + " is your verification code for e-Time Office Cloud Service. e-Time Office Softech Pvt. Ltd.";

                string url = $"{baseUrl}{mobile}&msg={WebUtility.UrlEncode(msg)}";

                using HttpClient client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                    {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                    return true;
                    }
                return false;
                }
            catch (Exception ex)
                {
                Console.WriteLine(ex.Message);
                return false;
                }
            }
        }
    }
