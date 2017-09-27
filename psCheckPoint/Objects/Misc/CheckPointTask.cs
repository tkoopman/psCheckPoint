using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace psCheckPoint.Objects.Misc
{
    [JsonObject]
    public class CheckPointTask : CheckPointObjectFull
    {
        [JsonConstructor]
        private CheckPointTask(string name, string uID, string type, CheckPointDomain domain, string icon, CheckPointMetaInfo metaInfo, bool readOnly, CheckPointObject[] tags, string color, string comments,
            string taskID, string taskName, string status, int progressPercentage, CheckPointTime startTime, CheckPointTime lastUpdateTime, bool suppressed, List<CheckPointTaskDetails> taskDetails) :
            base(name, uID, type, domain, icon, metaInfo, readOnly, tags, color, comments)
        {
            TaskID = taskID;
            TaskName = taskName;
            Status = status;
            ProgressPercentage = progressPercentage;
            StartTime = startTime;
            LastUpdateTime = lastUpdateTime;
            Suppressed = suppressed;
            TaskDetails = (taskDetails == null) ? new List<CheckPointTaskDetails>() : taskDetails;
        }

        [JsonProperty(PropertyName = "task-id")]
        public string TaskID { get; private set; }

        [JsonProperty(PropertyName = "task-name")]
        public string TaskName { get; private set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; private set; }

        [JsonProperty(PropertyName = "progress-percentage")]
        public int ProgressPercentage { get; private set; }

        [JsonProperty(PropertyName = "start-time")]
        public CheckPointTime StartTime { get; private set; }

        [JsonProperty(PropertyName = "last-update-time")]
        public CheckPointTime LastUpdateTime { get; private set; }

        [JsonProperty(PropertyName = "suppressed")]
        public bool Suppressed { get; private set; }

        [JsonProperty(PropertyName = "task-details")]
        public List<CheckPointTaskDetails> TaskDetails { get; private set; }
    }

    public class CheckPointTaskDetails : CheckPointObjectFull
    {
        [JsonConstructor]
        private CheckPointTaskDetails(string name, string uID, string type, CheckPointDomain domain, string icon, CheckPointMetaInfo metaInfo, bool readOnly, CheckPointObject[] tags, string color, string comments,
            string statusCode, string statusDescription, string taskNotification, string gatewayId, string gatewayName, string transactionId, string responseMessage64, string responseError64) :
            base(name, uID, type, domain, icon, metaInfo, readOnly, tags, color, comments)
        {
            StatusCode = statusCode;
            StatusDescription = statusDescription;
            TaskNotification = taskNotification;
            GatewayId = gatewayId;
            GatewayName = gatewayName;
            TransactionId = transactionId;
            ResponseMessage64 = responseMessage64;
            ResponseError64 = responseError64;
        }

        [JsonProperty(PropertyName = "statusCode")]
        public string StatusCode { get; private set; }

        [JsonProperty(PropertyName = "statusDescription")]
        public string StatusDescription { get; private set; }

        [JsonProperty(PropertyName = "taskNotification")]
        public string TaskNotification { get; private set; }

        [JsonProperty(PropertyName = "gatewayId")]
        public string GatewayId { get; private set; }

        [JsonProperty(PropertyName = "gatewayName")]
        public string GatewayName { get; private set; }

        [JsonProperty(PropertyName = "transactionId")]
        public string TransactionId { get; private set; }

        [JsonProperty(PropertyName = "responseMessage")]
        protected string ResponseMessage64 { get; private set; }

        public string ResponseMessage
        {
            get
            {
                return (ResponseMessage64 == null) ? null : System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(ResponseMessage64));
            }
        }

        [JsonProperty(PropertyName = "responseError")]
        protected string ResponseError64 { get; private set; }

        public string ResponseError
        {
            get
            {
                return (ResponseError64 == null) ? null : System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(ResponseError64));
            }
        }
    }

    [JsonObject]
    public class CheckPointTasks : IEnumerable<CheckPointTask>
    {
        [JsonConstructor]
        private CheckPointTasks(List<CheckPointTask> tasks)
        {
            Tasks = tasks;
        }

        [JsonProperty(PropertyName = "tasks")]
        public List<CheckPointTask> Tasks { get; private set; }

        public IEnumerator<CheckPointTask> GetEnumerator()
        {
            return ((IEnumerable<CheckPointTask>)Tasks).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<CheckPointTask>)Tasks).GetEnumerator();
        }
    }
}