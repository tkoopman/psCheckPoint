using Koopman.CheckPoint;
using Koopman.CheckPoint.FastUpdate;
using System.Management.Automation;

namespace psCheckPoint.Objects.GroupWithExclusion
{
    /// <api cmd="set-group-with-exclusion">Set-CheckPointGroupWithExclusion</api>
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Set, "CheckPointGroupWithExclusion")]
    [OutputType(typeof(Koopman.CheckPoint.GroupWithExclusion))]
    public class SetCheckPointGroupWithExclusion : SetCheckPointCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">Object to exclude.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Except { get; set; }

        /// <summary>
        /// <para type="description">Network object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject GroupWithExclusion { get => Object; set => Object = value; }

        /// <summary>
        /// <para type="description">Object to include.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string Include { get; set; }

        /// <inheritdoc />
        protected override string InputName => nameof(GroupWithExclusion);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void Set(string value)
        {
            var o = Session.UpdateGroupWithExclusion(value);
            UpdateProperties(o);
            o.AcceptChanges(Ignore);
            WriteObject(o);
        }

        /// <inheritdoc />
        protected override bool UpdateProperty(IObjectSummary obj, string name, object value)
        {
            if (base.UpdateProperty(obj, name, value)) return true;

            var o = (Koopman.CheckPoint.GroupWithExclusion)obj;
            switch (name)
            {
                case nameof(Include):
                    o.SetInclude(Include);
                    return true;

                case nameof(Except):
                    o.SetExcept(Except);
                    return true;

                default:
                    return false;
            }
        }

        #endregion Methods
    }
}