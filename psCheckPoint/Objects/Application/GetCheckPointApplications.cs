using System.Management.Automation;

namespace psCheckPoint.Objects.Application
{
    /// <api cmd="show-application-sites">Get-CheckPointApplications</api>
    /// <summary>
    /// <para type="synopsis">Retrieve all objects.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointApplications")]
    [OutputType(typeof(CheckPointObjects<CheckPointApplication>))]
    public class GetCheckPointApplications : GetCheckPointObjects
    {
        public override string Command { get { return "show-application-sites"; } }
    }
}