using System.Management.Automation;

namespace psCheckPoint.Objects.ApplicationCategory
{
    /// <api cmd="show-application-site-categories">Get-CheckPointApplicationCategories</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointApplicationCategories")]
    [OutputType(typeof(CheckPointObjects))]
    public class GetCheckPointApplicationCategories : GetCheckPointObjects
    {
        public override string Command { get { return "show-application-site-categories"; } }
    }
}