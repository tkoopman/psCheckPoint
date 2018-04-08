using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using Koopman.CheckPoint;
using Koopman.CheckPoint.Common;

namespace psCheckPoint
{
    /// <summary>
    /// <para type="description">Base class for New-CheckPoint*ObjectName* classes</para>
    /// </summary>
    public abstract class NewCheckPointCmdlet : UpdateCheckPointCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">Object name. Should be unique in the domain.</para>
        /// </summary>
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string Name { get; set; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        protected override bool UpdateProperty(IObjectSummary obj, string name, object value)
        {
            if (base.UpdateProperty(obj, name, value)) return true;

            switch (name)
            {
                case nameof(Tags):
                    var tags = obj.GetProperty<MemberMembershipChangeTracking<Tag>>("Tags");
                    foreach (var t in Tags ?? Enumerable.Empty<string>())
                        tags.Add(t);
                    return true;

                default:
                    return false;
            }
        }

        #endregion Methods
    }
}