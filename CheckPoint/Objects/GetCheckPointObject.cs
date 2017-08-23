using Newtonsoft.Json;
using System;
using System.Management.Automation;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CheckPoint.Objects
{
    public abstract class GetCheckPointObject : CheckPointCmdlet
    {
        /// <summary>
        /// <para type="description">Object unique identifier.</para>
        /// </summary>
        [JsonProperty(PropertyName = "uid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(Mandatory = true, ParameterSetName = "By UID", ValueFromPipelineByPropertyName = true)]
        public string UID { get; set; }

        /// <summary>
        /// <para type="description">Object name.</para>
        /// </summary>
        [JsonProperty(PropertyName = "name", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(Position = 1, Mandatory = true, ParameterSetName = "By Name", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">The level of detail for some of the fields in the response can vary from showing only the UID value of the object to a fully detailed representation of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "details-level", DefaultValueHandling = DefaultValueHandling.Include)]
        [Parameter]
        [ValidateSet("uid", "standard", "full")]
        public string DetailsLevel { get; set; } = "full";

        protected void _ProcessRecord<T>(string Command)
        {
            // Debug Output Request
            string strJson = JsonConvert.SerializeObject(this);
            this.WriteDebug($@"JSON Request to {Session.URL}/{Command}
{strJson}");

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-chkp-sid", Session.SID);
                HttpResponseMessage response = client.PostAsync($"{Session.URL}/{Command}", new StringContent(strJson, Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    strJson = response.Content.ReadAsStringAsync().Result;

                    // Debug Output Request
                    this.WriteDebug($@"JSON Response
{strJson}");

                    T result = JsonConvert.DeserializeObject<T>(strJson);
                    WriteObject(result);
                }
                else
                {
                    WriteWarning($"Server returned status code: {(int)response.StatusCode} [{response.StatusCode}]");
                    strJson = response.Content.ReadAsStringAsync().Result;
                    WriteDebug(strJson);
                    CheckPointError error = JsonConvert.DeserializeObject<CheckPointError>(strJson);
                    WriteObject(error);
                }
            }
            catch (Exception e)
            {
                while (e.InnerException != null) e = e.InnerException;
                this.WriteError(new ErrorRecord(e, e.Message, ErrorCategory.ConnectionError, this));
            }
        }
    }
}