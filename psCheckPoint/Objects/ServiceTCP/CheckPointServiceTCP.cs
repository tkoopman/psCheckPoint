using Newtonsoft.Json;
using psCheckPoint.Objects.Service;

namespace psCheckPoint.Objects.ServiceTCP
{
    /// <summary>
    /// <para type="synopsis">Response from New-CheckPointServiceTCP, Get-CheckPointServiceTCP & Get-CheckPointServicesTCP</para>
    /// <para type="description">TCP object details.</para>
    /// </summary>
    public class CheckPointServiceTCP : CheckPointService
    {
        public override string ToString()
        {
            return $"{Name} (tcp/{Port})";
        }
    }
}