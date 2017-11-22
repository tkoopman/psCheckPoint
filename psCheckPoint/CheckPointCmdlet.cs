using Newtonsoft.Json;
using psCheckPoint.Objects;
using psCheckPoint.Session;
using System;
using System.Collections;
using System.Management.Automation;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace psCheckPoint
{
    /// <summary>
    /// <para type="description">Base class for other Cmdlets that call a Web-API</para>
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class CheckPointCmdlet<T> : CheckPointCmdletBase
    {
        /// <summary>
        /// <para type="description">Check Point Web-API command that should be called.</para>
        /// </summary>
        public abstract string Command { get; }

        /// <summary>
        /// <para type="description">Returns valid JSON request data</para>
        /// </summary>
        internal virtual string GetJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// <para type="description">Process results from the Web-API call</para>
        /// </summary>
        protected virtual T ProcessRecordResponse(string JSON)
        {
            // Debug Output Request
            WriteDebug($@"JSON Response
{JSON}");

            T result = JsonConvert.DeserializeObject<T>(JSON);

            return result;
        }

        protected virtual void WriteRecordResponse(T result)
        {
            if (result is CheckPointObject)
            {
                WriteVerbose($"{Command}: {(result as CheckPointObject).Name}");
            }
            WriteObject(result);
        }

        /// <summary>
        /// <para type="description">Standard method for calling Check Point Web-API commands and processing the results.</para>
        /// </summary>
        protected override void ProcessRecord()
        {
            // Debug Output Request
            string strJson = GetJSON();
            WriteDebug($@"JSON Request to {Session.URL}/{Command}
{strJson}");

            try
            {
                HttpClient client = Session.GetHttpClient();
                using (HttpResponseMessage response = client.PostAsync($"{Command}", new StringContent(strJson, Encoding.UTF8, "application/json")).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        strJson = response.Content.ReadAsStringAsync().Result;

                        // Debug Output Request
                        WriteDebug($@"JSON Response
{strJson}");

                        T result = ProcessRecordResponse(strJson);
                        WriteRecordResponse(result);
                    }
                    else
                    {
                        if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                        {
                            WriteVerbose($"Server returned status code: {(int)response.StatusCode} [{response.StatusCode}]");
                        }
                        strJson = response.Content.ReadAsStringAsync().Result;
                        WriteDebug(strJson);
                        CheckPointError error = JsonConvert.DeserializeObject<CheckPointError>(strJson);
                        GenerateError(error);
                    }
                }
            }
            catch (Exception e)
            {
                while (e.InnerException != null) e = e.InnerException;
                WriteError(new ErrorRecord(e, e.Message, ErrorCategory.ConnectionError, this));
            }
        }

        protected virtual void GenerateError(CheckPointError error)
        {
            CheckPointError.GenerateError(this, error);
        }
    }
}