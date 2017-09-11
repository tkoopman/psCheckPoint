using Newtonsoft.Json;
using psCheckPoint.Objects;
using psCheckPoint.Objects.Misc;
using psCheckPoint.Session;
using System;
using System.Management.Automation;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace psCheckPoint
{
    /// <summary>
    /// <para type="description">Base class for other Cmdlets that call a Web-API</para>
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class CheckPointCmdlet<T> : Cmdlet
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public abstract string Command { get; }

        /// <summary>
        /// <para type="description">Session object from Open-CheckPointSession</para>
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public CheckPointSession Session { get; set; }

        /// <summary>
        /// <para type="description">Returns valid JSON request data</para>
        /// </summary>
        internal virtual string getJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// <para type="description">Process results from the Web-API call</para>
        /// </summary>
        protected virtual void ProcessRecordResponse(string JSON)
        {
            // Debug Output Request
            WriteDebug($@"JSON Response
{JSON}");

            T result = JsonConvert.DeserializeObject<T>(JSON);

            if (result is CheckPointMessage)
            {
                WriteVerbose((string)(typeof(CheckPointMessage).GetProperty("Message").GetValue(result)));
            }
            else if (result is CheckPointWhereUsed)
            {
                WriteObject(result);
            }
            else if (result is CheckPointObject)
            {
                WriteVerbose($"{Command}: {(string)(typeof(CheckPointObject).GetProperty("Name").GetValue(result))}");
                WriteObject(result);
            }
            else
            {
                WriteObject(result);
            }
        }

        /// <summary>
        /// <para type="description">Standard method for calling Check Point Web-API commands and processing the results.</para>
        /// </summary>
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

        /// <summary>
        /// <para type="description">Used by Cmdlet parameters that accept arrays</para>
        /// <para type="description">Allows arrays to also be accepted in CSV format with either a , (comma) or ; (semicolon) seperator.</para>
        /// </summary>
        public static string[] CreateArray(String[] values)
        {
            if (values == null)
            {
                return null;
            }
            else
            {
                if (values.Length == 1)
                {
                    string value = values[0];
                    values = value.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                }

                return values;
            }
        }
    }
}