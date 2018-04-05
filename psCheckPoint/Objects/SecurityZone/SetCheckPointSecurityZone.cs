using Koopman.CheckPoint.FastUpdate;
using System.Management.Automation;

namespace psCheckPoint.Objects.SecurityZone
{
    /// <api cmd="set-security-zone">Set-CheckPointSecurityZone</api>
    /// <summary>
    /// <para type="synopsis">Edit existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example></example>
    [Cmdlet(VerbsCommon.Set, "CheckPointSecurityZone")]
    [OutputType(typeof(Koopman.CheckPoint.SecurityZone))]
    public class SetCheckPointSecurityZone : SetCheckPointCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">Security Zone object, name or UID.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, ValueFromRemainingArguments = true)]
        [Alias("Name", "UID")]
        public PSObject SecurityZone { get => Object; set => Object = value; }

        /// <inheritdoc />
        protected override string InputName => nameof(SecurityZone);

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override void Set(string value)
        {
            var zone = Session.UpdateSecurityZone(value);

            // Only change values user called
            foreach (var p in MyInvocation.BoundParameters.Keys)
            {
                switch (p)
                {
                    case nameof(SecurityZone): break;

                    case nameof(TagAction):
                        if (TagAction == MembershipActions.Replace && Tags == null)
                            zone.Tags.Clear();
                        break;

                    case nameof(Tags):
                        zone.Tags.Add(TagAction, Tags);
                        break;

                    case nameof(NewName):
                        zone.Name = NewName;
                        break;

                    default:
                        zone.SetProperty(p, MyInvocation.BoundParameters[p]);
                        break;
                }
            }

            zone.AcceptChanges(Ignore);

            WriteObject(zone);
        }

        #endregion Methods
    }
}