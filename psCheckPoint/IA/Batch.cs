using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;

namespace psCheckPoint.IA
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class Batch<RequestType, ResponseType> : IDisposable
    {
        private HttpClient client;

        public Batch(string gateway, string sharedSecret, string command, bool NoCertificateValidation)
        {
            Gateway = gateway;
            SharedSecret = sharedSecret;
            Command = command;

            HttpClientHandler handler = new HttpClientHandler();
#if NETCOREAPP1_1
            if (NoCertificateValidation)
            {
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            }
#else
            if (NoCertificateValidation)
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
            }
            else
            {
                ServicePointManager.ServerCertificateValidationCallback = null;
            }
#endif

            client = new HttpClient(handler);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Clear()
        {
            Requests.Clear();
        }

        public string Gateway { get; set; }

        [JsonProperty(PropertyName = "shared-secret")]
        public string SharedSecret { get; set; }

        public string Command { get; set; }

        [JsonProperty(PropertyName = "requests")]
        public List<RequestType> Requests { get; set; } = new List<RequestType>(10);

        public void Post(Cmdlet cmdlet)
        {
            string URL = $"https://{Gateway}/_IA_API/v1.0/{Command}";

            // Debug Output Request
            JsonSerializerSettings jss = new JsonSerializerSettings();
            jss.MaxDepth = 4;
            string strJson = JsonConvert.SerializeObject(this, jss);
            cmdlet.WriteDebug($@"JSON Request to {URL}
{strJson}");

            try
            {
                using (HttpResponseMessage response = client.PostAsync(URL, new StringContent(strJson, Encoding.UTF8, "application/json")).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        strJson = response.Content.ReadAsStringAsync().Result;

                        // Debug Output Request
                        cmdlet.WriteDebug($@"JSON Response
{strJson}");

                        Responses<ResponseType> responses = JsonConvert.DeserializeObject<Responses<ResponseType>>(strJson);

                        cmdlet.WriteObject(responses.responses);
                    }
                    else
                    {
                        cmdlet.WriteWarning($"Server returned status code: {(int)response.StatusCode} [{response.StatusCode}]");
                        strJson = response.Content.ReadAsStringAsync().Result;

                        // Debug Output Request
                        cmdlet.WriteDebug($@"JSON Response
{strJson}");

                        CheckPointError error = JsonConvert.DeserializeObject<CheckPointError>(strJson);
                        cmdlet.WriteObject(error);
                    }
                }
            }
            catch (Exception e)
            {
                while (e.InnerException != null) e = e.InnerException;
                cmdlet.WriteError(new ErrorRecord(e, e.Message, ErrorCategory.ConnectionError, this));
            }
        }

        public void Dispose()
        {
            ((IDisposable)client).Dispose();
        }
    }
}