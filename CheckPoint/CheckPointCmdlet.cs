using CheckPoint.Session;
using System.Management.Automation;

namespace CheckPoint
{
    public abstract class CheckPointCmdlet : Cmdlet
    {
        /// <summary>
        /// <para type="description">Session object from Open-CheckPointSession</para>
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public CheckPointSession Session { get; set; }
    }
}