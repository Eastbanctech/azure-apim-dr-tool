using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace APIM.DR
{
    public class ApiRequestData
    {
        public string storageAccount { get; set; }
        public string accessKey { get; set; }
        public string containerName { get; set; }
        public string backupName { get; set; }

        public static ApiRequestData GetRequestData()
        {
            return new ApiRequestData { 
                storageAccount = APIM.Config.Config.StorageAccount,
                accessKey = APIM.Config.Config.AccessKey,
                containerName = APIM.Config.Config.ContainerName,
                backupName = APIM.Config.Config.BackupName,
            };
        }
    }

    public class ApiClient : IDisposable
    {
        private HttpClient client = new HttpClient();

        public ApiClient(string accessToken)
        {
            client.BaseAddress = new Uri("https://management.azure.com");
            client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", accessToken));
        }

        async public Task SendRequest(string method)
        {
            var url = string.Format(APIM.Config.Config.ServiceUrl
                , APIM.Config.Config.SubscriptionId
                , APIM.Config.Config.ResourceGroupName
                , APIM.Config.Config.ServiceName
                , method
                , APIM.Config.Config.Version);

            try
            {
                var response = await client.PostAsJsonAsync<ApiRequestData>(url
                    , ApiRequestData.GetRequestData());

                Console.WriteLine("\nResult: " + response.StatusCode);

                var responseMessage = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(responseMessage))
                {
                    Console.WriteLine("Respose: {0}", responseMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nException: " + ex.Message);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (client != null)
                {
                    client.Dispose();
                    client = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ApiClient() 
        {
            Dispose(false);
        }


    }
}
