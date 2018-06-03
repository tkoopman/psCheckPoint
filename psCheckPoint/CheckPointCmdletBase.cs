using System;
using System.Management.Automation;

namespace psCheckPoint
{
    /// <summary>
    /// <para type="description">Action to take when changing membership of object.</para>
    /// </summary>
    public enum MembershipActions
    {
        /// <summary>
        /// Replace existing membership with new items
        /// </summary>
        Replace,

        /// <summary>
        /// Add new items to existing membership
        /// </summary>
        Add,

        /// <summary>
        /// Remove items from existing membership
        /// </summary>
        Remove
    };

    /// <summary>
    /// <para type="description">Base class for other Cmdlets that call a Web-API</para>
    /// </summary>
    public abstract class CheckPointCmdletBase : PSCmdletAsync
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance's session is sourced from the PowerShell
        /// session state or manually supplied by the Cmdlet.
        /// </summary>
        /// <value><c>true</c> if this instance is from session state; otherwise, <c>false</c>.</value>
        public bool IsPSSession { get; private set; } = false;

        /// <summary>
        /// <para type="description">Session object from Open-CheckPointSession</para>
        /// </summary>
        [Parameter()]
        public Koopman.CheckPoint.Session Session { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// <para type="description">Used by Cmdlet parameters that accept arrays</para>
        /// <para type="description">
        /// Allows arrays to also be accepted in CSV format with either a , (comma) or ; (semicolon) separator.
        /// </para>
        /// </summary>
        protected static string[] CreateArray(string[] values)
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

        /// <inheritdoc />
        protected sealed override void BeginProcessing()
        {
            if (Session == null)
            {
                IsPSSession = true;
                Session = SessionState.PSVariable.GetValue("CheckPointSession") as Koopman.CheckPoint.Session;
                if (Session == null)
                    throw new PSArgumentNullException("Session");
            }

            base.BeginProcessing();
        }

        #endregion Methods
    }
}