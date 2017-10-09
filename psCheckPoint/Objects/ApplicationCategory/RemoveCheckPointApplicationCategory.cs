using System.Management.Automation;

namespace psCheckPoint.Objects.ApplicationCategory
{
    /// <api cmd="delete-application-site-category">Remove-CheckPointApplicationCategory</api>
    /// <summary>
    /// <para type="synopsis">Delete existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "CheckPointApplicationCategory")]
    public class RemoveCheckPointApplicationCategory : RemoveCheckPointObject
    {
        public override string Command { get { return "delete-application-site-category"; } }
    }
}