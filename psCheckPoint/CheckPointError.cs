using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint
{
    /// <summary>
    /// <para type="description">Extra details of errors or warnings</para>
    /// </summary>
    public class CheckPointErrorDetail : CheckPointMessage
    {
        /// <summary>
        /// JSON Constructor for Errors
        /// </summary>
        [JsonConstructor]
        private CheckPointErrorDetail(string message,
            bool currentSession) :
            base(message)
        {
            CurrentSession = currentSession;
        }

        /// <summary>
        /// <para type="description">Validation related to the current session.</para>
        /// </summary>
        [JsonProperty(PropertyName = "current-session", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public bool CurrentSession { get; private set; }
    }

    /// <summary>
    /// <para type="description">Result when commands return an error.</para>
    /// </summary>
    public class CheckPointError : CheckPointMessage
    {
        /// <summary>
        /// Constructor for Check Point Error. Normally only called by JsonConvert.DeserializeObject when there is an error returned.
        /// </summary>
        [JsonConstructor]
        protected CheckPointError(string message,
            CheckPointErrorCodes code, CheckPointErrorDetail[] warnings, CheckPointErrorDetail[] errors, CheckPointErrorDetail[] blockingErrors) :
            base(message)
        {
            Code = code;
            Warnings = warnings;
            Errors = errors;
            BlockingErrors = blockingErrors;
        }

        /// <summary>
        /// <para type="description">Error code.</para>
        /// </summary>
        [JsonProperty(PropertyName = "code", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointErrorCodes Code { get; private set; }

        /// <summary>
        /// <para type="description">Validation warnings.</para>
        /// </summary>
        [JsonProperty(PropertyName = "warnings", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointErrorDetail[] Warnings { get; private set; }

        /// <summary>
        /// <para type="description">Validation errors.</para>
        /// </summary>
        [JsonProperty(PropertyName = "errors", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointErrorDetail[] Errors { get; private set; }

        /// <summary>
        /// <para type="description">Validation blocking-errors.</para>
        /// </summary>
        [JsonProperty(PropertyName = "blocking-errors", DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public CheckPointErrorDetail[] BlockingErrors { get; private set; }

        internal static void GenerateError(PSCmdlet cmdlet, CheckPointError error)
        {
            switch (error.Code)
            {
                case CheckPointErrorCodes.not_implemented:
                    cmdlet.ThrowTerminatingError(new ErrorRecord(new PSNotImplementedException(error.Message), error.Code.ToString(), ErrorCategory.NotImplemented, error));
                    break;

                case CheckPointErrorCodes.generic_err_command_not_found:
                case CheckPointErrorCodes.generic_err_command_version_not_found:
                    cmdlet.ThrowTerminatingError(new ErrorRecord(new CommandNotFoundException(error.Message), error.Code.ToString(), ErrorCategory.InvalidOperation, error));
                    break;

                case CheckPointErrorCodes.generic_err_missing_required_parameters:
                case CheckPointErrorCodes.generic_err_missing_required_header:
                    cmdlet.WriteError(new ErrorRecord(new PSArgumentNullException("", error.Message), error.Code.ToString(), ErrorCategory.SyntaxError, error));
                    break;

                case CheckPointErrorCodes.generic_err_invalid_header:
                case CheckPointErrorCodes.generic_err_invalid_parameter:
                    cmdlet.WriteError(new ErrorRecord(new PSArgumentException(error.Message), error.Code.ToString(), ErrorCategory.InvalidArgument, error));
                    break;

                case CheckPointErrorCodes.err_login_failed_wrong_username_or_password:
                case CheckPointErrorCodes.err_login_failed_more_than_one_opened_session:
                case CheckPointErrorCodes.err_login_failed:
                    cmdlet.ThrowTerminatingError(new ErrorRecord(new PSSecurityException(error.Message), error.Code.ToString(), ErrorCategory.AuthenticationError, error));
                    break;

                case CheckPointErrorCodes.generic_err_missing_session_id:
                case CheckPointErrorCodes.generic_err_wrong_session_id:
                case CheckPointErrorCodes.generic_err_session_expired:
                case CheckPointErrorCodes.generic_err_session_in_use:
                    cmdlet.ThrowTerminatingError(new ErrorRecord(new PSSecurityException(error.Message), error.Code.ToString(), ErrorCategory.ProtocolError, error));
                    break;

                case CheckPointErrorCodes.err_switch_session_failed:
                case CheckPointErrorCodes.generic_err_no_permissions:
                case CheckPointErrorCodes.err_forbidden:
                case CheckPointErrorCodes.err_not_a_system_domain_session:
                    cmdlet.WriteError(new ErrorRecord(new PSSecurityException(error.Message), error.Code.ToString(), ErrorCategory.PermissionDenied, error));
                    break;

                case CheckPointErrorCodes.generic_err_object_not_found:
                    cmdlet.WriteError(new ErrorRecord(new ItemNotFoundException(error.Message), error.Code.ToString(), ErrorCategory.ObjectNotFound, error));
                    break;

                case CheckPointErrorCodes.generic_err_object_type_wrong:
                    cmdlet.WriteError(new ErrorRecord(new PSInvalidCastException(error.Message), error.Code.ToString(), ErrorCategory.InvalidType, error));
                    break;

                case CheckPointErrorCodes.generic_err_object_locked:
                    cmdlet.WriteError(new ErrorRecord(new PSInvalidCastException(error.Message), error.Code.ToString(), ErrorCategory.DeadlockDetected, error));
                    break;

                default:
                    cmdlet.WriteError(new ErrorRecord(new PSInvalidOperationException(error.Message), error.Code.ToString(), ErrorCategory.NotSpecified, error));
                    break;
            }
        }
    }

    public enum CheckPointErrorCodes
    {
        generic_error,
        generic_err_invalid_syntax,
        generic_err_invalid_parameter_name,
        not_implemented,
        generic_internal_error,
        generic_server_error,
        generic_server_initializing,
        generic_err_command_not_found,
        generic_err_command_version_not_found,
        generic_err_invalid_api_type,
        generic_err_invalid_api_object_feature,
        generic_err_missing_required_parameters,
        generic_err_missing_required_header,
        generic_err_invalid_header,
        generic_err_invalid_parameter,
        generic_err_normalize,
        err_bad_url,
        err_unknown_api_version,
        err_login_failed_wrong_username_or_password,
        err_login_failed_more_than_one_opened_session,
        err_login_failed,
        err_normalization_failed,
        err_validation_failed,
        err_publish_failed,
        generic_err_missing_session_id,
        generic_err_wrong_session_id,
        generic_err_session_expired,
        generic_err_session_in_use,
        err_switch_session_failed,
        generic_err_no_permissions,
        err_forbidden,
        err_not_a_system_domain_session,
        err_inappropriate_domain_type,
        generic_err_object_not_found,
        generic_err_object_field_not_unique,
        generic_err_object_type_wrong,
        generic_err_object_locked,
        generic_err_object_deletion,
        err_policy_installation_failed,
        err_rulebase_invalid_operation
    }
}