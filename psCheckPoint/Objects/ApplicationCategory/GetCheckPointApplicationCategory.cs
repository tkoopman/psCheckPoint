using Newtonsoft.Json;
using System.Management.Automation;

namespace psCheckPoint.Objects.ApplicationCategory
{
    /// <api cmd="show-application-site-category">Get-CheckPointApplicationCategory</api>
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPointApplicationCategory")]
    [OutputType(typeof(CheckPointApplicationCategory))]
    public class GetCheckPointApplicationCategory : GetCheckPointObject<CheckPointApplicationCategory>
    {
        public override string Command { get { return "show-application-site-category"; } }
    }
}