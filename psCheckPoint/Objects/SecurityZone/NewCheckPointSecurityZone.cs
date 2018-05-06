using System.Linq;
using System.Management.Automation;
using System.Threading.Tasks;

namespace psCheckPoint.Objects.SecurityZone
{
    /// <api cmd="add-security-zone">New-CheckPointSecurityZone</api>
    /// <summary>
    /// <para type="synopsis">Create new object.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// <code></code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "CheckPointSecurityZone")]
    [OutputType(typeof(Koopman.CheckPoint.SecurityZone))]
    public class NewCheckPointSecurityZone : NewCheckPointObject
    {
        #region Methods

        /// <inheritdoc />
        protected override async Task ProcessRecordAsync()
        {
            var zone = new Koopman.CheckPoint.SecurityZone(Session, SetIfExists.IsPresent)
            {
                Name = Name,
                Color = Color,
                Comments = Comments
            };

            foreach (string t in Tags ?? Enumerable.Empty<string>())
                zone.Tags.Add(t);

            await zone.AcceptChanges(Ignore, cancellationToken: CancelProcessToken);

            WriteObject(zone);
        }

        #endregion Methods
    }
}