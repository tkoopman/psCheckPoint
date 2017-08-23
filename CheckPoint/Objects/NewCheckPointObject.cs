using Newtonsoft.Json;
using System;
using System.Management.Automation;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CheckPoint.Objects
{
    public abstract class NewCheckPointObject : CheckPointColorCmdlet
    {
        /// <summary>
        /// <para type="description">Object name. Should be unique in the domain.</para>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        [Parameter(Position = 1, Mandatory = true)]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Collection of tag identifiers.</para>
        /// </summary>
        [JsonProperty(PropertyName = "tags", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Tags { get; set; }

        /// <summary>
        /// <para type="description">If another object with the same identifier already exists, it will be updated. The command behaviour will be the same as if originally a set command was called. Pay attention that original object's fields will be overwritten by the fields provided in the request payload!</para>
        /// </summary>
        [JsonProperty(PropertyName = "set-if-exists", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter SetIfExists { get; set; }

        /// <summary>
        /// <para type="description">Comments string.</para>
        /// </summary>
        [JsonProperty(PropertyName = "comments", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Comments { get; set; }

        /// <summary>
        /// <para type="description">The level of detail for some of the fields in the response can vary from showing only the UID value of the object to a fully detailed representation of the object.</para>
        /// </summary>
        [JsonProperty(PropertyName = "details-level", DefaultValueHandling = DefaultValueHandling.Include)]
        [Parameter]
        [ValidateSet("uid", "standard", "full")]
        public string DetailsLevel { get; set; } = "full";

        /// <summary>
        /// <para type="description">Apply changes ignoring warnings.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ignore-warnings", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter IgnoreWarnings { get; set; }

        /// <summary>
        /// <para type="description">Apply changes ignoring errors. You won't be able to publish such a changes. If ignore-warnings flag was omitted - warnings will also be ignored.</para>
        /// </summary>
        [JsonProperty(PropertyName = "ignore-errors", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(SwitchJsonConverter))]
        [Parameter]
        public SwitchParameter IgnoreErrors { get; set; }

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

                    T session = JsonConvert.DeserializeObject<T>(strJson);
                    WriteObject(session);
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