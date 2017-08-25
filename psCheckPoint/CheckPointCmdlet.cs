using Newtonsoft.Json;
using psCheckPoint.Session;
using System;
using System.Management.Automation;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace psCheckPoint
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class CheckPointCmdlet<T> : Cmdlet
    {
        // Check Point Web-API Command
        public abstract string Command { get; }

        /// <summary>
        /// <para type="description">Session object from Open-CheckPointSession</para>
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public CheckPointSession Session { get; set; }

        internal virtual string getJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        protected virtual void ProcessRecordResponse(string JSON)
        {
            // Debug Output Request
            this.WriteDebug($@"JSON Response
{JSON}");

            T result = JsonConvert.DeserializeObject<T>(JSON);

            if (result.GetType() == typeof(CheckPointMessage))
            {
                WriteVerbose((string)(typeof(CheckPointMessage).GetProperty("Message").GetValue(result)));
            }
            else
            {
                WriteObject(result);
            }
        }

        protected override void ProcessRecord()
        {
            // Debug Output Request
            string strJson = getJSON();
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

                    ProcessRecordResponse(strJson);
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
                WriteError(new ErrorRecord(e, e.Message, ErrorCategory.ConnectionError, this));
            }
        }
    }
}