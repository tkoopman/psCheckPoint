using Newtonsoft.Json;
using System;
using System.Management.Automation;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CheckPoint.Session
{
    /// <summary>
    /// <para type="synopsis">Log out of a sesison.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    ///   <code>Close-CheckPointSession -Session $Session</code>
    /// </example>
    [Cmdlet(VerbsCommon.Close, "CheckPointSession")]
    public class CloseCheckPointSession : CheckPointCmdlet
    {
        /// <summary>
        /// <para type="description">The session will be continued next time your open SmartConsole. In case 'uid' is not provided, use current session. In order for the session to pass successfully to SmartConsole, make sure you don't have any other active GUI sessions.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter ContinueSessionInSmartconsole { get; set; }

        protected override void ProcessRecord()
        {
            string command = (ContinueSessionInSmartconsole.IsPresent) ? "continue-session-in-smartconsole" : "logout";

            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-chkp-sid", Session.SID);
                HttpResponseMessage response = client.PostAsync($"{Session.URL}/{command}", new StringContent("{ }", Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                }
                else
                {
                    WriteWarning($"Server returned status code: {(int)response.StatusCode} [{response.StatusCode}]");
                    string strJson = response.Content.ReadAsStringAsync().Result;
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