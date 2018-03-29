using System.Management.Automation;

namespace psCheckPoint.Objects
{
    /// <summary>
    /// <para type="description">Base class for New-CheckPoint*ObjectName* classes</para>
    /// </summary>
    public abstract class NewCheckPointObject : NewCheckPointCmdlet
    {
        /// <summary>
        /// <para type="description">If another object with the same identifier already exists, it will be updated. The command behaviour will be the same as if originally a set command was called. Pay attention that original object's fields will be overwritten by the fields provided in the request payload!</para>
        /// </summary>
        [Parameter]
        public SwitchParameter SetIfExists { get; set; }
    }
}