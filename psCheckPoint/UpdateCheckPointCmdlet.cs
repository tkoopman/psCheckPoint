using Koopman.CheckPoint;
using Koopman.CheckPoint.Common;
using System;
using System.ComponentModel;
using System.Management.Automation;
using System.Reflection;

namespace psCheckPoint
{
    /// <summary>
    /// <para type="description">Base class for New and Set classes</para>
    /// </summary>
    public abstract class UpdateCheckPointCmdlet : CheckPointCmdletBase
    {
        #region Fields

        private string[] _tags;

        #endregion Fields

        #region Properties

        /// <summary>
        /// <para type="description">Color of the object. Should be one of existing colors.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        [Alias("Colour")]
        public Koopman.CheckPoint.Colors Color { get; set; } = Koopman.CheckPoint.Colors.Black;

        /// <summary>
        /// <para type="description">Comments string.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Comments { get; set; }

        /// <summary>
        /// <para type="description">Apply changes ignoring warnings or errors.</para>
        /// </summary>
        [Parameter]
        public Koopman.CheckPoint.Ignore Ignore { get; set; } = Koopman.CheckPoint.Ignore.No;

        /// <summary>
        /// <para type="description">Return the updated object.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// <para type="description">Collection of tag identifiers.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string[] Tags
        {
            get { return _tags; }
            set { _tags = CreateArray(value); }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Updates the properties that have been set in the command.
        /// </summary>
        /// <param name="obj">The object to set properties on.</param>
        protected void UpdateProperties(IObjectSummary obj)
        {
            // Only change values user called
            foreach (var p in MyInvocation.BoundParameters.Keys)
            {
                switch (p)
                {
                    case "Debug":
                    case "ErrorAction":
                    case "ErrorVariable":
                    case "Ignore":
                    case "InformationAction":
                    case "InformationVariable":
                    case "OutBuffer":
                    case "OutVariable":
                    case "PassThru":
                    case "PipelineVariable":
                    case "Session":
                    case "Verbose":
                    case "WarningAction":
                    case "WarningVariable":
                        break;

                    default:
                        object value = MyInvocation.BoundParameters[p];
                        if (!UpdateProperty(obj, p, value))
                            obj.SetProperty(p, value);
                        break;
                }
            }
        }

        /// <summary>
        /// Updates the property on the object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <c>true</c> if property was handled. <c>false</c> if it was not which will case the
        /// default basic set to happen.
        /// </returns>
        protected virtual bool UpdateProperty(IObjectSummary obj, string name, object value)
        {
            return false;
        }

        /// <summary>
        /// Writes the object only if PassThru is set.
        /// </summary>
        /// <param name="result">The result.</param>
        protected new void WriteObject(object result)
        {
            if (PassThru.IsPresent) { base.WriteObject(result); }
        }

        #endregion Methods
    }
}