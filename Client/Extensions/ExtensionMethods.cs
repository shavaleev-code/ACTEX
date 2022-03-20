using Newtonsoft.Json;

namespace Client.Extensions
{
    public static class ExtensionMethods
    {
        public static T? Get<T>(string url)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);
            try
            {
                var response = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result.ToString();
                return JsonConvert.DeserializeObject<T>(response);
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        public static void Set(string url)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(clientHandler);

            try
            {
                client.GetAsync(url);
            }
            catch (Exception e)
            {
            }
        }
    }
}
