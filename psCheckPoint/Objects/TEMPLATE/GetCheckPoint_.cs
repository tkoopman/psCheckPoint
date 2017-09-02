using System.Management.Automation;

namespace psCheckPoint.Objects._
{
    /// <summary>
    /// <para type="synopsis">Retrieve existing object using object name or uid.</para>
    /// <para type="description"></para>
    /// </summary>
    /// <example>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "CheckPoint_")]
    [OutputType(typeof(CheckPoint_))]
    public class GetCheckPoint_ : GetCheckPointObject<CheckPoint_>
    {
        public override string Command { get { return "show-_"; } }
    }
}